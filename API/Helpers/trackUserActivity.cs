using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
namespace API.Helpers
{
    public class trackUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            // Checks if the user is authenticated
            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            var userId = resultContext.HttpContext.User.GetUserId();
            var repository = resultContext.HttpContext.RequestServices.GetService<IUserRepository>();
            var user = await repository.GetUserByIdAsync(userId);
            user.LastActive = DateTime.Now;
            await repository.SaveAllAsync();
        }
    }
}