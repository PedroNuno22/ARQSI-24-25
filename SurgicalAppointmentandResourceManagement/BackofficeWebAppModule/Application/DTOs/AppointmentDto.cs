using System;

namespace Application.DTOs
{
    public class AppointmentDto
    {
        // Unique identifier for the appointment
        public int Id { get; set; }

        // Information about the patient involved in the appointment
        public string PatientMedicalRecordNumber { get; set; } // Unique identifier for the patient
        public string PatientFullName { get; set; } // Full name for display

        // Information about the doctor involved in the appointment
        public string DoctorId { get; set; } // Unique identifier for the doctor
        public string DoctorFullName { get; set; } // Full name for display
        public string DoctorSpecialization { get; set; } // Specialization of the doctor

        // Details about the surgery room
        public string SurgeryRoomNumber { get; set; } // Room number where the appointment takes place
        public string SurgeryRoomType { get; set; } // Type of room, e.g., "Operating Room"

        // Date and time details for the appointment
        public DateTime StartTime { get; set; } // Start time of the appointment
        public DateTime EndTime { get; set; } // End time of the appointment

        // Status of the appointment
        public string Status { get; set; } // E.g., "Scheduled", "Completed", "Canceled"

        // Additional notes or details about the appointment
        public string Notes { get; set; }

        // Constructor to initialize AppointmentDto
        public AppointmentDto() { }
    }
}