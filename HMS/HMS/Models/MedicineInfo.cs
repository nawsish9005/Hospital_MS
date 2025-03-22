using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HMS.Models
{
    public class MedicineInfo
    {
        public int Id { get; set; }
        public string Medicines { get; set; }
        public string Dosage { get; set; }
        public DateTime From_Date { get; set; }
        public DateTime ToDate { get; set; }
        public int PrescriptionId { get; set; }

        [JsonIgnore]
        [ForeignKey("PrescriptionId")]

        public Prescription? Prescription { get; set; }
    }
}
