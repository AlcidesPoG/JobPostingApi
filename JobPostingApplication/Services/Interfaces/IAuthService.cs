using JobPostingDomain.Criteria.Auth;
using JobPostingDomain.Entities;
using JobPostingInfraestructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPostingApplication.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserLoginDTO?> Login(UserCriteria userCriteria);
    }
}
