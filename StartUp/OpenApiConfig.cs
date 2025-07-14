using JobPostingUtilities.shared;
using Microsoft.OpenApi.Models;

namespace JobPostingApi.StartUp
{
    public static class OpenApiConfig
    {
        public static void UseOpenApi(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "JobPost API V1");
                });
            }

            app.UseCors("AllowAnyOrigin");
            app.UseAuthentication();
            app.UseAuthorization();
        }

    }
}
