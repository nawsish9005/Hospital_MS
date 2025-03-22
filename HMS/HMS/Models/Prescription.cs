using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HMS.Models
{
    public class Prescription
    {
        [Key]
        public int Id { get; set; }
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        [JsonIgnore]
        [ValidateNever]
        public Doctor Doctor { get; set; }

        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        [JsonIgnore]
        [ValidateNever]
        public Patient Patient { get; set; }
        public ICollection<MedicineInfo> MedicineInfos { get; set; } = new List<MedicineInfo>();
        public string Duration { get; set; }
        public string Notes { get; set; }
    }
}
