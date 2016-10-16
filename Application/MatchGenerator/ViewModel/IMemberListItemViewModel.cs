using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MatchGenerator.ViewModel
{
	/// <summary>
	/// <see cref="System.Windows.Controls.ListView"/>で表示するときの, メンバーのViewModelのインタフェース
	/// </summary>
	public interface IMemberListItemViewModel : IViewModelEquitable
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
		event EventHandler<MemberClickEventArgs> MemberClick;

		/// <summary>
		/// このメンバーをクリックしたときの処理をするコマンド
		/// </summary>
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
