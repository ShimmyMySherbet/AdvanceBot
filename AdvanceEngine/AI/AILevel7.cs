using System;
using System.Collections.Generic;
using System.Linq;
using AdvanceEngine.Models;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Exceptions;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.AI
{
	public class AILevel7 : IAdvanceAI
	{
		public string Name => "AI Level 7";

		public IAdvanceAI EnemyPredictor { get; set; }
		public IAdvanceAI SelfPredictor { get; set; }

		private Random m_Random = new Random();

		public AILevel7(IAdvanceAI predictor)
		{
			EnemyPredictor = predictor;
			SelfPredictor = predictor;
		}

		public AILevel7(IAdvanceAI enemyPredictor, IAdvanceAI selfPredictor)
		{
			EnemyPredictor = enemyPredictor;
			SelfPredictor = selfPredictor;
		}

		public Move? DetermineMove(IPieceMap pieceMap, ETeam team)
		{
			var info = pieceMap.GetBoardInfo(team);

			var danger = pieceMap.CheckForDanger(info.Self.X, info.Self.Y, info.Self.Piece) != null;
			var currentMax = int.MinValue;
			var bestMoves = new List<Move>();

			foreach (var friendly in info.Friendly)
			{

				using (var moves = friendly.Piece.GetMoves(friendly.X, friendly.Y, pieceMap))
				{
					while (moves.MoveNext())
					{
						var move = moves.Current;

						if (move.TargetPiece == EPieceType.General)
						{
							Console.WriteLine("fuck");
						}

						if (danger)
						{
							// We're in check, only allow moves that get us out of check
							try
							{
								var dangerMutated = pieceMap.Mutate(move);
								if (dangerMutated.CheckState(team) != ECheckState.None)
								{
									continue;
								}
							}
							catch (InvalidOperationException)
							{
								continue;
							}
						}

						var mutated = pieceMap.Mutate(move);
						var mutatedCheck = mutated.CheckState(team.Enemy());
						if (mutatedCheck == ECheckState.Checkmate)
						{
							return move;
						}


						if (move.ScoreChange > currentMax)
						{
							bestMoves.Clear();
							currentMax = move.ScoreChange;
							bestMoves.Add(move);
						}
						else if (move.ScoreChange == currentMax)
						{
							bestMoves.Add(move);
						}
					}
				}
			}

			if (bestMoves.Count == 0)
				return null;

			if (bestMoves.Count == 1)
				return bestMoves[0];

			var futureEvaluations = new List<(Move move, (int evaluation, int discriminator) score)>();

			foreach (var move in bestMoves)
			{
				try
				{
					futureEvaluations.Add((move, EvaluateFutureValue(pieceMap, team, move)));
				}
				catch (Exception)
				{
					futureEvaluations.Add((move, (move.ScoreChange, 3)));
				}
			}
			var maxEvaluation = futureEvaluations.Max(x => x.score.evaluation);
			var highestOrder = futureEvaluations.Where(x => x.score.evaluation == maxEvaluation).ToArray();

			var highestDiscriminator = highestOrder.Max(x => x.score.discriminator);
			var discriminated = highestOrder.Where(x => x.score.discriminator == highestDiscriminator).ToArray();

			return discriminated[m_Random.Next(discriminated.Length)].move;
		}

		private (int score, int discriminator) EvaluateFutureValue(IPieceMap baseMap, ETeam team, Move move)
		{
			var moves = new List<Move>();
			var discriminator = 0;

			if (move.TargetPiece == EPieceType.General)
			{
				Console.WriteLine("fuck");
			}

			// our first move
			var mutated = baseMap.Mutate(move);
			moves.Add(move);

			var mutatedInfo = mutated.GetBoardInfo(team);
			if (mutated.CheckForDanger(mutatedInfo.Opponent.X, mutatedInfo.Opponent.Y, mutatedInfo.Opponent.Piece) != null)
			{
				// enemy is threatened 
				discriminator++;
			}

			// Opponents move
			try
			{
				var opponentMove = EnemyPredictor.DetermineMove(mutated, team.Enemy());
				if (opponentMove != null)
				{
					if (opponentMove.TargetPiece == EPieceType.General)
					{
						Console.WriteLine("fuck");
					}
					moves.Add(opponentMove);
					mutated = mutated.Mutate(opponentMove);
				}
			}
			catch (CheckmatedException)
			{
				// We checkmate the opponent
				return (int.MaxValue, discriminator);
			}

			// our response
			try
			{
				var selfMove = SelfPredictor.DetermineMove(mutated, team);

				if (selfMove != null)
				{
					if (selfMove.TargetPiece == EPieceType.General)
					{
						Console.WriteLine("fuck");
					}
					moves.Add(selfMove);
					mutated = mutated.Mutate(selfMove);
				}
			}
			catch (CheckmatedException)
			{
				// We're checkmated by the opponent
				return (int.MinValue, discriminator);
			}

			mutatedInfo = mutated.GetBoardInfo(team);
			if (mutated.CheckForDanger(mutatedInfo.Opponent.X, mutatedInfo.Opponent.Y, mutatedInfo.Opponent.Piece) != null)
			{
				// enemy is threatened 
				discriminator++;
			}

			return (mutated.GetTeamLead(team), discriminator);
		}
	}
}
