using System;
using MatchGenerator.Model;

namespace MatchGeneratorTest.Model
{
	internal class PersonMock : IPerson
	{
		public Func<string> DescriptionFunc = () => null;
		public int DescriptionCount = 0;
		public string Description
		{
			get
			{
				DescriptionCount++;
				return DescriptionFunc();
			}
		}

		public Func<string> NameFunc = () => null;
		public int NameCount = 0;
		public string Name
		{
			get
			{
				NameCount++;
				return NameFunc();
			}
		}
	}

	public class PersonTest
	{
	}
}
