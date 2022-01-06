using Microsoft.Extensions.Logging;

namespace Application.Pages
{
	public class IndexModel : SitePageModel
	{
		#region Constructors

		public IndexModel(ILoggerFactory loggerFactory) : base(loggerFactory) { }

		#endregion

		#region Methods

		public virtual void OnGet() { }

		#endregion
	}
}