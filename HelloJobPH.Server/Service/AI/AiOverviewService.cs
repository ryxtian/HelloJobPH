using GenerativeAI;
using Microsoft.Extensions.Configuration;
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

        public AiOverviewService(IConfiguration config)
        {
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
        public async Task<string> GenerateOverviewAsync(string resumeText, string jobPostText)
        {
            if (string.IsNullOrWhiteSpace(resumeText) || string.IsNullOrWhiteSpace(jobPostText))
                throw new ArgumentException("Both ResumeText and JobPostText are required.");

            var prompt = $@"
Analyze the following resume and job posting, then write a short, professional AI-generated overview
(3-5 sentences) highlighting how the candidate's skills and experience align with the job.

Resume:
{resumeText}

Job Posting:
{jobPostText}

Your output should be concise, formal, and written in third person, suitable for a LinkedIn summary or job-matching overview.
";

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
