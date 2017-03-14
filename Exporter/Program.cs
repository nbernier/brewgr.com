using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Brewgr.Web.App_Start;
using Brewgr.Web.Core.Model;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject.Web.Common;

namespace Exporter
{
    class Program
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();
        static void Main(string[] args)
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(NinjectWebCommon.CreateKernel);

            DirectoryInfo dir = new DirectoryInfo(@"d:\tmp\recipe\");
            IBeerXmlRecipeImporter importer = (IBeerXmlRecipeImporter)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IBeerXmlRecipeImporter));
            foreach (var file in dir.GetFiles())
            {
                try
                {
                    string text = File.ReadAllText(file.FullName);
                    Recipe recipe=importer.Import(text);
                    Console.WriteLine($" recipe {recipe.RecipeName} imported");
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
