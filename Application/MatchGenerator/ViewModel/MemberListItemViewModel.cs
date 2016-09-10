using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchGenerator.Core;

namespace MatchGenerator.ViewModel
{
	/// <summary>
	/// <see cref="System.Windows.Controls.ListView"/>で表示するときの, メンバーのViewModel
	/// </summary>
	class MemberListItemViewModel : Microsoft.Practices.Prism.Mvvm.BindableBase
	{
		/// <summary>
		/// このViewModelで扱うModel
		/// </summary>
		private Person Model;
	}
}
