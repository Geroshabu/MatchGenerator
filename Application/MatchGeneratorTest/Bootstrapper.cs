using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mef;

namespace MatchGeneratorTest
{
	internal class Bootstrapper : MefBootstrapper
	{
		private IList<Action> composeActions = new List<Action>();

		protected override void ConfigureContainer()
		{
			base.ConfigureContainer();

			foreach(Action composeAction in composeActions)
			{
				composeAction();
			}
		}

		public void ReserveComposing<T>(T instance)
		{
			composeActions.Add(() => Container.ComposeExportedValue(instance));
		}
	}
}
