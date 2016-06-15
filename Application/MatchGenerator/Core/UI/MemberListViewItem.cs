using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchGenerator.Core.UI
{
	class MemberListViewItem
	{
		public int Id { get; }
		public bool IsChecked { get; set; }
		public Person Person { get; set; }

		private static int NumberOfPerson = 0;

		public MemberListViewItem(Person person)
		{
			IsChecked = false;
			Person = person;
			NumberOfPerson++;
			Id = NumberOfPerson;
		}
	}
}
