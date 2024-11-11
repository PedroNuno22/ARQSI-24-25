using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.ValueObjects;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSet properties for each entity
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Staff> StaffMembers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<SurgeryRoom> SurgeryRooms { get; set; }
        public DbSet<OperationRequest> OperationRequests { get; set; }
        public DbSet<OperationType> OperationTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring TimeSlot as an owned type since it's a Value Object
            modelBuilder.Owned<TimeSlot>();


            modelBuilder.Entity<Patient>()
            .HasKey(p => p.MedicalRecordNumber);

            // Example of a unique constraint on the LicenseNumber in the Doctor entity
            modelBuilder.Entity<Doctor>()
                .HasIndex(d => d.LicenseNumber)
                .IsUnique();

            // Configuring relationships and constraints

            // Doctor to OperationRequests (one-to-many relationship)
            modelBuilder.Entity<OperationRequest>()
                .HasOne(or => or.Doctor)
                .WithMany(d => d.OperationRequests)
                .HasForeignKey(or => or.DoctorId);

            // Patient to OperationRequests (one-to-many relationship)
            modelBuilder.Entity<OperationRequest>()
                .HasOne(or => or.Patient)
                .WithMany(p => p.OperationRequests)
                .HasForeignKey(or => or.PatientId);

            // Appointment to Doctor (many-to-one relationship)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.AssignedAppointments)
                .HasForeignKey(a => a.DoctorId);

            // Appointment to Patient (many-to-one relationship)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.AppointmentHistory)
                .HasForeignKey(a => a.MedicalRecordNumber);

            // Appointment to SurgeryRoom (many-to-one relationship)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.SurgeryRoom)
                .WithMany()
                .HasForeignKey(a => a.RoomId);

            // Configuring Staff to Appointment relationship (many-to-many)
            modelBuilder.Entity<Appointment>()
                .HasMany(a => a.AssignedStaff)
                .WithMany(s => s.AssignedAppointments)
                .UsingEntity(j => j.ToTable("AppointmentStaff"));
        }
    }
}