﻿using AdvanceEngine.Models;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Exceptions;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.AI
{
	public class AILevel6 : IAdvanceAI
	{
		public string Name => "AI Level 6";

		private Random m_Random = new Random();

		public Move? DetermineMove(IPieceMap pieceMap, ETeam team)
		{
			var state = pieceMap.CheckState(team);
			var enemy = team == ETeam.White ? ETeam.Black : ETeam.White;

			if (state == ECheckState.Checkmate)
			{
				throw new CheckmatedException(team);
			}
			else if (state == ECheckState.Check)
			{
				return DetermineMoveCheck(pieceMap, team);
			}

			var info = pieceMap.GetBoardInfo(team);

			var allMoves = new List<(Move move, ECheckState state)>();
			foreach (var piece in info.Friendly)
			{
				using (var moves = piece.Piece.GetMoves(piece.X, piece.Y, pieceMap))
				{
					while (moves.MoveNext())
					{
						var move = moves.Current;

						var mutated = pieceMap.Mutate(move);

						var checkState = mutated.CheckState(enemy);

						if (checkState == ECheckState.Checkmate)
						{
							return move;
						}

						// total shift in score

						allMoves.Add((move, checkState));
					}
				}
			}

			if (allMoves.Count == 0)
			{
				// no moves possible
				return null;
			}

			// Selects a move with the max score increase. Prefers moves that threaten the opponent.
			var sorted = allMoves.OrderByDescending(x => x.move.ScoreChange).ToArray();
			var max = sorted[0];
			var maxed = sorted.Where(x => x.move.ScoreChange == max.move.ScoreChange).ToArray();

			foreach (var m in maxed)
			{
				if (m.state == ECheckState.Check)
				{
					return m.move;
				}
			}

			var mmaxed = sorted.Where(x => x.move.ScoreChange == max.move.ScoreChange).ToArray();
			return maxed[m_Random.Next(maxed.Length)].move;
		}

		private Move? DetermineMoveCheck(IPieceMap pieceMap, ETeam team)
		{
			var info = pieceMap.GetBoardInfo(team);

			var allMoves = new List<Move>();
			foreach (var piece in info.Friendly)
			{
				using (var moves = piece.Piece.GetMoves(piece.X, piece.Y, pieceMap))
				{
					while (moves.MoveNext())
					{
						var move = moves.Current;

						var mutated = pieceMap.Mutate(move);

						var checkState = mutated.CheckState(team);

						if (checkState == ECheckState.None)
						{
							allMoves.Add(move);
						}
					}
				}
			}

			if (allMoves.Count == 0)
			{
				throw new CheckmatedException(team);
			}

			return allMoves.OrderByDescending(x => x.ScoreChange).FirstOrDefault();

		}
	}
}
