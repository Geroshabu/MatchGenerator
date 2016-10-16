namespace MatchGenerator.ViewModel
{
	/// <summary>
	/// ViewModelとして同一であるかを判定できることを表す.
	/// </summary>
	public interface IViewModelEquitable
	{
		/// <summary>
		/// このViewModelと与えられたViewModelが等しいかどうかを判定する.
		/// 同じ型のViewModelであること, かつViewModelが表現している
		/// Modelが同じインスタンスであるとき, 等しいと判定する.
		/// </summary>
		/// <param name="other">このViewModelと比較するViewModel</param>
		/// <returns>このViewModelと与えられたViewModelが等しいときtrue, 等しくないときfalse</returns>
		bool EqualsModel(object other);
	}
}
