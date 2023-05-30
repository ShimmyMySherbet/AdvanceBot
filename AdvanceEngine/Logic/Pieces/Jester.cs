using System.Collections.Generic;
using AdvanceEngine.Models;
using AdvanceEngine.Models.Attributes;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Logic.Pieces
{
	/// <summary>
	/// Jester Advance Piece
	/// </summary>
	[AdvancePiece(value: 3, type: EPieceType.Jester)]
	public class Jester : Piece
	{
		/// <summary>
		/// Jester
		/// </summary>
		/// <param name="team">Initial Team</param>
		/// <param name="x">X Coordinate</param>
		/// <param name="y">Y Coordinate</param>
		public Jester(ETeam team, int x, int y) : base(team, x, y) { }

		/// <summary>
		/// Converts the piece to another team
		/// </summary>
		/// <param name="team">Team to convert to</param>
		/// <param name="x">New X Coordinate</param>
		/// <param name="y">New Y Coordinate</param>
		/// <returns>A new instance of the piece in the desired team</returns>
		public override IPiece Mutate(ETeam team, int x, int y) => new Jester(team, x, y);

		/// <summary>
		/// Defines potential moves for this piece
		/// </summary>
		/// <param name="x">Piece X coordinate</param>
		/// <param name="y">Piece Y coordinate</param>
		/// <param name="team">The team of the playing team</param>
		/// <returns>An enumeration of potential moves</returns>
		public override IEnumerator<PotentialMove> GetMoveDefinitions(int x, int y, ETeam team)
		{
			for (int offsetX = -1; offsetX < 2; offsetX++)
			{
				for (int offsetY = -1; offsetY < 2; offsetY++)
				{
					var newX = x + offsetX;
					var newY = y + offsetY;
					if (newX == x && newY == y)
						continue;

					yield return new PotentialMove(newX, newY, canMove: false, convertsEnemy: true);
					yield return new PotentialMove(newX, newY, canAttack: false, swapsPlaces: true);
				}
			}
		}
	}
}
