using System.Collections.Generic;
using AdvanceEngine.Models;
using AdvanceEngine.Models.Attributes;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Logic.Pieces
{
	/// <summary>
	/// Dragon Advance Piece
	/// </summary>
	[AdvancePiece(value: 7, type: EPieceType.Dragon)]
	public class Dragon : Piece
	{
		/// <summary>
		/// Dragon
		/// </summary>
		/// <param name="team">Piece Team</param>
		/// <param name="x">X Coordinate</param>
		/// <param name="y">Y Coordinate</param>
		public Dragon(ETeam team, int x, int y) : base(team, x, y) { }

		/// <summary>
		/// Converts the piece to another team
		/// </summary>
		/// <param name="team">Team to convert to</param>
		/// <param name="x">New X Coordinate</param>
		/// <param name="y">New Y Coordinate</param>
		/// <returns>A new instance of the piece in the desired team</returns>
		public override IPiece Mutate(ETeam team, int x, int y) => new Dragon(team, x, y);

		/// <summary>
		/// Defines potential moves for this piece
		/// </summary>
		/// <param name="x">Piece X coordinate</param>
		/// <param name="y">Piece Y coordinate</param>
		/// <param name="team">The team of the playing team</param>
		/// <returns>An enumeration of potential moves</returns>
		public override IEnumerator<PotentialMove> GetMoveDefinitions(int x, int y, ETeam team)
		{
			var check = new List<(int x, int y)>();
			var slideDir = new (int x, int y)[] { (1, 1), (1, -1), (-1, 1), (-1, -1), (1, 0), (0, 1), (-1, 0), (0, -1) };

			foreach (var slDir in slideDir)
			{
				(int x, int y) pos = (x + slDir.x, y + slDir.y);

				var capture = false;
				check.Clear();
				while (pos.x >= 0 && pos.x < 9 && pos.y >= 0 && pos.y < 9)
				{
					yield return new PotentialMove(pos.x, pos.y, canAttack: capture, mustBeEmpty: check.ToArray());
					check.Add(pos);
					capture = true;
					pos = (pos.x + slDir.x, pos.y + slDir.y);
				}
			}
		}
	}
}
