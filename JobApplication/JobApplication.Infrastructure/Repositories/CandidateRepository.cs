using JobApplication.Application.Interfaces.Repositories;
using JobApplication.Domain.Entities;
using JobApplication.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.Infrastructure.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly AppDbContext _context;

        public CandidateRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Candidate?> GetByIdAsync(int id)
        {
            return await _context.Candidates.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Candidate candidate)
        {
            await _context.Candidates.AddAsync(candidate);
            await _context.SaveChangesAsync();
        }

        public async Task<Candidate?> GetByUserIdAsync(string userId)
        {
            return await _context.Candidates.FirstOrDefaultAsync(c => c.UserId == userId);
        }
        public async Task UpdateAsync(Candidate candidate)
        {
            _context.Candidates.Update(candidate);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Candidate>> GetAllAsync()
        {
            return await _context.Candidates
                .ToListAsync();
        }
    }
}