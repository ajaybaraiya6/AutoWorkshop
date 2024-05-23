using AutoWorkshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshop.Controllers
{
    public class SlotBookingController : Controller
    {
        private readonly workshopDBContext _context;

        public SlotBookingController(workshopDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Vehicles = await _context.Vehicles.ToListAsync();
            var slots = await _context.Slots.ToListAsync();

            DateTime currentTime = new DateTime(1, 1, 1, 9, 0, 0);

            var slotList = new List<SelectListItem>();
            foreach (Slot item in slots)
            {
                DateTime startTime = currentTime;

                // For now we have integer value fixed but we can create this dynamically but for simple functionality taking Fixed values from DB.
                currentTime = currentTime.AddHours((double)item.AssignedHours);
                slotList.Add(new SelectListItem { Value = $"{item.SlotId}", Text = $"Slot {item.SlotId}, {startTime.ToString("h:mm tt")} - {currentTime.ToString("h:mm tt")}" });
            }

            ViewBag.DisabledDates = await _context.VehicleServices.GroupBy(g => g.ServiceDate).Where(w => w.Count() == 4).Select(s => s.Key).Distinct().ToArrayAsync();

            ViewBag.Slots = slotList;
            return View();
        }

        // POST: SlotBooking/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("VehicleNumber,ServiceDate,SlotId")] VehicleService vehicleService)
        {
            //For now just preventing entering duplicate vehicle number. I can validate from fron side when submitting.
            if (ModelState.IsValid)
            {
                _context.Add(vehicleService);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetSlotsByDate(DateTime date)
        {
            var slotIds = await _context.VehicleServices.Where(c => c.ServiceDate == date.Date).Select(s => s.SlotId).Distinct().ToListAsync();
            var slotList = new List<SelectListItem>();

            var slots = await _context.Slots.ToListAsync();

            DateTime currentTime = new DateTime(1, 1, 1, 9, 0, 0);
            foreach (Slot item in slots)
            {
                if (slotIds.Count > 0 && slotIds.Contains(item.SlotId)) continue;
                DateTime startTime = currentTime;

                // For now we have integer value fixed but we can create this dynamically but for simple functionality taking Fixed values from DB.
                currentTime = currentTime.AddHours((double)item.AssignedHours);
                slotList.Add(new SelectListItem { Value = $"{item.SlotId}", Text = $"Slot {item.SlotId}, {startTime.ToString("h:mm tt")} - {currentTime.ToString("h:mm tt")}" });
            }

            return Json(slotList);
        }

        [HttpGet]
        public async Task<IActionResult> UpcomingServices()
        {
            DateTime today = DateTime.Today;
            int daysUntilNextSunday = ((int)DayOfWeek.Sunday - (int)today.DayOfWeek + 7) % 7; // Number of days until next Sunday
            DateTime nextSunday = today.AddDays(daysUntilNextSunday); // Next Sunday
            DateTime nextMonday = nextSunday.AddDays(1); // Next Monday

            // Calculate the end of the following Sunday
            DateTime followingSunday = nextSunday.AddDays(7);

            var services = await _context.VehicleServices.Where(s => s.ServiceDate >= nextMonday && s.ServiceDate < followingSunday).ToListAsync();
            if (services == null)
            {
                return NotFound();
            }
            return View(services);
        }

		// GET: SlotBooking/Search/Date
		public async Task<IActionResult> Search(DateTime? dueDate)
		{
			if (dueDate == null || dueDate == DateTime.MinValue || _context.Customers == null)
			{
				return NotFound();
			}

			var vehicleService = await _context.VehicleServices.Where(m => m.ServiceDate == dueDate).ToListAsync();
			if (vehicleService == null)
			{
				return NotFound();
			}

			return View(vehicleService);
		}
	}
}
