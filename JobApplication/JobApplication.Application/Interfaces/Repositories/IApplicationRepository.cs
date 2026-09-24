using JobApplication.Domain.Entities;

namespace JobApplication.Application.Interfaces.Repositories
{
    public interface IApplicationRepository
    {
        Task<Applicationn?> GetByIdAsync(int id);

        Task<List<Applicationn>> GetAllAsync();

        Task<List<Applicationn>> GetByCandidateIdAsync(int candidateId);

        Task AddAsync(Applicationn application);

        Task UpdateAsync(Applicationn application);

        Task<bool> ExistsAsync(int candidateId, int jobId);
    }
}