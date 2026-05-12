using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

namespace ClinicManagementSystem.Controllers
{
    public class MedicalRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedicalRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LIST

        public async Task<IActionResult> Index()
        {
            var medicalRecords = _context.MedicalRecords
                .Include(m => m.Patient);

            return View(await medicalRecords.ToListAsync());
        }

        // CREATE GET

        public IActionResult Create()
        {
            ViewBag.Patients = new SelectList(
                _context.Patients,
                "Id",
                "FullName");

            return View();
        }

        // CREATE POST

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(MedicalRecord medicalRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicalRecord);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Patients = new SelectList(
                _context.Patients,
                "Id",
                "FullName",
                medicalRecord.PatientId);

            return View(medicalRecord);
        }

        // EDIT GET

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalRecord = await _context.MedicalRecords.FindAsync(id);

            if (medicalRecord == null)
            {
                return NotFound();
            }

            ViewBag.Patients = new SelectList(
                _context.Patients,
                "Id",
                "FullName",
                medicalRecord.PatientId);

            return View(medicalRecord);
        }

        // EDIT POST

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, MedicalRecord medicalRecord)
        {
            if (id != medicalRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(medicalRecord);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Patients = new SelectList(
                _context.Patients,
                "Id",
                "FullName",
                medicalRecord.PatientId);

            return View(medicalRecord);
        }

        // DELETE GET

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalRecord = await _context.MedicalRecords
                .Include(m => m.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (medicalRecord == null)
            {
                return NotFound();
            }

            return View(medicalRecord);
        }

        // DELETE POST

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicalRecord = await _context.MedicalRecords.FindAsync(id);

            if (medicalRecord != null)
            {
                _context.MedicalRecords.Remove(medicalRecord);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // PRINT PDF

        public async Task<IActionResult> Print()
        {
            var medicalRecords = await _context.MedicalRecords
                .Include(m => m.Patient)
                .ToListAsync();

            return new ViewAsPdf("Print", medicalRecords)
            {
                FileName = "MedicalRecords.pdf"
            };
        }
    }
}