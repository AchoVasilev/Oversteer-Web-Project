namespace Oversteer.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    using Oversteer.Data;
    using Oversteer.Data.Models.Cars;

    public class CarsController : AdministrationController
    {
        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Administration/Cars
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Cars.Include(c => c.Brand).Include(c => c.CarType).Include(c => c.Color).Include(c => c.Company).Include(c => c.Fuel).Include(c => c.Location).Include(c => c.Model).Include(c => c.Transmission);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.CarType)
                .Include(c => c.Color)
                .Include(c => c.Company)
                .Include(c => c.Fuel)
                .Include(c => c.Location)
                .Include(c => c.Model)
                .Include(c => c.Transmission)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Administration/Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["CarBrandId"] = new SelectList(_context.CarBrands, "Id", "Name", car.CarBrandId);
            ViewData["CarTypeId"] = new SelectList(_context.CarTypes, "Id", "Name", car.CarTypeId);
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Name", car.ColorId);
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Description", car.CompanyId);
            ViewData["FuelId"] = new SelectList(_context.Fuels, "Id", "Name", car.FuelId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name", car.LocationId);
            ViewData["CarModelId"] = new SelectList(_context.CarModels, "Id", "Name", car.CarModelId);
            ViewData["TransmissionId"] = new SelectList(_context.Transmissions, "Id", "Name", car.TransmissionId);
            return View(car);
        }

        // POST: Administration/Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarBrandId,CarModelId,ModelYear,DailyPrice,SeatsCount,IsDeleted,IsAvailable,DeleteDate,Description,ColorId,FuelId,CarTypeId,TransmissionId,CompanyId,LocationId")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarBrandId"] = new SelectList(_context.CarBrands, "Id", "Name", car.CarBrandId);
            ViewData["CarTypeId"] = new SelectList(_context.CarTypes, "Id", "Name", car.CarTypeId);
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Name", car.ColorId);
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Description", car.CompanyId);
            ViewData["FuelId"] = new SelectList(_context.Fuels, "Id", "Name", car.FuelId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name", car.LocationId);
            ViewData["CarModelId"] = new SelectList(_context.CarModels, "Id", "Name", car.CarModelId);
            ViewData["TransmissionId"] = new SelectList(_context.Transmissions, "Id", "Name", car.TransmissionId);
            return View(car);
        }

        // GET: Administration/Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.CarType)
                .Include(c => c.Color)
                .Include(c => c.Company)
                .Include(c => c.Fuel)
                .Include(c => c.Location)
                .Include(c => c.Model)
                .Include(c => c.Transmission)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Administration/Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
