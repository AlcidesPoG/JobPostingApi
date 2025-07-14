using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPostingDomain.Entities
{
    public class CompanyDTO
    {
        public int CompanyId { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Photo { get; set; }

        public string? PageUrl { get; set; }

        public string Country { get; set; } = null!;

        public string State { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Address { get; set; } = null!;                                                                                                          

        public string? Description { get; set; }

    }
}
