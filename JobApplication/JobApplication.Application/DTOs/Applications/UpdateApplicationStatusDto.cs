using JobApplication.Domain.Enums;

namespace JobApplication.Application.DTOs.Applications
{
    public class UpdateApplicationStatusDto
    {
        public ApplicationStatus Status { get; set; }
    }
}