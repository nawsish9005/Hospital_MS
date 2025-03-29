using HMS.Data;
using HMS.Models;
using HMS.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalRecordController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        public MedicalRecordController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        // GET all medical records with optional filtering by PatientId
        [HttpGet]
        public async Task<IActionResult> GetAllMedicalRecords([FromQuery] int? patientId)
        {
            try
            {
                var query = _context.MedicalRecords.AsQueryable();

                if (patientId.HasValue)
                {
                    query = query.Where(mr => mr.PatientId == patientId.Value);
                }

                var medicalRecords = await query.ToListAsync();

                return Ok(medicalRecords);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET a specific medical record
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicalRecord(int id)
        {
            var medicalRecord = await _context.MedicalRecords.FindAsync(id);
            if (medicalRecord == null)
            {
                return NotFound($"Medical record with ID {id} not found.");
            }
            return Ok(medicalRecord);
        }

        // CREATE a new medical record
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateMedicalRecord([FromForm] MedicalRecordDTO medicalRecordDto)
        {
            try
            {
                var medicalRecord = new MedicalRecord
                {
                    PatientId = medicalRecordDto.PatientId,
                    Diagnosis = medicalRecordDto.Diagnosis,
                    Treatment = medicalRecordDto.Treatment,
                    Current_medications = medicalRecordDto.CurrentMedications,
                    Consultation_Notes = medicalRecordDto.ConsultationNotes,
                    FollowUp_Notes = medicalRecordDto.FollowUpNotes,
                    Laboratory_Results = medicalRecordDto.LaboratoryResults,
                    Therapies = medicalRecordDto.Therapies,
                    Next_Visit_Date = medicalRecordDto.NextVisitDate,
                    PaymentRecords = medicalRecordDto.PaymentRecords
                };

                // Handle image uploads
                if (medicalRecordDto.Images != null && medicalRecordDto.Images.Any())
                {
                    var uploadDir = Path.Combine(_env.WebRootPath, "images", "medicalRecords");
                    if (!Directory.Exists(uploadDir))
                    {
                        Directory.CreateDirectory(uploadDir);
                    }

                    List<string> imagePaths = new List<string>();
                    foreach (var image in medicalRecordDto.Images)
                    {
                        var filePath = Path.Combine(uploadDir, image.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }
                        imagePaths.Add("/images/medicalRecords/" + image.FileName);
                    }

                    medicalRecord.ImageUrls = string.Join(";", imagePaths);
                }

                _context.MedicalRecords.Add(medicalRecord);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetMedicalRecord), new { id = medicalRecord.Id }, medicalRecord);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // UPDATE an existing medical record
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateMedicalRecord(int id, [FromForm] MedicalRecordDTO medicalRecordDto)
        {
            try
            {
                var medicalRecord = await _context.MedicalRecords.FindAsync(id);
                if (medicalRecord == null)
                {
                    return NotFound($"Medical record with ID {id} not found.");
                }

                medicalRecord.Diagnosis = medicalRecordDto.Diagnosis;
                medicalRecord.Treatment = medicalRecordDto.Treatment;
                medicalRecord.Current_medications = medicalRecordDto.CurrentMedications;
                medicalRecord.Consultation_Notes = medicalRecordDto.ConsultationNotes;
                medicalRecord.FollowUp_Notes = medicalRecordDto.FollowUpNotes;
                medicalRecord.Laboratory_Results = medicalRecordDto.LaboratoryResults;
                medicalRecord.Therapies = medicalRecordDto.Therapies;
                medicalRecord.Next_Visit_Date = medicalRecordDto.NextVisitDate;
                medicalRecord.PaymentRecords = medicalRecordDto.PaymentRecords;

                // Handle image update
                if (medicalRecordDto.Images != null && medicalRecordDto.Images.Any())
                {
                    var uploadDir = Path.Combine(_env.WebRootPath, "images", "medicalRecords");
                    if (!Directory.Exists(uploadDir))
                    {
                        Directory.CreateDirectory(uploadDir);
                    }

                    List<string> imagePaths = new List<string>();
                    foreach (var image in medicalRecordDto.Images)
                    {
                        var filePath = Path.Combine(uploadDir, image.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }
                        imagePaths.Add("/images/medicalRecords/" + image.FileName);
                    }

                    medicalRecord.ImageUrls = string.Join(";", imagePaths);
                }

                _context.MedicalRecords.Update(medicalRecord);
                await _context.SaveChangesAsync();

                return Ok(medicalRecord);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE a medical record
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalRecord(int id)
        {
            try
            {
                var medicalRecord = await _context.MedicalRecords.FindAsync(id);
                if (medicalRecord == null)
                {
                    return NotFound($"Medical record with ID {id} not found.");
                }

                // Delete associated images if they exist
                if (!string.IsNullOrEmpty(medicalRecord.ImageUrls))
                {
                    var imagePaths = medicalRecord.ImageUrls.Split(";");
                    foreach (var imagePath in imagePaths)
                    {
                        var filePath = Path.Combine(_env.WebRootPath, imagePath.TrimStart('/'));
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }
                }

                _context.MedicalRecords.Remove(medicalRecord);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
