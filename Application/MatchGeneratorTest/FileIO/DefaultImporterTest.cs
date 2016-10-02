using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchGenerator.FileIO;

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
	}
}
