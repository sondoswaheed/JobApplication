namespace JobApplication.Domain.Entities
{
    public class Applicationn
    {
        public int Id { get; set; }

        public int CandidateId { get; set; }

        public int JobId { get; set; }

        public string Status { get; set; } = "Applied";

        public DateTime AppliedAt { get; set; }

        public DateTime? StatusUpdatedAt { get; set; }

        public DateTime? CancelledAt { get; set; }
    }
}