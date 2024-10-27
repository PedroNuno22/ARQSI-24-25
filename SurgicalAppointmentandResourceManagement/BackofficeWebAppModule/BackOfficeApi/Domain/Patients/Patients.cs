namespace PatientManagement.Domain.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MedicalHistory { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}