using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using MatchGenerator.Model;
using MatchGenerator.Core;

namespace MatchGenerator.FileIO
{
	/// <summary>
	/// メンバー情報を読み込む標準のインポーター
	/// </summary>
	[Export(typeof(IMemberImporter))]
	internal class DefaultImporter : IMemberImporter
	{
		/// <summary>
		/// メンバー情報を指定したファイルより読み込む
		/// </summary>
		/// <param name="FileName">読み込むファイルの, パス(絶対パスまたは相対パス)および拡張子を含むファイル名</param>
		/// <returns>読み込んだメンバー情報</returns>
		/// <exception cref="ArgumentNullException"><paramref name="FileName"/>が長さ 0 の文字列であるか, 空白しか含んでいないか, <see cref="Path.InvalidPathChars"/>で定義されている無効な文字を 1 つ以上含んでいる.</exception>
		/// <exception cref="ArgumentException"><paramref name="FileName"/>がnull.</exception>
		/// <exception cref="PathTooLongException">指定したパス, ファイル名, またはその両方がシステム定義の最大長を超えている. たとえば, Windowsベースのプラットフォームの場合, パスの長さは 248 文字未満, ファイル名の長さは 260 文字未満である必要がある.</exception>
		/// <exception cref="DirectoryNotFoundException">指定したパスが無効. (割り当てられていないドライブであるなど)</exception>
		/// <exception cref="FileNotFoundException">指定したファイルが見つからない.</exception>
		/// <exception cref="IOException">ファイルを開くときに I/O エラーが発生した.</exception>
		/// <exception cref="NotSupportedException"><paramref name="FileName"/>の形式が無効.</exception>
		/// <exception cref="System.Security.SecurityException">呼び出し元に必要なアクセス許可がない.</exception>
		/// <exception cref="UnauthorizedAccessException">読み取り専用のファイルが指定された.
		/// または, この操作は現在のプラットフォームではサポートされていない.
		/// または, <paramref name="FileName"/>によってディレクトリが指定された.
		/// または, 呼び出し元に必要なアクセス許可がない.</exception>
		/// <exception cref="FileFormatException">ファイルの形式が不正.</exception>
		public IList<IPerson> Import(string FileName)
		{
			IList<IPerson> all_data = new List<IPerson>();

			// いったん全行をファイルから読み込んでしまう
			string[] all_data_raw = DoReadAllLines(FileName);

			foreach (string data_raw in all_data_raw)
			{
				string[] elements = data_raw.Split(',');

				if (elements.Length != Person.PropertyCount)
				{
					throw new FileFormatException();
				}

				string name = elements[0];
				string description = elements[4];

				all_data.Add(new Person { Name = name, Description = description });
			}

			return all_data;
		}

		#region テストのために分離した処理

		private Func<string, string[]> DoReadAllLines = path => File.ReadAllLines(path);

		#endregion
	}
}
