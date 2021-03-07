// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using HorCup.IdentityServer.Data;
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
			
			services.AddIdentityServer(options =>
				{
					options.Events.RaiseErrorEvents = true;
					options.Events.RaiseInformationEvents = true;
					options.Events.RaiseFailureEvents = true;
					options.Events.RaiseSuccessEvents = true;

					// see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
					options.EmitStaticAudienceClaim = true;
				})
				.AddTestUsers(TestUsers.Users)
				// this adds the config data from DB (clients, resources, CORS)
				.AddConfigurationStore(options =>
				{
					options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString,
						sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName));
				})
				// this adds the operational data from DB (codes, tokens, consents)
				.AddOperationalStore(options =>
				{
					options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString,
						sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName));
					;

					// this enables automatic token cleanup. this is optional.
					options.EnableTokenCleanup = true;
				})
				// not recommended for production - you need to store your key material somewhere secure
				.AddDeveloperSigningCredential();

			services.AddSwaggerGen();

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

			app.UseSwagger();
			app.UseSwaggerUI(sw => { sw.SwaggerEndpoint("/swagger/v1/swagger.json", "Horbunov Home Cup v1"); });

			app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
		}
	}
}