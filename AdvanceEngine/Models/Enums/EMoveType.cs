namespace AdvanceEngine.Models.Enums
{
	/// <summary>
	/// Specifies the type of a move
	/// </summary>
	public enum EMoveType
	{
		/// <summary>
		/// The peice is moving
		/// </summary>
		Move,

		/// <summary>
		/// The piece is attacking another piece
		/// </summary>
		Attack,

		/// <summary>
		/// The piece is building a wall
		/// </summary>
		Build,

		/// <summary>
		/// The piece is converting an enemy to it's side
		/// </summary>
		Convert
	}
}
