using HMS.Data;
using HMS.Models;
using HMS.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment env;

        public DoctorController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }

        // Get All Doctors
        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {
            var doctors = await context.Doctors.ToListAsync();
            return Ok(doctors);
        }

        // Get Doctor by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctor(int id)
        {
            var doctor = await context.Doctors.FindAsync(id);
            if (doctor == null)
                return NotFound();

            return Ok(doctor);
        }

        //[HttpPost]
        //public async Task<ActionResult<Doctor>> CreateDoctor(Doctor doctor)
        //{
        //    await context.Doctors.AddAsync(doctor);
        //    await context.SaveChangesAsync();
        //    return Ok(doctor);
        //}

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateDoctor([FromForm] DoctorDTO doctorDto, [FromForm] IFormFile? image)
        {
            try
            {
                var doctor = new Doctor
                {
                    Name = doctorDto.Name,
                    Contact = doctorDto.Contact,
                    Email = doctorDto.Email,
                    Address = doctorDto.Address,
                    Region = doctorDto.Region,
                    Country = doctorDto.Country,
                    PostalCode = doctorDto.PostalCode,
                    Specialization = doctorDto.Specialization
                };

                if (image != null)
                {
                    var uploadsDir = Path.Combine(env.WebRootPath, "images", "doctor");
                    if (!Directory.Exists(uploadsDir))
                    {
                        Directory.CreateDirectory(uploadsDir);
                    }

                    var filePath = Path.Combine(uploadsDir, image.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    doctor.ImageUrl = "/images/doctor/" + image.FileName;
                }

                context.Doctors.Add(doctor);
                await context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetDoctor), new { id = doctor.Id }, doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdatePatient(int id, [FromForm] DoctorDTO doctorDto, [FromForm] IFormFile? image)
        {
            try
            {
                var doctor = await context.Doctors.FindAsync(id);
                if (doctor == null)
                {
                    return NotFound($"Patient with ID {id} not found.");
                }

                // Update patient details
                doctor.Name = doctorDto.Name;
                doctor.Contact = doctorDto.Contact;
                doctor.Email = doctorDto.Email;
                doctor.Address = doctorDto.Address;
                doctor.Region = doctorDto.Region;
                doctor.Country = doctorDto.Country;
                doctor.PostalCode = doctorDto.PostalCode;

                if (image != null)
                {
                    var uploadsDir = Path.Combine(env.WebRootPath, "images", "doctor");
                    if (!Directory.Exists(uploadsDir))
                    {
                        Directory.CreateDirectory(uploadsDir);
                    }

                    var filePath = Path.Combine(uploadsDir, image.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    doctor.ImageUrl = "/images/doctor/" + image.FileName;
                }

                context.Doctors.Update(doctor);
                await context.SaveChangesAsync();

                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            try
            {
                var doctor = await context.Doctors.FindAsync(id);
                if (doctor == null)
                {
                    return NotFound($"Doctor with ID {id} not found.");
                }

                // Delete the patient image if it exists
                if (!string.IsNullOrEmpty(doctor.ImageUrl))
                {
                    var filePath = Path.Combine(env.WebRootPath, doctor.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                context.Doctors.Remove(doctor);
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
