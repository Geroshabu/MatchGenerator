using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Mef;

namespace MatchGenerator
{
	internal class Bootstrapper : MefBootstrapper
	{
		protected override void ConfigureAggregateCatalog()
		{
			base.ConfigureAggregateCatalog();

			AggregateCatalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
		}

		protected override DependencyObject CreateShell()
		{
			return Container.GetExportedValue<MainWindow>();
		}

		protected override void InitializeShell()
		{
			base.InitializeShell();

			Application.Current.MainWindow = Shell as Window;
			Application.Current.MainWindow.Show();
		}
	}
}
