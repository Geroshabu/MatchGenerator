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

		private List<Viewbox> ViewBoxes;

		public LayoutWindow(List<MatchInformation> matches, LayoutInformation layout)
		{
			InitializeComponent();

			if (matches.Count > layout.CourtCount)
			{
				throw new ArgumentOutOfRangeException("matches", "コート数より試合数のほうが多いです.");
			}

			Matches = matches;
			Layout = layout;
			ViewBoxes = new List<Viewbox>();
			for (int i = 0; i < Layout.Row * Layout.Column; i++)
			{
				ViewBoxes.Add(new Viewbox());
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			for (int r = 0; r < Layout.Row; r++)
			{
				for (int c = 0; c < Layout.Column; c++)
				{
					int index = r * Layout.Column + c;
					CourtView court = new CourtView(index < Layout.CourtCount ? Matches[index] : null);
					Viewbox viewBox = ViewBoxes[index];
					viewBox.Child = court;

					layoutGrid.Children.Add(viewBox);
				}
			}
		}

		private void layoutCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			double court_width_with_margin = e.NewSize.Width / Layout.Column;
			double court_height_with_margin = e.NewSize.Height / Layout.Row;
			double margin_width = 10;
			double margin_height = 10;

			for (int r = 0; r < Layout.Row; r++)
			{
				for (int c = 0; c < Layout.Column; c++)
				{
					Viewbox viewBox = ViewBoxes[r * Layout.Column + c];
					viewBox.Margin = new Thickness(
						c * court_width_with_margin + margin_width,
						r * court_height_with_margin + margin_height,
						(Layout.Column - c - 1) * court_width_with_margin + margin_width,
						(Layout.Row - r - 1) * court_height_with_margin + margin_height);
				}
			}
		}
	}
}
