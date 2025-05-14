using System.ComponentModel.DataAnnotations;

namespace Agri_energy_Connect_ST10369044.Models
{
    public class LoginViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password), MinLength(5)]
        public string Password { get; set; }
    }
}
