using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HMS.Models
{
    public class MedicineInfo
    {
        public int Id { get; set; }
        public string MedicineName { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }

        public int PrescriptionId { get; set; }
        [ForeignKey("PrescriptionId")]
        public Prescription Prescription { get; set; }
    }
}
