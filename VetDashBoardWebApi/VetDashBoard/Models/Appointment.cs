namespace VetDashBoard.Models
{
	public class Appointment
	{
        public int AppointmentId { get; set; }
        public string AppointmentType { get; set; }
        public string CreateDateTime { get; set; }
        public string RequestedDateTimeOffset { get; set; }
        public int User_UserId { get; set; }
        public User User { get; set; }
        public int Animal_AnimalId { get; set; }
        public Animal Animal { get; set; }

    }
}
