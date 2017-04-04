﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Web;
using System.Web.Mvc;
using FluentValidation;
using StackExchange.Exceptional;
using ctorx.Core.Collections;
using ctorx.Core.Messaging;
using ctorx.Core.Ninject;
using ctorx.Core.Security;
using ctorx.Core.Validation;
using ctorx.Core.Web;
using Brewgr.Web.Core.Configuration;
using Brewgr.Web.Core.Model;
using Brewgr.Web.Core.Service;
using Common.Logging;
using NLog;
using LogManager = Common.Logging.LogManager;

namespace Brewgr.Web.Controllers
{
	public abstract class BrewgrController : Controller
	{
	    protected static ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        readonly IMessageStore ForwardedMessageStore;
		readonly IUserResolver UserResolver;
		readonly IUserService UserService;
		readonly IAuthenticationService AuthenticationService;

		protected UserSummary ActiveUser;
		protected readonly IWebSettings WebSettings;

		/// <summary>
		/// ctor the Mighty
		/// </summary>
		protected BrewgrController()
		{
			// Setup Messaging
			this.ViewBag.Messages = new List<IMessage>();
			this.ForwardedMessageStore = new TempDataMessageStore(this.TempData); 

			// Set Environment
			ViewBag.Environment = ConfigurationManager.AppSettings["Environment"];

			// Dependencies (manual injection to avoid ctor pollution)
			var kernel = KernelPersister.Get();
			this.UserResolver = kernel.GetService(typeof(IUserResolver)) as IUserResolver;
			this.UserService = kernel.GetService(typeof(IUserService)) as IUserService;
			this.WebSettings = kernel.GetService(typeof(IWebSettings)) as IWebSettings;
			this.AuthenticationService = kernel.GetService(typeof (IAuthenticationService)) as IAuthenticationService;
		}

		#region LIFECYCLE 

		/// <summary>
		/// Begins to invoke the action in the current controller context.
		/// </summary>
		protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
		{
			// Resolve the Active User
			if (this.User.Identity.IsAuthenticated)
			{
				try
				{
					this.ActiveUser = this.UserResolver.Resolve();
					ViewBag.ActiveUser = this.ActiveUser;
				}
				catch (SecurityException se)
				{
					this.AuthenticationService.SignOut();
					Session.Abandon();
				}
			}

            // Site Announcement
            ViewBag.SiteAnnouncement = null;
            
            return base.BeginExecuteCore(callback, state);
		}

		/// <summary>
		/// Fires on Action Executing
		/// </summary>
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			// Append any Forwarded Messages
			var pushedMessages = this.ForwardedMessageStore.GetMessages();
			if (pushedMessages != null)
			{
				pushedMessages.ForEach(this.AppendMessage);
			}

			// Set View Bag Values for Client
			ViewBag.StaticRoot = this.WebSettings.StaticRootPathSecure;

			base.OnActionExecuting(filterContext);
		}

		#endregion

		#region MESSAGING

		/// <summary>
		/// Determines if there are any messages in the message store
		/// </summary>
		public bool HasMessages()
		{
			if(ViewBag.Messages == null)
			{
				return false;
			}

			return (this.ViewBag.Messages as List<IMessage>).Any();
		}

		/// <summary>
		/// Appends a Message to the Rendered View
		/// </summary>
		public void AppendMessage(IMessage message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}

			this.ViewBag.Messages.Add(message);
		}

		/// <summary>
		/// Forwards a message to the new request
		/// </summary>
		protected void ForwardMessage(IMessage message)
		{
			this.ForwardedMessageStore.AddMessage(message);
		}

		/// <summary>
		/// Validates a model without messaging
		/// </summary>
		protected bool ValidateWithoutMessaging(IValidatingViewModel validatingViewModel)
		{
			return this.ValidateAndMessage(validatingViewModel);
		}

		/// <summary>
		/// Validates and appends any error messages
		/// </summary>
		protected bool ValidateAndAppendMessages(IValidatingViewModel validatingViewModel)
		{
			return this.ValidateAndMessage(validatingViewModel, this.AppendMessage);
		}

		/// <summary>
		/// Validates and forwards any error messages
		/// </summary>
		protected bool ValidateAndForwardMessages(IValidatingViewModel validatingViewModel)
		{
			return ValidateAndMessage(validatingViewModel, this.ForwardMessage);
		}

		/// <summary>
		/// Validates and Messages
		/// </summary>
		protected bool ValidateAndMessage(IValidatingViewModel validatingViewModel, Action<IMessage> messagingAction = null)
		{
			var result = validatingViewModel.Validate();
			if (!result.IsValid)
			{
				if(messagingAction != null)
				{
					result.Errors.ForEach(x => messagingAction(new ErrorMessage { Text = x.ErrorMessage }));
				}

				return false;
			}

			return true;
		}

		/// <summary>
		/// Validates and appends any error messages
		/// </summary>
		protected bool ValidateAndAppendMessages<TModel>(TModel model, IValidator<TModel> validator) where TModel : class
		{
			return this.ValidateAndMessage(model, validator, this.AppendMessage);
		}

		/// <summary>
		/// Validates and forwards any error messages
		/// </summary>
		protected bool ValidateAndForwardMessages<TModel>(TModel model, IValidator<TModel> validator) where TModel : class
		{
			return ValidateAndMessage(model, validator, this.ForwardMessage);
		}

		/// <summary>
		/// Validates and Messages
		/// </summary>
		protected bool ValidateAndMessage<TModel>(TModel model, IValidator<TModel> validator, Action<IMessage> messagingAction)
		{
			var result = validator.Validate(model);
			if (!result.IsValid)
			{
				result.Errors.ForEach(x => messagingAction(new ErrorMessage { Text = x.ErrorMessage }));
				return false;
			}

			return true;
		}

		#endregion

		#region UTILITY 

		/// <summary>
		/// Logs a handled exception
		/// </summary>
		public void LogHandledException(Exception exception)
		{
			ErrorStore.LogException(exception, System.Web.HttpContext.Current);
		}

		/// <summary>
		/// Disables the browser cache for the response
		/// </summary>
		public void DisableBrowserCache()
		{
			Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
			Response.Cache.SetValidUntilExpires(false);
			Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.Cache.SetNoStore();
		}

		#endregion

		#region FORCE STATUS CODE

		/// <summary>
		/// Issues a 404 Response
		/// </summary>
		public EmptyResult Issue404()
		{
			throw new HttpException(404, "Not Found");
		}

		/// <summary>
		/// Issues a 500 Response
		/// </summary>
		public EmptyResult Issue500()
		{
			throw new HttpException(500, "Server Error");
		}

		#endregion

		#region REUSABLE DATA 

		/// <summary>
		/// Adds country data to the ViewBag
		/// </summary>
		public void AddCountryDataToViewBag(bool addChooseOption = true)
		{
			ViewBag.Countries = this.UserService.GetCountryList();
			if(addChooseOption)
			{
				ViewBag.Countries.Insert(0, new Country { CountryCode = "", CountryName = "Choose..." });
			}
		}

		#endregion
	}
}