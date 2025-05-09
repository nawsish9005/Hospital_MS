using HMS.Data.Repository.IRepository;
using HMS.Models;

namespace HMS.Data.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IDoctorRepository Doctor { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            
            Doctor = new DoctorRepository(_db);
        }


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
