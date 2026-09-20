namespace JobApplication.Application.Interfaces
{
    public interface IJobService
    {
        Task<bool> CloseJobAsync(int jobId);
        Task<int> CreateJobAsync(string title);
    }
}