using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using Microsoft.Win32;
using Microsoft.Practices.Prism.Commands;
using MatchGenerator.Model;

namespace MatchGenerator.ViewModel
{
	/// <summary>
	/// メイン画面を表示するときのViewModel
	/// </summary>
	internal class MainViewModel : Microsoft.Practices.Prism.Mvvm.BindableBase, IMainViewModel
	{
		/// <summary>
		/// このViewModelが表現するModel
		/// </summary>
		private ObservableCollection<IPerson> model = new ObservableCollection<IPerson>();

		private CompositionContainer mefContainers;

		private Type DefaultMemberImporterType { get; } = typeof(FileIO.DefaultImporter);

		[ImportMany]
		private IEnumerable<FileIO.IMemberImporter> memberImporters { get; set; }

		private IMemberListViewModel allMembersField;
		/// <summary>
		/// 全てのメンバーを表示するリスト
		/// </summary>
		public IMemberListViewModel AllMembers
		{
			get
			{
				return new MemberListViewModel(model);
			}

			set
			{
				SetProperty(ref allMembersField, value);
			}
		}

		private IMemberListViewModel AttendanceMembersField;
		/// <summary>
		/// 出席するメンバーを表示するリスト
		/// </summary>
		public IMemberListViewModel AttendanceMembers
		{
			get
			{
				return AttendanceMembersField;
			}
			set
			{
				SetProperty(ref AttendanceMembersField, value);
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
			// MEFによる収集
			mefContainers = CreateMefContainer();
			mefContainers.ComposeParts(this);

			string memberDataFileName = "MemberData.csv";
			FileIO.IMemberImporter defaultImporter = memberImporters.Single(importer => importer.GetType().Equals(DefaultMemberImporterType));
			IList<Model.IPerson> allMembers = defaultImporter.Import(memberDataFileName);

			IList<Model.IPerson> attendanceMembers = new List<Model.IPerson>();

			AllMembers = MemberListViewModel.CreateMemberListViewModel(allMembers);
			AttendanceMembers = MemberListViewModel.CreateMemberListViewModel(attendanceMembers);
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

				FileIO.IMemberImporter importer = memberImporters.Single(i => i.GetType().Equals(DefaultMemberImporterType));
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
