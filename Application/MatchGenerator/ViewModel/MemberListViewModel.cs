using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchGenerator.ViewModel
{
	/// <summary>
	/// メンバーのリストのViewModel
	/// </summary>
	internal class MemberListViewModel : Microsoft.Practices.Prism.Mvvm.BindableBase, IMemberListViewModel
	{
		private IList<IMemberListItemViewModel> MembersField;
		/// <summary>
		/// リストに表示するすべてのメンバー
		/// </summary>
		public IList<IMemberListItemViewModel> Members
		{
			get
			{
				return MembersField;
			}

			set
			{
				SetProperty(ref MembersField, value);
			}
		}

		private IList<IMemberListItemViewModel> SelectedMembersField;
		/// <summary>
		/// 現在選択されているメンバー
		/// </summary>
		public IList<IMemberListItemViewModel> SelectedMembers
		{
			get
			{
				return SelectedMembersField;
			}

			set
			{
				SetProperty(ref SelectedMembersField, value);
			}
		}
	}
}
