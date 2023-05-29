using System;
using AdvanceEngine.Models.Enums;

namespace AdvanceEngine.Models.Exceptions
{
	/// <summary>
	/// Raised when a side is checkmated and the game has ended
	/// </summary>
	public class CheckmatedException : Exception
	{
		public ETeam Team { get; }
		/// <summary>
		/// Checkmated Exception
		/// </summary>
		/// <param name="team">The team that is checkmated</param>
		public CheckmatedException(ETeam team) : base($"Team {team} is checkmated")
		{
			Team = team;
		}
	}
}
