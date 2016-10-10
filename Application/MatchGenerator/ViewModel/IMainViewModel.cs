using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

		/// <summary>
		/// ファイルからメンバー情報を読み込むコマンドを取得する
		/// </summary>
		ICommand ReadMemberFromFileCommand { get; }
	}
}
