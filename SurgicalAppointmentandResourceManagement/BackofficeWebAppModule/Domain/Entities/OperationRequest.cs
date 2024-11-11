using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class OperationRequest
    {
        [Key]
        public Guid Id { get; set; }

        // Foreign key to the Patient entity
        [ForeignKey("Patient")]
        [Required]
        public Guid PatientId { get; set; }  // Use Guid instead of MedicalRecordNumber
        public Patient Patient { get; set; }  // Navigation property

        // Foreign key to the Doctor (Staff) entity
        [ForeignKey("Doctor")]
        [Required]
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }  // Navigation property

        // Foreign key to the OperationType entity
        [ForeignKey("OperationType")]
        [Required]
        public Guid OperationTypeId { get; set; }
        public OperationType OperationType { get; set; }  // Navigation property

        // Suggested deadline for the operation
        [Required]
        public DateTime Deadline { get; set; }

        // Priority of the operation
        [Required]
        [Range(1, 5, ErrorMessage = "Priority must be between 1 and 5")]
        public int Priority { get; set; }

        // Status of the request (e.g., requested, scheduled, completed, canceled)
        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "requested";  // Default value

        // Date the request was created
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;  // Default value

        // Date the request was last updated
        public DateTime UpdatedAt { get; set; } = DateTime.Now;  // Default value

        // Optional: Notes or additional details
        [MaxLength(500)]
        public string Notes { get; set; }
    }
}