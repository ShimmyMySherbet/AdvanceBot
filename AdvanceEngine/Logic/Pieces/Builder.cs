using System.Collections.Generic;
using AdvanceEngine.Models;
using AdvanceEngine.Models.Attributes;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Logic.Pieces
{
	[AdvancePiece(value: 2, type: EPieceType.Builder)]
	public class Builder : Piece
	{
		public Builder(ETeam team) : base(team) { }

		public override IPiece Convert(ETeam team) => new Builder(team);

		public override IEnumerator<PotentialMove> GetMoveDefinitions(int x, int y, int dir)
		{
			for (int offsetX = -1; offsetX < 2; offsetX++)
			{
				for (int offsetY = -1; offsetY < 2; offsetY++)
				{

					var newX = x + offsetX;
					var newY = y + offsetY;

					if (offsetX == newX && offsetY == newY)
						continue;

					yield return new PotentialMove(newX, newY);
					yield return new PotentialMove(newX, newY, isBuildMove: true);
				}
			}
		}
	}
}
