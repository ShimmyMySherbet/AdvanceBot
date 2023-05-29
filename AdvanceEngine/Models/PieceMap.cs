using System;
using System.Collections.Generic;
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

		/// <summary>
		/// An iterable collection of all black pieces on the baord
		/// </summary>
		public IReadOnlyList<IPiece> BlackPieces { get; }

		/// <summary>
		/// An iterable collection of all white pieces on the board
		/// </summary>
		public IReadOnlyList<IPiece> WhitePieces { get; }


		public IPiece BlackGeneral { get; }

		public IPiece WhiteGeneral { get; }

		/// <summary>
		/// Piece Map
		/// </summary>
		/// <param name="map">The board state</param>
		public PieceMap(IPiece?[,] map)
		{
			m_Map = map;

			var black = new List<IPiece>(16);
			var white = new List<IPiece>(16);

			IPiece? blackGeneral = null;
			IPiece? whiteGeneral = null;

			IPiece? piece;
			for(int x = 0; x < 9; x++)
			{
				for(int y = 0; y < 9; y++)
				{
					piece = map[x, y];
					if (piece != null)
					{
						switch(piece.Team)
						{
							case ETeam.Black:
								black.Add(piece);
								
								if (piece is General)
									blackGeneral = piece;
								break;

							case ETeam.White:
								white.Add(piece);

								if (piece is General)
									whiteGeneral = piece;
								break;
						}
					}
				}
			}

			if (blackGeneral == null || whiteGeneral == null)
			{
				throw new InvalidOperationException("Invalid board state");
			}

			BlackGeneral = blackGeneral;
			WhiteGeneral = whiteGeneral;

			BlackPieces = black.AsReadOnly();
			WhitePieces = white.AsReadOnly();
		}

		/// <summary>
		/// Tests if a coordinate is valid
		/// </summary>
		/// <param name="x">X Coordinate</param>
		/// <param name="y">Y Coordinate</param>
		/// <returns>true when the coordinate is valid</returns>
		public bool IsValidCoordinate(int x, int y)
		{
			var r = x >= 0 && x <= 8 && y >= 0 && y <= 8;
			return r;
		}

		/// <summary>
		/// Tests if a coordinate is occupied
		/// </summary>
		/// <param name="x">X Coordinate</param>
		/// <param name="y">Y Coordinate</param>
		/// <returns>True if the location is occupied by a peice</returns>
		public bool IsOccupied(int x, int y)
		{
			return m_Map[x, y] != null;
		}

		/// <summary>
		/// Gets a piece at a position, or null
		/// </summary>
		/// <param name="x">X Coordinate</param>
		/// <param name="y">Y Coordinate</param>
		/// <returns>The occupying piece or null</returns>
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

		/// <summary>
		/// Tests if a location is protected by a sentinel
		/// </summary>
		/// <param name="x">X Coordinate</param>
		/// <param name="y">Y Coordinate</param>
		/// <param name="attacker">The attacking team</param>
		/// <returns>True if the piece at the specified coordinates is protected</returns>
		public bool IsProtected(int x, int y, ETeam attacker)
		{

			var positions = new (int x, int y)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };

			foreach (var p in positions)
			{
				if (IsValidCoordinate(x + p.x, y + p.y))
				{
					var piece = m_Map[x + p.x, y + p.y];
					if (piece != null && piece.Team != attacker && piece is Sentinel)
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Mutates the board
		/// </summary>
		/// <param name="mutator">Mutator to apply</param>
		/// <returns>A new instance of the board with the mutator applied</returns>
		public IPieceMap Mutate(MapMutator mutator)
		{
			// Clone the map
			var newMap = new IPiece?[9, 9];
			Array.Copy(m_Map, newMap, m_Map.Length);

			// Mutate the new 2D map to represent the move
			mutator(newMap);

			return new PieceMap(newMap);
		}

		/// <summary>
		/// Checks if a piece at a position is in danger
		/// </summary>
		/// <param name="x">X Coordinate to test</param>
		/// <param name="y">Y Coordinate to test</param>
		/// <param name="piece">The piece at the coordinates</param>
		/// <param name="move">Optionally, a move to simulate before testing safety</param>
		/// <returns>The piece threatening the location, or <see langword="null"/> if it is safe</returns>
		public IPiece? CheckForDanger(int x, int y, IPiece piece, Move? move = null)
		{

			var mutated = move != null ? Mutate(move) : this;

			for (int locX = 0; locX < 9; locX++)
			{
				for (int locY = 0; locY < 9; locY++)
				{
					var lookupPiece = mutated.GetPieceAtPosition(locX, locY);

					if (lookupPiece != null && lookupPiece.Team == piece.Team.Enemy())
					{
						using (var enemyMoves = lookupPiece.GetMoves(locX, locY, mutated, x, y, true))
						{
							while (enemyMoves.MoveNext())
							{
								return lookupPiece;
							}
						}
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Checks the check-state for a team
		/// </summary>
		/// <param name="team">The team to test</param>
		/// <returns>The check-state for the specified team</returns>
		public ECheckState CheckState(ETeam team)
		{
			var info = GetBoardInfo(team);

			var inDanger = CheckForDanger(info.Self.X, info.Self.Y, info.Self.Piece) != null;

			foreach (var friendly in info.Friendly)
			{
				using (var moves = friendly.Piece.GetMoves(friendly.X, friendly.Y, this))
				{
					while (moves.MoveNext())
					{
						var move = moves.Current;
						var mutated = Mutate(move);
						var subInfo = mutated.GetBoardInfo(team);

						if (mutated.CheckForDanger(subInfo.Self.X, subInfo.Self.Y, subInfo.Self.Piece) == null)
						{
							// can make a move that is safe

							if (inDanger)
							{
								return ECheckState.Check;
							}
							return ECheckState.None;
						}
					}
				}
			}

			return ECheckState.Checkmate;
		}

		/// <summary>
		/// Finds all pieces on the board and provides the information in a side-biased structure
		/// </summary>
		/// <param name="team">The point-of-view team</param>
		/// <returns>Board info for this board</returns>
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

		/// <summary>
		/// Gets the total piece value for a team
		/// </summary>
		/// <param name="team">Team to check</param>
		/// <returns>The total score value of a team</returns>
		public int GetTeamValue(ETeam team)
		{
			var value = 0;
			for (int x = 0; x < 9; x++)
			{
				for (int y = 0; y < 9; y++)
				{
					var piece = m_Map[x, y];
					if (piece != null && piece.Team == team)
					{
						value += piece.ScoreValue;
					}
				}
			}
			return value;
		}

		/// <summary>
		/// Gets the score difference between both teams.
		/// </summary>
		/// <param name="team">Team to check</param>
		/// <returns>The score difference. Positive indicating the team is winning</returns>
		public int GetTeamLead(ETeam team)
		{
			var value = 0;
			for (int x = 0; x < 9; x++)
			{
				for (int y = 0; y < 9; y++)
				{
					var piece = m_Map[x, y];
					if (piece != null)
					{
						if (piece.Team == team)
						{
							value += piece.ScoreValue;
						}
						else
						{
							value -= piece.ScoreValue;
						}
					}
				}
			}
			return value;
		}
	}
}