using HMS.Data;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/Appointment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAllAppointments()
        {
            return await _context.Appointments
                .Include(a => a.Doctor) // Include Doctor details
                .Include(a => a.Patient) // Include Patient details
                .ToListAsync();
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointmentById(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Doctor) // Include Doctor details
                .Include(a => a.Patient) // Include Patient details
                .FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
            {
                return NotFound();
            }

            return appointment;
        }

        // POST: api/Appointments
        [HttpPost]
        public async Task<ActionResult<Appointment>> CreateAppointment([FromBody] Appointment appointment)
        {
            if (appointment == null)
            {
                return BadRequest("Appointment data is required.");
            }

            // Check if the Doctor and Patient exist
            var doctorExists = await _context.Doctors.AnyAsync(d => d.Id == appointment.DoctorId);
            var patientExists = await _context.Patients.AnyAsync(p => p.Id == appointment.PatientId);

            if (!doctorExists || !patientExists)
            {
                return NotFound("Doctor or Patient not found.");
            }

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.Id }, appointment);
        }

            // PUT: api/Appointments/5
            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateAppointment(int id, [FromBody] Appointment updatedAppointment)
            {
                if (id != updatedAppointment.Id)
                {
                    return BadRequest("ID mismatch.");
                }

                // Check if the Doctor and Patient exist
                var doctorExists = await _context.Doctors.AnyAsync(d => d.Id == updatedAppointment.DoctorId);
                var patientExists = await _context.Patients.AnyAsync(p => p.Id == updatedAppointment.PatientId);

                if (!doctorExists || !patientExists)
                {
                    return NotFound("Doctor or Patient not found.");
                }

                _context.Entry(updatedAppointment).State = EntityState.Modified;

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
            public async Task<IActionResult> DeleteAppointment(int id)
            {
                var appointment = await _context.Appointments.FindAsync(id);
                if (appointment == null)
                {
                    return NotFound();
                }

                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();

                return NoContent();
            }
    }
}
