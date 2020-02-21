namespace SmartCarRentals.Web.Areas.Administration.Controllers
{
    using SmartCarRentals.Common;
    using SmartCarRentals.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
