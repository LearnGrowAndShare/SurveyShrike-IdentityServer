using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MediatR;
using AutoMapper;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.AspNetCore;
using SurveyShrike_IdentityServer.Application.Infrastructure.AutoMapperProfile;
using SurveyShrike_IdentityServer.Persistence.DBContext.Identity;
using Microsoft.Extensions.Hosting;
using SurveyShrike_IdentityServer.Application.Interfaces.Implemenation;
using SurveyShrike_IdentityServer.Application.Interfaces;
using SurveyShrike_IdentityServer.Application.Infrastructure;
using SurveyShrike_IdentityServer.Domain.Entities;
using SurveyShrike_IdentityServer.Filters;
using SurveyShrike_IdentityServer.Application.Users.Command.CreateCommand;
using SurveyShrike_IdentityServer.Application.Users.Command.Queries.Authentication;
using SurveyShrike_IdentityServer.Persistence.DBContext.AppConfiguration;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.OpenApi.Models;
using System.Linq;
using IdentityServer4.EntityFramework.Mappers;

namespace SurveyShrike_IdentityServer
{
    //[ApiController]
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            IWebHostEnvironment env = serviceProvider.GetService<IWebHostEnvironment>();
            var migrationsAssembly = "SurveyShrike-IdentityServer.Persistence";
           services.AddControllers();
            //swagger
            // Register the Swagger generator, defining 1 or more Swagger documents
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        

        // Add AutoMapper
        services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });

            // Add DbContext using SQL Server Provider
            services.AddDbContext<IdentityServerDbContext>(options =>
                options.UseSqlite(GetConnectionString(env.IsDevelopment())).UseLazyLoadingProxies());

            // Add framework services.
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IIdentityServerDbContext, IdentityServerDbContext>();
            services.AddTransient<IReturnUrlParser, ReturnUrlParser>();

            // Add MediatR
            services.AddMediatR(typeof(AuthenticationHandler).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

          //cors
            services.AddCors(setup =>
            {
                setup.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    //policy.AllowAnyOrigin();
                    policy.WithOrigins("http://localhost:5000", "http://localhost:4200", "http://localhost:8080");
                    policy.AllowCredentials();
                });
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.Lockout = new LockoutOptions()
                {
                    AllowedForNewUsers = true,
                    MaxFailedAccessAttempts = 5,
                    DefaultLockoutTimeSpan = TimeSpan.FromHours(1)
                };
                option.Password = new PasswordOptions()
                {
                    RequireDigit = true,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = true
                };
                option.User = new UserOptions() { RequireUniqueEmail = true };

            })

               .AddEntityFrameworkStores<IdentityServerDbContext>()
               .AddDefaultTokenProviders();

            services.AddMvcCore()
                    .AddApiExplorer()
                    .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);


            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.UserInteraction.LoginUrl = "http://localhost:5000/index.html";
                options.UserInteraction.ErrorUrl = "http://localhost:5000/error.html";
                options.UserInteraction.LogoutUrl = "http://localhost:5000/logout.html";
            })
                .AddAspNetIdentity<ApplicationUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseSqlite(GetConnectionString(env.IsDevelopment()),
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                    // this adds the operational data from DB (codes, tokens, consents)
                    .AddOperationalStore(options =>
                    {
                        options.ConfigureDbContext = b =>
                            b.UseSqlite(GetConnectionString(env.IsDevelopment()),
                                sql => sql.MigrationsAssembly(migrationsAssembly));

                        // this enables automatic token cleanup. this is optional.
                        options.EnableTokenCleanup = true;
                        options.TokenCleanupInterval = 300;
                    });
            if (env.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("no certificate");
            }

            //custom attribute
            services
             .AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
             .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
             .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserCommandValidator>());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //init app
            InitializeDatabase(app);

            if (env.IsDevelopment())
            {
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity server");

                });


                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
            app.UseCors();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();

                if (context.Clients.Any()) {
                    context.Clients.RemoveRange(context.Clients.ToList());
                    context.SaveChanges();
                }
                if (!context.Clients.Any())
                {
                    foreach (var client in IdServerConfigurations.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in IdServerConfigurations.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
                if (context.ApiResources.Any())
                {
                    context.ApiResources.RemoveRange(context.ApiResources.ToList());
                    context.SaveChanges();
                }
                if (!context.ApiResources.Any())
                {
                    foreach (var resource in IdServerConfigurations.GetApis())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }

        private string GetConnectionString(bool isDevelopment)
        {
            IConfigurationRoot configuration = null;
            if (isDevelopment)
            {
                configuration = new ConfigurationBuilder()
                           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                           .AddJsonFile("appsettings.json")
                           .AddJsonFile("appsettings.development.json", true)
                           .Build();

                return configuration.GetConnectionString("IdentityServer");
            }
            else
            {

                configuration = new ConfigurationBuilder()
                           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                           .AddJsonFile("appsettings.json")
                           .Build();
                return configuration.GetConnectionString("IdentityServer");
            }


        }
    }
}
