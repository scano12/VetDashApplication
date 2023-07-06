using VetDashBoard.Dtos;
using VetDashBoard.Models;

namespace VetDashBoard.Services.IServices
{
	public interface IVetAppointmentsServices
	{
		Task<ServiceResponse> GetAppointmentRequests();
		ServiceResponse GetConfirmedAppointment();
		ServiceResponse AddConfirmedAppointments(ConfirmedAppointmentDto dto);
		ServiceResponse DeleteAppointmentRequests(int id);
	}
}
