namespace HMS.Models.DTO
{
    public class PrescriptionDTO
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public List<MedicineInfoDTO> MedicineInfos { get; set; } = new();
        public string Duration { get; set; }
        public string Notes { get; set; }
    }
}
