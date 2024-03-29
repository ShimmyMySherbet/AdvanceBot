﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AdvanceEngine.Models;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Exceptions;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.AI
{
	/// <summary>
	/// Advance AI implemented to AI Level 6
	/// </summary>
	public class AILevel6 : IAdvanceAI
	{
		/// <summary>
		/// AI Model Name
		/// </summary>
		public string Name => "AI Level 6";

		private Random m_Random = new Random();

		/// <summary>
		/// Determines what move to make next, if possible.
		/// </summary>
		/// <param name="pieceMap">Current board state</param>
		/// <param name="team">Playing side</param>
		/// <returns>The next move to make, if possible</returns>
		/// <exception cref="CheckmatedException">Raised when the current player is checkmated</exception>
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

			var goodMoves = new List<(Move move, ECheckState enemyState)>();
			var badMoves = new List<(Move move, ECheckState enemyState)>();
			foreach (var piece in info.Friendly)
			{
				using (var moves = piece.GetMoves(pieceMap))
				{
					while (moves.MoveNext())
					{
						var move = moves.Current;

						if (move.TargetPiece == EPieceType.General)
						{
							continue;
						}

						var mutated = pieceMap.Mutate(move);

						var enemyCheckState = mutated.CheckState(enemy);

						if (enemyCheckState == ECheckState.Checkmate)
						{
							return move;
						}

						var selfCheck = mutated.CheckState(team);

						if (selfCheck == ECheckState.Check || selfCheck == ECheckState.Checkmate)
						{
							badMoves.Add((move, enemyCheckState));
						}

						// total shift in score
						goodMoves.Add((move, enemyCheckState));
					}
				}
			}

			if (goodMoves.Count == 0)
			{
				goodMoves = badMoves;
			}

			if (goodMoves.Count == 0)
			{
				// no moves possible
				return null;
			}

			// Selects a move with the max score increase. Prefers moves that threaten the opponent.
			var sorted = goodMoves.OrderByDescending(x => x.move.ScoreChange).ToArray();
			var max = sorted[0];
			var maxed = sorted.Where(x => x.move.ScoreChange == max.move.ScoreChange).ToArray();

			foreach (var m in maxed)
			{
				if (m.enemyState == ECheckState.Check)
				{
					return m.move;
				}
			}

			var mmaxed = sorted.Where(x => x.move.ScoreChange == max.move.ScoreChange).ToArray();
			return maxed[m_Random.Next(maxed.Length)].move;
		}

		/// <summary>
		/// Determines what move to make when the player is in check
		/// </summary>
		/// <param name="pieceMap">The board state</param>
		/// <param name="team">Currently playing side</param>
		/// <returns>The next move to make, if possible</returns>
		/// <exception cref="CheckmatedException">Raised when the current side is checkmated, and cannot move</exception>
		private Move? DetermineMoveCheck(IPieceMap pieceMap, ETeam team)
		{
			var info = pieceMap.GetBoardInfo(team);

			var allMoves = new List<Move>();
			foreach (var piece in info.Friendly)
			{
				using (var moves = piece.GetMoves(pieceMap))
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
