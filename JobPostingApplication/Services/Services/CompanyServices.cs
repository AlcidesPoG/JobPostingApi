using AutoMapper;
using JobPostingApplication.Services.Interfaces;
using JobPostingDomain.Entities;
using JobPostingInfraestructure.Persistence;
using JobPostingUtilities.shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JobPostingApplication.Services.Services
{
    public class CompanyServices : ICompanyServices
    {
        private readonly string imageFolder = "Company";
        private readonly IMapper _mapper;
        private JobPostDbContext dbContext;
        private readonly IConfiguration configuration;

        public CompanyServices(IMapper mapper, JobPostDbContext dbContext, IConfiguration configuration)
        {
            this._mapper = mapper;
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

        public async Task<UserLoginDTO> CreateCompany(CompanyCreateDTO createDTO, string baseURL)
        {

            using ( var transaction = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var EmailExist = await dbContext.Companies.Where(c => c.Email == createDTO.Email).FirstOrDefaultAsync();
                    if (EmailExist != null) throw new Exception("This Email was already registeres");
                    createDTO.Password = PasswordEncrypter.Encrypt(createDTO.Password);

                    string? photoUrl = await FileUploadService.UploadFileAsync(createDTO.PhotoFile, imageFolder, baseURL);

                    Company newCompany = _mapper.Map<Company>(createDTO);
                    newCompany.Photo = photoUrl;
                    await dbContext.AddAsync(newCompany);
                    await dbContext.SaveChangesAsync();

                    User user = new()
                    {
                        CompanyId = newCompany.CompanyId,
                        Name = createDTO.Name,
                        Email = createDTO.Email,
                        Password = createDTO.Password,
                        ProfileImage = photoUrl,
                        Role = "Admin"
                    };

                    await dbContext.AddAsync(user);
                    await dbContext.SaveChangesAsync();
                    var companyDTO = _mapper.Map<CompanyDTO>(newCompany);
                    await transaction.CommitAsync();
                    var newUser = _mapper.Map<UserLoginDTO>(user);
                    newUser.Token = JWTGenerator.createJWT(user, configuration);
                    return newUser;

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Error creating company and user: " + ex.Message);
                }
            }
        }

        public async Task<CompanyDTO> EditCompany(CompanyEditDTO editDTO, int companyId, string baseURL)
        {
            try
            {

                var company = await dbContext.Companies.FirstOrDefaultAsync( x => x.CompanyId == companyId);
                if (company == null)
                {
                    throw new Exception("404 not found");
                }

                string? photoUrl = await FileUploadService.UploadFileAsync(editDTO.PhotoFile, imageFolder, baseURL);

                company = _mapper.Map(editDTO,company);

                if (photoUrl != null)
                {
                    company.Photo = photoUrl;
                }
                await dbContext.SaveChangesAsync();
                var companyDTO = _mapper.Map<CompanyDTO>(company);
                return(companyDTO);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CompanyDTO?> searchCompany(int id)
        {
            try
            {
                var company = await dbContext.Companies.Where(x=> x.CompanyId == id).FirstOrDefaultAsync();
                if (company == null) return null;
                var companyDTO = _mapper.Map<CompanyDTO>(company);
                return companyDTO;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> RegisterUser(UserCreateDTO userCreateDTO, int companyId)
        {
            try
            {
                var user = await dbContext.Users.Where(x => x.Email == userCreateDTO.Email).FirstOrDefaultAsync();
                if (user != null) throw new Exception("This email already exists.");

                user.Password = PasswordEncrypter.Encrypt(user.Password);
                User newUser = _mapper.Map<User>(userCreateDTO);
                newUser.CompanyId = companyId;

                await dbContext.Users.AddAsync(newUser);

                var result = await dbContext.SaveChangesAsync();

                return result > 0 ? "User register successfully!!!" : "Error creating new user!";
            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}
