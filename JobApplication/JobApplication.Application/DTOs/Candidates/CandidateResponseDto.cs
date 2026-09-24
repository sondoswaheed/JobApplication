namespace JobApplication.Application.DTOs.Candidates
{
    public class CandidateResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string CVUrl { get; set; } = string.Empty;
    }
}