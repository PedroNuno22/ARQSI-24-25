namespace Application.DTOs
{
    public class StaffDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }  // E.g., "Doctor", "Nurse", "Technician"
        public string Specialization { get; set; }  // E.g., "Cardiac Nurse", "Anesthesia Technician"
        public bool IsAvailable { get; set; }  // Indicates if the staff member is available for the requested surgery time

        public StaffDto()
        {
        }
    }
}