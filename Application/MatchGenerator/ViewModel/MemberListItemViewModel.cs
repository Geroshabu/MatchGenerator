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
	class MemberListItemViewModel : Microsoft.Practices.Prism.Mvvm.BindableBase, IMemberListItemViewModel, ICloneable
	{
		/// <summary>
		/// このViewModelで扱うModel
		/// </summary>
		private IPerson Model;

		/// <summary>
		/// <see cref="MemberListItemViewModel"/>の新しいインスタンスを生成する.
		/// </summary>
		/// <param name="model">ViewModelの表示対象となるModel</param>
		private MemberListItemViewModel(IPerson model)
		{
			Model = model;
			MemberClickCommand = new DelegateCommand(ClickMember);
			MemberExtendedClickCommand = new DelegateCommand(ExtendClickMember);
		}

		/// <summary>
		/// Copy Constructor
		/// </summary>
		/// <remarks>値型と, <see cref="Model"/>フィールドは引き継がれる.</remarks>
		private MemberListItemViewModel(MemberListItemViewModel other)
		{
			this.Model = other.Model;
			this.IsCheckedField = other.IsCheckedField;
			this.MemberClickCommand = new DelegateCommand(ClickMember);
			this.MemberExtendedClickCommand = new DelegateCommand(ExtendClickMember);
		}

		/// <summary>
		/// <see cref="MemberListItemViewModel"/>の新しいインスタンスを作成する.
		/// </summary>
		public static Func<IPerson, IMemberListItemViewModel> CreateMemberListItemViewModel { get; } =
			person => new MemberListItemViewModel(person);

		/// <summary>
		/// 指定されたインスタンスをコピーし, <see cref="MemberListItemViewModel"/>の新しいインスタンスを作成する.
		/// </summary>
		public static Func<MemberListItemViewModel, IMemberListItemViewModel> CopyMemberListItemViewModel { get; } =
			other => new MemberListItemViewModel(other);

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

		/// <summary>
		/// このメンバーを連続選択クリックしたときの処理をするコマンド
		/// </summary>
		public ICommand MemberExtendedClickCommand { get; }
		/// <summary>
		/// <see cref="MemberExtendedClickCommand"/>の<see cref="ICommand.Execute"/>の処理
		/// </summary>
		private void ExtendClickMember()
		{
			this.IsChecked = !this.IsChecked; // MouseBindingなのでチェック状態は自動で変わらない

			MemberClickEventArgs e = new MemberClickEventArgs();
			e.IsChecked = this.IsChecked;
			MemberExtendedClick?.Invoke(this, e);
		}

		/// <summary>
		/// 現在のインスタンスのコピーである新しいオブジェクトを作成する.
		/// <returns>このインスタンスのコピーである新しいオブジェクト.</returns>
		public object Clone()
		{
			return MemberListItemViewModel.CopyMemberListItemViewModel(this);
		}

		/// <summary>
		/// このViewModelと与えられたViewModelが等しいかどうかを判定する.
		/// 同じ型のViewModelであること, かつViewModelが表現している
		/// Modelが同じインスタンスであるとき, 等しいと判定する.
		/// </summary>
		/// <param name="other">このViewModelと比較するViewModel</param>
		/// <returns>このViewModelと与えられたViewModelが等しいときtrue, 等しくないときfalse</returns>
		public bool EqualsModel(object other)
		{
			if (other is MemberListItemViewModel)
			{
				return (other as MemberListItemViewModel).Model == this.Model;
			}

			return false;
		}
	}
}
