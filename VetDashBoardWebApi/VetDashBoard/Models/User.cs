using System.ComponentModel.DataAnnotations;

namespace VetDashBoard.Models
{
	public class User
	{
        [Required]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string VetDataId { get; set; }
    }
}
