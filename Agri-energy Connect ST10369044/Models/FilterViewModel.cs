using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agri_energy_Connect_ST10369044.Models
{
    public class FilterViewModel
    {
        [Display(Name = "Farmers")]
        public List<long> FarmerIds { get; set; } = new();

        [Display(Name = "Categoris")]
        public string? Category { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public IEnumerable<SelectListItem> Farmers { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<Products> Products { get; set; } = Enumerable.Empty<Products>();
    }

    public class EmployeeDashboardViewModel
    {
        public int TotalProducts { get; set; }
        public int TotalFarmers { get; set; }
        public int TotalCategories { get; set; }
        public int TotalUsers { get; set; }
    }
}
