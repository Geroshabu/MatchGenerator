using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchGenerator.Core
{
	public class Pair
	{
		private Person[] _players;
		public Person player1 { get { return _players[0]; } set { _players[0] = value; } }
		public Person player2 { get { return _players[1]; } set { _players[1] = value; } }

		public Pair(Person p1, Person p2)
		{
			_players = new Person[2];
			player1 = p1;
			player2 = p2;
		}
	}
}
