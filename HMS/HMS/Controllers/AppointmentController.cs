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
                .Select(a => new AppointmentDTO
                {
                    Id = a.Id,
                    DoctorId = a.DoctorId,
                    PatientId = a.PatientId,
                    Purpose = a.Purpose,
                    Date = a.Date
                }).ToListAsync();

            return Ok(appointments);
        }

        // GET: api/appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDTO>> GetAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
                return NotFound();

            return Ok(new AppointmentDTO
            {
                Id = appointment.Id,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                Purpose = appointment.Purpose,
                Date = appointment.Date
            });
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentDTO>> CreateAppointment(AppointmentDTO dto)
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

            dto.Id = appointment.Id;

            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, dto);
        }

        // PUT: api/Appointment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, AppointmentDTO dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound();

            appointment.DoctorId = dto.DoctorId;
            appointment.PatientId = dto.PatientId;
            appointment.Purpose = dto.Purpose;
            appointment.Date = dto.Date;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Appointment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound();

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
