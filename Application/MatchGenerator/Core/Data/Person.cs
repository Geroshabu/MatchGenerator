using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchGenerator.Core
{
	public class Person
	{
		/// <summary>このパーソンの情報の数</summary>
		/// <remarks>
		/// ファイルから読み込むときに,
		/// 全データをちゃんと読み込めたかどうかの判定に使われる.
		/// </remarks>
		public static int PropertyCount { get; } = 4;   // Need C# 6.0 or newer

		/// <summary>
		/// 名前
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// グループ名
		/// </summary>
		public string Group { get; set; }

		/// <summary>
		/// Geroshabuスコア
		/// </summary>
		public int GScore { get; set; }

		/// <summary>
		/// 自由記述のコメント
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// パーソンの作成. 初期値は言語仕様による規定値.
		/// </summary>
		public Person() { }

		/// <summary>
		/// 指定した初期値を持ったパーソンの作成.
		/// </summary>
		/// <param name="info">各情報の初期値. 添字と各情報は, 次のとおり一致させる必要がる.
		/// 0:Name, 1:Group, 2:GScore 3:Description</param>
		/// <exception cref="System.IO.FileFormatException">与えられた初期値の数が多い, または少ない.</exception>
		/// <exception cref="System.FormatException"><see cref="GScore"/>の初期値が<see cref="System.Int32"/>で表せない形式.</exception>
		/// <exception cref="System.OverflowException"><see cref="GScore"/>の初期値が<see cref="System.Int32"/>の上限, または下限を超えている.</exception>"
		public Person(string[] info)
		{
			if (info.Length != PropertyCount)
			{
				throw new System.IO.FileFormatException();
			}

			Name = info[0];
			Group = info[1];
			GScore = int.Parse(info[2]);
			Description = info[3];
		}
	}
}
