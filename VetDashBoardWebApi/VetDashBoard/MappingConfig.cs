using AutoMapper;
using VetDashBoard.Dtos;
using VetDashBoard.Models;

namespace VetDashBoard
{
	public class MappingConfig : Profile
	{
		public MappingConfig() 
		{
			CreateMap<Appointment, AppointmentDto>().ReverseMap();
			CreateMap<ConfirmedAppointment, ConfirmedAppointmentDto>().ReverseMap();
		}
	}
}
