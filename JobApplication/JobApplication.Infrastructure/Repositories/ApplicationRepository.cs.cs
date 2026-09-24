using JobApplication.Application.Interfaces.Repositories;
using JobApplication.Domain.Entities;
using JobApplication.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Infrastructure.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly AppDbContext _context;
        public ApplicationRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Applicationn?> GetByIdAsync(int id)
        {
            return await _context.Applications
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Applicationn application)
        {
            await _context.Applications.AddAsync(application);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Applicationn app)
        {
            _context.Applications.Update(app);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int candidateId, int jobId)
        {
            return await _context.Applications
                .AnyAsync(a =>
                    a.CandidateId == candidateId &&
                    a.JobId == jobId);
        }

        public async Task<List<Applicationn>> GetAllAsync()
        {
            return await _context.Applications
                .ToListAsync();
        }

        public async Task<List<Applicationn>> GetByCandidateIdAsync(
            int candidateId)
        {
            return await _context.Applications
                .Where(a => a.CandidateId == candidateId)
                .ToListAsync();
        }

    }
}

