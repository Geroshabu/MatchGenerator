using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace MatchGenerator.ViewModel
{
	/// <summary>
	/// メイン画面を表示するときのViewModel
	/// </summary>
	internal class MainViewModel : Microsoft.Practices.Prism.Mvvm.BindableBase, IMainViewModel
	{
		private IMemberListViewModel AllMembersField;
		/// <summary>
		/// 全てのメンバーを表示するリスト
		/// </summary>
		public IMemberListViewModel AllMembers
		{
			get
			{
				return AllMembersField;
			}

			set
			{
				SetProperty(ref AllMembersField, value);
			}
		}

		/// <summary>
		/// メイン画面に表示するデータを読み込むコマンド
		/// </summary>
		public ICommand InitializeCommand { get; }

		/// <summary>
		/// メイン画面に表示するデータを読み込む
		/// </summary>
		private void InitializeData()
		{
			string memberDataFileName = "MemberData.csv";
			FileIO.DefaultImporter importer = new FileIO.DefaultImporter();
			IList<Model.IPerson> allMembers = importer.Import(memberDataFileName);

			AllMembers = MemberListViewModel.CreateMemberListViewModel(allMembers);
		}

		/// <summary>
		/// ファイルからメンバー情報を読み込むコマンドを取得する
		/// </summary>
		public ICommand ReadMemberFromFileCommand { get; }

		public MainViewModel()
		{
			InitializeCommand = new DelegateCommand(InitializeData);
		}
	}
}
