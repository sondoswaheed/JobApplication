using JobApplication.Application.DTOs.Applications;

namespace JobApplication.Application.Interfaces
{
    public interface IApplicationService
    {
        Task<int> CreateApplicationAsync(CreateApplicationDto dto);

        Task<bool> CancelApplicationAsync(int id);
    }
}