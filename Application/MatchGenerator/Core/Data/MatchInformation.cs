using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchGenerator.Core
{
	public class MatchInformation
	{
		private Pair[] _pairs;
		public Pair Team1 { get { return _pairs[0]; } set { _pairs[0] = value; } }
		public Pair Team2 { get { return _pairs[1]; } set { _pairs[1] = value; } }

		public MatchInformation()
		{
			_pairs = new Pair[2];
		}
	}
}
