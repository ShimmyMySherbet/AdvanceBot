using System.Collections.Generic;
using AdvanceEngine.Models;
using AdvanceEngine.Models.Attributes;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Logic.Pieces
{
	/// <summary>
	/// Miner Advance Piece
	/// </summary>
	[AdvancePiece(value: 4, type: EPieceType.Miner)]
	public class Miner : Piece
	{
		/// <summary>
		/// Miner
		/// </summary>
		/// <param name="team">Initial Team</param>
		public Miner(ETeam team) : base(team) { }

		/// <summary>
		/// Converts the piece to another team
		/// </summary>
		/// <param name="team">team to convert to</param>
		/// <returns>A new instance of the peice in the specified team</returns>
		public override IPiece Convert(ETeam team) => new Miner(team);

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
			var slideDir = new (int x, int y)[] {  (1, 0), (0, 1), (-1, 0), (0, -1) };

			foreach (var slDir in slideDir)
			{
				(int x, int y) pos = (x + slDir.x, y + slDir.y);


				if (pos.x == -1 && pos.y == 0)
				{
					System.Console.WriteLine();
				}

				check.Clear();
				while (pos.x >= 0 && pos.x < 9 && pos.y >= 0 && pos.y < 9)
				{
					if (pos.x == 3 && pos.y == 0)
					{
						System.Console.WriteLine();
					}


					yield return new PotentialMove(pos.x, pos.y, canBreakWalls: true, mustBeEmpty: check.ToArray());
					check.Add(pos);
					pos = (pos.x + slDir.x, pos.y + slDir.y);
				}
			}
		}
	}
}
