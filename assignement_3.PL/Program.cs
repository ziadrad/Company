using System.Security.Claims;
using assignement_3.BLL.Interfaces;
using assignement_3.BLL.Reprositories;
using assignement_3.DAL.Data.contexts;
using assignement_3.DAL.Models;
using assignement_3.PL.Helpers;
using assignement_3.PL.Interface;
using assignement_3.PL.Mapping;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Facebook;

namespace assignement_3.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IDepartmentReprositories, DepartmentReprositories>();
            builder.Services.AddScoped<IEmployeeRespositry, EmployeeResporitory>();
            builder.Services.AddScoped<IUnit_of_Work, UnitOfWork>();
            builder.Services.AddScoped<IMailServices,MailServices>();
            builder.Services.AddScoped<ITwilioServices, TwilioService>();

            builder.Services.AddDbContext<CompanyDbContext>(options =>
            
            options.UseSqlServer(builder.Configuration.GetConnectionString("Defualt"), sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 10,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorNumbersToAdd: null);
            }
            )
            );
            
            builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            builder.Services.AddAutoMapper(typeof(DepartmentProfile));
            builder.Services.AddIdentity < AppUser, IdentityRole>()
                .AddEntityFrameworkStores<CompanyDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthorization(options => {
                options.AddPolicy("AddPolicy",
                policy => { policy.RequireClaim("CreatePermission", "True"); }



                );
            options.AddPolicy("editPolicy",
           policy => policy.RequireClaim("EditPermission", "True")

           );

                options.AddPolicy("deletePolicy",
                                policy => policy.RequireClaim("DeletePermission", "True")

                                );
        });

       
            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.AccessDeniedPath = "/Account/AccessDenied";
                config.LoginPath = "/Account/SignIn";
           

            });
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection(nameof(EmailSettings)));

            builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection(nameof(TwilioSettings)));
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
              //  options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
                .AddCookie()
                .AddFacebook(FacebookDefaults.AuthenticationScheme, options =>
                {
                    options.ClientId = builder.Configuration["Authentication:Facebook:ClientId"]!;
                    options.ClientSecret = builder.Configuration["Authentication:Facebook:ClientSecret"]!;
                    // options.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
                })
              .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
              {
                  options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
                  options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
                 // options.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
              });
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
          

            app.UseAuthentication();

            app.UseAuthorization();
            app.UseSession();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
