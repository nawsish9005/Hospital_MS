using HMS.Models;

namespace HMS.Data.Repository.IRepository
{
    public interface IDoctorRepository: IRepository<Doctor>
    {
        Task<Doctor> AddWithImageAsync(Doctor doctor, IFormFile? image);
        Task<Doctor?> UpdateWithImageAsync(int id, Doctor doctor, IFormFile? image);
        Task<bool> DeleteWithImageAsync(int id);
    }
}
