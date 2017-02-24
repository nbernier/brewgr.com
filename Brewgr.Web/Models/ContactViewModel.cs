using System;
using System.Linq;
using System.Net;
using Brewgr.Web.Validators;
using ctorx.Core.Validation;

namespace Brewgr.Web.Models
{
	public class ContactViewModel : ValidatesWith<ContactViewModelValidator>
	{
		/// <summary>
		/// Gets or sets the Name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the EmailAddress
		/// </summary>
		public string EmailAddress { get; set; }

        public string ShopEmail { get; set; }
        /// <summary>
        /// Gets or sets the MessageContent
        /// </summary>
        public string MessageContent { get; set; }
	}
}