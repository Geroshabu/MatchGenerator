using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MatchGenerator.Core;

namespace MatchGenerator
{
	/// <summary>
	/// LayoutConfigureWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class LayoutConfigureWindow : Window
	{
		public LayoutConfigureWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			SettingImporter importer = new SettingImporter();
			LayoutInformation info = importer.Import("Setting.ini");

			courtCountRowTextBox.Text = info.Row.ToString();
			courtCountColumnTextBox.Text = info.Column.ToString();
			MatchCountTextBox.Text = info.CourtCount.ToString();
		}
	}
}
