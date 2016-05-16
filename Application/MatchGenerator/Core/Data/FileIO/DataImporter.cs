using System;
using System.IO;
using System.Collections.Generic;

namespace MatchGenerator.Core
{
	class DataImporter
	{
		/// <summary>
		/// 全メンバー情報をファイルから読み込む
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		/// <exception cref="System.IO.FileNotFoundException"><paramref name="filePath"/>で指定したファイルが無い.</exception>
		/// <exception cref="System.UnauthorizedAccessException"><paramref name="filePath"/>でディレクトリが指定されたり,
		/// ファイルのアクセス許可が無かったり, もうなんかいろいろ.</exception>
		/// <exception cref="System.IO.FileFormatException">1行の項目数が多い, または少ない.</exception>
		/// <exception cref="System.FormatException">GScoreの形式が<see cref="System.Int32"/>で表せない形式.</exception>
		/// <exception cref="System.OverflowException">GScoreの値が<see cref="System.Int32"/>の上限, または下限を超えている.</exception>"
		public List<Person> importAllData(string filePath)
		{
			List<Person> all_data = new List<Person>();

			// いったん全行をファイルから読み込んでしまう
			string[] all_data_raw = File.ReadAllLines(filePath);

			// 読み込んだ文字列群を1行ずつ変換
			foreach(string data_raw in all_data_raw)
			{
				string[] elements = data_raw.Split(new char[] { ',' });

				Person person = new Person(elements);
				all_data.Add(person);
			}

			return all_data;
		}
	}
}
