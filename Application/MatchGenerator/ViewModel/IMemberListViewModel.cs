using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchGenerator.Model;

namespace MatchGenerator.ViewModel
{
	/// <summary>
	/// メンバーのリストのViewModelのインタフェース
	/// </summary>
	public interface IMemberListViewModel : IViewModel<IList<IPerson>>, INotifyCollectionChanged, IEnumerable<IMemberListItemViewModel>
	{
		/// <summary>
		/// リストに表示するすべてのメンバー
		/// </summary>
		IList<IMemberListItemViewModel> Members { get; set; }

		/// <summary>
		/// 現在選択されているメンバー
		/// </summary>
		IList<IMemberListItemViewModel> SelectedMembers { get; set; }
	}
}
