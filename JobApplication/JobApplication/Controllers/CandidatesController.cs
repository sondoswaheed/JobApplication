using JobApplication.Application.DTOs.Candidates;
using JobApplication.Application.Features.Command.CreateCandidate;
using JobApplication.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CandidatesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CandidatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCandidate(CreateCandidateDto dto ,CancellationToken cancellation)
        {
            var candidateId = await _mediator
                .Send(new CreateCandidateCommand { CVUrl = dto.CVUrl, Email = dto.Email, Name = dto.Email }, cancellation);
            
            return Ok(new
            {
                message = "Candidate created successfully.",
                candidateId = candidateId
            });
        }
    }
}