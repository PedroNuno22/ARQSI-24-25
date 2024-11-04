using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Staff
    {
        [Key]
        public int Id { get; set; } // Unique identifier for each staff member

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; } // e.g., Nurse, Technician, Anesthetist

        [Required]
        [StringLength(50)]
        public string Specialization { get; set; } // e.g., Surgery Assistant, Orthopedics Specialist

        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(15)]
        [Phone]
        public string Phone { get; set; }

        // Availability slots, representing specific time periods the staff member is available
        public ICollection<AvailabilitySlot> AvailabilitySlots { get; set; } = new List<AvailabilitySlot>();

        // Navigation properties
        public ICollection<Appointment> AssignedAppointments { get; set; } = new List<Appointment>();

        // Calculated property for Full Name
        public string FullName => $"{FirstName} {LastName}";

        // Constructor
        public Staff()
        {
            AvailabilitySlots = new List<AvailabilitySlot>();
            AssignedAppointments = new List<Appointment>();
        }
    }

    // Class representing an availability slot for the staff member
    public class AvailabilitySlot
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}