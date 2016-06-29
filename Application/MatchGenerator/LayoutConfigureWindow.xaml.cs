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
		LayoutInformation NewCourtLayout;

		public LayoutConfigureWindow(LayoutInformation courtLayout)
		{
			InitializeComponent();

			CourtLayout = courtLayout;
			NewCourtLayout = new LayoutInformation();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			SettingImporter importer = new SettingImporter();
			NewCourtLayout = importer.Import("Setting.ini");

			courtCountRowTextBox.Text = NewCourtLayout.Row.ToString();
			courtCountColumnTextBox.Text = NewCourtLayout.Column.ToString();
			MatchCountTextBox.Text = NewCourtLayout.CourtCount.ToString();
		}

		private void okButton_Click(object sender, RoutedEventArgs e)
		{
			CourtLayout.Row = NewCourtLayout.Row;
			CourtLayout.Column = NewCourtLayout.Column;
			CourtLayout.CourtCount = NewCourtLayout.CourtCount;

			this.Close();
		}

		private void cancelButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
