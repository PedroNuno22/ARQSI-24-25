using System.Collections.Generic; // Imports collections like List
using System.Linq; // First or default querying collections
using BackOfficeApi.Infrastructure;
using PatientManagement.Domain.Entities; //Imports Patient entity
using PatientManagement.Domain.Interfaces; //Imports IPatientRepository interface

// This class implements an in-memory repository for patient entity 
namespace PatientManagement.Infrastructure.Repositories
{
    public class EfPatientRepository : IPatientRepository // This class implements IPatientRepository with EF Core methods, interacting with the PatientDbContext.
    {
        private readonly PatientDbContext _context;

        public EfPatientRepository(PatientDbContext context)
        {
            _context = context;
        }

        public List<Patient> GetAll() => _context.Patients.ToList();

        public Patient GetById(int id) => _context.Patients.Find(id);

        public Patient Create(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
            return patient;
        }

        public void Update(Patient patient)
        {
            var existingPatient = _context.Patients.Find(patient.Id);
            if (existingPatient != null)
            {
                existingPatient.FirstName = patient.FirstName;
                existingPatient.LastName = patient.LastName;
                existingPatient.MedicalHistory = patient.MedicalHistory;
                existingPatient.Email = patient.Email;
                existingPatient.Phone = patient.Phone;

                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                _context.SaveChanges();
            }
        }
    }
}
