using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Application.Features.Command.CreateCandidate
{
    public class CreateCandidateCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string CVUrl { get; set; } = string.Empty;
    }
}
