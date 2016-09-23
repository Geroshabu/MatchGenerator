using System;

namespace MatchGenerator.ViewModel
{
	/// <summary>
	/// リスト中のメンバーがクリックされた時のイベントデータを格納する.
	/// </summary>
	class MemberClickEventArgs : EventArgs
	{
		/// <summary>
		/// クリック後のメンバーの選択状態
		/// </summary>
		public bool IsChecked;
	}
}
