using JobApplication.Application.DTOs.Candidates;

namespace JobApplication.Application.Interfaces
{
    public interface ICandidateService
    {
        Task<int> CreateCandidateAsync(CreateCandidateDto dto);
    }
}