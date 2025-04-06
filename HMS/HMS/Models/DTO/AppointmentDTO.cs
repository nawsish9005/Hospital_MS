namespace HMS.Models.DTO
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string Purpose { get; set; }
        public DateTime Date { get; set; }
    }
}
