using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoWorkshop.Models;

namespace AutoWorkshop.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly workshopDBContext _context;

        public VehiclesController(workshopDBContext context)
        {
            _context = context;
        }

        // GET: Vehicles by Customer Id
        public async Task<IActionResult> Index(int id)
        {
            var customer = await _context.Customers
               .FirstOrDefaultAsync(m => m.CustomerId == id);

            if (customer == null)
            {
                return NotFound();
            }

            ViewBag.CustomerName = $"{customer.LastName}, {(!string.IsNullOrWhiteSpace(customer.FirstName) ? customer.FirstName : string.Empty)}";
            ViewBag.CustomerId = customer.CustomerId;

            var vehicles = await _context.Vehicles.Where(m => m.CustomerId == id).ToListAsync();

            if (vehicles == null)
            {
                return NotFound();
            }

            return View(vehicles);
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Vehicles == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.Customer)
                .FirstOrDefaultAsync(m => m.VehicleNumber == id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewBag.CustomerId = vehicle.CustomerId;
            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create(string id)
        {
            ViewBag.CustomerId = id;
            return View();
        }

        // POST: Vehicles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleNumber,CustomerId,VehicleMake,VehicleModel,VehicleYear")] Vehicle vehicle)
        {
            //For now just preventing entering duplicate vehicle number. I can validate from fron side when submitting.
            if (ModelState.IsValid && _context.Vehicles != null && await _context.Vehicles.Where(v => v.VehicleNumber == vehicle.VehicleNumber).CountAsync() == 0)
            {
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                
                return RedirectToAction("Index", "Vehicles", new { id = vehicle.CustomerId });
            }
            return View(vehicle.CustomerId);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Vehicles == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            ViewBag.CustomerId = vehicle.CustomerId;
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("VehicleNumber,CustomerId,VehicleMake,VehicleModel,VehicleYear")] Vehicle vehicle)
        {
            if (id != vehicle.VehicleNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.VehicleNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Vehicles", new { id = vehicle.CustomerId }); ;
            }
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Vehicles == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.Customer)
                .FirstOrDefaultAsync(m => m.VehicleNumber == id);
            if (vehicle == null)
            {
                return NotFound();
            }
			ViewBag.CustomerId = vehicle.CustomerId;
			return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Vehicles == null)
            {
                return Problem("Entity set 'workshopDBContext.Vehicles'  is null.");
            }
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Vehicles", new { id = vehicle.CustomerId }); ;
        }

        private bool VehicleExists(string id)
        {
          return (_context.Vehicles?.Any(e => e.VehicleNumber == id)).GetValueOrDefault();
        }
    }
}
