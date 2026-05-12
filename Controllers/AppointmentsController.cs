using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;


namespace ClinicManagementSystem.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LIST

        public async Task<IActionResult> Index()
        {
            var appointments = _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor);

            return View(await appointments.ToListAsync());
        }

        // CREATE GET

        public IActionResult Create()
        {
            ViewBag.Patients = new SelectList(
                _context.Patients,
                "Id",
                "FullName");

            ViewBag.Doctors = new SelectList(
                _context.Doctors,
                "Id",
                "FullName");

            return View();
        }

        // CREATE POST

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Patients = new SelectList(
                _context.Patients,
                "Id",
                "FullName",
                appointment.PatientId);

            ViewBag.Doctors = new SelectList(
                _context.Doctors,
                "Id",
                "FullName",
                appointment.DoctorId);

            return View(appointment);
        }

        // EDIT GET

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            ViewBag.Patients = new SelectList(
                _context.Patients,
                "Id",
                "FullName",
                appointment.PatientId);

            ViewBag.Doctors = new SelectList(
                _context.Doctors,
                "Id",
                "FullName",
                appointment.DoctorId);

            return View(appointment);
        }

        // EDIT POST

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(appointment);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(appointment);
        }

        // DELETE GET

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // DELETE POST

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Print()
{
    var appointments = await _context.Appointments
        .Include(a => a.Patient)
        .Include(a => a.Doctor)
        .ToListAsync();

    return new ViewAsPdf("Print", appointments)
    {
        FileName = "Appointments.pdf"
    };
}
    }
}