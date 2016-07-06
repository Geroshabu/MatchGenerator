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
		LayoutInformation CourtLayout;

		public LayoutConfigureWindow(LayoutInformation courtLayout)
		{
			InitializeComponent();

			CourtLayout = courtLayout;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			SettingImporter importer = new SettingImporter();
			LayoutInformation court_layout = importer.Import("Setting.ini");

			courtCountRowTextBox.Text = court_layout.Row.ToString();
			courtCountColumnTextBox.Text = court_layout.Column.ToString();
			MatchCountTextBox.Text = court_layout.CourtCount.ToString();
		}

		private void okButton_Click(object sender, RoutedEventArgs e)
		{
			int parsed_value;
			CourtLayout.Row = int.TryParse(courtCountRowTextBox.Text, out parsed_value) ? parsed_value : CourtLayout.Row;
			CourtLayout.Column = int.TryParse(courtCountColumnTextBox.Text, out parsed_value) ? parsed_value : CourtLayout.Column;
			CourtLayout.CourtCount = int.TryParse(MatchCountTextBox.Text, out parsed_value) ? parsed_value : CourtLayout.CourtCount;

			SettingImporter exporter = new SettingImporter();
			exporter.Export("Setting.ini", CourtLayout);

			this.Close();
		}

		private void cancelButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
