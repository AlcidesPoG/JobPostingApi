using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPostingDomain.Criteria.Auth
{
    public class UserCriteria
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
