using Microsoft.EntityFrameworkCore;
using PatientManagement.Domain.Entities;

namespace BackOfficeApi.Infrastructure
{
    public class PatientDbContext : DbContext // PatientDbContext inherits from DbContext, which handles database connections, queries, and transactions.
    {
        public PatientDbContext(DbContextOptions<PatientDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; } // The DbSet<Patient> property represents a database table (or in this case, the in-memory collection) that holds Patient entities
    }
}
