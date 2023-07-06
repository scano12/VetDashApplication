using Microsoft.AspNetCore.Mvc;
using VetDashBoard.Dtos;
using VetDashBoard.Models;
using VetDashBoard.Services.IServices;

namespace VetDashBoard.Controllers
{
	[Route("api/vetAppointmentApi")]
	[ApiController]
	public class VetAppointmentsController : ControllerBase
	{
		private readonly IVetAppointmentsServices _vetAppointmentServices;
		private readonly ILogger<VetAppointmentsController> _logger;
		protected ServiceResponse _apiResponse;



		public VetAppointmentsController(IVetAppointmentsServices vetAppointmentsServices, ILogger<VetAppointmentsController> logger)
		{
			_vetAppointmentServices = vetAppointmentsServices;
			_logger = logger;
			_apiResponse = new ServiceResponse();
		}


		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ServiceResponse>> GetAppointmentRequests()
		{
			try
			{
				_logger.LogInformation("Starting retrieval of appointment requests");

				_apiResponse = await _vetAppointmentServices.GetAppointmentRequests();

				_logger.LogInformation("Done with the retrieval of appointment requests");

				return Ok(_apiResponse);

			}
			catch (Exception ex)
			{

				_apiResponse.Data = null;
				_apiResponse.Success = false;
				_apiResponse.Message = ex.Message.ToString();

				return _apiResponse;
			}
		}

		[HttpGet("ConfirmedAppointments")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<ServiceResponse> GetConfirmedAppointment()
		{
			try
			{
				_logger.LogInformation("Starting retrieval of upcoming appointments");

				_apiResponse = _vetAppointmentServices.GetConfirmedAppointment();

				_logger.LogInformation("Done with the retrieval of upcoming appointments");

				return Ok(_apiResponse);

			}
			catch (Exception ex)
			{

				_apiResponse.Data = null;
				_apiResponse.Success = false;
				_apiResponse.Message = ex.Message.ToString();

				return _apiResponse;
			}
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<ServiceResponse> CreateAppointments([FromBody] ConfirmedAppointmentDto dto)
		{
			try
			{
				_logger.LogInformation("Starting to add appointment to list of upcoming appontments");

				if (!ModelState.IsValid)
				{
					_logger.LogError("Model state is invalid");
					_apiResponse.Data = null;
					_apiResponse.Success = false;
					_apiResponse.Message = "Model is invalid";
					return BadRequest(_apiResponse);
				}

				_apiResponse = _vetAppointmentServices.AddConfirmedAppointments(dto);

				if (!_apiResponse.Success)
				{
					return BadRequest(_apiResponse);
				}

				_logger.LogInformation("Finsihed with adding appointment to list of upcoming appointments");

				return Ok(_apiResponse);

			}
			catch (Exception ex)
			{
				_apiResponse.Data = null;
				_apiResponse.Success = false;
				_apiResponse.Message = ex.Message.ToString();

				return _apiResponse;
			}
		}

		[HttpDelete("{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public ActionResult<ServiceResponse> DeleteAppointmentRequest(int id)
		{
			try
			{
				_logger.LogInformation("Starting to remove appointment from list of appointment request");
                
				if (id == 0)
                {
					_apiResponse.Data = null;
					_apiResponse.Success = false;
					_apiResponse.Message = "Id cannot be 0";

					return BadRequest(_apiResponse);
                }
                _apiResponse = _vetAppointmentServices.DeleteAppointmentRequests(id);


				_logger.LogInformation("Finsihed with removing appointment from list of upcoming appointments");

				return Ok(_apiResponse);

			}
			catch (Exception ex)
			{
				_apiResponse.Data = null;
				_apiResponse.Success = false;
				_apiResponse.Message = ex.Message.ToString();

				return _apiResponse;
			}
		}
	}
}
