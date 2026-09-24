using JobApplication.Domain.Enums;

namespace JobApplication.Application.DTOs.Applications
{
    public class ApplicationResponseDto
    {
        public int Id { get; set; }

        public int CandidateId { get; set; }

        public string CandidateName { get; set; } = string.Empty;

        public int JobId { get; set; }

        public string JobTitle { get; set; } = string.Empty;

        public ApplicationStatus Status { get; set; } = ApplicationStatus.Applied;

        public DateTime AppliedAt { get; set; }

        public DateTime? StatusUpdatedAt { get; set; }
    }
}