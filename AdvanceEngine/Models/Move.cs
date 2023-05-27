using AdvanceEngine.Models.Enums;

namespace AdvanceEngine.Models
{
	/// <summary>
	///  Represents a move that can mutate a board
	/// </summary>
	public class Move
	{
		public float EnemyScoreChange { get; }
		public float OwnScoreChange { get; }

		public EPieceType? TargetPiece { get; set; }

		public (int x, int y)? TargetPosition { get; set; }

		public bool IsAttack { get; set; }
		public (int x, int y) Origin { get; set; }

		public MapMutator Mutator { get; }

		public Move(float scoreChange, int enemyScoreChange, MapMutator mutator)
		{
			OwnScoreChange = scoreChange;
			EnemyScoreChange = enemyScoreChange;
			Mutator = mutator;
		}
	}
}
