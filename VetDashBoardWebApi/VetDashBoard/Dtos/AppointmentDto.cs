using VetDashBoard.Models;

namespace VetDashBoard.Dtos
{
	public class AppointmentDto
	{
		public int AppointmentId { get; set; }
		public string AppointmentType { get; set; }
		public string RequestedDateTimeOffset { get; set; }
		public User User { get; set; }
		public Animal Animal { get; set; }
	}
}
