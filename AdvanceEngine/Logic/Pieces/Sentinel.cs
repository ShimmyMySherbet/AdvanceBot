using AdvanceEngine.Models;
using AdvanceEngine.Models.Attributes;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Logic.Pieces
{
	[AdvancePiece(value: 6, EPieceType.Sentinel)]
	public class Sentinel : Piece
	{
		public Sentinel(ETeam team) : base(team) { }

		public override IPiece Convert(ETeam team) => new Sentinel(team);

		public override IEnumerator<PotentialMove> GetMoveDefinitions(int x, int y, int dir)
		{
			yield return new PotentialMove(x - 2, y - 1);
			yield return new PotentialMove(x - 1, y - 2);
			yield return new PotentialMove(x + 1, y - 2);
			yield return new PotentialMove(x + 2, y - 1);

			yield return new PotentialMove(x + 2, y + 1);
			yield return new PotentialMove(x + 1, y + 2);
			yield return new PotentialMove(x - 1, y + 2);
			yield return new PotentialMove(x - 2, y + 1);
		}
	}
}
