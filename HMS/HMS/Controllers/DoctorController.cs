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
        [HttpGet]
        public async Task<ActionResult<List<Doctor>>> GetDoctor()
        {
            var data = await context.Doctors.ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctorById(int id)
        {
            var std = await context.Doctors.FindAsync(id);
            if (std == null)
            {
                return NotFound();
            }
            return Ok(std);
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
        public async Task<ActionResult<Doctor>> UpdateDoctor(int id, Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return BadRequest();
            }
            context.Entry(doctor).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(doctor);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Doctor>> DeleteDoctor(int id)
        {
            var doc = await context.Doctors.FindAsync(id);
            if (doc == null)
            {
                return NotFound();
            }
            context.Doctors.Remove(doc);
            await context.SaveChangesAsync();
            return Ok();

        }
    }
}
