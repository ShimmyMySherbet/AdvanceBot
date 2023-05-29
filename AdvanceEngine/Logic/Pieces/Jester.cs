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
		public Jester(ETeam team) : base(team) { }

		/// <summary>
		/// Converts the piece to another team
		/// </summary>
		/// <param name="team">Team to convert to</param>
		/// <returns>New instance of the piece in the specified team</returns>
		public override IPiece Convert(ETeam team) => new Jester(team);

		/// <summary>
		/// Defines potential moves for this piece
		/// </summary>
		/// <param name="x">Piece X coordinate</param>
		/// <param name="y">Piece Y coordinate</param>
		/// <param name="dir">Direction multiplier. 1 when White, and -1 when Black</param>
		/// <returns>An enumeration of potential moves</returns>
		public override IEnumerator<PotentialMove> GetMoveDefinitions(int x, int y, int dir)
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
