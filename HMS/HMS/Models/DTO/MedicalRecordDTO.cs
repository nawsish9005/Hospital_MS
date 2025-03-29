namespace HMS.Models.DTO
{
    public class MedicalRecordDTO
    {
        public int PatientId { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public string CurrentMedications { get; set; }
        public string ConsultationNotes { get; set; }
        public string FollowUpNotes { get; set; }
        public string LaboratoryResults { get; set; }
        public List<IFormFile>? Images { get; set; }
        public string Therapies { get; set; }
        public DateTime NextVisitDate { get; set; }
        public string PaymentRecords { get; set; }
    }

}
