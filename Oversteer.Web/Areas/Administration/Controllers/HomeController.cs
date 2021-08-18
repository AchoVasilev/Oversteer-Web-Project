namespace Oversteer.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Oversteer.Data.Common.Constants.WebConstants;

    [Authorize(Roles = AdministratorRoleName)]
    public class HomeController : AdministrationController
    {
        public IActionResult Administration()
        {
            return View();
        }
    }
}
