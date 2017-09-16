using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using smnetcoreseed.core.Data;
using smnetcoreseed.core.Data.Identity;
using smnetcoreseed.core.Data.Repositories;
using smnetcoreseed.core.DomainModels;
using smnetcoreseed.core.Extensions.Identity;
using smnetcoreseed.core.Extensions.Repositories;
using smnetcoreseed.core.Interfaces.Identity;
using smnetcoreseed.core.Interfaces.Repositories;
using smnetcoreseed.core.Services;
using System.IO;
using AppPermissions = smnetcoreseed.core.Extensions.Repositories.ApplicationPermissions;

namespace smnetcoreseed.web
{
    public class Startup
    {

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                //builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            Configuration = configuration;
        }

        public IConfigurationRoot ConfigurationRoot { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // ADD DB CONTEXTS
            services.AddDbContext<CoreIdentityDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("CoreIdentityDbContextConnection")));

            services.AddIdentity<CoreIdentityUser, CoreIdentityRole>(options =>
            {
                // User settings
                options.User.RequireUniqueEmail = true;

                //    //// Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<CoreIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddDbContext<CoreRepositoriesDbContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("CoreRepositoriesDbContextConnection")));

            services.AddIdentity<smnetcoreseed.core.Models.ApplicationUser, smnetcoreseed.core.Models.ApplicationRole>(options =>
           {
                // User settings
                options.User.RequireUniqueEmail = true;

                //    //// Password settings
                options.Password.RequireDigit = false;
               options.Password.RequiredLength = 8;
               options.Password.RequireNonAlphanumeric = false;
               options.Password.RequireUppercase = false;
               options.Password.RequireLowercase = false;

                //    //// Lockout settings
                //    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                //    //options.Lockout.MaxFailedAccessAttempts = 10;

                //options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                //options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                //options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            })
                .AddEntityFrameworkStores<CoreRepositoriesDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();

            // Add CookieTempDataProvider after AddMvc and include ViewFeatures.
            // using Microsoft.AspNetCore.Mvc.ViewFeatures;
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();

            //ADD AUTHORIZATION OPTIONS

            services.AddAuthorization(options =>
            {
                //Set default authorization
                options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireRole("administrator").Build();

                options.AddPolicy(AuthPolicies.ViewUserByUserIdPolicy, policy => policy.Requirements.Add(new ViewUserByIdRequirement()));

                options.AddPolicy(AuthPolicies.ViewUsersPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ViewUsers));

                options.AddPolicy(AuthPolicies.ManageUserByUserIdPolicy, policy => policy.Requirements.Add(new ManageUserByIdRequirement()));

                options.AddPolicy(AuthPolicies.ManageUsersPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageUsers));

                options.AddPolicy(AuthPolicies.ViewRoleByRoleNamePolicy, policy => policy.Requirements.Add(new ViewRoleByNameRequirement()));

                options.AddPolicy(AuthPolicies.ViewRolesPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ViewRoles));

                options.AddPolicy(AuthPolicies.AssignRolesPolicy, policy => policy.Requirements.Add(new AssignRolesRequirement()));

                options.AddPolicy(AuthPolicies.ManageRolesPolicy, policy => policy.RequireClaim(CustomClaimTypes.Permission, AppPermissions.ManageRoles));

                //options.AddPolicy("Users", policy => policy.RequireAuthenticatedUser().RequireRole("user"));

                //options.AddPolicy("AddEditUser", policy => {
                //    policy.RequireClaim("Add User", "Add User");
                //    policy.RequireClaim("Edit User", "Edit User");
                //});
                //options.AddPolicy("DeleteUser", policy => policy.RequireClaim("Delete User", "Delete User"));
            });

            //set to apply default authorization for empty controllers and [Authorize] declarations (Default Auth declared above)
            services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, OverridableDefaultAuthorizationApplicationModelProvider>());

            //services.AddAuthentication();

            // Add application services.

            //ADD Account Manager Services
            services.AddScoped<ICoreAccountManager, CoreAccountManager>();
            services.AddScoped<IRepositoriesAccountManager, RepositoriesAccountManager>();

            // Auth Policies (Repositories Context)
            services.AddSingleton<IAuthorizationHandler, ViewUserByIdHandler>();
            services.AddSingleton<IAuthorizationHandler, ManageUserByIdHandler>();
            services.AddSingleton<IAuthorizationHandler, ViewRoleByNameHandler>();
            services.AddSingleton<IAuthorizationHandler, AssignRolesHandler>();

            // DB Creation and Seeding
            services.AddTransient<IDatabaseInitializer, CoreIdentityDbInitializer>();
            services.AddTransient<IDatabaseInitializer, CoreRepositoriesDbInitializer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                       Path.Combine(env.ContentRootPath, "node_modules")),
                RequestPath = "/node_modules"
            });

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                 name: "areas",
               template: "{area:exists}/{controller}/{action}/{id?}"
               );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute("spa-fallback", new { controller = "Home", action = "Index" });
            });
        }
    }
}