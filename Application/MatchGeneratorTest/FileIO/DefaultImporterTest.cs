using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Xunit;
using MatchGenerator.FileIO;
using MatchGenerator.Model;
using MatchGenerator.Core;
using MatchGeneratorTest.Model;

namespace MatchGeneratorTest.FileIO
{
	public class DefaultImporterTest : IDisposable
	{
		private DefaultImporter Instance;

		/// <summary>
		/// このテストのワーキングディレクトリ
		/// </summary>
		private string TestDirectoryPath = $@"{Directory.GetCurrentDirectory()}\{nameof(DefaultImporterTest)}Directory";

		public DefaultImporterTest()
		{
			Instance = new DefaultImporter();

			if (Directory.Exists(TestDirectoryPath))
			{
				Directory.Delete(TestDirectoryPath, true);
			}
			Directory.CreateDirectory(TestDirectoryPath);
		}

		public void Dispose()
		{
			if (Directory.Exists(TestDirectoryPath))
			{
				Directory.Delete(TestDirectoryPath, true);
			}
		}

		[Fact(DisplayName = "Importメソッド : 正常系")]
		[Trait("category", "FileIO")]
		[Trait("type", "正常系")]
		public void ImportTest()
		{
			// Arrange
			string inputFileName = $@"{TestDirectoryPath}\{nameof(ImportTest)}.txt";
			string[] fileContents = new string[]{
				"花中島,セクシーコマンドー部,M,42,ウォンチュッ",
				"藤山,セクシーコマンドー部,M,43,目指すは友達100人！",
				"北原,セクシーコマンドー部,F,44,ヒゲは女の命なんだよ！"
			};
			File.WriteAllLines(inputFileName, fileContents);
			// Expected data
			IList<string> expectedReturnName = new List<string> { "花中島", "藤山", "北原" };
			IList<string> expectedReturnDescription = new List<string> { "ウォンチュッ", "目指すは友達100人！", "ヒゲは女の命なんだよ！" };

			// Act
			IList<IPerson> actualReturn = Instance.Import(inputFileName);

			// Assert
			Assert.True(actualReturn.Select(person => person.Name).SequenceEqual(expectedReturnName));
			Assert.True(actualReturn.Select(person => person.Description).SequenceEqual(expectedReturnDescription));
		}

		[Fact(DisplayName = "Importメソッド : 異常系 : null引数")]
		[Trait("category", "FileIO")]
		[Trait("type", "異常系")]
		public void ImportTest_ArgumentNullException()
		{
			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => Instance.Import(null));
		}

		[Fact(DisplayName = "Importメソッド : 異常系 : 不正なパス形式")]
		[Trait("category", "FileIO")]
		[Trait("type", "異常系")]
		public void ImportTest_ArgumentException()
		{
			// Input data
			string inputFileName = $@"{TestDirectoryPath}\foo{Path.GetInvalidPathChars().First()}bar.txt";

			// Act & Assert
			Assert.Throws<ArgumentException>(() => Instance.Import(inputFileName));
		}

		[Fact(DisplayName = "Importメソッド : 異常系 : 長すぎるパス")]
		[Trait("category", "FileIO")]
		[Trait("type", "異常系")]
		public void ImportTest_PathTooLongException()
		{
			// Input data
			string inputFileName = TestDirectoryPath + "\\f" + new string('o', 300);

			// Act & Assert
			Assert.Throws<PathTooLongException>(() => Instance.Import(inputFileName));
		}

		[Fact(DisplayName = "Importメソッド : 異常系 : パスが無効")]
		[Trait("category", "FileIO")]
		[Trait("type", "異常系")]
		public void ImportTest_DirectoryNotFoundException()
		{
			// Input data
			string inputFileName = TestDirectoryPath + @"\NotExistDirectory\foobar.txt";

			// Act & Assert
			Assert.Throws<DirectoryNotFoundException>(() => Instance.Import(inputFileName));
		}

		[Fact(DisplayName = "Importメソッド : 異常系 : 存在しないファイル")]
		[Trait("category", "FileIO")]
		[Trait("type", "異常系")]
		public void ImportTest_FileNotFoundException()
		{
			// Input data
			string inputFileName = TestDirectoryPath + "\\NotExistFile.txt";

			// Act & Assert
			Assert.Throws<FileNotFoundException>(() => Instance.Import(inputFileName));
		}

		[Fact(DisplayName = "Importメソッド : 異常系 : IOエラー")]
		[Trait("category", "FileIO")]
		[Trait("type", "異常系")]
		public void ImportTest_IOException()
		{
			// Arrange
			string inputFileName = TestDirectoryPath + "\\foobar.txt";
			Instance.SetPrivateField("DoReadAllLines",
				new Func<string, string[]>(_ => { throw new IOException(); }));

			// Act & Assert
			Assert.Throws<IOException>(() => Instance.Import(inputFileName));
		}

		[Fact(DisplayName = "Importメソッド : 異常系 : パスの形式が無効")]
		[Trait("category", "FileIO")]
		[Trait("type", "異常系")]
		public void ImportTest_NotSupportedException()
		{
			// Input data
			string inputFileName = TestDirectoryPath + "\\foo:bar.txt";

			// Act & Assert
			Assert.Throws<NotSupportedException>(() => Instance.Import(inputFileName));
		}

		[Fact(DisplayName = "Importメソッド : 異常系 : アクセス権がない")]
		[Trait("category", "FileIO")]
		[Trait("type", "異常系")]
		public void ImportTest_SecurityException()
		{
			// Arrange
			string inputFileName = TestDirectoryPath + "\\foobar.txt";
			Instance.SetPrivateField("DoReadAllLines",
				new Func<string, string[]>(_ => { throw new System.Security.SecurityException(); }));

			// Act & Assert
			Assert.Throws<System.Security.SecurityException>(() => Instance.Import(inputFileName));
		}

		[Fact(DisplayName = "Importメソッド : 異常系 : ディレクトリを指定")]
		[Trait("category", "FileIO")]
		[Trait("type", "異常系")]
		public void ImportTest_UnauthorizedAccessException()
		{
			// Input data
			string inputFileName = TestDirectoryPath;

			// Act & Assert
			Assert.Throws<UnauthorizedAccessException>(() => Instance.Import(inputFileName));
		}

		[Fact(DisplayName = "Importメソッド : 異常系 : 項目数が多い行がある")]
		[Trait("category", "FileIO")]
		[Trait("type", "異常系")]
		public void ImportTest_FileFormatException()
		{
			// Arrange
			string inputFileName = $@"{TestDirectoryPath}\{nameof(ImportTest_FileFormatException)}.txt";
			string[] fileContents = new string[]{
				"花中島,セクシーコマンドー部,M,42,ウォンチュッ,タバサ～",
				"藤山,セクシーコマンドー部,M,43,目指すは友達100人！",
				"北原,セクシーコマンドー部,F,44,ヒゲは女の命なんだよ！"
			};
			File.WriteAllLines(inputFileName, fileContents);

			// Act & Assert
			Assert.Throws<FileFormatException>(() => Instance.Import(inputFileName));
		}
	}
}
