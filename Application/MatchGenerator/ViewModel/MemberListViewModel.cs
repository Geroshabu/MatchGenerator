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
	internal class MemberListViewModel : IMemberListViewModel
	{
		/// <summary>
		/// リストに表示するすべてのメンバー
		/// </summary>
		public IList<IMemberListItemViewModel> Members
		{
			get
			{
				throw new NotImplementedException();
			}

			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// 現在選択されているメンバー
		/// </summary>
		public IList<IMemberListItemViewModel> SelectedMembers
		{
			get
			{
				throw new NotImplementedException();
			}

			set
			{
				throw new NotImplementedException();
			}
		}
	}
}
