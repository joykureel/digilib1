using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DigiLib.Data;
using DigiLib.Models;
using DigiLib.Services;
using Microsoft.AspNetCore.Identity;


namespace DigiLib
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }
      


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
           // CreateRoles(serviceProvider);
        }
        public async void CreateRoles(IServiceProvider serviceProvider)
        {
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "Member" };
            IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {

                bool roleExist = _roleManager.RoleExistsAsync(roleName).Result;
                if (!roleExist)
                {
                    roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            var poweruser = new ApplicationUser
            {
                UserName = Configuration.GetSection("UserSettings")["UserEmail"],
                Email = Configuration.GetSection("UserSettings")["UserEmail"],
                FirstName = "Addmin",
                LastName = "Admin",
                Enabled = true
            };
            var test = _userManager.FindByEmailAsync(Configuration.GetSection("UserSettings")["UserEmail"]);
            if (test == null)
            {
                string UserPassword = Configuration.GetSection("UserSettings")["UserPassword"];
                poweruser.EmailConfirmed = true;
                _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var createPowerUser = await _userManager.CreateAsync(poweruser, UserPassword);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the "Admin" role 
                    await _userManager.AddToRoleAsync(poweruser, "Admin");
                }
            }
        }
    }
}
