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
		/// <param name="x">X Coordinate</param>
		/// <param name="y">Y Coordinate</param>
		public Zombie(ETeam team, int x, int y) : base(team, x ,y) { }

		/// <summary>
		/// Converts the piece to another team
		/// </summary>
		/// <param name="team">Team to convert to</param>
		/// <param name="x">New X Coordinate</param>
		/// <param name="y">New Y Coordinate</param>
		/// <returns>A new instance of the piece in the desired team</returns>
		public override IPiece Mutate(ETeam team, int x, int y) => new Zombie(team, x, y);

		/// <summary>
		/// Defines potential moves for this piece
		/// </summary>
		/// <param name="x">Piece X coordinate</param>
		/// <param name="y">Piece Y coordinate</param>
		/// <param name="team">The team of the playing team</param>
		/// <returns>An enumeration of potential moves</returns>
		public override IEnumerator<PotentialMove> GetMoveDefinitions(int x, int y, ETeam team)
		{
			// direction multiplier
			var dir = (int)team;

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
