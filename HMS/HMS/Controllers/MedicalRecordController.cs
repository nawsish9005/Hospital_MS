using HMS.Data;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalRecordController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MedicalRecordController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/MedicalRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalRecord>>> GetAllMedicalRecords()
        {
            return await _context.MedicalRecords
                .Include(a => a.Patient)
                .ToListAsync();
        }

        // GET: api/MedicalRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecord>> GetMedicalRecordById(int id)
        {
            var record = await _context.MedicalRecords
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (record == null)
            {
                return NotFound();
            }

            return record;
        }

        // POST: api/MedicalRecords
        [HttpPost]
        public async Task<ActionResult<MedicalRecord>> CreateMedicalRecord([FromBody] MedicalRecord medicalRecord)
        {
            if (medicalRecord == null)
            {
                return BadRequest("Medical Record data is required.");
            }

            // Manually validate required fields
            if (medicalRecord.PatientId == 0 || string.IsNullOrEmpty(medicalRecord.Diagnosis))
            {
                return BadRequest("PatientId and Diagnosis are required.");
            }

            // Check if the Patient exists
            var patientExists = await _context.Patients.AnyAsync(p => p.Id == medicalRecord.PatientId);

            if (!patientExists)
            {
                return NotFound("Patient not found.");
            }

            // Ensure the navigation property is null (it should not be provided in the request)
            medicalRecord.Patient = null;

            _context.MedicalRecords.Add(medicalRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMedicalRecordById), new { id = medicalRecord.Id }, medicalRecord);
        }

        // PUT: api/Appointments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicalRecord(int id, [FromBody] MedicalRecord medicalRecord)
        {
            if (id != medicalRecord.Id)
            {
                return BadRequest("ID mismatch.");
            }

            var patientExists = await _context.Patients.AnyAsync(p => p.Id == medicalRecord.PatientId);

            if (!patientExists)
            {
                return NotFound("Patient not found.");
            }

            _context.Entry(medicalRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Appointments.Any(a => a.Id == id))
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

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalRecord(int id)
        {
            var records = await _context.MedicalRecords.FindAsync(id);
            if (records == null)
            {
                return NotFound();
            }

            _context.MedicalRecords.Remove(records);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
