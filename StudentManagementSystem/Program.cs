using Microsoft.EntityFrameworkCore;

namespace StudentManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Configure EF Core DbContext using ConnectionStrings:DefaultConnection
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<StudentManagementSystem.Models.AppDb>(options =>
                options.UseSqlServer(connectionString));

            var app = builder.Build();

            // Ensure database is migrated and Admins table exists (create if missing)
            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    var db = scope.ServiceProvider.GetRequiredService<StudentManagementSystem.Models.AppDb>();
                    // apply any pending migrations
                    try { db.Database.Migrate(); } catch { /* ignore migration errors here */ }

                    // Check for Admins table and create if missing (simple fallback)
                    var conn = db.Database.GetDbConnection();
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Admins'";
                        var result = cmd.ExecuteScalar();
                        var count = 0;
                        if (result != null && int.TryParse(result.ToString(), out var rc)) count = rc;

                        if (count == 0)
                        {
                            cmd.CommandText = @"CREATE TABLE [Admins](
                                                    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                                                    [FullName] NVARCHAR(MAX) NOT NULL,
                                                    [Email] NVARCHAR(MAX) NOT NULL,
                                                    [Phone] NVARCHAR(MAX) NULL,
                                                    [Password] NVARCHAR(MAX) NOT NULL
                                                );";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    conn.Close();
                }
                catch
                {
                    // ignore any errors here; SQL exceptions should be inspected when running the app
                }
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admin}/{action=AdminLogin}/{id?}");

            app.Run();
        }
    }
}
