using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchGenerator.Core;

namespace MatchGenerator.Core
{
	class MatchDecider
	{
		public int NumberOfCourt { get; set; }

		public List<MatchInformation> generateMatches(List<Person> entrants)
		{
			if (entrants.Count < 4 * NumberOfCourt)
			{
				throw new ArgumentException("試合を組めるほどの人数がいません.");
			}

			List<MatchInformation> matches = new List<MatchInformation>();

			// この部分は後で作る.
			// とりあえず3つの試合にただリストの先頭の人から割り当てていくだけ.

			for (int i_court = 0; i_court < NumberOfCourt; i_court++)
			{
				MatchInformation match = new MatchInformation();

				match.Team1 = new Pair(entrants[i_court * 4], entrants[i_court * 4 + 1]);
				match.Team2 = new Pair(entrants[i_court * 4 + 2], entrants[i_court * 4 + 3]);

				matches.Add(match);
			}

			return matches;
		}
	}
}
