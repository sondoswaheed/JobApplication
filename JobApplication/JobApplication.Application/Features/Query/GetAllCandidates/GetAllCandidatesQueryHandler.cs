using JobApplication.Application.DTOs.Candidates;
using JobApplication.Application.Interfaces.Repositories;
using MediatR;

namespace JobApplication.Application.Features.Query.GetAllCandidates
{
    public class GetAllCandidatesQueryHandler
        : IRequestHandler<
            GetAllCandidatesQuery,
            List<CandidateResponseDto>>
    {
        private readonly ICandidateRepository _candidateRepository;

        public GetAllCandidatesQueryHandler( ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<List<CandidateResponseDto>> Handle( GetAllCandidatesQuery request,CancellationToken cancellationToken)
        {
            var candidates = await _candidateRepository.GetAllAsync();

            return candidates.Select(c => new CandidateResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    CVUrl = c.CVUrl
                }).ToList();
        }
    }
}