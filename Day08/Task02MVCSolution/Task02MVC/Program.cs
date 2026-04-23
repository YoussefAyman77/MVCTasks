using Microsoft.EntityFrameworkCore;
using Task02MVC.Data.Contexts;
using Task02MVC.Reposatories;

namespace Task02MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<UniDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Sab3"));
            });
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IStudentReposatory,StudentReposatory>();
            builder.Services.AddScoped<IDepartmentReposatory,DepartmentReposatory>();
            builder.Services.AddScoped<ICourseReposatory,CourseReposatory>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
