using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using MatchGenerator.Core;
using MatchGenerator.Model;

namespace MatchGenerator.ViewModel
{
	/// <summary>
	/// <see cref="System.Windows.Controls.ListView"/>で表示するときの, メンバーのViewModel
	/// </summary>
	class MemberListItemViewModel : Microsoft.Practices.Prism.Mvvm.BindableBase, IMemberListItemViewModel
	{
		/// <summary>
		/// このViewModelで扱うModel
		/// </summary>
		private IPerson Model;

		/// <summary>
		/// <see cref="MemberListItemViewModel"/>の新しいインスタンスを生成する.
		/// </summary>
		/// <param name="model">ViewModelの表示対象となるModel</param>
		public MemberListItemViewModel(IPerson model)
		{
			Model = model;
			MemberClickCommand = new DelegateCommand(ClickMember);
		}

		/// <summary>
		/// "名前"欄に表示する文字列を取得
		/// </summary>
		public string Name
		{
			get { return Model.Name; }
		}

		/// <summary>
		/// "コメント"欄に表示する文字列を取得
		/// </summary>
		public string Description
		{
			get { return Model.Description; }
		}

		private bool IsCheckedField;
		/// <summary>
		/// この項目がチェックされているかどうかを取得・設定
		/// </summary>
		public bool IsChecked
		{
			get
			{
				return IsCheckedField;
			}

			set
			{
				SetProperty(ref IsCheckedField, value);
			}
		}

		/// <summary>
		/// このメンバーが選択されているかどうかを示すコントロールが,
		/// クリックされたときに発生する.
		/// </summary>
		public event EventHandler<MemberClickEventArgs> MemberClick;

		/// <summary>
		/// このメンバーをクリックしたときの処理をするコマンド
		/// </summary>
		public ICommand MemberClickCommand { get; }
		/// <summary>
		/// <see cref="MemberClickCommand"/>の<see cref="ICommand.Execute"/>の処理
		/// </summary>
		private void ClickMember()
		{
			MemberClickEventArgs e = new MemberClickEventArgs();
			e.IsChecked = this.IsChecked;
			MemberClick?.Invoke(this, e);
		}

		/// <summary>
		/// このメンバーが選択されているかどうかを示すコントロールが,
		/// 連続選択クリックされたときに発生する.
		/// </summary>
		public event EventHandler<MemberClickEventArgs> MemberExtendedClick;
	}
}
