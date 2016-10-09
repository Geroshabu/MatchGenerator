using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public MainViewModel()
		{
			AllMembers = new MemberListViewModel();
		}
	}
}
