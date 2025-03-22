using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HMS.Models
{
    public class Appointment
    {
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

        public string Purpose { get; set; }
        public DateTime Date { get; set; }
    }
}
