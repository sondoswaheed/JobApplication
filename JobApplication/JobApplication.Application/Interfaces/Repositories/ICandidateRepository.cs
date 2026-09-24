using JobApplication.Domain.Entities;

namespace JobApplication.Application.Interfaces.Repositories
{
    public interface ICandidateRepository
    {
        Task<Candidate?> GetByIdAsync(int id);

        Task<Candidate?> GetByUserIdAsync(string userId);

        Task AddAsync(Candidate candidate);

        Task UpdateAsync(Candidate candidate);
        Task<List<Candidate>> GetAllAsync();
    }
}