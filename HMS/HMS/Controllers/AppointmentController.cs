using HMS.Data;
using HMS.Models;
using HMS.Models.DTO;
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
        // GET: api/appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAppointments()
        {
            var appointments = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Select(a => new AppointmentDTO
                {
                    Id = a.Id,
                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor.Name,
                    PatientId = a.PatientId,
                    PatientName = a.Patient.Name,
                    Purpose = a.Purpose,
                    Date = a.Date
                })
                .ToListAsync();

            return Ok(appointments);
        }

        // GET: api/appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDTO>> GetAppointment(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Where(a => a.Id == id)
                .Select(a => new AppointmentDTO
                {
                    Id = a.Id,
                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor.Name,
                    PatientId = a.PatientId,
                    PatientName = a.Patient.Name,
                    Purpose = a.Purpose,
                    Date = a.Date
                })
                .FirstOrDefaultAsync();

            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment);
        }

        // POST: api/appointments
        [HttpPost]
        public async Task<ActionResult> CreateAppointment([FromBody] AppointmentDTO dto)
        {
            var appointment = new Appointment
            {
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                Purpose = dto.Purpose,
                Date = dto.Date
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Appointment created successfully!" });
        }

        // PUT: api/appointments/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAppointment(int id, [FromBody] AppointmentDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound();

            appointment.DoctorId = dto.DoctorId;
            appointment.PatientId = dto.PatientId;
            appointment.Purpose = dto.Purpose;
            appointment.Date = dto.Date;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Appointment updated successfully!" });
        }

        // DELETE: api/appointments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound();

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Appointment deleted successfully!" });
        }
    }
}
