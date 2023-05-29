using System;
using System.Collections.Generic;
using AdvanceEngine.Logic.Pieces;
using AdvanceEngine.Models;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Exceptions;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.AI
{
	public class AILevel5 : IAdvanceAI
	{
		public string Name => "AI Level 5";

		private Random m_Random = new Random();

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

		private Move? DetermineMoveChecked(IPieceMap pieceMap, ETeam team, List<(int x, int y, IPiece piece)> friendly, List<(int x, int y, IPiece piece)> enemy, (int x, int y, IPiece piece) self)
		{
			// need to get out of check!

			foreach (var a in friendly)
			{
				using (var moves = a.piece.GetMoves(a.x, a.y, pieceMap))
				{
					while (moves.MoveNext())
					{
						var move = moves.Current;
						if (a.piece is General)
						{
							// General cannot make move that puts it in danger, no need to check

							var attackord = pieceMap.CheckForDanger(move.TargetPosition?.x ?? 0, move.TargetPosition?.y ?? 0, team);
							if (attackord != null)
							{
								return move;
							}
							continue;
						}

						var mutated = pieceMap.Mutate(move);
						var attackor = mutated.CheckForDanger(self.x, self.y, team);

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
