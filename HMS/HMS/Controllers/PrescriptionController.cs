using HMS.Data;
using HMS.Models;
using HMS.Models.DTO;
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

        // GET: api/prescriptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrescriptionDTO>>> GetPrescriptions()
        {
            var prescriptions = await _context.Prescriptions
                .Include(p => p.Doctor)
                .Include(p => p.Patient)
                .Include(p => p.MedicineInfos)
                .Select(p => new PrescriptionDTO
                {
                    Id = p.Id,
                    DoctorId = p.DoctorId,
                    DoctorName = p.Doctor.Name,
                    PatientId = p.PatientId,
                    PatientName = p.Patient.Name,
                    Duration = p.Duration,
                    Notes = p.Notes,
                    MedicineInfos = p.MedicineInfos.Select(m => new MedicineInfoDTO
                    {
                        Id = m.Id,
                        MedicineName = m.MedicineName,
                        Dosage = m.Dosage,
                        Frequency = m.Frequency
                    }).ToList()
                }).ToListAsync();

            return Ok(prescriptions);
        }

        // GET: api/prescriptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PrescriptionDTO>> GetPrescription(int id)
        {
            var p = await _context.Prescriptions
                .Include(p => p.Doctor)
                .Include(p => p.Patient)
                .Include(p => p.MedicineInfos)
                .Where(p => p.Id == id)
                .Select(prescription => new PrescriptionDTO
                {
                    Id = prescription.Id,
                    DoctorId = prescription.DoctorId,
                    DoctorName = prescription.Doctor.Name,
                    PatientId = prescription.PatientId,
                    PatientName = prescription.Patient.Name,
                    Duration = prescription.Duration,
                    Notes = prescription.Notes,
                    MedicineInfos = prescription.MedicineInfos.Select(m => new MedicineInfoDTO
                    {
                        Id = m.Id,
                        MedicineName = m.MedicineName,
                        Dosage = m.Dosage,
                        Frequency = m.Frequency
                    }).ToList()
                }).FirstOrDefaultAsync();

            if (p == null)
                return NotFound();

            return Ok(p);
        }

        // POST: api/prescriptions
        [HttpPost]
        public async Task<ActionResult> CreatePrescription([FromBody] PrescriptionDTO dto)
        {
            var prescription = new Prescription
            {
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                Duration = dto.Duration,
                Notes = dto.Notes,
                MedicineInfos = dto.MedicineInfos.Select(m => new MedicineInfo
                {
                    MedicineName = m.MedicineName,
                    Dosage = m.Dosage,
                    Frequency = m.Frequency
                }).ToList()
            };

            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Prescription created successfully!" });
        }

        // PUT: api/prescriptions/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePrescription(int id, [FromBody] PrescriptionDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            var prescription = await _context.Prescriptions
                .Include(p => p.MedicineInfos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prescription == null)
                return NotFound();

            prescription.DoctorId = dto.DoctorId;
            prescription.PatientId = dto.PatientId;
            prescription.Duration = dto.Duration;
            prescription.Notes = dto.Notes;

            // Remove old meds
            _context.MedicineInfos.RemoveRange(prescription.MedicineInfos);

            // Add new meds
            prescription.MedicineInfos = dto.MedicineInfos.Select(m => new MedicineInfo
            {
                MedicineName = m.MedicineName,
                Dosage = m.Dosage,
                Frequency = m.Frequency
            }).ToList();

            await _context.SaveChangesAsync();

            return Ok(new { message = "Prescription updated successfully!" });
        }

        // DELETE: api/prescriptions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePrescription(int id)
        {
            var prescription = await _context.Prescriptions
                .Include(p => p.MedicineInfos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prescription == null)
                return NotFound();

            _context.MedicineInfos.RemoveRange(prescription.MedicineInfos);
            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Prescription deleted successfully!" });
        }




    }
}
