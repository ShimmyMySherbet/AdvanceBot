using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Models
{
	/// <summary>
	/// Provides board state info for a piece
	/// </summary>
	public struct PieceInfo
	{
		/// <summary>
		/// Piece X Coordinate
		/// </summary>
		public int X { get; }

		/// <summary>
		/// Piece Y Coordinate
		/// </summary>
		public int Y { get; }

		/// <summary>
		/// Piece instance
		/// </summary>
		public IPiece Piece { get; }

		/// <summary>
		/// Piece Info
		/// </summary>
		/// <param name="x">X Coordinate</param>
		/// <param name="y">Y Coordinate</param>
		/// <param name="piece">Piece instance</param>
		public PieceInfo(int x, int y, IPiece piece)
		{
			X = x;
			Y = y;
			Piece = piece;
		}

		/// <summary>
		/// Porivdes a human readable form of this piece for debugging
		/// </summary>
		/// <returns>Human readable view</returns>
		public override string ToString()
		{
			return $"[{Piece} ({X}, {Y})]";
		}
	}
}
