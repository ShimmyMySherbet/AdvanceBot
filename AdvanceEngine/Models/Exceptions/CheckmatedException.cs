using System;
using AdvanceEngine.Models.Enums;

namespace AdvanceEngine.Models.Exceptions
{
	public class CheckmatedException : Exception
	{
		public CheckmatedException(ETeam team) : base($"Team {team} is checkmated")
		{
		}
	}
}
