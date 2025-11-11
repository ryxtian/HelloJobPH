using GenerativeAI;
using HelloJobPH.Employer.Pages.JobPost;
using HelloJobPH.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UglyToad.PdfPig;
using System;
using System.Threading.Tasks;

namespace HelloJobPH.Server.Service.AI
{
    /// <summary>
    /// Generates AI-powered overviews for resumes and social media posts using Gemini AI.
    /// </summary>
    public class AiOverviewService
    {
        private readonly GenerativeModel _model;
        private readonly ApplicationDbContext _context;
       
        public AiOverviewService(ApplicationDbContext context,IConfiguration config)
        {

            _context = context;
            // Load the API key from appsettings.json or User Secrets.
            var apiKey = config["Google:ApiKey"];
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new InvalidOperationException("Gemini API key not configured. Please set 'Gemini:ApiKey' in your settings.");

            _model = new GenerativeModel(apiKey: apiKey, model: "gemini-2.0-flash");
        }

        /// <summary>
        /// Generates a concise, professional overview based on a resume or post text.
        /// </summary>
        /// <param name="content">Raw text (resume or post).</param>
        /// <param name="type">"resume" or "post".</param>
        /// <returns>AI-generated summary.</returns>
        public async Task<string> GenerateOverviewAsync(int id)
        {
            // 1️⃣ Fetch application by ID
            var application = await _context.Application
                                            .Include(a => a.Resume)
                                            .FirstOrDefaultAsync(a => a.ApplicationId == id);
            if (application == null || application.Resume == null)
                throw new ArgumentException("Application or resume not found.");

            // 2️⃣ Save byte[] as a PDF file
            string tempPdfPath = Path.Combine(Path.GetTempPath(), $"Resume_{id}.pdf");
            if (application.Resume.ResumeFileData != null && application.Resume.ResumeFileData.Length > 0)
            {
                await File.WriteAllBytesAsync(tempPdfPath, application.Resume.ResumeFileData);
            }
            else
            {
                throw new ArgumentException("No resume file available for this application.");
            }

            // 3️⃣ (Optional) If your AI can read PDF directly, pass the file path
            // Or, if your AI requires text, convert PDF to text using a library like PdfSharp, iText7, or PdfPig
            string resumeText;
            using (var pdf = UglyToad.PdfPig.PdfDocument.Open(tempPdfPath)) // example using PdfPig
            {
                resumeText = string.Join("\n", pdf.GetPages().Select(p => p.Text));
            }

            // 4️⃣ Get job posting text
            var jobPost = await _context.JobPosting.FirstOrDefaultAsync(j => j.JobPostingId == application.JobPostingId);
            string jobPostText = $"{jobPost.Title}\n{jobPost.Description}\n{jobPost.JobRequirements}";

            // 5️⃣ Prepare AI prompt
            var prompt = $@"
                Analyze the following resume and job posting, then write a short, professional AI-generated overview
                (3-5 sentences) highlighting how the candidate's skills and experience align with the job.

                Resume:
                {resumeText}

                Job Posting:
                {jobPostText}
                
                Instructions:
                1. Begin the overview by explicitly stating the applicant's full name as it appears in the resume.
                2. Use third-person, formal language suitable for LinkedIn or a professional job summary.
                3. Highlight key skills, experience, and achievements relevant to the job posting.
                4. Keep it concise (3-5 sentences).
                5. Evaluate the applicant if they are 'Qualified', 'To be Considered', or 'Unqualified' enclose the evaluate in square bracker '[]'

                Your output should start like: 'Applicant Name: [Full Name from resume]' followed by the overview.
                ";

            // 6️⃣ Call AI model
            try
            {
                var response = await _model.GenerateContentAsync(prompt);
                return response?.Text?.Trim() ?? "No overview could be generated.";
            }
            catch (Exception ex)
            {
                return $"Error generating overview: {ex.Message}";
            }
        }


    }
}
