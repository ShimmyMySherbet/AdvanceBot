namespace AdvanceEngine.Models
{
	public struct PotentialMove
	{
		public int TargetX { get; }
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
