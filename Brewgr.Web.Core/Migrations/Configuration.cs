﻿using System.Diagnostics;
using System.IO;

namespace Brewgr.Web.Core.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Brewgr.Web.Core.Data.BrewgrContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Brewgr.Web.Core.Data.BrewgrContext context)
        {
            //check if db is already seeded
            if (context.Fermentables == null || !context.Fermentables.Any())
            {
                DirectoryInfo baseDir =
                    new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin\\Migrations\\Initial"));
                if (!baseDir.Exists)
                {
                    baseDir = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Migrations\\Initial"));
                }
                foreach (var file in baseDir.GetFiles().ToList().OrderBy(x => x.Name))
                {
                    Console.WriteLine(file.Name);
                    context.Database.ExecuteSqlCommand(File.ReadAllText(file.FullName));
                }
            }
        }
    }
}
