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
		public Dragon(ETeam team) : base(team) { }

		/// <summary>
		/// Converts the piece to another team
		/// </summary>
		/// <param name="team">Team to convert to</param>
		/// <returns>New instance of the piece in the specified team</returns>
		public override IPiece Convert(ETeam team) => new Dragon(team);

		/// <summary>
		/// Defines potential moves for this piece
		/// </summary>
		/// <param name="x">Piece X coordinate</param>
		/// <param name="y">Piece Y coordinate</param>
		/// <param name="dir">Direction multiplier. 1 when White, and -1 when Black</param>
		/// <returns>An enumeration of potential moves</returns>
		public override IEnumerator<PotentialMove> GetMoveDefinitions(int x, int y, int dir)
		{
			var check = new List<(int x, int y)>();
			var slideDir = new (int x, int y)[] { (1, 1), (1, -1), (-1, 1), (-1, -1), (1, 0), (0, 1), (-1, 0), (0, -1) };

			foreach(var slDir in slideDir)
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
