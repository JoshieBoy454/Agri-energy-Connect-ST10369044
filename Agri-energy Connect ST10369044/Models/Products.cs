namespace Agri_energy_Connect_ST10369044.Models
{
    public class Products
    {
        public long ProductID { get; set; }
        public required string pName { get; set; }
        public required string pDescription { get; set; }
        public required string pCategory { get; set; }
        public required string pProductionDate { get; set; }
        public  int pPictureData { get; set; }
        public  string? pPictureFileName { get; set; }
        public  string? pPictureMimeType { get; set; }
        //--------------------------------------------------->
        // Foreign Key for Users table
        //-------------------------------------------------->
        public long UserID { get; set; }
    }
}
