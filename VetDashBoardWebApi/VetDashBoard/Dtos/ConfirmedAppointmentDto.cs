using System.ComponentModel.DataAnnotations;
using VetDashBoard.Models;

namespace VetDashBoard.Dtos
{
	public class ConfirmedAppointmentDto
	{
        [Required]
        public int AppointmentId { get; set; }
        [Required]
		public string AppointmentType { get; set; }
		[Required]
		public string RequestedDateTimeOffset { get; set; }
        public string AppointmentTime { get; set; }
        public User User { get; set; }
		public Animal Animal { get; set; }
	}
}
