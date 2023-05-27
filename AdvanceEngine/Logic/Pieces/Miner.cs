﻿using AdvanceEngine.Models;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Logic.Pieces
{
	public class Miner : IPiece
	{
		public string Name => "Miner";
		public int ScoreValue => 4;
		public ETeam Team { get; set; }
		public EPieceType PieceType => EPieceType.Miner;

		public Miner(ETeam team)
		{
			Team = team;
		}

		public IEnumerator<Move> GetMoves(int x, int y, IPieceMap map)
		{
			yield break;
		}
	}
}