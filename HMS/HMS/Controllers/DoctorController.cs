using HMS.Data;
using HMS.Data.Repository.IRepository;
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
        private readonly IDoctorRepository _doctorRepo;

        public DoctorController(IDoctorRepository doctorRepo)
        {
            _doctorRepo = doctorRepo;
        }

        // GET: api/Doctor
        [HttpGet]
        public IActionResult GetDoctors()
        {
            var doctors = _doctorRepo.GetAll();
            return Ok(doctors);
        }

        // GET: api/Doctor/5
        [HttpGet("{id}")]
        public IActionResult GetDoctor(int id)
        {
            var doctor = _doctorRepo.Get(d => d.Id == id);
            if (doctor == null)
                return NotFound();

            return Ok(doctor);
        }

        // POST: api/Doctor
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

                var result = await _doctorRepo.AddWithImageAsync(doctor, image);
                return CreatedAtAction(nameof(GetDoctor), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Doctor/5
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateDoctor(int id, [FromForm] DoctorDTO doctorDto, [FromForm] IFormFile? image)
        {
            try
            {
                var updateDoctor = new Doctor
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

                var result = await _doctorRepo.UpdateWithImageAsync(id, updateDoctor, image);
                if (result == null)
                    return NotFound($"Doctor with ID {id} not found.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Doctor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            try
            {
                var deleted = await _doctorRepo.DeleteWithImageAsync(id);
                if (!deleted)
                    return NotFound($"Doctor with ID {id} not found.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
