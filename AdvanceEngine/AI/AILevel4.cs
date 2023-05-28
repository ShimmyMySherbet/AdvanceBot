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
			var info = pieceMap.GetBoardInfo(team);

			var threat = pieceMap.CheckForDanger(info.Self.X, info.Self.Y, team);
			if (threat != null)
			{
				return DetermineMoveChecked(pieceMap, team, info);
			}

			var moveList = new List<Move>();
			foreach(var friendly in info.Friendly)
			{
				using (var moves = friendly.Piece.GetMoves(friendly.X, friendly.Y, pieceMap))
				{
					while(moves.MoveNext())
					{
						moveList.Add(moves.Current);
					}
				}
			}

			if (moveList.Count > 0)
			{
				return moveList[m_Random.Next(moveList.Count)];
			}

			return null;
		}

		public Move? DetermineMoveChecked(IPieceMap map, ETeam team, BoardInfo info)
		{
			foreach (var friendly in info.Friendly)
			{
				using (var moves = friendly.Piece.GetMoves(friendly.X, friendly.Y, map))
				{
					while (moves.MoveNext())
					{
						var move = moves.Current;
						if (friendly.Piece is General)
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
						var attackor = mutated.CheckForDanger(info.Self.X, info.Self.Y, team);

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
