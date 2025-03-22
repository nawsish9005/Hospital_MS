using System.ComponentModel.DataAnnotations;

namespace HMS.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string BloodGroup { get; set; }
        public string Address { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string ImageUrls { get; set; }
        public string Email { get; set; }
    }
}
