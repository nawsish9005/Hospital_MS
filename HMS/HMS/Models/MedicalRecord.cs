using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HMS.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        [JsonIgnore]
        [ValidateNever]
        public Patient Patient { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public string Current_medications { get; set; }
        public string Consultation_Notes { get; set; }
        public string FollowUp_Notes { get; set; }
        public string Laboratory_Results { get; set; }
        public string ImageUrls { get; set; }
        public string Therapies { get; set; }
        public DateTime Next_Visit_Date { get; set; }
        public string PaymentRecords { get; set; }
    }
}
