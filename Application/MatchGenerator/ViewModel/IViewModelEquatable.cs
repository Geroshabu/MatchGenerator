using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchGenerator.ViewModel
{
	/// <summary>
	/// ViewModelとして同一であるかを判定できることを表す.
	/// </summary>
	public interface IViewModelEquatable
	{
		/// <summary>
		/// このViewModelが表現しているModelが,
		/// 与えられたViewModelが表現しているModelと
		/// 等しいかどうかを判定する.
		/// </summary>
		/// <param name="other">このViewModelと比較するViewModel</param>
		/// <returns>このViewModelが表現しているModelが,
		/// 与えられたViewModelが表現しているModelと
		/// 等しいかどうか.
		/// 等しければtrue, 等しくなければfalse</returns>
		bool EqualsModel(IViewModelEquatable other);
	}
}
