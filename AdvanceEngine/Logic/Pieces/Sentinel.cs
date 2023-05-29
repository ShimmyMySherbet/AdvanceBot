using System.Collections.Generic;
using AdvanceEngine.Models;
using AdvanceEngine.Models.Attributes;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Logic.Pieces
{
	/// <summary>
	/// Sentinel Advance AI piece
	/// </summary>
	[AdvancePiece(value: 6, EPieceType.Sentinel)]
	public class Sentinel : Piece
	{
		/// <summary>
		/// Sentinel
		/// </summary>
		/// <param name="team">Initial Team</param>
		public Sentinel(ETeam team) : base(team) { }

		/// <summary>
		/// Converts the piece to another team
		/// </summary>
		/// <param name="team">Team to convert to</param>
		/// <returns>A new instance of the piece in the specified team</returns>
		public override IPiece Convert(ETeam team) => new Sentinel(team);

		/// <summary>
		/// Defines potential moves for this piece
		/// </summary>
		/// <param name="x">Piece X coordinate</param>
		/// <param name="y">Piece Y coordinate</param>
		/// <param name="dir">Direction multiplier. 1 when White, and -1 when Black</param>
		/// <returns>An enumeration of potential moves</returns>
		public override IEnumerator<PotentialMove> GetMoveDefinitions(int x, int y, int dir)
		{
			yield return new PotentialMove(x - 2, y - 1);
			yield return new PotentialMove(x - 1, y - 2);
			yield return new PotentialMove(x + 1, y - 2);
			yield return new PotentialMove(x + 2, y - 1);

			yield return new PotentialMove(x + 2, y + 1);
			yield return new PotentialMove(x + 1, y + 2);
			yield return new PotentialMove(x - 1, y + 2);
			yield return new PotentialMove(x - 2, y + 1);
		}
	}
}
