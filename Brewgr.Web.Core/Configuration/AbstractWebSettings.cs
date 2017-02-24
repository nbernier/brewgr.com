using System;
using System.Collections.Generic;
using System.Configuration;

namespace Brewgr.Web.Core.Configuration
{
	public abstract class AbstractWebSettings : IWebSettings
	{
	    public bool IsProduction()
	    {
	        return "prod".Equals(Environment, StringComparison.InvariantCultureIgnoreCase);
	    }
		/// <summary>
		/// Gets the RootPath
		/// </summary>
		public abstract string RootPath { get; }

		/// <summary>
		/// Gets the RootPathSecure
		/// </summary>
		public abstract string RootPathSecure { get;  }

		/// <summary>
		/// Gets the static root path
		/// </summary>
		public abstract string StaticRootPath { get;  }

		/// <summary>
		/// Gets the secure static root path
		/// </summary>
		public abstract string StaticRootPathSecure { get;  }

		/// <summary>
		/// Gets a value specifying whether or not https is disabled
		/// </summary>
		public abstract bool DisableHttps { get; }

		/// <summary>
		/// Gets the MediaPhysicalRoot
		/// </summary>
		public abstract string MediaPhysicalRoot { get;  }

		/// <summary>
		/// Gets the MediaUrlRoot
		/// </summary>
		public string MediaUrlRoot 
		{ 
			get { return MediaUrlRootSecure; }
		}

		/// <summary>
		/// Gets the MediaUrlRoot Secure
		/// </summary>
		public string MediaUrlRootSecure
		{
			get { return "https://brewgr.com/Media"; }
		}

		/// <summary>
		/// Gets or sets the SenderName
		/// </summary>
		public virtual string SenderDisplayName
		{
			get { return "Brewgr"; }
		}

		/// <summary>
		/// Gets or sets the SenderAddress
		/// </summary>
		public virtual string SenderAddress
		{
            get { return ConfigurationManager.AppSettings[System.Reflection.MethodBase.GetCurrentMethod().Name]; }
		}

		/// <summary>
		/// Gets the contact form Email Address
		/// </summary>
		public virtual IList<string> ContactFormEmailAddress
		{
			get { return new[] { this.ShopEmail }; }
		}

        public virtual string ShopEmail
        {
            get { return ConfigurationManager.AppSettings[System.Reflection.MethodBase.GetCurrentMethod().Name]; }
        }

        /// <summary>
        /// Gets the default number of Recipes per page
        /// </summary>
        public int DefaultRecipesPerPage
		{
			get { return 10; }
		}

		/// <summary>
		/// Gets the default image root
		/// </summary>
		public string DefaultRecipeImageRoot
		{
			get { return "/img/mug/"; }
		}

	    public abstract string Environment { get; }
	}
}