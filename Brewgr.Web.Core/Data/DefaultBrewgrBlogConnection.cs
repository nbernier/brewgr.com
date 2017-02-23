using System;
using System.Configuration;

namespace Brewgr.Web.Core.Data
{
    public class DefaultBrewgrBlogConnection : IBrewgrBlogConnection
    {
        /// <summary>
        /// Gets the connection string
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["BrewgrBlog_ConnectionString"].ConnectionString;
            }
        }
    }
}