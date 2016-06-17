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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatchGenerator.Core.UI
{
	/// <summary>
	/// CourtView.xaml の相互作用ロジック
	/// </summary>
	public partial class CourtView : UserControl
	{
		private Rectangle Court1FieldRect;
		private Rectangle Court2FieldRect;

		public CourtView(MatchInformation matchInfomation)
		{
			InitializeComponent();

			Court1FieldRect = new Rectangle();
			Court1FieldRect.Stroke = Brushes.Black;
			Court1FieldRect.StrokeThickness = 1;
			Court1FieldRect.VerticalAlignment = VerticalAlignment.Top;
			Court1LayoutGrid.Children.Add(Court1FieldRect);

			Court2FieldRect = new Rectangle();
			Court2FieldRect.Stroke = Brushes.Black;
			Court2FieldRect.StrokeThickness = 1;
			Court2FieldRect.VerticalAlignment = VerticalAlignment.Bottom;
			Court2LayoutGrid.Children.Add(Court2FieldRect);

			// Labels
			if (matchInfomation == null)
			{
				Court1Player1NameLabel.Content = "";
				Court1Player1DescriptionLabel.Content = "";
				Court1Player2NameLabel.Content = "";
				Court1Player2DescriptionLabel.Content = "";
				Court2Player1NameLabel.Content = "";
				Court2Player1DescriptionLabel.Content = "";
				Court2Player2NameLabel.Content = "";
				Court2Player2DescriptionLabel.Content = "";
			}
			else
			{
				Court1Player1NameLabel.Content = matchInfomation.Team1.player1.Name;
				Court1Player1DescriptionLabel.Content = matchInfomation.Team1.player1.Description;
				Court1Player2NameLabel.Content = matchInfomation.Team1.player2.Name;
				Court1Player2DescriptionLabel.Content = matchInfomation.Team1.player2.Description;
				Court2Player1NameLabel.Content = matchInfomation.Team2.player1.Name;
				Court2Player1DescriptionLabel.Content = matchInfomation.Team2.player1.Description;
				Court2Player2NameLabel.Content = matchInfomation.Team2.player2.Name;
				Court2Player2DescriptionLabel.Content = matchInfomation.Team2.player2.Description;
			}
		}

		private void CourtLayoutGrid_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			Court1LayoutGrid.Height = e.NewSize.Height / 2;
			Court2LayoutGrid.Height = e.NewSize.Height / 2;
		}

		private void Court1LayoutGrid_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			Court1Player1LayoutGrid.Height = e.NewSize.Height / 2;
			Court1Player2LayoutGrid.Height = e.NewSize.Height / 2;
			Court1FieldRect.Height = e.NewSize.Height;
		}

		private void Court2LayoutGrid_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			Court2Player1LayoutGrid.Height = e.NewSize.Height / 2;
			Court2Player2LayoutGrid.Height = e.NewSize.Height / 2;
			Court2FieldRect.Height = e.NewSize.Height;
		}
	}
}
