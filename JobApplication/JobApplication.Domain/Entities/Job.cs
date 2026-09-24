using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Domain.Entities
{
    public class Job
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsActive { get; set; }

        public string RecruiterId { get; set; }

        public DateTime? ClosedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public string? ClosedBy { get; set; }

        public void Close(string recruiterId)
        {
            IsActive = false;
            ClosedAt = DateTime.UtcNow;
            ClosedBy = recruiterId;
        }

        
    }
}
