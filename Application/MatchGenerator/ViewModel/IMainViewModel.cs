using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchGenerator.ViewModel
{
	/// <summary>
	/// メイン画面を表示するときのViewModelのインタフェース
	/// </summary>
	public interface IMainViewModel
	{
		/// <summary>
		/// 全てのメンバーを表示するリスト
		/// </summary>
		IMemberListViewModel AllMembers { get; set; }
	}
}