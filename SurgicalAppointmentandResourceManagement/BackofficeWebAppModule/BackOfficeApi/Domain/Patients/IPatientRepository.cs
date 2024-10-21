using System.Collections.Generic;
using PatientManagement.Domain.Entities;

namespace PatientManagement.Domain.Interfaces
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