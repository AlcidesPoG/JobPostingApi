using JobPostingApi.Endpoints;
using JobPostingApi.StartUp;

var builder = WebApplication.CreateBuilder(args);

builder.AddDependecies();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var app = builder.Build();
app.UseStaticFiles();

app.UseOpenApi();

app.UseHttpsRedirection();

app.MapCompanyEndpoints();
app.MapAuthEndpoints();
app.MapJobPostEndpoints();


app.Run();



