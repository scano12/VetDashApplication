namespace VetDashBoard.Models
{
	public class ServiceResponse
	{
        public object Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;

    }
}
