namespace AdvanceEngine.Models.Enums
{
	/// <summary>
	/// Represents what team a piece belonsg to
	/// </summary>
	public enum ETeam : int
	{
		/// <summary>
		/// White pieces.
		/// </summary>
		White = -1,

		/// <summary>
		/// Black pieces
		/// </summary>
		Black = 1,

		/// <summary>
		/// Does not belong to a team.
		/// </summary>
		Neutral = 0
	}
}
