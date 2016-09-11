using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
	}
}
