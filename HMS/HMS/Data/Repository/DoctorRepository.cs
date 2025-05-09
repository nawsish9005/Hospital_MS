using HMS.Data.Repository.IRepository;
using HMS.Models;

namespace HMS.Data.Repository
{
    public class DoctorRepository: Repository<Doctor>, IDoctorRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public DoctorRepository(ApplicationDbContext context, IWebHostEnvironment env) : base(context)
        {
            _context = context;
            _env = env;
        }

        public DoctorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Doctor> AddWithImageAsync(Doctor doctor, IFormFile? image)
        {
            if (image != null)
            {
                doctor.ImageUrl = await SaveImageAsync(image);
            }

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }

        public async Task<Doctor?> UpdateWithImageAsync(int id, Doctor doctorDto, IFormFile? image)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null) return null;

            doctor.Name = doctorDto.Name;
            doctor.Contact = doctorDto.Contact;
            doctor.Email = doctorDto.Email;
            doctor.Address = doctorDto.Address;
            doctor.Region = doctorDto.Region;
            doctor.Country = doctorDto.Country;
            doctor.PostalCode = doctorDto.PostalCode;
            doctor.Specialization = doctorDto.Specialization;

            if (image != null)
            {
                doctor.ImageUrl = await SaveImageAsync(image);
            }

            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }

        public async Task<bool> DeleteWithImageAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null) return false;

            if (!string.IsNullOrEmpty(doctor.ImageUrl))
            {
                var filePath = Path.Combine(_env.WebRootPath, doctor.ImageUrl.TrimStart('/'));
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<string> SaveImageAsync(IFormFile image)
        {
            var uploadDir = Path.Combine(_env.WebRootPath, "images", "doctor");
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            var filePath = Path.Combine(uploadDir, image.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return "/images/doctor/" + image.FileName;
        }
    }
}

