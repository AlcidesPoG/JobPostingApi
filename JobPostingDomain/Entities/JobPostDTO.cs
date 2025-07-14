using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPostingDomain.Entities
{
    public class JobPostDTO
    {
        public int PostId { get; set; }

        public string Title { get; set; } = null!;

        public string? Location { get; set; }

        public string? Type { get; set; }

        public string? Category { get; set; }

        public string? Description { get; set; }

        public string? Requirements { get; set; }

        public string? Salary { get; set; }

        public string? ApplyUrl { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? Status { get; set; }

        public int? CompanyId { get; set; }

        public string? CompanyName { get; set; }

        public string? CompanyImgUrl { get; set; }

    }
}
