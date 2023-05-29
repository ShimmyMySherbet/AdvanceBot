namespace AdvanceEngine.Models
{
	/// <summary>
	/// A Potential Move definition for a piece
	/// </summary>
	public struct PotentialMove
	{
		/// <summary>
		/// Target X Coordinate
		/// </summary>
		public int TargetX { get; }

		/// <summary>
		/// Target Y Coordinate
		/// </summary>
		public int TargetY { get; }

		/// <summary>
		/// An array of coordinates that must be devoid of pieces for this move to be valid
		/// </summary>
		public (int x, int y)[]? MustBeEmpty { get; init; }

		/// <summary>
		/// Can move to position when there is no enemy piece
		/// </summary>
		public bool CanMove { get; }

		/// <summary>
		/// Can attack position when there is an enemy
		/// </summary>
		public bool CanAttack { get; }

		/// <summary>
		/// Specifies if the piece moves to the attack target destination
		/// </summary>
		public bool MovesOnAttack { get; } = true;

		/// <summary>
		/// Specifies if this move is to build a Wall
		/// </summary>
		public bool IsBuildMove { get; } = false;

		/// <summary>
		/// Specifies if this move is capable of breaking walls
		/// </summary>
		public bool CanBreakWalls { get; } = false;

		/// <summary>
		/// Specifies if this move converts opponents when captured.
		/// When this is true, <seealso cref="MovesOnAttack"/> is implicitly false
		/// </summary>
		public bool ConvertsEnemy { get; } = false;

		/// <summary>
		/// Specifies if this move can swap places with a friendly piece
		/// </summary>
		public bool SwapsPositions { get; } = false;

		/// <summary>
		/// Specifies that any moves that place this piece in danger are not allowed.
		/// </summary>
		public bool MustNotBeCaptured { get; } = false;

		/// <summary>
		/// Creates a new instance of a potential move
		/// </summary>
		/// <param name="x">Target X Coordinate</param>
		/// <param name="y">Target Y Coordinate</param>
		/// <param name="canMove">Flag specifying if the piece can move</param>
		/// <param name="canAttack">Flag specifying if the peice can attack</param>
		/// <param name="movesOnAttack">Flag specifying if the piece moves to the target coordinates on attack</param>
		/// <param name="isBuildMove">Flag specifying this move places a wall</param>
		/// <param name="swapsPlaces">Flag specifying this piece can swap places with the target piece on move</param>
		/// <param name="convertsEnemy">Flag specifying if this move converts the enemy on attack</param>
		/// <param name="canBreakWalls">Flag specifying if this attack can target walls</param>
		/// <param name="mustNotBeCaptured">Flag specifying that this piece cannot put itself in danger</param>
		/// <param name="mustBeEmpty">List of coordinates that must not be occupied for this move to be valid</param>
		public PotentialMove(int x, int y, bool canMove = true, bool canAttack = true, bool movesOnAttack = true, bool isBuildMove = false,
			bool swapsPlaces = false ,bool convertsEnemy = false , bool canBreakWalls = false, bool mustNotBeCaptured = false, params (int x, int y)[]? mustBeEmpty)
		{
			TargetX = x;
			TargetY = y;
			MustBeEmpty = mustBeEmpty?.Length == 0 ? null : mustBeEmpty;
			CanMove = canMove;
			CanAttack = canAttack;
			MovesOnAttack = movesOnAttack;
			IsBuildMove = isBuildMove;
			ConvertsEnemy = convertsEnemy;
			CanBreakWalls = canBreakWalls;
			SwapsPositions = swapsPlaces;
			MustNotBeCaptured = mustNotBeCaptured;
		}
	}
}
