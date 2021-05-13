using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using ProjectEarthServerAPI.Util;
using Microsoft.AspNetCore.Authentication;
using ProjectEarthServerAPI.Authentication;
using Serilog;

namespace ProjectEarthServerAPI
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
			services.AddControllers();

			services.AddResponseCompression(options =>
			{
				options.Providers.Add<GzipCompressionProvider>();
			});

			services.AddResponseCaching();

			services.AddApiVersioning(config =>
			{
				config.DefaultApiVersion = new ApiVersion(1, 1);
				config.AssumeDefaultVersionWhenUnspecified = true;
				config.ReportApiVersions = true;
			});

			services.AddAuthentication("GenoaAuth")
				.AddScheme<AuthenticationSchemeOptions, GenoaAuthenticationHandler>("GenoaAuth", null);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				//app.UseDeveloperExceptionPage();
			}

			//app.UseSerilogRequestLogging();

			app.UseETagger();
			//app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

            app.UseWebSockets(new WebSocketOptions{KeepAliveInterval = TransactionManager.MaximumTimeout});

			app.UseResponseCaching();

			app.UseResponseCompression();

			//app.UseSession();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
