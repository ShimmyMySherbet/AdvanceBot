using System;

namespace AdvanceEngine.Models.Exceptions
{
	/// <summary>
	/// Raised when an unknown piece is read when loading a board state
	/// </summary>
	public class UnknownPieceException : Exception
	{
		/// <summary>
		/// Unknown Piece
		/// </summary>
		/// <param name="c">The unknown character read</param>
		public UnknownPieceException(char c) : base($"Unknown piece type for character '{c}'")
		{
		}
	}
}
