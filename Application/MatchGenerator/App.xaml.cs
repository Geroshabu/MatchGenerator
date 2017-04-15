using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MatchGenerator
{
	/// <summary>
	/// App.xaml の相互作用ロジック
	/// </summary>
	public partial class App : Application
	{
		public void Application_DispatchUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			string message = string.Format(
				"エラーが発生しました. バグかもしれないっす(´・Д・`)\n" +
				"改善のためにも, エラー発生時の状況を\n" +
				"教えてくれるとありがたいっすm(_ _)m\n\n" +
				"(エラー内容 : {0} {1})",
				e.Exception.GetType(), e.Exception.Message);
			MessageBox.Show(message, "予期せぬエラー");
			e.Handled = true;
		}

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			var bootstrapper = new Bootstrapper();
			bootstrapper.Run();
		}
	}
}
