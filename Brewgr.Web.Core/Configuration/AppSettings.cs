using System;
using System.Collections.Generic;
using System.Configuration;

namespace Brewgr.Web.Core.Configuration
{
	public class AppSettings : AbstractWebSettings
	{
	    public override string Environment
	    {
            get { return ConfigurationManager.AppSettings[System.Reflection.MethodBase.GetCurrentMethod().Name]; }
        }
        /// <summary>
        /// Gets the RootPath
        /// </summary>
        public override string RootPath
		{
			get { return ConfigurationManager.AppSettings[System.Reflection.MethodBase.GetCurrentMethod().Name]; }
		}

		/// <summary>
		/// Gets the RootPathSecure
		/// </summary>
		public override string RootPathSecure
		{
            get { return ConfigurationManager.AppSettings[System.Reflection.MethodBase.GetCurrentMethod().Name]; }
        }

        /// <summary>
        /// Gets the static root path
        /// </summary>
        public override string StaticRootPath
		{
            get { return ConfigurationManager.AppSettings[System.Reflection.MethodBase.GetCurrentMethod().Name]; }
        }

        /// <summary>
        /// Gets the secure static root path
        /// </summary>
        public override string StaticRootPathSecure
		{
            get { return ConfigurationManager.AppSettings[System.Reflection.MethodBase.GetCurrentMethod().Name]; }
        }

        /// <summary>
        /// Gets a value specifying whether or not https is disabled
        /// </summary>
        public override bool DisableHttps
		{
            get { return Boolean.Parse(ConfigurationManager.AppSettings[System.Reflection.MethodBase.GetCurrentMethod().Name]); }
        }

        /// <summary>
        /// Gets the MediaPhysicalRoot
        /// </summary>
        public override string MediaPhysicalRoot
		{
            get { return ConfigurationManager.AppSettings[System.Reflection.MethodBase.GetCurrentMethod().Name]; }
        }


    }
}