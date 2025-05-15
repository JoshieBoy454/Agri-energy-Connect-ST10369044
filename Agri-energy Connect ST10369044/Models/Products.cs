namespace Agri_energy_Connect_ST10369044.Models
{
    public class Products
    {
        public long ProductID { get; set; }
        public  string pName { get; set; }
        public  string pCategory { get; set; }
        public  DateTime pProductionDate { get; set; }
        public  byte[]? pPictureData { get; set; }
        public  string? pPictureFileName { get; set; }
        public  string? pPictureMimeType { get; set; }
        //--------------------------------------------------->
        // Foreign Key for Users table
        //-------------------------------------------------->
        public long UserID { get; set; }
    }
}
