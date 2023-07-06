using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VetDashBoard.Dtos;
using VetDashBoard.Models;
using VetDashBoard.Services;
using VetDashBoard.Services.IServices;

namespace VetDashboardUnitTests.Services
{
	public class VetAppointmentsServicesUnitTests
	{
		private readonly VetAppointmentsservices _vetAppointmentsservices;
		private readonly Mock<IMapper> _mapper = new();
		private readonly Mock<IHttpClientFactory> _httpClientFactoryMock = new();

		public VetAppointmentsServicesUnitTests()
		{
			_vetAppointmentsservices = new VetAppointmentsservices(_httpClientFactoryMock.Object, _mapper.Object);
		}

		[Fact]
		public void AddConfirmedAppointments_UniqueAppointment_ReturnsSuccessResponse()
		{
			// Arrange
			var dto = new ConfirmedAppointmentDto { 
				AppointmentId = 1, 
				AppointmentType = "Other", 
				RequestedDateTimeOffset = DateTime.Now.ToLocalTime().ToString(), 
				AppointmentTime= "", 
				User = new User {
					UserId = 1,
					FirstName = "User1",
					LastName = "",
					VetDataId = Guid.NewGuid().ToString(),
				},
				Animal = new Animal {
					AnimalId = 1,
					FirstName = "Animal1",
					Species = "Dog",
					Breed = "Breed"
				}
			};
			var model = new ConfirmedAppointment {
				AppointmentId = 1,
				AppointmentType = "Other",
				RequestedDateTimeOffset = DateTime.Now.ToLocalTime().ToString(),
				AppointmentTime = "",
				User = new User
				{
					UserId = 1,
					FirstName = "User1",
					LastName = "",
					VetDataId = Guid.NewGuid().ToString(),
				},
				Animal = new Animal
				{
					AnimalId = 1,
					FirstName = "Animal1",
					Species = "Dog",
					Breed = "Breed"
				}
			};
			var listConfirmedDto = new List<ConfirmedAppointmentDto>() { dto };
			var expectedResponse = new ServiceResponse
			{
				Data = new List<ConfirmedAppointmentDto>() { dto},
				Success = true,
				Message = ""
			};

			_mapper.Setup(m => m.Map<ConfirmedAppointment>(dto)).Returns(model);
			_mapper.Setup(m => m.Map<List<ConfirmedAppointmentDto>>(It.IsAny<List<ConfirmedAppointment>>())).Returns(listConfirmedDto);

			var result = _vetAppointmentsservices.AddConfirmedAppointments(dto);
			// Assert
			Assert.Equal(expectedResponse.Success, result.Success);
			Assert.Equal(expectedResponse.Message, result.Message);
			Assert.Equal(expectedResponse.Data, result.Data);
		}

		[Fact]
		public void AddConfirmedAppointments_Valid_RequestListUpdated()
		{
			// Arrange
			VetAppointmentsservices._appointmentRequests = new List<Appointment> { 
				new Appointment {
					AppointmentId = 1,
					AppointmentType = "Other",
					CreateDateTime = DateTime.UtcNow.ToString(),
					RequestedDateTimeOffset = DateTime.UtcNow.AddDays(1).ToString(),
					User_UserId = 1,
					User = new User 
					{
						UserId = 1,
						FirstName = "User1",
						LastName = "UserLastName",
						VetDataId = Guid.NewGuid().ToString(),
					},
					Animal_AnimalId = 1,
					Animal = new Animal 
					{
						AnimalId = 1,
						FirstName = "Animal1",
						Species = "Dog",
						Breed = "Breed"
					}
				} 
			};
			var dto = new ConfirmedAppointmentDto
			{
				AppointmentId = 1,
				AppointmentType = "Other",
				RequestedDateTimeOffset = VetAppointmentsservices._appointmentRequests[0].RequestedDateTimeOffset,
				AppointmentTime = "",
				User = new User
				{
					UserId = 1,
					FirstName = "User1",
					LastName = "UserLastName",
					VetDataId = VetAppointmentsservices._appointmentRequests[0].User.VetDataId,
				},
				Animal = new Animal
				{
					AnimalId = 1,
					FirstName = "Animal1",
					Species = "Dog",
					Breed = "Breed"
				}
			};
			var model = new ConfirmedAppointment
			{
				AppointmentId = 1,
				AppointmentType = "Other",
				RequestedDateTimeOffset = dto.RequestedDateTimeOffset,
				AppointmentTime = "",
				User = new User
				{
					UserId = 1,
					FirstName = "User1",
					LastName = "UserLastName",
					VetDataId = dto.User.VetDataId,
				},
				Animal = new Animal
				{
					AnimalId = 1,
					FirstName = "Animal1",
					Species = "Dog",
					Breed = "Breed"
				}
			};
			var listConfirmedDto = new List<ConfirmedAppointmentDto>() { dto };
			var expectedResponse = new ServiceResponse
			{
				Data = new List<ConfirmedAppointmentDto>() { dto },
				Success = true,
				Message = ""
			};

			_mapper.Setup(m => m.Map<ConfirmedAppointment>(dto)).Returns(model);
			_mapper.Setup(m => m.Map<List<ConfirmedAppointmentDto>>(It.IsAny<List<ConfirmedAppointment>>())).Returns(listConfirmedDto);

			var result = _vetAppointmentsservices.AddConfirmedAppointments(dto);
			// Assert
			Assert.Equal(expectedResponse.Success, result.Success);
			Assert.Equal(expectedResponse.Message, result.Message);
			Assert.Equal(expectedResponse.Data, result.Data);
			Assert.True(VetAppointmentsservices._appointmentRequests.Count == 0);
		}

		[Fact]
		public void AddConfirmedAppointments_NotUniqueAppointment_ReturnsUnSuccessResponse()
		{
			// Arrange
			var dto = new ConfirmedAppointmentDto
			{
				AppointmentId = 1,
				AppointmentType = "Other",
				RequestedDateTimeOffset = DateTime.Now.ToLocalTime().ToString(),
				AppointmentTime = "",
				User = new User
				{
					UserId = 1,
					FirstName = "User1",
					LastName = "",
					VetDataId = Guid.NewGuid().ToString(),
				},
				Animal = new Animal
				{
					AnimalId = 1,
					FirstName = "Animal1",
					Species = "Dog",
					Breed = "Breed"
				}
			};
			var model = new ConfirmedAppointment
			{
				AppointmentId = 1,
				AppointmentType = "Other",
				RequestedDateTimeOffset = DateTime.Now.ToLocalTime().ToString(),
				AppointmentTime = "",
				User = new User
				{
					UserId = 1,
					FirstName = "User1",
					LastName = "",
					VetDataId = Guid.NewGuid().ToString(),
				},
				Animal = new Animal
				{
					AnimalId = 1,
					FirstName = "Animal1",
					Species = "Dog",
					Breed = "Breed"
				}
			};
			var expectedResponse = new ServiceResponse
			{
				Data = null,
				Success = false,
				Message = "Duplication of appointment."
			};

			_mapper.Setup(m => m.Map<ConfirmedAppointment>(dto)).Returns(model);

			VetAppointmentsservices._confirmedAppointmentList.Add(model);

			var result = _vetAppointmentsservices.AddConfirmedAppointments(dto);
			// Assert
			Assert.Equal(expectedResponse.Success, result.Success);
			Assert.Equal(expectedResponse.Message, result.Message);
		}

		[Fact]
		public void AddConfirmedAppointments_InvalidAppointment_ReturnsErrorResponse()
		{
			// Arrange
			var dto = new ConfirmedAppointmentDto
			{
				AppointmentId = 2,
				AppointmentType = "Other",
				RequestedDateTimeOffset = "2023-08-01T08:00:00-08:30",
				AppointmentTime = "",
				User = new User
				{
					UserId = 1,
					FirstName = "User1",
					LastName = "",
					VetDataId = Guid.NewGuid().ToString(),
				},
				Animal = new Animal
				{
					AnimalId = 1,
					FirstName = "Animal1",
					Species = "Dog",
					Breed = "Breed"
				}
			};
			var confirmedAppointment = new ConfirmedAppointment
			{
				AppointmentId = 1,
				AppointmentType = "Other",
				RequestedDateTimeOffset = "2023-08-01T08:00:00-08:00",
				AppointmentTime = "",
				User = new User
				{
					UserId = 1,
					FirstName = "User1",
					LastName = "",
					VetDataId = Guid.NewGuid().ToString(),
				},
				Animal = new Animal
				{
					AnimalId = 1,
					FirstName = "Animal1",
					Species = "Dog",
					Breed = "Breed"
				}
			};
			var model = new ConfirmedAppointment
			{
				AppointmentId = 2,
				AppointmentType = "Other",
				RequestedDateTimeOffset = "2023-08-01T08:00:00-08:00",
				AppointmentTime = "",
				User = new User
				{
					UserId = 1,
					FirstName = "User1",
					LastName = "",
					VetDataId = Guid.NewGuid().ToString(),
				},
				Animal = new Animal
				{
					AnimalId = 1,
					FirstName = "Animal1",
					Species = "Dog",
					Breed = "Breed"
				}
			};
			var expectedResponse = new ServiceResponse
			{
				Data = null,
				Success = false,
				Message = "Times OverLap."
			};

			_mapper.Setup(m => m.Map<ConfirmedAppointment>(dto)).Returns(model);

			VetAppointmentsservices._confirmedAppointmentList.Add(confirmedAppointment);

			var result = _vetAppointmentsservices.AddConfirmedAppointments(dto);

			// Assert
			Assert.Equal(expectedResponse.Success, result.Success);
			Assert.Equal(expectedResponse.Message, result.Message);
		}

	}
}
