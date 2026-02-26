using Microsoft.EntityFrameworkCore;

namespace StudentManagementSystem.Models
{
    public class AppDb : DbContext
    {
        public AppDb(DbContextOptions<AppDb> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }      // table 

        //Add
        public DbSet<HOD> HOD{ get; set; }
    }
}
