using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchGenerator.ViewModel
{
	/// <summary>
	/// <see cref="System.Windows.Controls.ListView"/>で表示するときの, メンバーのViewModelのインタフェース
	/// </summary>
	public interface IMemberListItemViewModel
	{
		/// <summary>
		/// "名前"欄に表示する文字列を取得
		/// </summary>
		string Name { get; }

		/// <summary>
		/// "コメント"欄に表示する文字列を取得
		/// </summary>
		string Description { get; }
	}
}
