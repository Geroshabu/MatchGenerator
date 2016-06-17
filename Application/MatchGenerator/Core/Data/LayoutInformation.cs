using System;

namespace MatchGenerator.Core
{
	public class LayoutInformation
	{
		public int CourtCount
		{
			get
			{
				return Row * Column;
			}
		}
		public int Row { get; set; }
		public int Column { get; set; }
	}
}
