using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Win32;
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

		/// <summary>
		/// ファイルからメンバー情報を読み込む
		/// </summary>
		private void ReadMemberFromFile()
		{
			OpenFileDialog dialog = new OpenFileDialog();

			bool? result = dialog.ShowDialog();

			if (result == true)
			{
				string memberDataFileName = dialog.FileName;

				FileIO.DefaultImporter importer = new FileIO.DefaultImporter();
				IList<Model.IPerson> allMembers = importer.Import(memberDataFileName);

				AllMembers = MemberListViewModel.CreateMemberListViewModel(allMembers);
			}
		}

		public MainViewModel()
		{
			InitializeCommand = new DelegateCommand(InitializeData);
			ReadMemberFromFileCommand = new DelegateCommand(ReadMemberFromFile);

			// 呼び出すメソッドをセット
			CreateMefContainer = CreateMefContainerBody;
		}

		private Func<CompositionContainer> CreateMefContainer { get; }
		/// <summary>
		/// 自身のアセンブリと既定のアセンブリを探索し, MEFコンテナを作る.
		/// </summary>
		/// <returns>作成されたMEFコンテナ</returns>
		private CompositionContainer CreateMefContainerBody()
		{
			AggregateCatalog mefCatalog = new AggregateCatalog();
			// 自身
			mefCatalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
			// 既定のアセンブリ
			// 今はなし

			return new CompositionContainer(mefCatalog);
		}
	}
}
