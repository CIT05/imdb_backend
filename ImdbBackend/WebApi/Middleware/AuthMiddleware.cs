
using DataLayer.Users;

namespace WebApi.Middleware;

public static class AuthExt
{
    public static void UseAuth(this IApplicationBuilder app)
    {
        app.UseMiddleware<AuthMiddleware>();
    }
}
   
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUserDataService _dataService;

        public AuthMiddleware(RequestDelegate next, IUserDataService userDataService)
        {
            _next = next;
            _dataService = userDataService;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var userName = context.Request.Headers.Authorization.FirstOrDefault();
            _dataService.GetUserByName(userName);

            if (!string.IsNullOrEmpty(userName)) {
                var user = _dataService.GetUserByName(userName);
                if (user != null)
                {
                    context.Items["User"] = user;
                }
            }
            await _next(context);

        }
    }

