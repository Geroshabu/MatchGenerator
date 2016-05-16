using System;
using System.Collections.Generic;
using System.IO;
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
using System.Collections.ObjectModel;
using MatchGenerator.Core;

namespace MatchGenerator
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		private ObservableCollection<Person> AllMemberData;

		public MainWindow()
		{
			InitializeComponent();
		}
		
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			InitializeMemberData();
		}

		/// <summary>
		/// 起動時にメンバー情報を読み込む.
		/// 読み込めんかったらなにもしない.
		/// </summary>
		private void InitializeMemberData()
		{
			string file_name = "MemberData.csv";
			DataImporter importer = new DataImporter();

			try
			{
				AllMemberData = new ObservableCollection<Person>(importer.importAllData(file_name));
			}
			#region 例外処理 (returnあり)
			catch (FileNotFoundException)
			{
				mainStatusBar.Content =
					"ファイル自動読み込みに失敗した. MemberData.csvが無かったよ(´・ω・`)";
				return;
			}
			catch (UnauthorizedAccessException)
			{
				mainStatusBar.Content = "ファイル自動読み込みに失敗した. MemberData.csvにアクセスできねぇ(´・ω・`)";
				return;
			}
			catch (FileFormatException)
			{
				mainStatusBar.Content = "ファイル自動読み込みに失敗した. MemberData.csvの行中の項目の数とかおかしいかも？";
				return;
			}
			catch (FormatException)
			{
				mainStatusBar.Content = "ファイル自動読み込みに失敗した. MemberData.csvのGScoreの形式とかおかしいかも？";
				return;
			}
			catch (OverflowException)
			{
				mainStatusBar.Content = "ファイル自動読み込みに失敗した. MemberData.csvのGScoreが大きすぎたり小さすぎたりするかも？";
				return;
			}
			#endregion
			
			allMembersListView.DataContext = AllMemberData;

			mainStatusBar.Content = "Importing success (" + AllMemberData.Count.ToString() + " members)";
		}
	}
}
