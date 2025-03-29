using HMS.Data;
using HMS.Models;
using HMS.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment env;

        public PatientController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }
        // Get All Doctors
        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            var patients = await context.Patients.ToListAsync();
            return Ok(patients);
        }

        // Get Doctor by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatient(int id)
        {
            var patient = await context.Patients.FindAsync(id);
            if (patient == null)
                return NotFound();

            return Ok(patient);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreatePatient([FromForm] PatientDTO patientDto, [FromForm] IFormFile? image)
        {
            try
            {
                var patient = new Patient
                {
                    Name = patientDto.Name,
                    Contact = patientDto.Contact,
                    Email = patientDto.Email,
                    Address = patientDto.Address,
                    Region = patientDto.Region,
                    Country = patientDto.Country,
                    BloodGroup = patientDto.BloodGroup,
                    DateOfBirth = patientDto.DateOfBirth
                };

                if (image != null)
                {
                    var uploadsDir = Path.Combine(env.WebRootPath, "images", "patient");
                    if (!Directory.Exists(uploadsDir))
                    {
                        Directory.CreateDirectory(uploadsDir);
                    }

                    var filePath = Path.Combine(uploadsDir, image.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    patient.ImageUrls = "/images/patient/" + image.FileName;
                }

                context.Patients.Add(patient);
                await context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPatient), new { id = patient.Id }, patient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdatePatient(int id, [FromForm] PatientDTO patientDto, [FromForm] IFormFile? image)
        {
            try
            {
                var patient = await context.Patients.FindAsync(id);
                if (patient == null)
                {
                    return NotFound($"Patient with ID {id} not found.");
                }

                // Update patient details
                patient.Name = patientDto.Name;
                patient.Contact = patientDto.Contact;
                patient.Email = patientDto.Email;
                patient.Address = patientDto.Address;
                patient.Region = patientDto.Region;
                patient.Country = patientDto.Country;
                patient.BloodGroup = patientDto.BloodGroup;
                patient.DateOfBirth = patientDto.DateOfBirth;

                if (image != null)
                {
                    var uploadsDir = Path.Combine(env.WebRootPath, "images", "patient");
                    if (!Directory.Exists(uploadsDir))
                    {
                        Directory.CreateDirectory(uploadsDir);
                    }

                    var filePath = Path.Combine(uploadsDir, image.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    patient.ImageUrls = "/images/patient/" + image.FileName;
                }

                context.Patients.Update(patient);
                await context.SaveChangesAsync();

                return Ok(patient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            try
            {
                var patient = await context.Patients.FindAsync(id);
                if (patient == null)
                {
                    return NotFound($"Patient with ID {id} not found.");
                }

                // Delete the patient image if it exists
                if (!string.IsNullOrEmpty(patient.ImageUrls))
                {
                    var filePath = Path.Combine(env.WebRootPath, patient.ImageUrls.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                context.Patients.Remove(patient);
                await context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
