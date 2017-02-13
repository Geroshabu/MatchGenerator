using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MatchGenerator.Model;

namespace MatchGenerator.ViewModel
{
	/// <summary>
	/// <see cref="System.Windows.Controls.ListView"/>で表示するときの, メンバーのViewModelのインタフェース
	/// </summary>
	public interface IMemberListItemViewModel : IViewModel<IPerson>, IViewModelEquitable
	{
		/// <summary>
		/// "名前"欄に表示する文字列を取得
		/// </summary>
		string Name { get; }

		/// <summary>
		/// "コメント"欄に表示する文字列を取得
		/// </summary>
		string Description { get; }

		/// <summary>
		/// この項目がチェックされているかどうかを取得・設定
		/// </summary>
		bool IsChecked { get; set; }

		/// <summary>
		/// このメンバーが選択されているかどうかを示すコントロールが,
		/// クリックされたときに発生する.
		/// </summary>
		/// <remarks>イベント発生元は自インスタンスである.</remarks>
		event EventHandler<MemberClickEventArgs> MemberClick;

		/// <summary>
		/// このメンバーをクリックしたときに, Viewから実行されるコマンド.
		/// </summary>
		/// <remarks>
		/// <see cref="MemberClick"/>を発生させる.
		/// クリック時, Viewとバインディングしている<see cref="IsChecked"/>の
		/// 値は自動で反転されるため, イベントデータには<see cref="IsChecked"/>の
		/// 値が, クリック後のメンバーの選択状態としてセットされる.
		/// </remarks>
		ICommand MemberClickCommand { get; }

		/// <summary>
		/// このメンバーが選択されているかどうかを示すコントロールが,
		/// 連続選択クリックされたときに発生する.
		/// </summary>
		event EventHandler<MemberClickEventArgs> MemberExtendedClick;

		/// <summary>
		/// このメンバーを連続選択クリックしたときの処理をするコマンド
		/// </summary>
		ICommand MemberExtendedClickCommand { get; }
	}
}
