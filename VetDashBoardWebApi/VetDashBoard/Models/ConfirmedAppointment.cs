using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace VetDashBoard.Models
{
	public class ConfirmedAppointment
	{
		public int AppointmentId { get; set; }
		public string AppointmentType { get; set; }
		public string CreateDateTime { get; set; } = DateTime.Now.ToString();
		public string RequestedDateTimeOffset { get; set; }
		public string AppointmentTime{ get; set; }
        public User User { get; set; }
		public Animal Animal { get; set; }
	}
}
