using System.Diagnostics;
using AdvanceEngine.Models;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Logic.PieceLogic
{
	public class ZombieLogic
	{
		public static IEnumerator<Move> GetMoves(IPieceMap map, int x, int y, EPieceType piece, ETeam team)
		{
			var dir = team == ETeam.White ? -1 : 1;
			var self = map.GetPieceAtPosition(x, y);

			using (var moves = GetPotentialMoves(x, y, dir))
			{
				while (moves.MoveNext())
				{
					var current = moves.Current;
					Debug.WriteLine($"Test Move: [{x}, {y} -> {current.TargetX}, {current.TargetY}] [Attackable: {current.CanAttack}] [Movable: {current.CanMove}]");

					if (!map.IsValidCoordinate(current.TargetX, current.TargetY))
					{
						Debug.WriteLine("[Fail] Invalid Coords");
						continue;
					}

					var target = map.GetPieceAtPosition(current.TargetX, current.TargetY);

					if (target != null)
					{
						if (!current.CanAttack)
						{
							// Cannot attack
							Debug.WriteLine("[Fail] Occupied, cannot attack");
							continue;
						}

						if (target.Team == team)
						{
							Debug.WriteLine("[Fail] Occupied, same team");
							// Same team
							continue;
						}

						if (!CheckSpaces(current.MustBeEmpty, map))
						{
							// Move blocked
							Debug.WriteLine("[Fail] Occupied, movement blocked");
							continue;
						}

						if (map.IsProtected(current.TargetX, current.TargetY, team))
						{
							Debug.WriteLine("[Fail] Occupied, enemy protected");

							// protected by enemy sentinel
							continue;
						}

						Debug.WriteLine($"[Pass] Attack Move");
						MapMutator attackMutator = (IPiece?[,] map) =>
						{
							if (current.MovesOnAttack)
							{
								map[current.TargetX, current.TargetY] = self;
								map[x, y] = null;

							}
							else
							{
								map[current.TargetX, current.TargetY] = null;
							}
						};
						yield return new Move(0, -1, attackMutator)
						{
							IsAttack = true,
							Origin = (x, y),
							TargetPiece = target.PieceType,
							TargetPosition = (current.TargetX, current.TargetY)
						};
						continue;
					}

					if (!current.CanMove)
					{
						Debug.WriteLine($"[Fail] Not occupied, Cannot move");

						// Cannot move
						continue;
					}

					if (!CheckSpaces(current.MustBeEmpty, map))
					{
						// Move blocked
						Debug.WriteLine($"[Fail] Not occupied, movement blocked");
						continue;
					}

					Debug.WriteLine($"[Pass] Movement Move");

					MapMutator moveMutator = (IPiece?[,] map) =>
					{
						map[x, y] = null;
						map[current.TargetX, current.TargetY] = self;
					};
					yield return new Move(0, 0, moveMutator)
					{
						IsAttack = false,
						Origin = (x, y),
						TargetPiece = null,
						TargetPosition = (current.TargetX, current.TargetY)
					};
				}
			}
		}

		private static bool CheckSpaces((int x, int y)[]? mustBeEmpty, IPieceMap map)
		{
			if (mustBeEmpty == null)
			{
				return true;
			}

			foreach (var move in mustBeEmpty)
			{
				if (map.IsOcupied(move.x, move.y))
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Enumerates all possible moves for a piece with no conditional checking
		/// </summary>
		private static IEnumerator<PotentialMove> GetPotentialMoves(int x, int y, int dir)
		{

			// left
			yield return new PotentialMove()
			{
				CanAttack = true,
				CanMove = true,
				MovesOnAttack = true,
				MustBeEmpty = null,
				TargetX = x - 1,
				TargetY = y + (1 * dir)
			};

			// forward
			yield return new PotentialMove()
			{
				CanAttack = true,
				CanMove = true,
				MovesOnAttack = true,
				MustBeEmpty = null,
				TargetX = x,
				TargetY = y + (1 * dir)
			};

			// right
			yield return new PotentialMove()
			{
				CanAttack = true,
				CanMove = true,
				MovesOnAttack = true,
				MustBeEmpty = null,
				TargetX = x + 1,
				TargetY = y + (1 * dir)
			};

			// leap left
			yield return new PotentialMove()
			{
				CanAttack = true,
				CanMove = false,
				MovesOnAttack = true,
				MustBeEmpty = new (int x, int y)[] { (x - 1, y + (1 * dir)) },
				TargetX = x - 2,
				TargetY = y + (2 * dir)
			};

			// leap forward
			yield return new PotentialMove()
			{
				CanAttack = true,
				CanMove = false,
				MovesOnAttack = true,
				MustBeEmpty = new (int x, int y)[] { (x, y + (1 * dir)) },
				TargetX = x,
				TargetY = y + (2 * dir)
			};

			// leap right
			yield return new PotentialMove()
			{
				CanAttack = true,
				CanMove = false,
				MovesOnAttack = true,
				MustBeEmpty = new (int x, int y)[] { (x + 1, y + (1 * dir)) },
				TargetX = x + 2,
				TargetY = y + (2 * dir)
			};
		}
	}
}
