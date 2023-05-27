using AdvanceEngine.Logic;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Models
{
	/// <summary>
	/// Stores the piece state of the board, and provides fast high-performance lookups of pieces and positions.
	/// </summary>
	public readonly struct PieceMap : IPieceMap
	{
		/// <summary>
		/// Creates a default empty piece map
		/// </summary>
		public static PieceMap Default => new PieceMap(new IPiece?[9, 9]);

		/// <summary>
		/// Stores a 2D array that represents the positions of pieces on the board. Provides fast locational lookups of the board's state
		/// </summary>
		private readonly IPiece?[,] m_Map;

		public PieceMap(IPiece?[,] map)
		{
			m_Map = map;
		}

		public bool IsValidCoordinate(int x, int y)
		{
			var r = x >= 0 && x <= 8 && y >= 0 && y <= 8;
			return r;
		}

		public bool IsOcupied(int x, int y)
		{
			return m_Map[x, y] != null;
		}

		public IPiece? GetPieceAtPosition(int x, int y)
		{
			return m_Map[x, y];
		}

		/// <summary>
		/// Mutates the board state with a move
		/// </summary>
		/// <returns>A new piece map that represents the state of the board</returns>
		public IPieceMap Mutate(Move move)
		{
			return Mutate(move.Mutator);
		}

		public bool IsProtected(int x, int y, ETeam attacker)
		{
			return false;
		}

		public IPieceMap Mutate(MapMutator mutator)
		{
			// Clone the map
			var newMap = new IPiece?[AdvanceConstants.BoardSize_Width, AdvanceConstants.BoardSize_Height];
			Array.Copy(m_Map, newMap, m_Map.Length);

			// Mutate the new 2D map to represent the move
			mutator(newMap);

			return new PieceMap(newMap);
		}
	}
}
