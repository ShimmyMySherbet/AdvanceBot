namespace AdvanceEngine.Models.Enums
{
	/// <summary>
	/// Represents the check state of a team
	/// </summary>
	public enum ECheckState
	{
		/// <summary>
		/// No check state
		/// </summary>
		None = 0,

		/// <summary>
		/// The side is in check
		/// </summary>
		Check = 1,

		/// <summary>
		/// The side is in checkmate and the game has ended
		/// </summary>
		Checkmate = 2
	}
}
