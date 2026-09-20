using JobApplication.Domain.Entities;

namespace JobApplication.Application.Interfaces.Repositories
{
    public interface IApplicationRepository
    {
        Task<Applicationn?> GetByIdAsync(int id);
        Task AddAsync(Applicationn application);
        Task UpdateAsync(Applicationn application);
    }
}