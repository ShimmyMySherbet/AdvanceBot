using System.Collections.Generic;
using AdvanceEngine.Models;
using AdvanceEngine.Models.Attributes;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Logic.Pieces
{
	/// <summary>
	/// Wall Advance Piece
	/// </summary>
	[AdvancePiece(value: 0, type: EPieceType.Wall)]
	public class Wall : Piece
	{
		/// <summary>
		/// Wall Constructor
		/// </summary>
		public Wall() : base(ETeam.Neutral) { }

		/// <summary>
		/// Does nothing, as the wall cannot belong to a playing team
		/// </summary>
		/// <param name="team">Not Used</param>
		/// <returns>Reference to self</returns>
		public override IPiece Convert(ETeam team)
		{
			return this;
		}

		/// <summary>
		/// Yields nothing, as the wall cannot move
		/// </summary>
		/// <param name="x">Not used</param>
		/// <param name="y">Not used</param>
		/// <param name="dir">Not used</param>
		/// <returns>Nil</returns>
		public override IEnumerator<PotentialMove> GetMoveDefinitions(int x, int y, int dir)
		{
			yield break;
		}
	}
}
