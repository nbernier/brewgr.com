using System;
using System.Web.Mvc;
using Brewgr.Web.Core.Configuration;
using ctorx.Core.Ninject;

namespace Brewgr.Web
{
	public class ForceHttps : RequireHttpsAttribute
	{
		/// <summary>
		/// Fires on Authorization
		/// </summary>
		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			if(filterContext == null)
			{
				throw new ArgumentNullException("filterContext");
			}

			base.OnAuthorization(filterContext);
		}
	}
}