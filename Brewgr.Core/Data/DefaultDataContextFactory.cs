using System;

namespace ctorx.Core.Data
{
	public class DefaultDataContextFactory<TDataContext> : IDataContextFactory<TDataContext> where TDataContext : IDataContext
	{
		readonly IDataContextActivationInfo<TDataContext> DataContextActivationInfo;

		/// <summary>
		/// ctor the Mighty
		/// </summary>
		public DefaultDataContextFactory(IDataContextActivationInfo<TDataContext> dataContextActivationInfo)
		{
			this.DataContextActivationInfo = dataContextActivationInfo;
		}

		/// <summary>
		/// Makes a TDataContext
		/// </summary>
		public TDataContext Make()
		{
			// Use reflection to instantiate TDataContext
			var contextType = typeof(TDataContext);
		    

            return (TDataContext)Activator.CreateInstance(contextType);
		}
	}
}