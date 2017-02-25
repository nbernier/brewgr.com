using System;

namespace Brewgr.Web.Core.Configuration
{
	public class DefaultFacebookConnectSettings : IFacebookConnectSettings
	{
		readonly IWebSettings _WebSettings;

		/// <summary>
		/// ctor the Mighty
		/// </summary>
		public DefaultFacebookConnectSettings(IWebSettings webSettings)
		{
			this._WebSettings = webSettings;
		}

		/// <summary>
		/// Gets or sets the ApplicationKey
		/// </summary>
		public string ApplicationKey
		{
			get { return _WebSettings.FB_ApplicationKey ?? "ThisIsTheDevFBKeyAndShouldNotChange"; }
		}

		/// <summary>
		/// Gets or sets the ApplicationSecret
		/// </summary>
		public string ApplicationSecret
		{
			get { return _WebSettings.FB_ApplicationSecret ?? "ThisIsTheDevFBSecretAndShouldNotChange"; }
		}
	}
}