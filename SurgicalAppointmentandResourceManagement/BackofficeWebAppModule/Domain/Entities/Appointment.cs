using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public MedicalRecordNumber MedicalRecordNumber { get; set; } // Foreign key to Patient entity

        [Required]
        public int DoctorId { get; set; } // Foreign key to Doctor entity

        [Required]
        public int RoomId { get; set; } // Foreign key to Room entity

        [Required]
        public DateTime StartTime { get; set; } // Start time of the appointment

        [Required]
        public DateTime EndTime { get; set; } // End time of the appointment

        [Required]
        public AppointmentStatus Status { get; set; } // Enum for Scheduled, Completed, or Canceled

        // Navigation properties
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public SurgeryRoom SurgeryRoom { get; set; }

        // Collection of nurses and technicians for this appointment
        public ICollection<Staff> AssignedStaff { get; set; } = new List<Staff>();

        // Constructor
        public Appointment()
        {
            AssignedStaff = new List<Staff>();
        }
    }

    // Enum for Appointment Status
    public enum AppointmentStatus
    {
        Scheduled,
        Completed,
        Canceled
    }
}