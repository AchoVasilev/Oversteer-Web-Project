namespace Oversteer.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Oversteer.Data.Common.Constants.WebConstants;

    [Area("Administration")]
    [Authorize(Roles = AdministratorRoleName)]
    public class AdministrationController : Controller
    {
    }
}
