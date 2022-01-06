using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Application.Pages
{
	public class EnvironmentModel : SitePageModel
	{
		#region Constructors

		public EnvironmentModel(IWebHostEnvironment environment, ILoggerFactory loggerFactory) : base(loggerFactory)
		{
			this.Environment = environment ?? throw new ArgumentNullException(nameof(environment));
		}

		#endregion

		#region Properties

		public virtual IWebHostEnvironment Environment { get; }

		#endregion
	}
}