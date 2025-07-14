using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPostingDomain.Entities
{
    public class UserLoginDTO
    {
        public int CompanyId { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? ProfileImage { get; set; }

        public string? Token { get; set; }

        public int? UserId { get; set; } = null;
    }
}
