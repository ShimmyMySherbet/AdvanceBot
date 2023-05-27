using AdvanceEngine.Models;
using AdvanceEngine.Models.Attributes;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Logic.Pieces
{
	[AdvancePiece(value: 1, type: EPieceType.Zombie)]
	public class Zombie : Piece
	{
		public Zombie(ETeam team) : base(team) { }

		public override IPiece Convert(ETeam team) => new Zombie(team);

		public override IEnumerator<PotentialMove> GetMoveDefinitions(int x, int y, int dir)
		{
			// left
			yield return new PotentialMove(x - 1, y + (1 * dir));

			// forward
			yield return new PotentialMove(x, y + (1 * dir));

			// right
			yield return new PotentialMove(x + 1, y + (1 * dir));

			// leap left
			yield return new PotentialMove(x - 2, y + (2 * dir), canMove: false, mustBeEmpty: (x - 1, y + (1 * dir)));

			// leap forward
			yield return new PotentialMove(x, y + (2 * dir), canMove: false, mustBeEmpty: (x, y + (1 * dir)));

			// leap right
			yield return new PotentialMove(x + 2, y + (2 * dir), canMove: false, mustBeEmpty: (x + 1, y + (1 * dir)));
		}
	}
}
