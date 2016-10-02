using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchGenerator.Model;

namespace MatchGenerator.FileIO
{
	/// <summary>
	/// メンバー情報を読み込む標準のインポーター
	/// </summary>
	internal class DefaultImporter : IMemberImporter
	{
		/// <summary>
		/// メンバー情報を指定したファイルより読み込む
		/// </summary>
		/// <param name="FileName">読み込むファイルの, パス(絶対パスまたは相対パス)および拡張子を含むファイル名</param>
		/// <returns>読み込んだメンバー情報</returns>
		public IList<IPerson> Import(string FileName)
		{
			IList<IPerson> all_data = new List<IPerson>();

			return all_data;
		}
	}
}
