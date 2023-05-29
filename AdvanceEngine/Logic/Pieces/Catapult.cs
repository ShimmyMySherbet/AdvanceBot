using System.Collections.Generic;
using AdvanceEngine.Models;
using AdvanceEngine.Models.Attributes;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Logic.Pieces
{
	[AdvancePiece(value: 6, type: EPieceType.Catapult)]
	public class Catapult : Piece
	{
		public Catapult(ETeam team) : base(team) { }

		public override IPiece Convert(ETeam team) => new Catapult(team);

		public override IEnumerator<PotentialMove> GetMoveDefinitions(int x, int y, int dir)
		{
			yield return new PotentialMove(x, y - 1, canAttack: false);
			yield return new PotentialMove(x, y + 1, canAttack: false);
			yield return new PotentialMove(x - 1, y, canAttack: false);
			yield return new PotentialMove(x + 1, y, canAttack: false);

			yield return new PotentialMove(x, y - 3, canMove: false, movesOnAttack: false);
			yield return new PotentialMove(x, y + 3, canMove: false, movesOnAttack: false);
			yield return new PotentialMove(x - 3, y, canMove: false, movesOnAttack: false);
			yield return new PotentialMove(x + 3, y, canMove: false, movesOnAttack: false);

			yield return new PotentialMove(x + 2, y + 2, canMove: false, movesOnAttack: false);
			yield return new PotentialMove(x + 2, y - 2, canMove: false, movesOnAttack: false);
			yield return new PotentialMove(x - 2, y + 2, canMove: false, movesOnAttack: false);
			yield return new PotentialMove(x - 2, y - 2, canMove: false, movesOnAttack: false);
		}
	}
}
