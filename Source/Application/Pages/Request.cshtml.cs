using Microsoft.Extensions.Logging;

namespace Application.Pages
{
	public class RequestModel : SitePageModel
	{
		#region Constructors

		public RequestModel(ILoggerFactory loggerFactory) : base(loggerFactory) { }

		#endregion
	}
}