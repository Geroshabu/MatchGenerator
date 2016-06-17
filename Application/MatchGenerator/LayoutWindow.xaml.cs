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
using MatchGenerator.Core.UI;

namespace MatchGenerator
{
	/// <summary>
	/// LayoutWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class LayoutWindow : Window
	{
		private List<MatchInformation> Matches;
		private LayoutInformation Layout;

		public LayoutWindow(List<MatchInformation> matches, LayoutInformation layout)
		{
			InitializeComponent();

			Matches = matches;
			Layout = layout;

			if (matches.Count != Layout.CourtCount)
			{
				throw new ArgumentOutOfRangeException("matches", "試合の数と, レイアウトのコート数が一致しませんです. ハイ.");
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			double court_width_with_margin = ActualWidth / Layout.Column;
			double court_height_with_margin = ActualHeight / Layout.Row;
			double margin_width = 10;
			double margin_height = 10;
			double court_width = court_width_with_margin - 2 * margin_width;
			double court_height = court_height_with_margin - 2 * margin_height;

			for (int r = 0; r < Layout.Row; r++)
			{
				for (int c = 0; c < Layout.Column; c++)
				{
					CourtView court = new CourtView(Matches[r * Layout.Column + c]);
					Viewbox viewBox = new Viewbox();
					viewBox.Child = court;

					viewBox.Margin = new Thickness(
						c * court_width_with_margin + margin_width,
						r * court_height_with_margin + margin_height,
						(Layout.Column - c - 1) * court_width_with_margin + margin_width,
						(Layout.Row - r - 1) * court_height_with_margin + margin_height);
					layoutGrid.Children.Add(viewBox);
				}
			}
		}
	}
}
