using System;
using System.Collections.Generic;
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

			var threat = pieceMap.CheckForDanger(info.Self.X, info.Self.Y, info.Self.Piece);
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



			while(moveList.Count > 0)
			{
				var indIndex = m_Random.Next(moveList.Count);

				var mutated = pieceMap.Mutate(moveList[indIndex]);
				var mutatedInfo = mutated.GetBoardInfo(team);
				if (mutated.CheckForDanger(mutatedInfo.Self.X, mutatedInfo.Self.Y, mutatedInfo.Self.Piece) != null)
				{
					moveList.RemoveAt(indIndex);
				}

				return moveList[indIndex];
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

							var attackord = map.CheckForDanger(move.TargetPosition?.x ?? 0, move.TargetPosition?.y ?? 0, friendly.Piece, move);
							if (attackord == null)
							{
								return move;
							}
							continue;
						}

						var mutated = map.Mutate(move);
						var attackor = mutated.CheckForDanger(info.Self.X, info.Self.Y, info.Self.Piece, move);

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
