using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class OperationType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } // E.g., "Appendectomy", "Heart Bypass"

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Duration should be greater than zero.")]
        public int EstimatedDurationInMinutes { get; set; } // Estimated duration in minutes

        [Required]
        public ICollection<string> RequiredSpecializations { get; set; } = new List<string>(); // Specializations required for this operation

        // Constructor
        public OperationType()
        {
            RequiredSpecializations = new List<string>();
        }
    }
}