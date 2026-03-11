using Hospitl_Mangement_MVC.Data;
using Hospitl_Mangement_MVC.Interface;
using Hospitl_Mangement_MVC.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Hospitl_Mangement_MVC.Models;
using Hospitl_Mangement_MVC.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Services;

namespace Hospitl_Mangement_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Load the connection string from appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Add the DbContext with the loaded connection string
            builder.Services.AddDbContext<HospitalDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "yourdomain.com",
                        ValidAudience = "yourdomain.com",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKeyHere"))
                    };
                });


            builder.Services.AddIdentity<BaseEntity, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false).
                AddEntityFrameworkStores<HospitalDbContext>()
                .AddDefaultUI().AddDefaultTokenProviders();
            // Dependancy Injection
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepositoy<>));

            builder.Services.AddSingleton<EmailService>();


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
            app.MapRazorPages();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Services.AddRole("Admin");
            app.Services.AddRole("Manager");
            app.Services.AddRole("Doctor");
            app.Services.AddRole("Nurse");
            app.Services.AddRole("Patient");
            app.Services.AddAdminRoles();
            app.Run();
        }
    }
    public static class RoleHelper
    {
        public static void AddRole(this IServiceProvider services, string v)
        {
            // ijnect explicitly RoleManager
            var RoleManger = services.CreateScope().ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            // create variable to take result of operation
            IdentityResult result;
            // check if role not exist
            if (!RoleManger.RoleExistsAsync(v).Result)
            {
                // create role using new IdentityRole
                result = RoleManger.CreateAsync(new IdentityRole(v)).Result;
            }
        }
        public static void AddAdminRoles(this IServiceProvider serviceProvider)
        {
            // inject explicitly UserManager and RoleManager
            var userManger = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<UserManager<BaseEntity>>();
            var roleManger = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            // inital result to recive result of operation
            IdentityResult result;
            // ensure from admin befor add role to him
            var user = userManger.FindByEmailAsync("Admin@gmail.com")?.Result;
            if (user == null)
            {
                user = new BaseEntity() { First_Name = "Admin", Last_Name = "Admin", UserName = "Admin", Email = "Admin@gmail.com" };
                var resul = userManger.CreateAsync(user, "Ad@123").Result;
                if (!resul.Succeeded)
                {
                    throw new ArgumentException("admin not create d");
                }
            }

            // add every role to admin
            roleManger.Roles.ToList().ForEach(r =>
            {

                result = userManger.AddToRoleAsync(user, r.Name).Result;
            });
        }
    }
}
