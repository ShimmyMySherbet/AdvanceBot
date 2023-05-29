using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Models
{
	/// <summary>
	///  Represents a move that can mutate a board
	/// </summary>
	public class Move
	{
		/// <summary>
		/// The total score change for a move
		/// </summary>
		public int ScoreChange => OwnScoreChange - EnemyScoreChange;
		
		/// <summary>
		/// The score change to the enemy
		/// </summary>
		public int EnemyScoreChange { get; }

		/// <summary>
		/// The score change to teh current player
		/// </summary>
		public int OwnScoreChange { get; }

		/// <summary>
		/// The target piece type, if applicable
		/// </summary>
		public EPieceType? TargetPiece { get; set; }

		/// <summary>
		/// The acting piece, if applicable
		/// </summary>
		public IPiece? Self { get; set; }

		/// <summary>
		/// The target position of the move, if applicable
		/// </summary>
		public (int x, int y)? TargetPosition { get; set; }

		/// <summary>
		/// The move type
		/// </summary>
		public EMoveType MoveType { get; }
		
		/// <summary>
		/// The location of the acting piece
		/// </summary>
		public (int x, int y) Origin { get; set; }

		/// <summary>
		/// The mutator to apply this move
		/// </summary>
		public MapMutator Mutator { get; }

		/// <summary>
		/// The initial move definition used to generate this move
		/// </summary>
		public PotentialMove Potential { get; }

		/// <summary>
		/// Move Info
		/// </summary>
		/// <param name="scoreChange">Self score change</param>
		/// <param name="enemyScoreChange">Enemy score change</param>
		/// <param name="mutator">Map mutator</param>
		/// <param name="type">The type of the move</param>
		/// <param name="self">The piece making the move</param>
		/// <param name="potential">The move definition for this move</param>
		public Move(int scoreChange, int enemyScoreChange, MapMutator mutator, EMoveType type, IPiece? self, PotentialMove potential)
		{
			OwnScoreChange = scoreChange;
			EnemyScoreChange = enemyScoreChange;
			Mutator = mutator;
			MoveType = type;
			Self = self;
			Potential = potential;
		}

		/// <summary>
		/// Creates a string view of this move, for debugging
		/// </summary>
		/// <returns>Human readable representation</returns>
		public override string ToString()
		{
			return $"[({Self} {Origin.x}, {Origin.y}) {MoveType} -> ({TargetPiece} {TargetPosition?.x}, {TargetPosition?.y})]";
		}
	}
}
