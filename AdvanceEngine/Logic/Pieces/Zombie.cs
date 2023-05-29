using System.Collections.Generic;
using AdvanceEngine.Models;
using AdvanceEngine.Models.Attributes;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Logic.Pieces
{
	/// <summary>
	/// Zombie Advance Piece
	/// </summary>
	[AdvancePiece(value: 1, type: EPieceType.Zombie)]
	public class Zombie : Piece
	{
		/// <summary>
		/// Zombie
		/// </summary>
		/// <param name="team">Initial Team</param>
		public Zombie(ETeam team) : base(team) { }

		/// <summary>
		/// Converts the piece to another team
		/// </summary>
		/// <param name="team">Team to convert to</param>
		/// <returns>A new instance of the piece in the specified team</returns>
		public override IPiece Convert(ETeam team) => new Zombie(team);

		/// <summary>
		/// Defines potential moves for this piece
		/// </summary>
		/// <param name="x">Piece X coordinate</param>
		/// <param name="y">Piece Y coordinate</param>
		/// <param name="dir">Direction multiplier. 1 when White, and -1 when Black</param>
		/// <returns>An enumeration of potential moves</returns>
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
