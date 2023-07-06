using AutoMapper;
using Newtonsoft.Json;
using VetDashBoard.Dtos;
using VetDashBoard.Models;
using VetDashBoard.Services.IServices;

namespace VetDashBoard.Services
{
	public class VetAppointmentsservices : IVetAppointmentsServices
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IMapper _mapper;

		public static List<ConfirmedAppointment> _confirmedAppointmentList = new();
		public static List<Appointment> _appointmentRequests = new();
		private static int _count = 0;

		public VetAppointmentsservices(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
			_httpClientFactory = httpClientFactory;
			_mapper = mapper;
		}
        public ServiceResponse AddConfirmedAppointments(ConfirmedAppointmentDto dto)
		{
			try
			{
				var apiResponse = new ServiceResponse();

				var model = _mapper.Map<ConfirmedAppointment>(dto);

				var isUnique = IsAppointmentUnique(model);

				if (!isUnique)
				{
					apiResponse.Data = null;
					apiResponse.Success = false;
					apiResponse.Message = "Duplication of appointment.";
					return apiResponse;
				}

				bool isValid = model.AppointmentType.ToLower().Contains("overnight") ? true : ValidateAppointmentData(model);

				if (!isValid)
				{
					apiResponse.Data = null;
					apiResponse.Success = false;
					apiResponse.Message = "Times OverLap.";
					return apiResponse;
				}

				MassageData(model);

				apiResponse.Data = _mapper.Map<List<ConfirmedAppointmentDto>>(_confirmedAppointmentList);
				return apiResponse;
			}
			catch (Exception)
			{
				throw new Exception("Something went wrong while adding to list of confirmed appointments.");
			}

		}

		public async Task<ServiceResponse> GetAppointmentRequests()
		{
			try
			{
				var serviceResponse = new ServiceResponse();

				if (_count == 0)				
				{
					_count++;
					var client = _httpClientFactory.CreateClient("VetAppointmentInfo");

					var mess = CreateMessage();

					using (var response = await client.SendAsync(mess))
					using (var content = response.Content)
					{
						var result = await content.ReadAsStringAsync();
						_appointmentRequests = JsonConvert.DeserializeObject<List<Appointment>>(result);

						foreach (var item in _appointmentRequests)
						{
							var dateFormat = DateTime.Parse(item.RequestedDateTimeOffset);

							item.RequestedDateTimeOffset = dateFormat.ToString();
						}

						serviceResponse.Data = _mapper.Map<List<AppointmentDto>>(_appointmentRequests);

						return serviceResponse;
					}
					
				}

				serviceResponse.Data = _appointmentRequests;
				return serviceResponse;

			}
			catch (Exception)
			{
				throw new Exception("Something went wrong while retrieving list of appointment requests");
			}
		}

		public ServiceResponse GetConfirmedAppointment()
		{
			return new ServiceResponse() { Data = _confirmedAppointmentList };
		}

		public ServiceResponse DeleteAppointmentRequests(int id)
		{
			try
			{
				//Using this method to remove the appointment off of the appoint request list on reschedule click from front end
				var request = _appointmentRequests.FirstOrDefault(x => x.AppointmentId == id);

				_appointmentRequests.Remove(request);

				return new ServiceResponse() { Data = _appointmentRequests };
			}
			catch (Exception)
			{

				throw new Exception("Something went wrong while trying to remove item from request list.");
			}
		}

		private HttpRequestMessage CreateMessage()
		{
			//private method to construc the http request message that retrieves the list of appointment requests
			HttpRequestMessage message = new HttpRequestMessage();
			message.Method = HttpMethod.Get;
			message.Headers.Add("Accept", "application/json");
			message.RequestUri = new Uri("https://723fac0a-1bff-4a20-bdaa-c625eae11567.mock.pstmn.io/appointments");

			return message;
		}

		private bool ValidateAppointmentData(ConfirmedAppointment model)
		{
			//Logic to make sure two accepted appointments do not overlap
			var AppointmentStart = DateTime.Parse(model.RequestedDateTimeOffset);

			var AppointmentEnd = DateTime.Parse(model.RequestedDateTimeOffset).AddHours(1);

			foreach (var appointment in _confirmedAppointmentList)
			{
				bool overLap = AppointmentStart < DateTime.Parse(appointment.RequestedDateTimeOffset).AddHours(1) && DateTime.Parse(appointment.RequestedDateTimeOffset) < AppointmentEnd;

				if (overLap)
				{
					return false;
				}

			}

			return true;

		}

		private bool IsAppointmentUnique(ConfirmedAppointment model)
		{
			//No two appointments should be the requested twice
			foreach (var appointment in _confirmedAppointmentList)
			{
				if (appointment.AppointmentId == model.AppointmentId)
				{
					return false;
				}
			}

			return true;
		}

		private void MassageData(ConfirmedAppointment model)
		{
			//In order to display the correct time of the confirmed appointment on the front end
			var AppointmentStart = DateTime.Parse(model.RequestedDateTimeOffset);

			//Surgery tends to take longer so I am accounting for it here
			var AppointmentEnd = model.AppointmentType.ToLower().Contains("surgery") ? DateTime.Parse(model.RequestedDateTimeOffset).AddHours(2.5) : DateTime.Parse(model.RequestedDateTimeOffset).AddMinutes(30);

			var timeOfAppointment = $"{AppointmentStart.ToShortDateString()} {AppointmentStart.ToShortTimeString()} - {AppointmentEnd.ToShortTimeString()}";


			model.AppointmentTime = timeOfAppointment;

			//Logic to remove the accepted appointment out of the request appointment list
			//Making sure that this only runs when there is requests in the list
			if (_appointmentRequests.Count > 0)
			{
				var appointmentToRemove = _appointmentRequests.FirstOrDefault(x => x.AppointmentId == model.AppointmentId);

				_appointmentRequests.Remove(appointmentToRemove);
			}

			_confirmedAppointmentList.Add(model);
		}

		
	}
}
