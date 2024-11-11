using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Doctor : Staff
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string LicenseNumber { get; set; } // Unique identifier for each doctor

        [Required]
        [StringLength(50)]
        public string Specialization { get; set; } // e.g., Cardiologist, Surgeon

        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(15)]
        [Phone]
        public string Phone { get; set; }

        // Available slots, such as specific times the doctor is available for appointments
        public ICollection<TimeSlot> AvailableSlots { get; set; } = new List<TimeSlot>();

        // Navigation properties
        public ICollection<OperationRequest> OperationRequests { get; set; } = new List<OperationRequest>();

        // Calculated property for Full Name
        public string FullName => $"{FirstName} {LastName}";

        // Constructor
        public Doctor()
        {
            AvailableSlots = new List<TimeSlot>();
            OperationRequests = new List<OperationRequest>();
        }
    }
}