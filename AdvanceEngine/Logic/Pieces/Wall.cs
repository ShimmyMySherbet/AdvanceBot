using AdvanceEngine.Models;
using AdvanceEngine.Models.Attributes;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Logic.Pieces
{
	[AdvancePiece(value: 0, type: EPieceType.Wall)]
	public class Wall : Piece
	{
		public Wall() : base(ETeam.Neutral) { }

		public override IPiece Convert(ETeam team)
		{
			return this;
		}

		public override IEnumerator<PotentialMove> GetMoveDefinitions(int x, int y, int dir)
		{
			yield break;
		}
	}
}
