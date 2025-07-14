using JobPostingApplication.Services.Interfaces;
using JobPostingDomain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace JobPostingApi.Endpoints
{
    public static class CompanyEndpoints
    {

        public static void MapCompanyEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/company");

            group.MapGet("/", async (ICompanyServices companyServices, HttpContext httpContext) =>
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
                    var result = await companyServices.searchCompany(companyId);
                    if (result == null) return Results.NotFound("Company not found");
                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return Results.Problem("An unexpected error occurred.");
                }

            }).WithOpenApi();

            group.MapGet("/{id}", async (int id, ICompanyServices companyServices) =>
            {
                try
                {
                    var result = await companyServices.searchCompany(id);
                    if(result == null) return Results.NotFound("Company not found");
                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return Results.Problem("An unexpected error occurred.");
                }

            }).WithOpenApi();

           
            group.MapPost("/create", async ([FromForm] CompanyCreateDTO companyCreateDTO, ICompanyServices companyServices, HttpRequest request) =>
             {
                if (companyCreateDTO == null) return Results.BadRequest();

                string baseUrl = $"{request.Scheme}://{request.Host}";

                try
                {
                    var result = await companyServices.CreateCompany(companyCreateDTO, baseUrl); 
                    if (result == null) return Results.BadRequest("Error creating company, Check that you haven't use this Email before");
                    return Results.Ok(result);
                }
                catch (Exception ex) {
                    Console.WriteLine($"Error: {ex.Message}");
                    return Results.Problem("An unexpected error occurred.");
                }
            }).DisableAntiforgery().WithOpenApi();

            group.MapPut("/edit", async ([FromForm] CompanyEditDTO companyEditDTO, ICompanyServices companyServices, HttpContext httpContext, HttpRequest request) =>
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

                string baseUrl = $"{request.Scheme}://{request.Host}";

                if (companyEditDTO == null || companyId == 0) return Results.BadRequest();

                try
                {
                    var result = await companyServices.EditCompany(companyEditDTO, companyId, baseUrl);
                    if (result == null) return Results.NotFound("Company not found");

                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return Results.Problem("An unexpected error occurred.");
                }
             }).DisableAntiforgery().RequireAuthorization().WithOpenApi();
        }
    }
}
