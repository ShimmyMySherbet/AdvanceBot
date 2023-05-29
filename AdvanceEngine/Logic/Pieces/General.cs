using System.Collections.Generic;
using AdvanceEngine.Models;
using AdvanceEngine.Models.Attributes;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Logic.Pieces
{
	/// <summary>
	/// General Advance Piece
	/// </summary>
	[AdvancePiece(value: 0, EPieceType.General, convertable: false)]
	public class General : Piece
	{
		/// <summary>
		/// General
		/// </summary>
		/// <param name="team">Initial Team</param>
		public General(ETeam team) : base(team) { }

		/// <summary>
		/// Converts the piece to another team
		/// </summary>
		/// <param name="team">Desired team</param>
		/// <returns>New instance of the piece in the specified team</returns>
		public override IPiece Convert(ETeam team) => this;

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

					yield return new PotentialMove(newX, newY, mustNotBeCaptured: true);
				}
			}
		}
	}
}
