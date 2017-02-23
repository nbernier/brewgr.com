using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ctorx.Core.Data
{
	public abstract class AbstractDbContext : DbContext
	{

        public AbstractDbContext(string cNN_KEY) : base(cNN_KEY)
        {
            
        }

        /// <summary>
        /// ctor the Mighty
        /// </summary>
        /// <summary>
        /// Fires when the model is created
        /// </summary>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}