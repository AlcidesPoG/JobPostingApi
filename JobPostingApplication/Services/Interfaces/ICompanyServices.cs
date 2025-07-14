using JobPostingDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPostingApplication.Services.Interfaces
{
    public interface ICompanyServices
    {
        Task<UserLoginDTO> CreateCompany(CompanyCreateDTO createDTO, string baseURL);
        Task<CompanyDTO> EditCompany(CompanyEditDTO editDTO, int companyId, string baseURL);
        Task<CompanyDTO?> searchCompany(int id);
        Task<string> RegisterUser(UserCreateDTO userCreateDTO, int companyId);
    }
}
