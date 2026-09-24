using JobApplication.Domain.Enums;

namespace JobApplication.Domain.Entities
{
    public class Applicationn
    {
        public int Id { get; set; }

        public int CandidateId { get; set; }

        public int JobId { get; set; }

        public ApplicationStatus Status { get; set; } = ApplicationStatus.Applied;

        public DateTime AppliedAt { get; set; }

        public DateTime? StatusUpdatedAt { get; set; }

        public DateTime? CancelledAt { get; set; }
    }
}