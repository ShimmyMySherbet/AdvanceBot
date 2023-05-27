using AdvanceEngine.Models;
using AdvanceEngine.Models.Attributes;
using AdvanceEngine.Models.Enums;

namespace AdvanceEngine.Logic.Pieces
{
	[AdvancePiece(value: 2, type: EPieceType.Builder)]
	public class Builder : Piece
	{
		public Builder(ETeam team) : base(team)
		{
		}
	}
}
