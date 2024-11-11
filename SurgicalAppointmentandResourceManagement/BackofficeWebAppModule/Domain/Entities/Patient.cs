using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Patient
    {
        [Key]
        public Guid Id { get; set; } 

        public MedicalRecordNumber MedicalRecordNumber { get; private set; } // Unique identifier for each patient

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; } // Consider using an enum for Gender if specific options are defined

        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(15)]
        [Phone]
        public string Phone { get; set; }

        public string Allergies { get; set; } // Optional field for patient allergies

        public string MedicalConditions { get; set; } // Optional field for medical conditions

        [Required]
        [StringLength(100)]
        public string EmergencyContact { get; set; }

        // Navigation properties
        public ICollection<Appointment> AppointmentHistory { get; set; }

        // Navigation property to represent the one-to-many relationship
        public ICollection<OperationRequest> OperationRequests { get; set; } = new List<OperationRequest>();

        // Calculated property for Full Name
        public string FullName => $"{FirstName} {LastName}";

        // Constructor for EF Core and initialization
        public Patient()
        {
            AppointmentHistory = new List<Appointment>();
        }

        // Additional constructor to initialize MedicalRecordNumber
        public Patient(string firstName, string lastName, DateTime dateOfBirth, string gender, MedicalRecordNumber medicalRecordNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            MedicalRecordNumber = medicalRecordNumber ?? throw new ArgumentNullException(nameof(medicalRecordNumber));
            AppointmentHistory = new List<Appointment>();
        }

        // Method to set the MedicalRecordNumber if required during registration
        public void SetMedicalRecordNumber(MedicalRecordNumber medicalRecordNumber)
        {
            MedicalRecordNumber = medicalRecordNumber ?? throw new ArgumentNullException(nameof(medicalRecordNumber));
        }
    }
}