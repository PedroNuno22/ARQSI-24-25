using System.Collections.Generic; // Imports collections like List
using System.Linq; // First or default querying collections
using PatientManagement.Domain.Entities; //Imports Patient entity
using PatientManagement.Domain.Interfaces; //Imports IPatientRepository interface

// This class implements an in-memory repository for patient entity 
namespace PatientManagement.Infrastructure.Repositories
{
    public class InMemoryPatientRepository : IPatientRepository
    {

        private static List<Patient> _patients = new List<Patient>(); // Static list to store patient in memory
        private static int _nextId = 1;

        //Method to return the list of all patients
        public List<Patient> GetAll() => _patients;

        // Method to get a patient by ID, or null if not found
        public Patient GetById(int id) => _patients.FirstOrDefault(p => p.Id == id);

        public Patient Create(Patient patient)
        {
            patient.Id = _nextId++;
            _patients.Add(patient);
            return patient;
        }

        public void Update(Patient patient)
        {
            var existingPatient = GetById(patient.Id);
            if (existingPatient != null)
            {
                existingPatient.FirstName = patient.FirstName;
                existingPatient.LastName = patient.LastName;
                existingPatient.MedicalHistory = patient.MedicalHistory;
                existingPatient.Email = patient.Email;
                existingPatient.Phone = patient.Phone;
            }
        }

        public void Delete(int id)
        {
            var patient = GetById(id);
            if (patient != null)
            {
                _patients.Remove(patient);
            }
        }
    }
}
