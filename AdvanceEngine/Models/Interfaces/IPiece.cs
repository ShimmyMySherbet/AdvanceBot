using System.Collections.Generic;
using AdvanceEngine.Models.Enums;

namespace AdvanceEngine.Models.Interfaces
{
	/// <summary>
	/// Represents an Advance Piece
	/// </summary>
	public interface IPiece
	{
		/// <summary>
		/// The score value of the piece
		/// </summary>
		public int ScoreValue { get; }

		/// <summary>
		/// The team the piece is on
		/// </summary>
		public ETeam Team { get; }

		/// <summary>
		/// The piece X-Coordinate
		/// </summary>
		public int X { get; }

		/// <summary>
		/// The piece Y-Coordinate
		/// </summary>
		public int Y { get; }

		/// <summary>
		/// The type of the piece
		/// </summary>
		public EPieceType PieceType { get; }

		/// <summary>
		/// Specifies if the piece can be converted to the enemy team
		/// </summary>
		public bool Convertable { get; }

		/// <summary>
		/// Determines what moves are possible given a board state
		/// </summary>
		/// <param name="x">Piece X Coordinate</param>
		/// <param name="y">Peice Y Coordinate</param>
		/// <param name="map">Board state</param>
		/// <param name="filterX">Filters to only moves targeting this X Coordinate, or -1 for no filtering</param>
		/// <param name="filterY">Filters to only moves targeting this Y Coordinate, or -1 for no filtering</param>
		/// <param name="ignoreSafety">Specifies pieces that cannot move to an unsafe location should ignore safety requirements</param>
		/// <returns>An enumeration of possible moves if any</returns>
		public IEnumerator<Move> GetMoves(IPieceMap map, int filterX = -1, int filyerY = -1, bool ignoreSafety = false);

		/// <summary>
		/// Converts the piece to another team
		/// </summary>
		/// <param name="team">Team to convert to</param>
		/// <param name="x">New X Coordinate</param>
		/// <param name="y">New Y Coordinate</param>
		/// <returns>A new instance of the piece as the specified team</returns>
		public IPiece Mutate(ETeam team, int x, int y);
	}
}
