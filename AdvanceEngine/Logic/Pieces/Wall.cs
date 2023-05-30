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
		/// <param name="x">X Coordinate</param>
		/// <param name="y">Y Coordinate</param>
		public Wall(int x, int y) : base(ETeam.Neutral, x, y) { }

		/// <summary>
		/// <summary>
		/// Converts the piece to another team
		/// </summary>
		/// <param name="team">Team to convert to</param>
		/// <param name="x">New X Coordinate</param>
		/// <param name="y">New Y Coordinate</param>
		/// <returns>A new instance of the piece in the desired team</returns>
		public override IPiece Mutate(ETeam _, int x, int y) => new Wall(x, y);

		/// <summary>
		/// Yields nothing, as the wall cannot move
		/// </summary>
		/// <param name="x">Not used</param>
		/// <param name="y">Not used</param>
		/// <param name="team">Not Used</param>
		/// <returns>Nil</returns>
		public override IEnumerator<PotentialMove> GetMoveDefinitions(int x, int y, ETeam team)
		{
			yield break;
		}
	}
}
