using StudentRecordMS.Repositories;
using StudentRecordMS.Services;

namespace StudentRecordMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllersWithViews();

            // Register Services and Repositories
            builder.Services.AddScoped<IStudentRepository, StudentRepositoryImpl>();
            builder.Services.AddScoped<IStudentServices, StudentServicesImpl>();
            builder.Services.AddScoped<IUserRepository, UserRepositoryImpl>();
            builder.Services.AddScoped<IUserServices, UserServicesImpl>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Logins}/{action=Login}/{id?}");

            app.Run();
        }
    }
}