using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchGenerator.Model;

namespace MatchGenerator.FileIO
{
	/// <summary>
	/// メンバー情報をファイルより読み込むインポーターを表す
	/// </summary>
	public interface IMemberImporter
	{
		/// <summary>
		/// メンバー情報を指定したファイルより読み込む
		/// </summary>
		/// <param name="FileName">読み込むファイルの, パス(絶対パスまたは相対パス)および拡張子を含むファイル名</param>
		/// <returns>読み込んだメンバー情報</returns>
		IList<IPerson> Import(string FileName);
	}
}
