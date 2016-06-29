using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace MatchGenerator.Core
{
	public class SettingImporter
	{
		/// <summary>
		/// コートの配置などの設定情報を読み込む.
		/// 設定情報が見つからなかったらデフォルトの設定となる.
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		/// <exception cref="FileFormatException">設定ファイルの形式がおかしい.</exception>
		/// <exception cref="IOException">ファイルを開くときにエラー発生. なんかよく分からない.</exception>
		/// <exception cref="System.Security.SecurityException">プログラムにファイルアクセス許可がない.</exception>
		/// <exception cref="UnauthorizedAccessException">ファイルが読み込み専用やったり, ディレクトリやったり, いろいろ.</exception>
		public LayoutInformation Import(string filePath)
		{
			Dictionary<string, string> setting_str = ReadAsDictionary(filePath);

			LayoutInformation layout_info = new LayoutInformation();
			layout_info.Row = setting_str.ContainsKey("Row") ? int.Parse(setting_str["Row"]) : 1;
			layout_info.Column = setting_str.ContainsKey("Column") ? int.Parse(setting_str["Column"]) : 3;
			layout_info.CourtCount = setting_str.ContainsKey("CourtCount") ? int.Parse(setting_str["CourtCount"]) : 3;

			return layout_info;
		}

		/// <summary>
		/// ファイルから設定を読み込んで, Dictionaryで返す.
		/// </summary>
		/// <remarks>
		/// <paramref name="filePath"/>がnullだったり, ファイルが見つからなかったら, 空のDictionaryを返す.
		/// ファイルが見つかったけど何かしらのエラーが出たときは例外発生.
		/// </remarks>
		/// <param name="filename">読み込むファイル名</param>
		/// <returns>読み込んだDictionary. 読み込めんかったら空のDictionary.</returns>
		/// <exception cref="FileFormatException">設定ファイルの形式がおかしい.</exception>
		/// <exception cref="IOException">ファイルを開くときにエラー発生. なんかよく分からない.</exception>
		/// <exception cref="System.Security.SecurityException">プログラムにファイルアクセス許可がない.</exception>
		/// <exception cref="UnauthorizedAccessException">ファイルが読み込み専用やったり, ディレクトリやったり, いろいろ.</exception>
		private Dictionary<string, string> ReadAsDictionary(string filePath)
		{
			IEnumerable<string> lines;
			try
			{
				lines = File.ReadLines(filePath);
			}
			#region 指定されたfilePathによって, ファイルが見つからなかった場合は, 空のDictionaryを返す.
			catch (ArgumentNullException)
			{
				return new Dictionary<string, string>();
			}
			catch (DirectoryNotFoundException)
			{
				return new Dictionary<string, string>();
			}
			catch (FileNotFoundException)
			{
				return new Dictionary<string, string>();
			}
			catch (PathTooLongException)
			{
				return new Dictionary<string, string>();
			}
			catch (ArgumentException)
			{
				return new Dictionary<string, string>();
			}
			#endregion

			Dictionary<string, string> setting_str = new Dictionary<string, string>();
			foreach (string line in lines)
			{
				string[] line_elements = line.Split(':');
				if (line_elements.Length != 2)
				{
					throw new FileFormatException("設定ファイルの項目と値の読み込みに失敗しました.");
				}

				try
				{
					setting_str.Add(line_elements[0], line_elements[1]);
				}
				catch (ArgumentNullException)
				{
					throw;
				}
				catch (ArgumentException)
				{
					// キーが重複してる...
					// 先に登録されているものを採用する. というわけで何もしない.
				}
			}

			return setting_str;
		}
	}
}
