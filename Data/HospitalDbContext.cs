using Hospitl_Mangement_MVC.Models; // السطر ده بقسم بيه المشروع الي اجزاء اصغر عشان ارف اتعامل معاها 
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hospitl_Mangement_MVC.Data
{
    public class HospitalDbContext :  IdentityDbContext<BaseEntity>
    {

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Doctor>().ToTable("Doctors");
            modelBuilder.Entity<Staff>().ToTable("Staffs");
            modelBuilder.Entity<Patient>().ToTable("Patients");
            modelBuilder.Entity<Treatment>().HasOne(p => p.Patient).WithOne(t => t.Treatment).OnDelete(DeleteBehavior.NoAction);

        }
        // baseentity
       
    }
}
