using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }
    }
}