using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VenuBooking.Data;
using VenuBooking.Models;
using VenuBooking.Services;

namespace VenuBooking.Controllers
{
    public class VenueController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly BlobService _blobService;

        public VenueController(ApplicationDbContext context, BlobService blobService)
        {
            _context = context;
            _blobService = blobService;
        }

        public async Task<IActionResult> Index(string? search)
        {
            var venues = _context.Venues.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                venues = venues.Where(v =>
                    v.Name.Contains(search) ||
                    v.Location.Contains(search));
            }

            ViewBag.Search = search;

            return View(await venues.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Venue venue, IFormFile? imageFile)
        {
            try
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    venue.ImageURL = await _blobService.UploadFileAsync(imageFile);
                }
                else
                {
                    venue.ImageURL = "https://picsum.photos/201";
                }

                if (ModelState.IsValid)
                {
                    _context.Venues.Add(venue);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Venue created successfully.";
                    return RedirectToAction(nameof(Index));
                }

                return View(venue);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(venue);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var venue = await _context.Venues.FindAsync(id);

            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hasBookings = await _context.Bookings.AnyAsync(b => b.VenueID == id);

            if (hasBookings)
            {
                TempData["Error"] = "Cannot delete this venue because it has active bookings.";
                return RedirectToAction(nameof(Index));
            }

            var venue = await _context.Venues.FindAsync(id);

            if (venue == null)
            {
                return NotFound();
            }

            _context.Venues.Remove(venue);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Venue deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}