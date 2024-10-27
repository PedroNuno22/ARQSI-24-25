using System.Collections.Generic;
using PatientManagement.Domain.Entities;

namespace PatientManagement.Domain.Interfaces //This interface defines methods for CRUD operations on Patient entities.
{
    public interface IPatientRepository
    {
        List<Patient> GetAll();
        Patient GetById(int id);
        Patient Create(Patient patient);
        void Update(Patient patient);
        void Delete(int id);
    }
}

// It allows the service layer to work independently of any specific data access method.