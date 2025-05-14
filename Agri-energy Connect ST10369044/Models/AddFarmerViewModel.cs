using System.ComponentModel.DataAnnotations;

namespace Agri_energy_Connect_ST10369044.Models
{
    public class AddFarmerViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required, DataType(DataType.Password), MinLength(6)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare("Password")]
        public string Confirm { get; set; }
    }
}
