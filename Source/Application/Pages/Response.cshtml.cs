using Microsoft.Extensions.Logging;

namespace Application.Pages
{
	public class ResponseModel : SitePageModel
	{
		#region Constructors

		public ResponseModel(ILoggerFactory loggerFactory) : base(loggerFactory) { }

		#endregion
	}
}