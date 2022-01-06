using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Pages
{
	[SuppressMessage("Naming", "CA1724:Type names should not match namespaces")]
	public class ConfigurationModel : SitePageModel
	{
		#region Fields

		private IDictionary<IConfigurationProvider, IDictionary<string, string>> _configurationDetails;
		private IEnumerable<Tuple<int, IConfigurationProvider, bool>> _configurationProviders;
		private const string _configurationProvidersName = "ConfigurationProviders";
		private static readonly PropertyInfo _dataProperty = typeof(ConfigurationProvider).GetProperty("Data", BindingFlags.Instance | BindingFlags.NonPublic);

		#endregion

		#region Constructors

		public ConfigurationModel(IConfiguration configuration, ILoggerFactory loggerFactory) : base(loggerFactory)
		{
			this.Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}

		#endregion

		#region Properties

		protected internal virtual IConfiguration Configuration { get; }

		public virtual IDictionary<IConfigurationProvider, IDictionary<string, string>> ConfigurationDetails
		{
			get
			{
				// ReSharper disable InvertIf
				if(this._configurationDetails == null)
				{
					var configurationDetails = new Dictionary<IConfigurationProvider, IDictionary<string, string>>();

					foreach(var configurationProvider in this.ConfigurationProviders.Where(item => item.Item3).Select(item => item.Item2))
					{
						var data = (IDictionary<string, string>)this.DataProperty.GetValue(configurationProvider);

						configurationDetails.Add(configurationProvider, data);
					}

					this._configurationDetails = configurationDetails;
				}
				// ReSharper restore InvertIf

				return this._configurationDetails;
			}
		}

		public virtual IEnumerable<Tuple<int, IConfigurationProvider, bool>> ConfigurationProviders
		{
			get
			{
				// ReSharper disable InvertIf
				if(this._configurationProviders == null)
				{
					if(this.Configuration is IConfigurationRoot configurationRoot)
					{
						var configurationProviders = new List<Tuple<int, IConfigurationProvider, bool>>();
						var items = configurationRoot.Providers.Where(provider => provider is not ChainedConfigurationProvider).ToArray();
						var selections = new HashSet<int>();

						foreach(var value in this.Request.Query[this.ConfigurationProvidersName])
						{
							if(int.TryParse(value, out var number))
								selections.Add(number);
						}

						for(var i = 0; i < items.Length; i++)
						{
							var configurationProvider = items[i];
							var selected = selections.Contains(i);
							configurationProviders.Add(Tuple.Create(i, configurationProvider, selected));
						}

						this._configurationProviders = configurationProviders.ToArray();
					}
					else
					{
						this._configurationProviders = Enumerable.Empty<Tuple<int, IConfigurationProvider, bool>>();
					}
				}
				// ReSharper restore InvertIf

				return this._configurationProviders;
			}
		}

		public virtual string ConfigurationProvidersName => _configurationProvidersName;
		protected internal virtual PropertyInfo DataProperty => _dataProperty;

		#endregion
	}
}