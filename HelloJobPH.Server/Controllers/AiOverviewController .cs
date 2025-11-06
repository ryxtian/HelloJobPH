using Microsoft.AspNetCore.Mvc;
using HelloJobPH.Server.Service.AI;
using System.Threading.Tasks;

namespace HelloJobPH.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AiOverviewController : ControllerBase
    {
        private readonly AiOverviewService _aiService;

        public AiOverviewController(AiOverviewService aiService)
        {
            _aiService = aiService;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> Generate([FromBody] OverviewRequest request)
        {

            if (string.IsNullOrWhiteSpace(request?.ResumeText) || string.IsNullOrWhiteSpace(request?.JobPostText))
                return BadRequest("Both ResumeText and JobPostText are required.");

            var result = await _aiService.GenerateOverviewAsync(request.ResumeText, request.JobPostText);
            return Ok(new { overview = result });
        }

        //[HttpPost("generate-from-file")]
        //public async Task<IActionResult> GenerateFromFile(IFormFile resumeFile, [FromForm] string jobPostText)
        //{
        //    if (resumeFile == null || string.IsNullOrWhiteSpace(jobPostText))
        //        return BadRequest("Resume file and JobPostText are required.");

        //    string resumeText;
        //    using (var stream = new MemoryStream())
        //    {
        //        await resumeFile.CopyToAsync(stream);
        //        resumeText = ExtractTextFromPdf(stream.ToArray());
        //    }

        //    var result = await _aiService.GenerateOverviewAsync(resumeText, jobPostText);
        //    return Ok(new { overview = result });
        //}

        //private string ExtractTextFromPdf(byte[] pdfBytes)
        //{
        //    using var reader = new iText.Kernel.Pdf.PdfReader(new MemoryStream(pdfBytes));
        //    using var doc = new iText.Kernel.Pdf.PdfDocument(reader);
        //    var text = new System.Text.StringBuilder();

        //    for (int i = 1; i <= doc.GetNumberOfPages(); i++)
        //    {
        //        var page = doc.GetPage(i);
        //        var strategy = new iText.Kernel.Pdf.Canvas.Parser.Listener.LocationTextExtractionStrategy();
        //        var parser = new iText.Kernel.Pdf.Canvas.Parser.PdfCanvasProcessor(strategy);
        //        parser.ProcessPageContent(page);
        //        text.Append(strategy.GetResultantText());
        //    }

        //    return text.ToString();
        //}
    }

    public class OverviewRequest
    {
        public string ResumeText { get; set; }
        public string JobPostText { get; set; }
    }
}