using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchGenerator.Model
{
	/// <summary>
	/// メンバーの情報を保持するModelのインタフェース
	/// </summary>
	public interface IPerson
	{
		/// <summary>
		/// このメンバーのお名前
		/// </summary>
		string Name { get; }

		/// <summary>
		/// このメンバーのコメントみたいなやつ. 自由記述のやつ.
		/// </summary>
		string Description { get; }
	}
}
