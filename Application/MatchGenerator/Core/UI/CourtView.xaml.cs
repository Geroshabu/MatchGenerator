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

		public CourtView()
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
