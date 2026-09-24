using JobApplication.Application.Interfaces.Repositories;
using JobApplication.Domain.Entities;
using JobApplication.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.Infrastructure.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly AppDbContext _context;
        public JobRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Job?> GetByIdAsync(int id)
        {
            return await _context.Jobs.FirstOrDefaultAsync(i=>i.Id== id);
        }


        public async Task AddAsync(Job job)
        {
            await _context.Jobs.AddAsync(job);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Job job)
        {
             _context.Jobs.Update(job);

            await _context.SaveChangesAsync();
                 
        }

        public async Task<List<Job>> GetAllAsync()
        {
            return await _context.Jobs
                .ToListAsync();
        }
    }
}
