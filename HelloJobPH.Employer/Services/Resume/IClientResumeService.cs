namespace HelloJobPH.Employer.Services.Resume
{
    public interface IClientResumeService
    {
        Task<Stream?> GetResumeAsync(int applicationId);//
    }
}
