using Domain.ValueObjects;

namespace Application.DTOs
{
    public class OperationRequestDto
    {
        public Guid Id { get; set; }

        public Guid PatientId { get; set; }
        public MedicalRecordNumber PatientMedicalRecordNumber { get; set; }
        public Guid DoctorId { get; set; }
        public Guid OperationTypeId { get; set; }
        public DateTime RequestedDate { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public int Priority { get; set; }
        public string Status { get; set; }
        public List<StaffDto> RequiredStaff { get; set; }
        public SurgeryRoomDto SurgeryRoom { get; set; }

        public OperationRequestDto()
        {
            RequiredStaff = new List<StaffDto>();
        }
    }
}
