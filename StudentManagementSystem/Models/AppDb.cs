using Microsoft.EntityFrameworkCore;

namespace StudentManagementSystem.Models
{
    public class AppDb : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-21FU26I;initial catalog=db_students;Integrated security=true;trustservercertificate=true");
        }

        public DbSet<User> Users { get; set; }      // table 

        //Add
    }
}
