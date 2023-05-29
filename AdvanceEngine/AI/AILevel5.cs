using System;
using System.Collections.Generic;
using AdvanceEngine.Logic.Pieces;
using AdvanceEngine.Models;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.AI
{
	/// <summary>
	/// Advance AI implemented to AI Level 5
	/// </summary>
	public class AILevel5 : IAdvanceAI
	{
		/// <summary>
		/// AI Model Name
		/// </summary>
		public string Name => "AI Level 5";

		private Random m_Random = new Random();

		/// <summary>
		/// determines what move to make, if possible
		/// </summary>
		/// <param name="pieceMap">Board state</param>
		/// <param name="team">Playing side</param>
		/// <returns>The next move to make, if possible</returns>
		/// <exception cref="InvalidOperationException">Raised when the current player is checkmated</exception>
		public Move? DetermineMove(IPieceMap pieceMap, ETeam team)
		{
			var enemy = new List<(int x, int y, IPiece piece)>();
			var friendly = new List<(int x, int y, IPiece piece)>();

			var enemyTeam = team == ETeam.White ? ETeam.Black : ETeam.White;

			(int x, int y, IPiece? piece) self = (0, 0, null);
			(int x, int y, IPiece? piece) opponent = (0, 0, null);

			for (int x = 0; x < 9; x++)
			{
				for (int y = 0; y < 9; y++)
				{
					var p = pieceMap.GetPieceAtPosition(x, y);

					if (p != null)
					{
						if (p.Team == ETeam.Neutral)
							continue;

						if (p.Team == team)
						{
							friendly.Add((x, y, p));
							if (p is General)
							{
								self = (x, y, p);
							}
						}
						else
						{
							enemy.Add((x, y, p));
							if (p is General)
							{
								opponent = (x, y, p);
							}
						}
					}
				}
			}

			if (self.piece == null || opponent.piece == null)
			{
				throw new InvalidOperationException("Invalid board state");
			}

			var threateningMoves = new List<Move>();
			var basicMoves = new List<Move>();

			foreach (var piece in friendly)
			{

				using (var moves = piece.piece.GetMoves(piece.x, piece.y, pieceMap))
				{
					while (moves.MoveNext())
					{
						var move = moves.Current;

						var mutated = pieceMap.Mutate(move);

						switch (mutated.CheckState(enemyTeam))
						{
							case ECheckState.Checkmate:
								return move;
							case ECheckState.Check:
								threateningMoves.Add(move);
								break;
							case ECheckState.None:
								basicMoves.Add(move);
								break;
						}
					}
				}
			}

			if (threateningMoves.Count > 0)
			{
				return threateningMoves[m_Random.Next(threateningMoves.Count)];
			}
			else if (basicMoves.Count > 0)
			{
				return basicMoves[m_Random.Next(basicMoves.Count)];
			}

			// no possible moves
			return null;
		}
	}
}
