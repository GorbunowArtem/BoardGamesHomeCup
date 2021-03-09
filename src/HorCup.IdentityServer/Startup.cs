using HorCup.IdentityServer.Data;
using HorCup.IdentityServer.Quickstart;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Identity;

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
			services.AddControllersWithViews();

			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<AppUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			var connectionString = Configuration.GetConnectionString("DefaultConnection");
			
			var builder = services.AddIdentityServer(options =>
				{
					options.Events.RaiseErrorEvents = true;
					options.Events.RaiseInformationEvents = true;
					options.Events.RaiseFailureEvents = true;
					options.Events.RaiseSuccessEvents = true;

					// see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
					options.EmitStaticAudienceClaim = true;
				})
				.AddInMemoryIdentityResources(Config.IdentityResources)
				.AddInMemoryApiScopes(Config.ApiScopes)
				.AddInMemoryClients(Config.Clients)
				.AddAspNetIdentity<AppUser>();

			services.AddSwaggerGen();

			
			services.AddCors(options => options.AddPolicy("AllowAll", p =>
			{
				p.AllowAnyOrigin()
					.AllowAnyHeader()
					.AllowAnyMethod();
				
			}));
			// services.AddAuthentication()
			// 	.AddGoogle(options =>
			// 	{
			// 		options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
			//
			// 		// register your IdentityServer with Google at https://console.developers.google.com
			// 		// enable the Google+ API
			// 		// set the redirect URI to https://localhost:5001/signin-google
			// 		options.ClientId = "";
			// 		options.ClientSecret = "";
			// 	});
		}

		public void Configure(IApplicationBuilder app)
		{
			if (Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}

			app.UseStaticFiles();

			app.UseRouting();
			app.UseIdentityServer();
			app.UseAuthorization();
			app.UseCors("AllowAll");

			app.UseSwagger();
			app.UseSwaggerUI(sw => { sw.SwaggerEndpoint("/swagger/v1/swagger.json", "Horbunov Home Cup v1"); });

			app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
		}
	}
}