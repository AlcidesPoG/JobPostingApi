using JobPostingDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPostingApplication.Services.Interfaces
{
    public interface IJobPostService
    {
        Task<List<JobPostDTO>> JobList(string title, string? location);
        Task<List<JobPostDTO>> JobListByCompany(int companyId);
        Task<JobPostDTO> JobById(int id);
        Task<string?> CreateJob(JobPostCreateDTO jobPostCreateDTO, int companyId);
        Task<string?> EditJob(JobPostEditDTO jobPostCreateDTO, int id, int companyId);
        Task<string?> DeleteJob(int id, int companyId);
    }
}
