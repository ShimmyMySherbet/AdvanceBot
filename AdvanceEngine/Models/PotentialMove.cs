using AdvanceEngine.Models.Enums;

namespace AdvanceEngine.Models
{
	public class PotentialMove
	{
		public int TargetX { get; init; }
		public int TargetY { get; init; }

		public (int x, int y)[]? MustBeEmpty { get; init; }

		/// <summary>
		/// Can move to position when there is no enemy piece
		/// </summary>
		public bool CanMove { get; init; }

		/// <summary>
		/// Can attack position when there is an enemy
		/// </summary>
		public bool CanAttack { get; init; }

		public bool MovesOnAttack { get; init; }
	}
}
