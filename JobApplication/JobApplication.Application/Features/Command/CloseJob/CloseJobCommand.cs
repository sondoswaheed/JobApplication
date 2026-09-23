using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Application.Features.Command.CloseJob
{
    public class CloseJobCommand : IRequest<bool>
    {
        public int JobId { get; set; }
    }
}
