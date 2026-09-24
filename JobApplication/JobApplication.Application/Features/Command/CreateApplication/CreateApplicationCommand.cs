using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Application.Features.Command.CreateApplication
{
    public class CreateApplicationCommand :IRequest<int>
    {

        public int JobId { get; set; }
    }
}
