using AdvanceEngine.Models;
using AdvanceEngine.Models.Attributes;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Logic.Pieces
{
	[AdvancePiece(value: 3, type: EPieceType.Jester)]
	public class Jester : Piece
	{
		public Jester(ETeam team) : base(team) { }

		public override IPiece Convert(ETeam team) => new Jester(team);

		public override IEnumerator<PotentialMove> GetMoveDefinitions(int x, int y, int dir)
		{
			for (int offsetX = -1; offsetX < 2; offsetX++)
			{
				for (int offsetY = -1; offsetY < 2; offsetY++)
				{
					if (offsetX == 0 && offsetY == 0)
						continue;

					yield return new PotentialMove(x + offsetX, y + offsetY, canMove: false, convertsEnemy: true);
					yield return new PotentialMove(x + offsetX, y + offsetY, canAttack: false, swapsPlaces: true);
				}
			}
		}
	}
}
