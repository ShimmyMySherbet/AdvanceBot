using AdvanceEngine.Logic.Pieces;
using AdvanceEngine.Models;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Exceptions;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.AI
{
	public class AILevel4 : IAdvanceAI
	{
		private Random m_Random = new Random();

		public string Name => "AI Level 4";

		public Move? DetermineMove(IPieceMap pieceMap, ETeam team)
		{
			IPiece? selfGeneral = null;
			(int x, int y) self = (0, 0);
			var pieces = new List<(IPiece piece, int x, int y)>(18);

			for (int x = 0; x < 9; x++)
			{
				for (int y = 0; y < 9; y++)
				{
					var p = pieceMap.GetPieceAtPosition(x, y);
					if (p != null && p.Team == team)
					{
						pieces.Add((p, x, y));
						if (p is General)
						{
							selfGeneral = p;
							self = (x, y);
						}
					}
				}
			}

			if (selfGeneral != null)
			{
				var threat = pieceMap.CheckForDanger(self.x, self.y, team);

				if (threat != null)
				{
					return DetermineMoveChecked(pieceMap, team, selfGeneral, self.x, self.y, pieces);
				}
			}

			var moveList = new List<Move>();
			while (pieces.Count > 0)
			{
				moveList.Clear();
				var index = m_Random.Next(pieces.Count);
				var piece = pieces[index];
				var moves = piece.piece.GetMoves(piece.x, piece.y, pieceMap);

				using (moves)
				{
					while (moves.MoveNext())
					{
						moveList.Add(moves.Current);
					}
				}

				if (moveList.Count == 0)
				{
					continue;
				}

				var moveIndex = m_Random.Next(moveList.Count);
				return moveList[moveIndex];
			}

			return null;
		}

		public Move? DetermineMoveChecked(IPieceMap map, ETeam team, IPiece general, int x, int y, List<(IPiece piece, int x, int y)> army)
		{
			foreach (var a in army)
			{
				using (var moves = a.piece.GetMoves(a.x, a.y, map))
				{
					while (moves.MoveNext())
					{
						var move = moves.Current;
						if (a.piece is General)
						{
							// General cannot make move that puts it in danger, no need to check


							var attackord = map.CheckForDanger(move.TargetPosition?.x ?? 0, move.TargetPosition?.y ?? 0, team);
							if (attackord != null)
							{
								return move;
							}
							continue;
						}

						var mutated = map.Mutate(move);
						var attackor = mutated.CheckForDanger(x, y, team);

						if (attackor == null)
						{
							// move neutralizes threat
							return move;
						}
					}
				}
			}

			// Checkmated
			throw new CheckmatedException(team);
		}
	}
}
