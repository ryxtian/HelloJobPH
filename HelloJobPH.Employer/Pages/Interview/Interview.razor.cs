using MudBlazor;

namespace HelloJobPH.Employer.Pages
{
    public partial class Weather
    {
        public class Applicant
        {
            public string Name { get; set; } = "";
            public string Status { get; set; } = "";
            public int ScreenerMet { get; set; }
            public DateTime Date { get; set; }
        }

        private List<Applicant> applicants = new()
    {
        new() { Name = "Nil Yeager", Status = "Awaiting Review", ScreenerMet = 1, Date = new DateTime(2025, 11, 19) },
        new() { Name = "Aditi Row", Status = "Awaiting Review", ScreenerMet = 1, Date = new DateTime(2025, 11, 19) },
        new() { Name = "Mical Doe", Status = "Awaiting Review", ScreenerMet = 0, Date = new DateTime(2025, 11, 19) },
        new() { Name = "Shone Marsh", Status = "Awaiting Review", ScreenerMet = 0, Date = new DateTime(2025, 11, 19) },
        new() { Name = "Riki Ponting", Status = "Contacting", ScreenerMet = 1, Date = new DateTime(2025, 11, 19) },
        new() { Name = "Gail Doe", Status = "Contacting", ScreenerMet = 1, Date = new DateTime(2025, 11, 19) },
        new() { Name = "Gama Doe", Status = "Awaiting Review", ScreenerMet = 1, Date = new DateTime(2025, 11, 19) }
    };

        private Color GetStatusColor(string status) =>
            status switch
            {
                "Contacting" => Color.Primary,
                "Awaiting Review" => Color.Secondary,
                _ => Color.Default
            };

        private Color GetScreenerColor(int met) => met > 0 ? Color.Success : Color.Error;
    }
}