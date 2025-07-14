using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPostingDomain.Entities
{
    public class UserCreateDTO
    {
        public DateTime? CreatedAt { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? ProfileImage { get; set; }

        public string Role { get; set; } = null!;

    }
}
