using System.Collections.Generic;
using AdvanceEngine.Logic.Pieces;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Models
{
	/// <summary>
	/// An inheritable class providing common definitions and functionality for pieces and piece logic
	/// </summary>
	public abstract partial class Piece : IPiece
	{
		/// <summary>
		/// Defines the potential moves a piece can make
		/// </summary>
		/// <param name="x">Piece X coordinate</param>
		/// <param name="y">Piece Y coordinate</param>
		/// <param name="dir">a value 1 or -1. Where 1 represents White, and -1 represents Black.</param>
		/// <returns>An enumeration of potential moves with no move validation.</returns>
		public abstract IEnumerator<PotentialMove> GetMoveDefinitions(int x, int y, int dir);

		/// <summary>
		/// Determines all the moves the piece can make
		/// </summary>
		/// <param name="map">The piece map that represents the board state</param>
		/// <param name="x">Piece x coodinate</param>
		/// <param name="y">Piece y coodinate</param>
		/// <param name="filterX">Filters moves to only ones targeting positions with the specified X value</param>
		/// <param name="filterY">Filters moves to only ones targeting positions with the specified Y value</param>
		/// <returns>An enumeration of possible moves, with their board mutator</returns>
		public virtual IEnumerator<Move> GetMoves(int x, int y, IPieceMap map, int filterX = -1, int filterY = -1, bool ignoreSafety = false)
		{
			var dir = Team == ETeam.White ? -1 : 1;
			var self = map.GetPieceAtPosition(x, y);

			if (PieceType == EPieceType.Miner)
			{
				System.Console.WriteLine();
			}

			using (var moves = GetMoveDefinitions(x, y, dir))
			{
				while (moves.MoveNext())
				{
					var current = moves.Current;

					if (!map.IsValidCoordinate(current.TargetX, current.TargetY))
					{
						continue;
					}

					if (filterX != -1 && current.TargetX != filterX)
					{
						continue;
					}

					if (filterY != -1 && current.TargetY != filterY)
					{
						continue;
					}

					var target = map.GetPieceAtPosition(current.TargetX, current.TargetY);

					if (target != null)
					{
						if (current.SwapsPositions && target.Team == Team)
						{
							MapMutator mutator = (IPiece?[,] map) =>
							{
								map[current.TargetX, current.TargetY] = self;
								map[x, y] = target;
							};
							yield return new Move(0, 0, mutator, EMoveType.Move, self, current)
							{
								Origin = (x, y),
								TargetPosition = (current.TargetX, current.TargetY),
								TargetPiece = target.PieceType
							};
						}

						if (!current.CanAttack)
						{
							// Cannot attack
							continue;
						}

						if (current.IsBuildMove)
						{
							continue;
						}

						if (target.Team == Team)
						{
							continue;
						}

						if (target.PieceType == EPieceType.Wall && !current.CanBreakWalls)
						{
							continue;
						}

						if (!CheckSpaces(current.MustBeEmpty, map))
						{
							// Move blocked
							continue;
						}

						if (map.IsProtected(current.TargetX, current.TargetY, Team))
						{
							continue;
						}

						if (current.ConvertsEnemy && !target.Convertable)
						{
							continue;
						}

						MapMutator attackMutator = (IPiece?[,] map) =>
						{
							if (current.ConvertsEnemy)
							{
								map[current.TargetX, current.TargetY] = target.Convert(Team);
								return;
							}

							if (current.MovesOnAttack)
							{
								map[current.TargetX, current.TargetY] = self;
								map[x, y] = null;
								return;
							}

							map[current.TargetX, current.TargetY] = null;
						};

						if (!ignoreSafety && current.MustNotBeCaptured)
						{
							var newBoard = map.Mutate(attackMutator);
							var danger = newBoard.CheckForDanger(current.TargetX, current.TargetY, this);
							if (danger != null)
							{
								// Danger!
								continue;
							}
						}

						var ownScore = current.ConvertsEnemy ? target.ScoreValue : 0;

						yield return new Move(ownScore, target.ScoreValue * -1, attackMutator, current.ConvertsEnemy ? EMoveType.Convert : EMoveType.Attack, self, current)
						{
							Origin = (x, y),
							TargetPiece = target.PieceType,
							TargetPosition = (current.TargetX, current.TargetY)
						};
						continue;
					}

					if (!CheckSpaces(current.MustBeEmpty, map))
					{
						// Move blocked
						continue;
					}

					if (current.IsBuildMove)
					{

						MapMutator mutator = (IPiece?[,] map) =>
						{
							map[current.TargetX, current.TargetY] = new Wall();
						};

						if (!ignoreSafety && current.MustNotBeCaptured)
						{
							var newBoard = map.Mutate(mutator);
							var danger = newBoard.CheckForDanger(current.TargetX, current.TargetY, this);
							if (danger != null)
							{
								// Danger!
								continue;
							}
						}

						yield return new Move(0, 0, mutator, EMoveType.Build, self, current)
						{
							Origin = (x, y),
							TargetPosition = (current.TargetX, current.TargetY)
						};
						continue;
					}

					if (!current.CanMove)
					{

						// Cannot move
						continue;
					}

					MapMutator moveMutator = (IPiece?[,] map) =>
					{
						map[x, y] = null;
						map[current.TargetX, current.TargetY] = self;
					};

					if (current.MustNotBeCaptured)
					{
						var newBoard = map.Mutate(moveMutator);
						var danger = newBoard.CheckForDanger(current.TargetX, current.TargetY, this);
						if (danger != null)
						{
							// Danger!
							continue;
						}
					}

					yield return new Move(0, 0, moveMutator, EMoveType.Move, self, current)
					{
						Origin = (x, y),
						TargetPiece = null,
						TargetPosition = (current.TargetX, current.TargetY)
					};
				}
			}
		}

		/// <summary>
		/// Checks if the 'Must be empty' requirements of a move are fufilled
		/// </summary>
		/// <param name="mustBeEmpty">coordinates to check</param>
		/// <param name="map">The piece map representing the board state</param>
		/// <returns>True if the listed spaces are empty. False if the move is obstructed</returns>
		private bool CheckSpaces((int x, int y)[]? mustBeEmpty, IPieceMap map)
		{
			if (mustBeEmpty == null)
			{
				return true;
			}

			foreach (var move in mustBeEmpty)
			{
				if (map.IsOccupied(move.x, move.y))
				{
					return false;
				}
			}

			return true;
		}
	}
}
