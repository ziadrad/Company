using assignement_3.BLL.Interfaces;
using assignement_3.BLL.Reprositories;
using assignement_3.DAL.Data.contexts;
using assignement_3.DAL.Models;
using assignement_3.PL.Mapping;
using Microsoft.EntityFrameworkCore;

namespace assignement_3.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IDepartmentReprositories, DepartmentReprositories>();
            builder.Services.AddScoped<IEmployeeRespositry, EmployeeResporitory>();
            builder.Services.AddScoped<IUnit_of_Work, UnitOfWork>();

            builder.Services.AddDbContext<CompanyDbContext>(options =>
            
            options.UseSqlServer(builder.Configuration.GetConnectionString("Defualt")

            )
            );
            
            builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            builder.Services.AddAutoMapper(typeof(DepartmentProfile));

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
