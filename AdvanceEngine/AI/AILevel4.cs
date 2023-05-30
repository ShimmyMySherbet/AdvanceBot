using System;
using System.Collections.Generic;
using AdvanceEngine.Logic.Pieces;
using AdvanceEngine.Models;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Exceptions;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.AI
{
	/// <summary>
	/// AI implemented to AI Level 4
	/// </summary>
	public class AILevel4 : IAdvanceAI
	{
		/// <summary>
		/// AI Model Name
		/// </summary>
		public string Name => "AI Level 4";

		private Random m_Random = new Random();

		/// <summary>
		/// Determines what move to make
		/// </summary>
		/// <param name="pieceMap">The board state</param>
		/// <param name="team">Playing side</param>
		/// <returns>Next move to make, if possible</returns>
		public Move? DetermineMove(IPieceMap pieceMap, ETeam team)
		{
			var info = pieceMap.GetBoardInfo(team);

			var threat = pieceMap.CheckForDanger(info.Self.X, info.Self.Y, info.Self);
			if (threat != null)
			{
				return DetermineMoveChecked(pieceMap, team, info);
			}

			var moveList = new List<Move>();
			foreach (var friendly in info.Friendly)
			{
				using (var moves = friendly.GetMoves(pieceMap))
				{
					while (moves.MoveNext())
					{
						moveList.Add(moves.Current);
					}
				}
			}

			while (moveList.Count > 0)
			{
				var indIndex = m_Random.Next(moveList.Count);

				var mutated = pieceMap.Mutate(moveList[indIndex]);
				var mutatedInfo = mutated.GetBoardInfo(team);
				if (mutated.CheckForDanger(mutatedInfo.Self.X, mutatedInfo.Self.Y, mutatedInfo.Self) != null)
				{
					moveList.RemoveAt(indIndex);
				}

				return moveList[indIndex];
			}

			return null;
		}

		/// <summary>
		/// Determiens what move to make in the event the player is in check.
		/// </summary>
		/// <param name="map">The board state</param>
		/// <param name="team">Playing side</param>
		/// <param name="info">Board metadata</param>
		/// <returns>The move to make, if any</returns>
		/// <exception cref="CheckmatedException">Raised when the player is in checkmate and the game has ended.</exception>
		public Move? DetermineMoveChecked(IPieceMap map, ETeam team, BoardInfo info)
		{
			foreach (var friendly in info.Friendly)
			{
				using (var moves = friendly.GetMoves(map))
				{
					while (moves.MoveNext())
					{
						var move = moves.Current;
						if (friendly is General)
						{
							// General cannot make move that puts it in danger, no need to check

							var attackord = map.CheckForDanger(move.TargetPosition?.x ?? 0, move.TargetPosition?.y ?? 0, friendly, move);
							if (attackord == null)
							{
								return move;
							}
							continue;
						}

						var mutated = map.Mutate(move);
						var attackor = mutated.CheckForDanger(info.Self.X, info.Self.Y, info.Self, move);

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
