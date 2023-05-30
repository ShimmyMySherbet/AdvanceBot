﻿using System.Collections.Generic;
using AdvanceEngine.Models;
using AdvanceEngine.Models.Attributes;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Logic.Pieces
{
	/// <summary>
	/// Sentinel Advance AI piece
	/// </summary>
	[AdvancePiece(value: 6, EPieceType.Sentinel)]
	public class Sentinel : Piece
	{
		/// <summary>
		/// Sentinel
		/// </summary>
		/// <param name="team">Initial Team</param>
		/// <param name="x">X Coordinate</param>
		/// <param name="y">Y Coordinate</param>
		public Sentinel(ETeam team, int x, int y) : base(team, x, y) { }

		/// <summary>
		/// Converts the piece to another team
		/// </summary>
		/// <param name="team">Team to convert to</param>
		/// <param name="x">New X Coordinate</param>
		/// <param name="y">New Y Coordinate</param>
		/// <returns>A new instance of the piece in the desired team</returns>
		public override IPiece Mutate(ETeam team, int x, int y) => new Sentinel(team, x, y);

		/// <summary>
		/// Defines potential moves for this piece
		/// </summary>
		/// <param name="x">Piece X coordinate</param>
		/// <param name="y">Piece Y coordinate</param>
		/// <param name="team">The team of the playing team</param>
		/// <returns>An enumeration of potential moves</returns>
		public override IEnumerator<PotentialMove> GetMoveDefinitions(int x, int y, ETeam team)
		{
			yield return new PotentialMove(x - 2, y - 1);
			yield return new PotentialMove(x - 1, y - 2);
			yield return new PotentialMove(x + 1, y - 2);
			yield return new PotentialMove(x + 2, y - 1);

			yield return new PotentialMove(x + 2, y + 1);
			yield return new PotentialMove(x + 1, y + 2);
			yield return new PotentialMove(x - 1, y + 2);
			yield return new PotentialMove(x - 2, y + 1);
		}
	}
}
