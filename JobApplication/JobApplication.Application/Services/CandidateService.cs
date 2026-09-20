using JobApplication.Application.DTOs.Candidates;
using JobApplication.Application.Interfaces;
using JobApplication.Application.Interfaces.Repositories;
using JobApplication.Domain.Entities;

namespace JobApplication.Application.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateService(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<int> CreateCandidateAsync(CreateCandidateDto dto)
        {
            var candidate = new Candidate
            {
                Name = dto.Name,
                Email = dto.Email,
                CVUrl = dto.CVUrl
            };

            await _candidateRepository.AddAsync(candidate);

            return candidate.Id;
        }
    }
}