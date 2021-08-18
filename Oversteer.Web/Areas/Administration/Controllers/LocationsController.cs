namespace Oversteer.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Oversteer.Services.Companies;

    using static Oversteer.Data.Common.Constants.WebConstants;

    [Authorize(Roles = AdministratorRoleName)]
    public class LocationsController : AdministrationController
    {
        private readonly ILocationService locationService;

        public LocationsController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        public IActionResult All()
        {
            var models = this.locationService.GetAllLocations();

            return View(models);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.locationService.DeleteLocationAsync(id);

            this.TempData["Message"] = "The location was removed successfully.";

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
