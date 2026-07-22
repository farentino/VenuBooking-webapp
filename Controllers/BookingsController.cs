using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VenuBooking.Data;
using VenuBooking.Models;

namespace VenuBooking.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(
            string? search,
            int? eventTypeId,
            int? venueId,
            DateTime? startDate,
            DateTime? endDate,
            string? availability)
        {
            var bookings = _context.Bookings
                .Include(b => b.Venue)
                .Include(b => b.Event)
                .ThenInclude(e => e.EventType)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                bookings = bookings.Where(b =>
                    b.BookingID.ToString().Contains(search) ||
                    b.Event.Name.Contains(search));
            }

            if (eventTypeId.HasValue && eventTypeId.Value > 0)
            {
                bookings = bookings.Where(b => b.Event.EventTypeID == eventTypeId.Value);
            }

            if (venueId.HasValue && venueId.Value > 0)
            {
                bookings = bookings.Where(b => b.VenueID == venueId.Value);
            }

            if (startDate.HasValue)
            {
                bookings = bookings.Where(b => b.BookingDate.Date >= startDate.Value.Date);
            }

            if (endDate.HasValue)
            {
                bookings = bookings.Where(b => b.BookingDate.Date <= endDate.Value.Date);
            }

            ViewBag.Search = search;
            ViewBag.EventTypeID = new SelectList(_context.EventTypes, "EventTypeID", "Name", eventTypeId);
            ViewBag.VenueID = new SelectList(_context.Venues, "VenueID", "Name", venueId);
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");
            ViewBag.Availability = availability;

            return View(await bookings.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.VenueID = new SelectList(_context.Venues, "VenueID", "Name");
            ViewBag.EventID = new SelectList(_context.Events, "EventID", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            bool doubleBooked = await _context.Bookings.AnyAsync(b =>
                b.VenueID == booking.VenueID &&
                b.BookingDate.Date == booking.BookingDate.Date);

            if (doubleBooked)
            {
                ModelState.AddModelError("", "This venue is already booked on this date.");
            }

            if (ModelState.IsValid)
            {
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Booking created successfully.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.VenueID = new SelectList(_context.Venues, "VenueID", "Name", booking.VenueID);
            ViewBag.EventID = new SelectList(_context.Events, "EventID", "Name", booking.EventID);

            return View(booking);
        }
    }
}