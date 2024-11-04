using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class SurgeryRoom
    {
        [Key]
        [StringLength(50)]
        public string RoomNumber { get; set; } // e.g., "OR-101"

        [Required]
        [StringLength(50)]
        public string Type { get; set; } // e.g., "Operating Room", "ICU", "Consultation Room"

        [Range(1, 20, ErrorMessage = "Capacity must be between 1 and 20.")]
        public int Capacity { get; set; } // Max capacity of the room

        [Required]
        public RoomStatus Status { get; set; } // Availability status

        public DateTime? NextMaintenanceDate { get; set; } // Scheduled maintenance date, if any

        // Constructor
        public SurgeryRoom()
        {
            Status = RoomStatus.Available; // Default to available on creation
        }

        // Method to mark the room as occupied
        public void MarkAsOccupied()
        {
            Status = RoomStatus.Occupied;
        }

        // Method to mark the room as available
        public void MarkAsAvailable()
        {
            Status = RoomStatus.Available;
        }

        // Method to mark the room as under maintenance
        public void ScheduleMaintenance(DateTime maintenanceDate)
        {
            Status = RoomStatus.UnderMaintenance;
            NextMaintenanceDate = maintenanceDate;
        }

        // Method to complete maintenance and mark as available
        public void CompleteMaintenance()
        {
            Status = RoomStatus.Available;
            NextMaintenanceDate = null;
        }
    }

    public enum RoomStatus
    {
        Available,
        Occupied,
        UnderMaintenance
    }
}