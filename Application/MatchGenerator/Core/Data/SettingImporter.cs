using System;
using System.IO;
using System.Linq;
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

		/// <summary>
		/// コートの配置などの設定情報を読み込む.
		/// 設定ファイルが既にあったら上書き.
		/// </summary>
		/// <param name="filePath">ファイルパス</param>
		/// <param name="layout_info">保存するコート配置設定</param>
		/// <exception cref="ArgumentNullException">引数<see cref="filePath"/>または<see cref="layout_info"/>がnull</exception>
		/// <exception cref="ArgumentException"><see cref="filePath"/>が空文字やったり, 無効な文字が含まれている.</exception>
		/// <exception cref="DirectoryNotFoundException"><see cref="filePath"/>が有効でないパス.例えばマップされていないドライブにあるなど.</exception>
		/// <exception cref="PathTooLongException">パス長すぎ.</exception>
		/// <exception cref="NotSupportedException"><see cref="filePath"/>の形式が無効. どういうのが無効なのかはよく分からん.</exception>
		/// <exception cref="System.Security.SecurityException">プログラムにファイルアクセス許可がない.</exception>
		/// <exception cref="UnauthorizedAccessException">ファイルが読み込み専用やったり, ディレクトリやったり, いろいろ.</exception>
		/// <exception cref="IOException">ファイルを開くときにエラー発生. なんかよく分からない.</exception>
		public void Export(string filePath, LayoutInformation layout_info)
		{
			#region 引数チェック
			if (filePath == null)
			{
				throw new ArgumentNullException(nameof(filePath));
			}
			if (layout_info == null)
			{
				throw new ArgumentNullException(nameof(layout_info));
			}
			#endregion

			Dictionary<string, string> setting_str = new Dictionary<string, string>();

			setting_str["Row"] = layout_info.Row.ToString();
			setting_str["Column"] = layout_info.Column.ToString();
			setting_str["CourtCount"] = layout_info.CourtCount.ToString();

			File.WriteAllLines(filePath, setting_str.Select(pair => pair.Key + ":" + pair.Value));
		}
	}
}
