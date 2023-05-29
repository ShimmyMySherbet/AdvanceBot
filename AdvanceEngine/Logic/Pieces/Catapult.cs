using System.Collections.Generic;
using AdvanceEngine.Models;
using AdvanceEngine.Models.Attributes;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Logic.Pieces
{
	/// <summary>
	/// Catapult Advance Piece
	/// </summary>
	[AdvancePiece(value: 6, type: EPieceType.Catapult)]
	public class Catapult : Piece
	{
		/// <summary>
		/// Catapult
		/// </summary>
		/// <param name="team">Initial Side</param>
		public Catapult(ETeam team) : base(team) { }

		/// <summary>
		/// Converts the piece to another team
		/// </summary>
		/// <param name="team">Team to convert to</param>
		/// <returns>New instance of the piece in the specified team</returns>
		public override IPiece Convert(ETeam team) => new Catapult(team);

		/// <summary>
		/// Defines potential moves for this piece
		/// </summary>
		/// <param name="x">Piece X coordinate</param>
		/// <param name="y">Piece Y coordinate</param>
		/// <param name="dir">Direction multiplier. 1 when White, and -1 when Black</param>
		/// <returns>An enumeration of potential moves</returns>
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
