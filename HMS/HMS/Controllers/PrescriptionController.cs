using HMS.Data;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PrescriptionController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/Prescriptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prescription>>> GetAllPrescriptions()
        {
            return await _context.Prescriptions
                .Include(p => p.Doctor)
                .Include(p => p.Patient)
                .Include(p => p.MedicineInfos)
                .ToListAsync();
        }

        // GET: api/Prescriptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prescription>> GetPrescriptionById(int id)
        {
            var prescription = await _context.Prescriptions
                .Include(p => p.Doctor)
                .Include(p => p.Patient)
                .Include(p => p.MedicineInfos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prescription == null)
            {
                return NotFound();
            }

            return prescription;
        }

        // POST: api/Prescriptions
        [HttpPost]
        public async Task<ActionResult<Prescription>> CreatePrescription([FromBody] Prescription prescription)
        {
            if (prescription == null)
            {
                return BadRequest("Prescription data is required.");
            }

            // Check if Doctor and Patient exist
            var doctorExists = await _context.Doctors.AnyAsync(d => d.Id == prescription.DoctorId);
            var patientExists = await _context.Patients.AnyAsync(p => p.Id == prescription.PatientId);

            if (!doctorExists || !patientExists)
            {
                return NotFound("Doctor or Patient not found.");
            }

            prescription.Doctor = null;
            prescription.Patient = null;

            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPrescriptionById), new { id = prescription.Id }, prescription);
        }

        // PUT: api/Prescriptions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrescription(int id, [FromBody] Prescription updatedPrescription)
        {
            if (id != updatedPrescription.Id)
            {
                return BadRequest("ID mismatch.");
            }

            // Check if Doctor and Patient exist
            var doctorExists = await _context.Doctors.AnyAsync(d => d.Id == updatedPrescription.DoctorId);
            var patientExists = await _context.Patients.AnyAsync(p => p.Id == updatedPrescription.PatientId);

            if (!doctorExists || !patientExists)
            {
                return NotFound("Doctor or Patient not found.");
            }

            updatedPrescription.Doctor = null;
            updatedPrescription.Patient = null;

            _context.Entry(updatedPrescription).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Prescriptions.Any(p => p.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Prescriptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrescription(int id)
        {
            var prescription = await _context.Prescriptions
                .Include(p => p.MedicineInfos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prescription == null)
            {
                return NotFound();
            }

            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
