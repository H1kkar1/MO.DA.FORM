using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualBasic;
using System.Security.Claims;

namespace MO.DA.FORM.infrastructure
{
    public class LeaderRequirement : IAuthorizationRequirement
    {
        protected internal string leader { get; set; }

        public LeaderRequirement( string led) {
            leader = led;
        }
    }
    public class LeaderHandler : AuthorizationHandler<LeaderRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            LeaderRequirement requirement)
        {
            if (context.User.Identity.Name == "True")
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
