using System;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
	public class Startup
	{
		#region Constructors

		public Startup(IConfiguration configuration, IWebHostEnvironment environment)
		{
			this.Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
			this.Environment = environment ?? throw new ArgumentNullException(nameof(environment));
		}

		#endregion

		#region Properties

		public virtual IConfiguration Configuration { get; }
		public virtual IWebHostEnvironment Environment { get; }

		#endregion

		#region Methods

		public virtual void Configure(IApplicationBuilder applicationBuilder)
		{
			applicationBuilder
				.UseDeveloperExceptionPage()
				.UseForwardedHeaders()
				.UseStaticFiles()
				.UseRouting()
				.UseAuthorization()
				.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
		}

		public virtual void ConfigureServices(IServiceCollection services)
		{
			// https://docs.microsoft.com/en-us/azure/app-service/configure-language-dotnetcore#detect-https-session
			services.Configure<ForwardedHeadersOptions>(options =>
			{
				options.ForwardedHeaders = ForwardedHeaders.All;
				// These three subnets encapsulate the applicable Azure subnets. At the moment, it's not possible to narrow it down further.
				options.KnownNetworks.Add(new IPNetwork(IPAddress.Parse("::ffff:10.0.0.0"), 104));
				options.KnownNetworks.Add(new IPNetwork(IPAddress.Parse("::ffff:192.168.0.0"), 112));
				options.KnownNetworks.Add(new IPNetwork(IPAddress.Parse("::ffff:172.16.0.0"), 108));
			});

			services.AddRazorPages();
		}

		#endregion
	}
}