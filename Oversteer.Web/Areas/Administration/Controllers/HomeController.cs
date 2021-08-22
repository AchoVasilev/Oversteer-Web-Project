namespace Oversteer.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : AdministrationController
    {
        public IActionResult Administration()
        {
            return View();
        }
    }
}
