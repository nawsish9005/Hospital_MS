using HMS.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HMS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<MedicineInfo> MedicineInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Specialization).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Contact).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.Email).HasMaxLength(100).IsUnicode(false);
                entity.Property(e => e.Address).HasMaxLength(255).IsUnicode(false);
                entity.Property(e => e.Region).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Country).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.PostalCode).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.ImageUrl).HasMaxLength(255).IsUnicode(false);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Contact).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.Email).HasMaxLength(100).IsUnicode(false);
                entity.Property(e => e.Address).HasMaxLength(255).IsUnicode(false);
                entity.Property(e => e.Region).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Country).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.ImageUrls).HasMaxLength(255).IsUnicode(false);
                entity.Property(e => e.BloodGroup).HasMaxLength(10).IsUnicode(false);
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.Purpose).HasMaxLength(255).IsUnicode(false);
                entity.HasOne(a => a.Doctor)
                    .WithMany()
                    .HasForeignKey(a => a.DoctorId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(a => a.Patient)
                    .WithMany()
                    .HasForeignKey(a => a.PatientId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<MedicineInfo>()
            .HasOne(id => id.Prescription)
            .WithMany(i => i.MedicineInfos)
            .HasForeignKey(id => id.PrescriptionId)
            .OnDelete(DeleteBehavior.Cascade);


            OnModelCreatingPartial(modelBuilder);
        }

        private void OnModelCreatingPartial(ModelBuilder modelBuilder) { }


    }
}
