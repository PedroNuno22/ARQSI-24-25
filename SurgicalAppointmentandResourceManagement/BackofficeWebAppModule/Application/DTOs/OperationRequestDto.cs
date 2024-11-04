
namespace Application.DTOs
{
    public class OperationRequestDto
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public string OperationType { get; set; }
        public DateTime RequestedDate { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public List<StaffDto> RequiredStaff { get; set; }
        public SurgeryRoomDto SurgeryRoom { get; set; }

        public OperationRequestDto()
        {
            RequiredStaff = new List<StaffDto>();
        }
    }
}
