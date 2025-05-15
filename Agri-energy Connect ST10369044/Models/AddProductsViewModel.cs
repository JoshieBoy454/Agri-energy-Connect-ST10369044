using System.ComponentModel.DataAnnotations;

namespace Agri_energy_Connect_ST10369044.Models
{
    public class AddProductsViewModel
    {
        [Required]
        [Display(Name = "Product Name")]
        public string pName { get; set; }
        [Required]
        [Display(Name = "Product Category")]
        public string pCategory { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Product Description")]
        public DateTime pProductionDate { get; set; }
        [Display(Name = "Product Description")]
        public IFormFile? pPicture { get; set; }
    }
}
