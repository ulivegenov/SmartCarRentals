namespace SmartCarRentals.Web.Infrastructure
{
    using Hangfire.Dashboard;
    using SmartCarRentals.Common;

    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            return httpContext.User.IsInRole(GlobalConstants.AdministratorRoleName);
        }
    }
}
