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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrescriptionDTO>>> GetAllPrescriptions()
        {
            var prescriptions = await _context.Prescriptions
                .Include(p => p.MedicineInfos)
                .ToListAsync();

            var result = prescriptions.Select(p => new PrescriptionDTO
            {
                Id = p.Id,
                DoctorId = p.DoctorId,
                PatientId = p.PatientId,
                Duration = p.Duration,
                Notes = p.Notes,
                MedicineInfos = p.MedicineInfos.Select(m => new MedicineInfoDTO
                {
                    Id = m.Id,
                    MedicineName = m.MedicineName,
                    Dosage = m.Dosage,
                    Frequency = m.Frequency
                }).ToList()
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PrescriptionDTO>> GetPrescriptionById(int id)
        {
            var prescription = await _context.Prescriptions
                .Include(p => p.MedicineInfos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prescription == null)
                return NotFound();

            var dto = new PrescriptionDTO
            {
                Id = prescription.Id,
                DoctorId = prescription.DoctorId,
                PatientId = prescription.PatientId,
                Duration = prescription.Duration,
                Notes = prescription.Notes,
                MedicineInfos = prescription.MedicineInfos.Select(m => new MedicineInfoDTO
                {
                    Id = m.Id,
                    MedicineName = m.MedicineName,
                    Dosage = m.Dosage,
                    Frequency = m.Frequency
                }).ToList()
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePrescription(PrescriptionDTO dto)
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

            return CreatedAtAction(nameof(GetPrescriptionById), new { id = prescription.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePrescription(int id, PrescriptionDTO dto)
        {
            var prescription = await _context.Prescriptions
                .Include(p => p.MedicineInfos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prescription == null)
                return NotFound();

            prescription.DoctorId = dto.DoctorId;
            prescription.PatientId = dto.PatientId;
            prescription.Duration = dto.Duration;
            prescription.Notes = dto.Notes;

            // Remove old medicines and add new
            _context.MedicineInfos.RemoveRange(prescription.MedicineInfos);

            prescription.MedicineInfos = dto.MedicineInfos.Select(m => new MedicineInfo
            {
                MedicineName = m.MedicineName,
                Dosage = m.Dosage,
                Frequency = m.Frequency
            }).ToList();

            await _context.SaveChangesAsync();
            return NoContent();
        }

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

            return NoContent();
        }
    }
}
