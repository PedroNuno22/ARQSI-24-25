namespace Application.DTOs
{
    public class SurgeryRoomDto
    {
        public string RoomNumber { get; set; }  // Room number, e.g., "OR-101"
        public string Type { get; set; }        // Room type, e.g., "Operating Room"
        public int Capacity { get; set; }       // Capacity of the room
        public string Status { get; set; }      // Availability status, e.g., "Available" or "Under Maintenance"
    }
}