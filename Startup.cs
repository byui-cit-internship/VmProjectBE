﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using VmProjectBE.DAL;
using VmProjectBE.Handlers;
using VmProjectBE.Services;

namespace VmProjectBE
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables("CONSTR");

            Configuration = builder.Build();
            Environment = env;
            Console.WriteLine(env.EnvironmentName);
            MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        public string MyAllowSpecificOrigins { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // This helps to prevent the Reference Loop that is caused
            // when you reference a model inside another model, model like
            // enrollment which reference Course and User and both making a Reference back to enrollment
            services.AddControllers()
           .AddNewtonsoftJson(
               opts => opts.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
           );

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
            // This is needed to register my Background service
            // UNCOMMENT LATER services.AddHostedService<BackgroundService1>();
            // Allow to use client Factory
            services.AddHttpClient();

            services.AddHttpContextAccessor();

            services.AddDistributedMemoryCache();


            services.AddControllers()
                .AddNewtonsoftJson(s =>
                {
                    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            // This allows for Cross-origin request Read more: https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-6.0 
            services.AddCors(options =>
            {
                options.AddPolicy(options.DefaultPolicyName, builder =>
                {
                    builder.WithOrigins("http://localhost:5501")
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(origin => true)
                        .AllowCredentials()
                        .AllowAnyMethod();
                });
            });

            //this helps to connect the authentication for controllers request to the BasicAuthenticationHandler 
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, AppAuthHandler>("BasicAuthentication", null);

            string dbServer = Configuration.GetConnectionString("DB_SERVER");
            string dbPort = Configuration.GetConnectionString("DB_PORT");
            string dbDatabase = Configuration.GetConnectionString("DB_DATABASE");
            string dbUser = Configuration.GetConnectionString("DB_USER");
            string dbPassword = Configuration.GetConnectionString("DB_PASSWORD");

            String connectionString = "";
            if (dbServer == null)
            {
                connectionString = Configuration.GetConnectionString("DatabaseString");
            }
            else
            {
                connectionString = $"Server={dbServer},{dbPort};Database={dbDatabase};User Id={dbUser}; Password={dbPassword};";
            }

            services.AddDbContext<VmEntities>(opt =>
                opt.UseSqlServer(connectionString)
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Microsoft.AspNetCore.Mvc.Infrastructure.IActionDescriptorCollectionProvider actionProvider)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseCors();

            // This tell app that it will use authentication
            app.UseAuthentication();
            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            Console.WriteLine("Available routes:");
            var routes = actionProvider.ActionDescriptors.Items.Where(x => x.AttributeRouteInfo != null);
            foreach (var route in routes)
            {
                Console.WriteLine($"{route.AttributeRouteInfo.Template}");
            }
            Console.WriteLine("Application configured successfully");
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<VmEntities>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
