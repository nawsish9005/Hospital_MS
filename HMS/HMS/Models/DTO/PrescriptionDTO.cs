namespace HMS.Models.DTO
{
    public class PrescriptionDTO
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public string Duration { get; set; }
        public string Notes { get; set; }
        public List<MedicineInfoDTO> MedicineInfos { get; set; } = new();
    }
}
