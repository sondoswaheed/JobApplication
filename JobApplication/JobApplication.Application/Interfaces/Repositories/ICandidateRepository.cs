using JobApplication.Domain.Entities;

namespace JobApplication.Application.Interfaces.Repositories
{
    public interface ICandidateRepository
    {
        Task<Candidate?> GetByIdAsync(int id);

        Task AddAsync(Candidate candidate);
    }
}