namespace HelloJobPH.Server.Service.Resume
{
    public interface IResumeService
    {
        Task<byte[]?> GetResumeBytesAsync(int applicationId);
    }
}
