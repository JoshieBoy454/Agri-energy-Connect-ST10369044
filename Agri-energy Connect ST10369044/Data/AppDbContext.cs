using Agri_energy_Connect_ST10369044.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Agri_energy_Connect_ST10369044.Data
{
    public class AppDbContext : DbContext
    {
        //--------------------------------------------------->
        // Adds a constructor to the AppDbContext class
        //-------------------------------------------------->
        public AppDbContext(DbContextOptions<AppDbContext> opts)
            : base(opts) { }

        //--------------------------------------------------->
        //Exposes the tables in the database 
        //-------------------------------------------------->
        public DbSet<Users> Users { get; set; }
        public DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //--------------------------------------------------->
            // Sets the users to the users table
            //-------------------------------------------------->
            builder.Entity<Users>(b =>
            {
                b.ToTable("User");
                b.HasKey(u => u.UserID);
                b.Property(u => u.UserID).HasColumnName("UserID");
                b.Property(u => u.Password).HasColumnName("password");
                b.Property(u => u.Email).HasColumnName("email");
                b.Property(u => u.Name).HasColumnName("name");
                b.Property(u => u.Surname).HasColumnName("surname");
            });

            //--------------------------------------------------->
            //Sets products to the products table
            //-------------------------------------------------->
            builder.Entity<Products>(b =>
            {
                b.ToTable("Products");
                b.HasKey(p => p.ProductID);
                b.Property(p => p.ProductID).HasColumnName("ProductID");
                b.Property(p => p.pName).HasColumnName("pName");
                b.Property(p => p.pCategory).HasColumnName("pCategory");
                b.Property(p => p.pProductionDate).HasColumnName("pProductionDate");
                b.Property(p => p.UserID).HasColumnName("UserID");
                b.Property(p => p.pPictureData).HasColumnName("pPictureData");
                b.Property(p => p.pPictureFileName).HasColumnName("pPictureFileName");
                b.Property(p => p.pPictureMimeType).HasColumnName("pPictureMimeType");

            });
        }
    }
}
