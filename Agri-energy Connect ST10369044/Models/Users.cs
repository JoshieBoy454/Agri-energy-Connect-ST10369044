namespace Agri_energy_Connect_ST10369044.Models
{
    public class Users : IdentityUser
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
