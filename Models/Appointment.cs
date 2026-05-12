using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }

        public Patient? Patient { get; set; }

        [Required]
        public int DoctorId { get; set; }

        public Doctor? Doctor { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public string AppointmentTime { get; set; } = string.Empty;

        public string? SpecialNote { get; set; }
    }
}