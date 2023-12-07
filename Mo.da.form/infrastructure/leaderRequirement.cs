using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MO.DA.FORM.infrastructure
{
    public class leaderRequirement
    {
        protected internal string leader { get; set; }

        public leaderRequirement( string led) {
            leader = led;
        }
    }
    //public class leaderHandler : AuthorizationHandler<leaderRequirement>
    //{
    //    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
    //        leaderRequirement requirement)
    //    {
    //        if (context.User.HasClaim(c => c.Type == Claim))
    //        {
    //            var year = 0;
    //            if (Int32.TryParse(context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth).Value, out year))
    //            {
    //                if ((DateTime.Now.Year - year) >= requirement.Age)
    //                {
    //                    context.Succeed(requirement);
    //                }
    //            }
    //        }
    //        return Task.CompletedTask;
    //    }
    //}
}
