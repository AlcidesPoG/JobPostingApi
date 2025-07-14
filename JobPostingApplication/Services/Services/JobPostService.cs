using AutoMapper;
using JobPostingApplication.Services.Interfaces;
using JobPostingDomain.Entities;
using JobPostingInfraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPostingApplication.Services.Services
{
    public class JobPostService : IJobPostService
    {

        private readonly IMapper _mapper;
        private JobPostDbContext dbContext;
        private readonly IConfiguration configuration;

        public JobPostService(IMapper mapper, JobPostDbContext dbContext, IConfiguration configuration)
        {
            this.configuration = configuration;
            this._mapper = mapper;
            this.dbContext = dbContext;
        }
        public async Task<List<JobPostDTO>> JobList(string title, string? location)
        {
            var Jobs = await (from C in dbContext.Companies
                        join P in dbContext.Posts on C.CompanyId equals P.CompanyId
                        where P.Title.Contains(title) &&
                         (string.IsNullOrEmpty(location) || P.Location.Contains(location)) &&
                              P.Status == "Active"
                              orderby P.CreatedAt descending
                              select new JobPostDTO
                        {
                            PostId = P.PostId,
                            Title = P.Title,
                            Location = P.Location,
                            Type = P.Type,
                            Category = P.Category,
                            Description = P.Description,
                            Requirements = P.Requirements,
                            Salary = P.Salary,
                            ApplyUrl = P.ApplyUrl,
                            CreatedAt = P.CreatedAt,
                            Status = P.Status,
                            CompanyName = C.Name,
                            CompanyImgUrl = C.Photo,
                            CompanyId = P.CompanyId
                        }).ToListAsync();
            return Jobs;
        }

        public async Task<List<JobPostDTO>> JobListByCompany(int companyId)
        {
            var Jobs = await (from C in dbContext.Companies
                              join P in dbContext.Posts on C.CompanyId equals P.CompanyId
                              where P.CompanyId == companyId
                              orderby P.CreatedAt descending
                              select new JobPostDTO
                              {
                                  PostId = P.PostId,
                                  Title = P.Title,
                                  Location = P.Location,
                                  Type = P.Type,
                                  Category = P.Category,
                                  Description = P.Description,
                                  Requirements = P.Requirements,
                                  Salary = P.Salary,
                                  ApplyUrl = P.ApplyUrl,
                                  CreatedAt = P.CreatedAt,
                                  Status = P.Status,
                                  CompanyName = C.Name,
                                  CompanyImgUrl = C.Photo,
                                  CompanyId = P.CompanyId
                              }).ToListAsync();
            return Jobs;
        }

        public async Task<JobPostDTO> JobById(int id)
        {
            var Jobs = await(from C in dbContext.Companies
                             join P in dbContext.Posts on C.CompanyId equals P.CompanyId
                             where P.PostId == id
                             select new JobPostDTO
                             {
                                 PostId = P.PostId,
                                 Title = P.Title,
                                 Location = P.Location,
                                 Type = P.Type,
                                 Category = P.Category,
                                 Description = P.Description,
                                 Requirements = P.Requirements,
                                 Salary = P.Salary,
                                 ApplyUrl = P.ApplyUrl,
                                 CreatedAt = P.CreatedAt,
                                 Status = P.Status,
                                 CompanyName = C.Name,
                                 CompanyImgUrl = C.Photo,
                                 CompanyId = P.CompanyId
                             }).FirstOrDefaultAsync();
            return Jobs;
        }

        public async Task<string?> CreateJob(JobPostCreateDTO jobPostCreateDTO, int companyId)
        {
            var newJob = _mapper.Map<Post>(jobPostCreateDTO);
            newJob.CompanyId = companyId;
            newJob.Status = "Active";
            try
            {
                await dbContext.Posts.AddAsync(newJob);
                await dbContext.SaveChangesAsync();
                return "Job created successfully";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating job: {ex.Message}");
            }
        }

        public async Task<string?> DeleteJob(int id, int companyId)
        {
            var searchJob = await dbContext.Posts.FirstOrDefaultAsync(x => x.PostId == id && x.CompanyId == companyId);
            if (searchJob == null)
            {
                throw new Exception("Job not found or does not belong to the company.");
            }
            try
            {
                dbContext.Posts.Remove(searchJob);
                await dbContext.SaveChangesAsync();
                return "Job deleted successfully";
            }
            catch (Exception ex)
            {
                return $"Error deleting job: {ex.Message}";
            }
        }

        public async Task<string?> EditJob(JobPostEditDTO jobPostCreateDTO, int id, int companyId)
        {
            var searchJob = await dbContext.Posts.FirstOrDefaultAsync(x => x.PostId == id && x.CompanyId == companyId);
            if(searchJob == null)
            {
                throw new Exception("Job not found or does not belong to the company.");
            }
            try
            {
                _mapper.Map(jobPostCreateDTO, searchJob);
                await dbContext.SaveChangesAsync();
                return "Job updated successfully";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating job: {ex.Message}");
            }
        }
      
    }
}
