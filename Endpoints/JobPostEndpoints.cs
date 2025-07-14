using JobPostingApplication.Services.Interfaces;
using JobPostingDomain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace JobPostingApi.Endpoints
{
    public static class JobPostEndpoints
    {
        public static void MapJobPostEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/Jobposts");

            group.MapGet("/", async (string title, string? location, IJobPostService jobPostService) =>
            {
                if (String.IsNullOrEmpty(title)) return Results.BadRequest("The title cannot be null");
                try
                {
                    var jobPosts = await jobPostService.JobList(title, location);
                    return Results.Ok(jobPosts);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return Results.Problem(ex.Message);
                }
            }).WithOpenApi();
        
            group.MapGet("/{id}", async (int id, IJobPostService jobPostService) =>
            {
                try
                {
                    var jobPosts = await jobPostService.JobById(id);
                    return Results.Ok(jobPosts);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return Results.Problem(ex.Message);
                }
            }).WithOpenApi();

            group.MapGet("/dashboardjobs", async (IJobPostService jobPostService, HttpContext httpContext) =>
            {

                var user = httpContext.User;

                if (!user.Identity.IsAuthenticated)
                    return Results.Unauthorized();

                var companyId = 0;

                if (int.TryParse(user.FindFirst("CompanyId")?.Value, out int parsedCompanyId))
                {
                    companyId = parsedCompanyId;
                }
                else if (companyId == 0)
                {
                    return Results.Unauthorized();
                }
                else
                {
                    return Results.Unauthorized();
                }

                try
                {
                    var jobPosts = await jobPostService.JobListByCompany(companyId);
                    return Results.Ok(jobPosts);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return Results.Problem(ex.Message);
                }
            }).WithOpenApi();

            group.MapGet("/company/{companyId}", async (int companyId, IJobPostService jobPostService, HttpContext httpContext) =>
            {
                if (companyId == 0 || companyId == null) return Results.BadRequest("The company id is required");
                
                try
                {
                    var jobPosts = await jobPostService.JobListByCompany(companyId);
                    return Results.Ok(jobPosts);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return Results.Problem(ex.Message);
                }
            }).RequireAuthorization().WithOpenApi();

            group.MapPost("/", async (JobPostCreateDTO jobPostCreateDTO, IJobPostService jobPostService, HttpContext httpContext) =>
            {
                var user = httpContext.User;

                if (!user.Identity.IsAuthenticated)
                    return Results.Unauthorized();

                var companyId = 0;

                if (int.TryParse(user.FindFirst("CompanyId")?.Value, out int parsedCompanyId))
                {
                    companyId = parsedCompanyId;
                }
                else if (companyId == 0)
                {
                    return Results.Unauthorized();
                }
                else
                {
                    return Results.Unauthorized();
                }

                if (jobPostCreateDTO == null) return Results.BadRequest("Job data is required.");
                try
                {
                    var result = await jobPostService.CreateJob(jobPostCreateDTO, companyId);
                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return Results.Problem(ex.Message);
                }

            }).RequireAuthorization().WithOpenApi();

            group.MapPut("/{id}", async (int id, JobPostEditDTO jobPostEditDTO, IJobPostService jobPostService, HttpContext httpContext) =>
            {
                    var user = httpContext.User;
                if (!user.Identity.IsAuthenticated)
                    return Results.Unauthorized();

                var companyId = 0;

                if (int.TryParse(user.FindFirst("CompanyId")?.Value, out int parsedCompanyId))
                {
                    companyId = parsedCompanyId;
                }
                else if (companyId == 0)
                {
                    return Results.Unauthorized();
                }
                else
                {
                    return Results.Unauthorized();
                }

                if (jobPostEditDTO == null) return Results.BadRequest("Job data is required.");
                try
                {
                    var result = await jobPostService.EditJob(jobPostEditDTO, id, companyId);
                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return Results.Problem(ex.Message);
                }

            }).RequireAuthorization().WithOpenApi();

            group.MapDelete("/{id}", async (int id, IJobPostService jobPostService, HttpContext httpContext) =>
            {
                var user = httpContext.User;
                if (!user.Identity.IsAuthenticated)
                    return Results.Unauthorized();
                var companyId = 0;
                if (int.TryParse(user.FindFirst("CompanyId")?.Value, out int parsedCompanyId))
                {
                    companyId = parsedCompanyId;
                }
                else if (companyId == 0)
                {
                    return Results.Unauthorized();
                }
                else
                {
                    return Results.Unauthorized();
                }
                    
                try
                {
                    var result = await jobPostService.DeleteJob(id, companyId);
                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return Results.Problem(ex.Message);
                }
            }).RequireAuthorization().WithOpenApi();
        }
    }
}
