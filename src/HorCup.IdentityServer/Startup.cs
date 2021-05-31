using System.Security.Cryptography.X509Certificates;
using HorCup.IdentityServer.Data;
using HorCup.IdentityServer.Extensions;
using HorCup.IdentityServer.Models;
using HorCup.Infrastructure.Filters;
using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace HorCup.IdentityServer
{
	public class Startup
	{
		public IWebHostEnvironment Environment { get; }
		public IConfiguration Configuration { get; }

		public Startup(IWebHostEnvironment environment, IConfiguration configuration)
		{
			Environment = environment;
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews(options => { options.Filters.Add(typeof(CustomExceptionFilter)); });

			var connectionString = Configuration.GetConnectionString("DefaultConnection");

			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(connectionString));

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			var builder = services.AddIdentityServer(options =>
				{
					options.Events.RaiseErrorEvents = true;
					options.Events.RaiseInformationEvents = true;
					options.Events.RaiseFailureEvents = true;
					options.Events.RaiseSuccessEvents = true;
					options.EmitStaticAudienceClaim = true;
				})
				.AddConfigurationStore(options =>
				{
					options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString,
						sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName));
				})
				.AddOperationalStore(options =>
				{
					options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString,
						sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName));
					options.EnableTokenCleanup = true;
				})
				.AddAspNetIdentity<ApplicationUser>();

			AddCertificate(builder);

			services.AddSwaggerGen();

			services.AddCors(options => options.AddPolicy("AllowAll", p =>
			{
				p.AllowAnyOrigin()
					.AllowAnyHeader()
					.AllowAnyMethod();
			}));
			services.AddAuthentication()
				.AddGoogle(options =>
				{
					options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

					// register your IdentityServer with Google at https://console.developers.google.com
					// enable the Google+ API
					// set the redirect URI to https://localhost:5001/signin-google
					options.ClientId = Configuration["ExternalClients:Google:ClientId"];
					options.ClientSecret = Configuration["ExternalClients:Google:ClientSecret"];
				});
		}

		

		public void Configure(
			IApplicationBuilder app,
			ILoggerFactory loggerFactory)
		{
			if (Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			loggerFactory.AddFile("Logs/identity-serever-log.txt");

			app.MigrateAndSeedDb(Configuration);
			app.UseCors("AllowAll");
			app.UseStaticFiles();

			app.UseRouting();
			app.UseIdentityServer();
			app.UseAuthorization();


			app.UseSwagger();
			app.UseSwaggerUI(sw => { sw.SwaggerEndpoint("/swagger/v1/swagger.json", "Horbunov Home Cup v1"); });

			app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
		}
		
		private void AddCertificate(IIdentityServerBuilder builder)
		{
			if (Environment.IsDevelopment())
			{
				builder.AddDeveloperSigningCredential();
			}
			else
			{
				X509Certificate2 cert = null;
				using (var certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser))
				{
					certStore.Open(OpenFlags.ReadOnly);
					var certCollection = certStore.Certificates.Find(
						X509FindType.FindByThumbprint,
						Configuration["Certificates:HorCupSigning:Thumbprint"],
						false);

					if (certCollection.Count > 0)
					{
						cert = certCollection[0];
					}
				}           

				builder.AddSigningCredential(cert);
			}
		}
	}
}