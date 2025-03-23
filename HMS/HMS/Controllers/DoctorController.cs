using HMS.Data;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext context;

        public DoctorController(ApplicationDbContext context)
        {
            this.context = context;
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

        [HttpPost]
        public async Task<ActionResult<Doctor>> CreateDoctor(Doctor doctor)
        {
            await context.Doctors.AddAsync(doctor);
            await context.SaveChangesAsync();
            return Ok(doctor);
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
