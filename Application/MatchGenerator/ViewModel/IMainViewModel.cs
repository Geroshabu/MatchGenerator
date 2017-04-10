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
		/// 出席するメンバーを表示するリスト
		/// </summary>
		IMemberListViewModel AttendanceMembers { get; set; }

		/// <summary>
		/// メイン画面に表示するデータを読み込むコマンド
		/// </summary>
		ICommand InitializeCommand { get; }

		/// <summary>
		/// ファイルからメンバー情報を読み込むコマンドを取得する
		/// </summary>
		ICommand ReadMemberFromFileCommand { get; }

        /// <summary>
        /// メンバーを参加リストに移動させるコマンドを取得する
        /// </summary>
        ICommand AttendCommand { get; }
	}
}
