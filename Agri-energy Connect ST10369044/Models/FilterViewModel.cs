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

        public List<string> CategoryLabels { get; set; } = new();
        public List<int> CategoryData { get; set; } = new();

        }

    public class RecentProduct
    {
        public string Name { get; set; } = "";
        public DateTime DateAdded { get; set; }
    }
    public class FarmerDashboardViewModel
    {
        public int TotalProducts { get; set; }
        public int TotalCategories { get; set; }
        public string LatestProductName { get; set; } = "";
        public DateTime LatestProductDate { get; set; }
        public List<RecentProduct> RecentProducts { get; set; } = new();
    }
}
