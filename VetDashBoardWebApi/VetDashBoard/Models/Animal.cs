using System.ComponentModel.DataAnnotations;

namespace VetDashBoard.Models
{
	public class Animal
	{
        [Required]
        public int AnimalId { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
    }
}
