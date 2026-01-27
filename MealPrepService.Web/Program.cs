using MealPrepService.BLL.Services;
using MealPrepService.DAL.Entities;
using MealPrepService.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
namespace MealPrepService.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();





            // ========== DATABASE CONFIGURATION ==========
            builder.Services.AddDbContext<MealPrepDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("MealPrepConnection"),
                    b => b.MigrationsAssembly("MealPrepService.DAL")));


            // ========== REPOSITORY REGISTRATION ==========
            builder.Services.AddScoped<IMealRepository, MealRepository>();
            // Note: Thêm các repository khác khi c?n
            // builder.Services.AddScoped<IUserRepository, UserRepository>();
            // builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            // ========== SERVICE REGISTRATION ==========
            // Existing services
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<NutritionService>();

            // AI Menu Service (THÊM M?I)
            builder.Services.AddScoped<IAIMenuService, AIMenuService>();


            var app = builder.Build();

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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
