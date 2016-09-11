using System;
using MatchGenerator.Model;

namespace MatchGeneratorTest.Model
{
	internal class PersonMock : IPerson
	{
		public string Description
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}

	public class PersonTest
	{
	}
}
