using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Domain.Entities
{
    
        public class Candidate
        {
            public int Id { get; set; }

            public string Name { get; set; } = string.Empty;

            public string Email { get; set; } = string.Empty;

            public string CVUrl { get; set; } = string.Empty;
        }
    
}

