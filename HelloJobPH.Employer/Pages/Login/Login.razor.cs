using HelloJobPH.Shared.DTOs;
using MudBlazor;

namespace HelloJobPH.Employer.Pages.Login
{
    public partial class Login
    {
        private LoginDtos model = new();

        private async Task HandleLogin()
        {
            var success = await AuthService.LoginAsync(model);

            if (success)
            {
                Snackbar.Add("Login successful!", Severity.Success);
                Nav.NavigateTo("/dashboard/dashboard");
            }
            else
            {
                Snackbar.Add("Invalid email or password.", Severity.Error);
            }
        }
    }
}