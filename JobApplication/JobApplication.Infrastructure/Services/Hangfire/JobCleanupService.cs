using JobApplication.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.Infrastructure.Services.Hangfire
{
    public class JobCleanupService
    {
        private readonly AppDbContext _context;

        public JobCleanupService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AutoCloseJobs()
        {
            var limitDate = DateTime.UtcNow.AddDays(-30);

            var jobs = await _context.Jobs
                .Where(j =>
                    j.IsActive &&
                    j.CreatedAt <= limitDate)
                .ToListAsync();

            foreach (var job in jobs)
            {
                job.IsActive = false;
                job.ClosedAt = DateTime.UtcNow;
                job.ClosedBy = "Hangfire";
            }

            await _context.SaveChangesAsync();
        }
    }
}