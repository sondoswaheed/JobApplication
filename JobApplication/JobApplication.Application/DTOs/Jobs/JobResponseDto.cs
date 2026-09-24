namespace JobApplication.Application.DTOs.Jobs
{
    public class JobResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime? ClosedAt { get; set; }
    }
}