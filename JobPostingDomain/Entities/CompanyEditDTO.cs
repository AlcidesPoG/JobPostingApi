using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPostingDomain.Entities
{
    public class CompanyEditDTO
    {

        public string Name { get; set; } = null!;

        public IFormFile? PhotoFile { get; set; }

        public string? PageUrl { get; set; }

        public string Country { get; set; } = null!;

        public string State { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string? Description { get; set; }

    }
}
