using AutoMapper;
using JobPostingDomain.Entities;
using JobPostingInfraestructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPostingUtilities.shared
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CompanyCreateDTO, Company>();
            CreateMap<CompanyEditDTO, Company>();
            CreateMap<Company, CompanyDTO>();
            CreateMap<UserCreateDTO, User>();
            CreateMap<User, UserLoginDTO>();
            CreateMap<JobPostCreateDTO, Post>();
            CreateMap<JobPostEditDTO, Post>();
        }
    }
}
