using JobPostingApplication.Services.Interfaces;
using JobPostingApplication.Services.Services;
using JobPostingInfraestructure.Persistence;
using JobPostingUtilities.shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace JobPostingApi.StartUp
{
    public static class DependenciesConfig
    {

        public static void AddDependecies(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JobPost API", Version = "v1" });
            });
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddDbContext<JobPostDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ICompanyServices, CompanyServices>();
            builder.Services.AddScoped<IJobPostService, JobPostService>();
            builder.Services.AddCors((options) => options.AddPolicy("AllowAnyOrigin", policy =>
            {
                policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            }));

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTKey"]))
                };
            });
            builder.Services.AddAuthorization();



        }
    }
}
