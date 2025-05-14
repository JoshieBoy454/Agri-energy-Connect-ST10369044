using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Agri_energy_Connect_ST10369044.Models
{
    public class Users : IdentityUser<long>
    {
        public int UserID { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
    }
}
