using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Models
{
	/// <summary>
	///  Represents a move that can mutate a board
	/// </summary>
	public class Move
	{
		public int ScoreChange => OwnScoreChange - EnemyScoreChange;
		public int EnemyScoreChange { get; }
		public int OwnScoreChange { get; }

		public EPieceType? TargetPiece { get; set; }
		public IPiece? Self { get; set; }

		public (int x, int y)? TargetPosition { get; set; }

		public EMoveType MoveType { get; }
		public (int x, int y) Origin { get; set; }

		public MapMutator Mutator { get; }

		public Move(int scoreChange, int enemyScoreChange, MapMutator mutator, EMoveType type, IPiece? self)
		{
			OwnScoreChange = scoreChange;
			EnemyScoreChange = enemyScoreChange;
			Mutator = mutator;
			MoveType = type;
			Self = self;
		}
	}
}
