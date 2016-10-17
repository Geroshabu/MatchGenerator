namespace MatchGenerator.ViewModel
{
	/// <summary>
	/// ViewModelの基本となるインタフェース
	/// </summary>
	/// <typeparam name="T">このViewModelが表現するModelの型</typeparam>
	public interface IViewModel<T>
	{
		/// <summary>
		/// このViewModelが表現するModelを取得する.
		/// </summary>
		T Model { get; }
	}
}
