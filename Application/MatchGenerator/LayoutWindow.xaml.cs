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

		public LayoutWindow(List<MatchInformation> matches)
		{
			InitializeComponent();

			Matches = matches;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			CourtView court1 = new CourtView(Matches[0]);
			Viewbox viewBox1 = new Viewbox();
			layoutGrid.Children.Add(viewBox1);
			viewBox1.Child = court1;
		}
	}
}
