using Microsoft.Extensions.Logging;

namespace Application.Pages
{
	public class ContextModel : SitePageModel
	{
		#region Constructors

		public ContextModel(ILoggerFactory loggerFactory) : base(loggerFactory) { }

		#endregion
	}
}