namespace Application.Models.Configuration
{
	public class AnalyticsOptions
	{
		#region Properties

		public virtual string Authority { get; set; } = "https://regionorebrolan.matomo.cloud";
		public virtual bool Enabled { get; set; }
		public virtual int? Id { get; set; }
		public virtual bool TagManagerEnabled { get; set; }
		public virtual string TagManagerId { get; set; }

		#endregion
	}
}