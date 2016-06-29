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
using System.ComponentModel;
using MatchGenerator.Core;
using MatchGenerator.Core.UI;

namespace MatchGenerator
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		private ObservableCollection<MemberListViewItem> AllMemberViewData;
		private ObservableCollection<MemberListViewItem> EntrantViewData;

		private LayoutInformation CourtLayout;

		public MainWindow()
		{
			InitializeComponent();
		}
		
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			SettingImporter setting_importer = new SettingImporter();
			CourtLayout = setting_importer.Import("Setting.ini");

			InitializeMemberData();
			InitializeEntrantData();
		}


		private void buttonAttend_Click(object sender, RoutedEventArgs e)
		{
			MoveToEntrantList();
		}

		private void buttonAbsent_Click(object sender, RoutedEventArgs e)
		{
			RemoveFromEntrantList();
		}

		private void buttonStart_Click(object sender, RoutedEventArgs e)
		{
			List<Person> entrants = new List<Person>();
			foreach(MemberListViewItem item in EntrantViewData)
			{
				entrants.Add(item.Person);
			}

			MatchDecider decider = new MatchDecider();
			decider.NumberOfMatches = 4;
			List<MatchInformation> matches = decider.generateMatches(entrants);

			LayoutInformation layout = new LayoutInformation();
			layout.Row = 2;
			layout.Column = 3;
			layout.CourtCount = 4;

			LayoutWindow layout_window = new LayoutWindow(matches, layout);
			layout_window.Show();
		}

		/// <summary>
		/// 起動時にメンバー情報を読み込む.
		/// 読み込めんかったらなにもしない.
		/// </summary>
		private void InitializeMemberData()
		{
			string file_name = "MemberData.csv";
			DataImporter importer = new DataImporter();
			List<Person> personData;

			try
			{
				personData = importer.importAllData(file_name);
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

			AllMemberViewData = new ObservableCollection<MemberListViewItem>();
			foreach (Person p in personData)
			{
				AllMemberViewData.Add(new MemberListViewItem(p));
			}

			ICollectionView view = CollectionViewSource.GetDefaultView(AllMemberViewData);
			view.SortDescriptions.Add(
				new SortDescription(
					"Id",
					ListSortDirection.Ascending));
			allMembersListView.DataContext = view;

			mainStatusBar.Content = "Importing success (" + AllMemberViewData.Count.ToString() + " members)";
		}

		/// <summary>
		/// 参加者データの初期化
		/// 参加者リストを空っぽにする
		/// </summary>
		private void InitializeEntrantData()
		{
			EntrantViewData = new ObservableCollection<MemberListViewItem>();
			ICollectionView view = CollectionViewSource.GetDefaultView(EntrantViewData);
			view.SortDescriptions.Add(
				new SortDescription(
					"Id",
					ListSortDirection.Ascending));
			entrantsListView.DataContext = view;
		}

		/// <summary>
		/// チェックされているメンバーを参加者リストに移動.
		/// </summary>
		private void MoveToEntrantList()
		{
			foreach(MemberListViewItem item in AllMemberViewData)
			{
				if (item.IsChecked)
				{
					if (!EntrantViewData.Contains(item))
					{
						EntrantViewData.Add(item);
					}
				}
			}
		}

		/// <summary>
		/// チェックされているメンバーを参加者リストから除去.
		/// </summary>
		private void RemoveFromEntrantList()
		{
			List<MemberListViewItem> removeItems = new List<MemberListViewItem>();
			foreach(MemberListViewItem item in EntrantViewData)
			{
				if (item.IsChecked)
				{
					removeItems.Add(item);
				}
			}
			foreach(MemberListViewItem item in removeItems)
			{
				EntrantViewData.Remove(item);
			}
		}

		private void layoutConfigureMenuItem_Click(object sender, RoutedEventArgs e)
		{
			LayoutConfigureWindow windoow = new LayoutConfigureWindow(CourtLayout);
			windoow.Show();
		}
	}
}
