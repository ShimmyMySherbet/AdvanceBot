using AdvanceEngine.Logic.PieceLogic;
using AdvanceEngine.Models;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Logic.Pieces
{
	public class Zombie : IPiece
	{
		public string Name => "Zombie";
		public int ScoreValue => 1;
		public ETeam Team { get; set; }
		public EPieceType PieceType => EPieceType.Zombie;

		public Zombie(ETeam team)
		{
			Team = team;
		}

		public IEnumerator<Move> GetMoves(int x, int y, IPieceMap map)
		{
			return ZombieLogic.GetMoves(map, x, y, EPieceType.Catapult, Team);
		}
	}
}
