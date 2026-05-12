using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Patients.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(patient);
        }
        // EDIT GET

public async Task<IActionResult> Edit(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var patient = await _context.Patients.FindAsync(id);

    if (patient == null)
    {
        return NotFound();
    }

    return View(patient);
}

// EDIT POST

[HttpPost]
[ValidateAntiForgeryToken]

public async Task<IActionResult> Edit(
    int id,
    Patient patient)
{
    if (id != patient.Id)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        _context.Update(patient);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    return View(patient);
}

// DELETE GET

public async Task<IActionResult> Delete(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var patient = await _context.Patients
        .FirstOrDefaultAsync(m => m.Id == id);

    if (patient == null)
    {
        return NotFound();
    }

    return View(patient);
}

// DELETE POST

[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]

public async Task<IActionResult> DeleteConfirmed(int id)
{
    var patient = await _context.Patients.FindAsync(id);

    if (patient != null)
    {
        _context.Patients.Remove(patient);
    }

    await _context.SaveChangesAsync();

    return RedirectToAction(nameof(Index));
}
    }
}