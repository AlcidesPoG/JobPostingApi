using JobPostingApplication.Services.Interfaces;
using JobPostingDomain.Criteria.Auth;

namespace JobPostingApi.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/auth");

            group.MapPost("/login", async (UserCriteria userCriteria, IAuthService authService) =>
            {
                var loginResult = await authService.Login(userCriteria);
                if (loginResult != null)
                {
                   return Results.Ok(loginResult);
                }

                return Results.BadRequest("Usuario o contraseña incorrecta");

            });

        }
    }
}
