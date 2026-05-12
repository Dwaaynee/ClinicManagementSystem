using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }

        public Patient? Patient { get; set; }

        [Required]
        public string Diagnosis { get; set; } = string.Empty;

        [Required]
        public string Treatment { get; set; } = string.Empty;

        [Required]
        public DateTime RecordDate { get; set; }
    }
}