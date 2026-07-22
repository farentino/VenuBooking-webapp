using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VenuBooking.Data;
using VenuBooking.Models;

namespace VenuBooking.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _context.Events
                .Include(e => e.EventType)
                .ToListAsync();

            return View(events);
        }

        public IActionResult Create()
        {
            ViewBag.EventTypeID = new SelectList(_context.EventTypes, "EventTypeID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event ev)
        {
            if (ev.EndDate < ev.StartDate)
            {
                ModelState.AddModelError("", "End Date cannot be before Start Date.");
            }

            if (string.IsNullOrEmpty(ev.ImageURL))
            {
                ev.ImageURL = "https://picsum.photos/203";
            }

            if (ModelState.IsValid)
            {
                _context.Events.Add(ev);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Event created successfully.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.EventTypeID = new SelectList(_context.EventTypes, "EventTypeID", "Name", ev.EventTypeID);
            return View(ev);
        }
    }
}