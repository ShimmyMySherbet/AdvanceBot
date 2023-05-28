using AdvanceEngine.Logic;
using AdvanceEngine.Logic.Pieces;
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

			var positions = new (int x, int y)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };

			foreach (var p in positions)
			{
				if (IsValidCoordinate(x + p.x, y + p.y))
				{
					var piece = GetPieceAtPosition(x + p.x, y + p.y);
					if (piece != null && piece.Team != attacker && piece is Sentinel)
					{
						return true;
					}
				}
			}
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

		public IPiece? CheckForDanger(int x, int y, ETeam team)
		{
			var attacker = team == ETeam.White ? ETeam.Black : ETeam.White;

			for (int px = 0; px < 9; px++)
			{
				for (int py = 0; py < 9; py++)
				{
					var piece = GetPieceAtPosition(px, py);

					if (piece != null && piece.Team == attacker)
					{
						using (var moves = piece.GetMoves(px, py, this, x, y))
						{
							while (moves.MoveNext())
							{
								return piece;
							}
						}
					}
				}
			}

			return null;
		}

		public ECheckState CheckState(ETeam team)
		{
			var info = GetBoardInfo(team);

			if (CheckForDanger(info.Self.X, info.Self.Y, team) == null)
			{
				return ECheckState.None;
			}

			foreach (var friendly in info.Friendly)
			{
				using (var moves = friendly.Piece.GetMoves(friendly.X, friendly.Y, this))
				{
					while (moves.MoveNext())
					{
						var move = moves.Current;

						if (friendly.Piece is General)
						{
							if (CheckForDanger(move.TargetPosition?.x ?? 0, move.TargetPosition?.y ?? 0, team) == null)
							{
								return ECheckState.Check;
							}
							continue;
						}

						var mutated = Mutate(move);
						if (mutated.CheckForDanger(friendly.X, friendly.Y, team) == null)
						{
							return ECheckState.Check;
						}
					}
				}
			}
			return ECheckState.Checkmate;
		}

		public BoardInfo GetBoardInfo(ETeam team)
		{
			PieceInfo? self = null;
			PieceInfo? opponent = null;

			var friendly = new List<PieceInfo>();
			var hostile = new List<PieceInfo>();

			for (int x = 0; x < 9; x++)
			{
				for (int y = 0; y < 9; y++)
				{
					var piece = GetPieceAtPosition(x, y);
					if (piece != null)
					{
						if (piece.Team == ETeam.Neutral)
							continue;

						if (piece.Team == team)
						{
							friendly.Add(new PieceInfo(x, y, piece));

							if (piece is General)
							{
								self = new PieceInfo(x, y, piece);
							}
						}
						else
						{
							hostile.Add(new PieceInfo(x, y, piece));
							if (piece is General)
							{
								opponent = new PieceInfo(x, y, piece);
							}
						}
					}
				}
			}

			if (self == null || opponent == null)
			{
				throw new InvalidOperationException("Invalid board state");
			}
			return new BoardInfo(self.Value, opponent.Value, friendly, hostile);
		}
	}
}