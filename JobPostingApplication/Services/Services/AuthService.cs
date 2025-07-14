using AutoMapper;
using JobPostingApplication.Services.Interfaces;
using JobPostingDomain.Criteria.Auth;
using JobPostingDomain.Entities;
using JobPostingInfraestructure.Persistence;
using JobPostingUtilities.shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JobPostingApplication.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private JobPostDbContext dbContext;
        private readonly IConfiguration configuration;

        public AuthService(JobPostDbContext dbContext, IConfiguration configuration, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
            this._mapper = mapper;
        }

        public async Task<UserLoginDTO?> Login(UserCriteria userCriteria)
        {
            try
            {
                var user = await dbContext.Users.Where(x => x.Email == userCriteria.Email).FirstOrDefaultAsync();
                if (user == null) return null;

                var hasher = new PasswordHasher<object>();

                var checkPassword = hasher.VerifyHashedPassword(null, user.Password, userCriteria.Password);

                if (checkPassword == PasswordVerificationResult.Success)
                {
                    var userVerified = _mapper.Map<UserLoginDTO>(user);
                    userVerified.Token = JWTGenerator.createJWT(user, configuration);
                    return userVerified;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
    }
}
