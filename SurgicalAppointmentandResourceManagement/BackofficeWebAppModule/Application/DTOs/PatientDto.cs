using System;
using System.Collections.Generic;

namespace Application.DTOs
{
    public class PatientDto
    {
        public string MedicalRecordNumber { get; set; } // Unique identifier for the patient

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; } // e.g., Male, Female, Other

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Allergies { get; set; } // Optional field for patient allergies

        public string MedicalConditions { get; set; } // Optional field for medical conditions

        public string EmergencyContact { get; set; }

        // FullName is a calculated property for convenience
        public string FullName => $"{FirstName} {LastName}";

        // Appointment history can be represented by a list of AppointmentDto if needed
        public List<AppointmentDto> AppointmentHistory { get; set; }

        // Constructor to initialize the list
        public PatientDto()
        {
            AppointmentHistory = new List<AppointmentDto>();
        }
    }
}