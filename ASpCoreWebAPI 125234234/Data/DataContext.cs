using AspWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AspWebAPI.Data
{
    public class DataContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DataContext()
        {

        }
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer("Data Source=SERVERNAME;" +
                "Initial Catalog=DATABASE;Persist Security Info=True;User ID=USERNAME;" +
                "Password=PASSQORD");
        }

        public Microsoft.EntityFrameworkCore.DbSet<Product> Products { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Category> Categories { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<SubCategory> SubCategories { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<User> Users { get; set; }
    }
}
