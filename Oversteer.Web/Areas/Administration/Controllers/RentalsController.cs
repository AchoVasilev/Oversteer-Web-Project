namespace Oversteer.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    using Oversteer.Data;
    using Oversteer.Data.Models.Rentals;

    [Area("Administration")]
    public class RentalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Administration/Rentals
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rentals.Include(r => r.Car).Include(r => r.Company).Include(r => r.DropOffLocation).Include(r => r.Feedback).Include(r => r.PickUpLocation).Include(r => r.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Rentals/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .Include(r => r.Car)
                .Include(r => r.Company)
                .Include(r => r.DropOffLocation)
                .Include(r => r.Feedback)
                .Include(r => r.PickUpLocation)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Administration/Rentals/Create
        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Description");
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Description");
            ViewData["DropOffLocationId"] = new SelectList(_context.Locations, "Id", "Name");
            ViewData["FeedbackId"] = new SelectList(_context.Feedbacks, "Id", "Comment");
            ViewData["PickUpLocationId"] = new SelectList(_context.Locations, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Administration/Rentals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatedOn,StartDate,ReturnDate,Price,IsDeleted,DeletedOn,OrderStatus,PickUpLocationId,DropOffLocationId,UserId,CompanyId,CarId,FeedbackId")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Description", rental.CarId);
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Description", rental.CompanyId);
            ViewData["DropOffLocationId"] = new SelectList(_context.Locations, "Id", "Name", rental.DropOffLocationId);
            ViewData["FeedbackId"] = new SelectList(_context.Feedbacks, "Id", "Comment", rental.FeedbackId);
            ViewData["PickUpLocationId"] = new SelectList(_context.Locations, "Id", "Name", rental.PickUpLocationId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", rental.UserId);
            return View(rental);
        }

        // GET: Administration/Rentals/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Description", rental.CarId);
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Description", rental.CompanyId);
            ViewData["DropOffLocationId"] = new SelectList(_context.Locations, "Id", "Name", rental.DropOffLocationId);
            ViewData["FeedbackId"] = new SelectList(_context.Feedbacks, "Id", "Comment", rental.FeedbackId);
            ViewData["PickUpLocationId"] = new SelectList(_context.Locations, "Id", "Name", rental.PickUpLocationId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", rental.UserId);
            return View(rental);
        }

        // POST: Administration/Rentals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,CreatedOn,StartDate,ReturnDate,Price,IsDeleted,DeletedOn,OrderStatus,PickUpLocationId,DropOffLocationId,UserId,CompanyId,CarId,FeedbackId")] Rental rental)
        {
            if (id != rental.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalExists(rental.Id))
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
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Description", rental.CarId);
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Description", rental.CompanyId);
            ViewData["DropOffLocationId"] = new SelectList(_context.Locations, "Id", "Name", rental.DropOffLocationId);
            ViewData["FeedbackId"] = new SelectList(_context.Feedbacks, "Id", "Comment", rental.FeedbackId);
            ViewData["PickUpLocationId"] = new SelectList(_context.Locations, "Id", "Name", rental.PickUpLocationId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", rental.UserId);
            return View(rental);
        }

        // GET: Administration/Rentals/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .Include(r => r.Car)
                .Include(r => r.Company)
                .Include(r => r.DropOffLocation)
                .Include(r => r.Feedback)
                .Include(r => r.PickUpLocation)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Administration/Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalExists(string id)
        {
            return _context.Rentals.Any(e => e.Id == id);
        }
    }
}
