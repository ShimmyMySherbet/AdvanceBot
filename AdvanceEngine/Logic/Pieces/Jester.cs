using AdvanceEngine.Models;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Logic.Pieces
{
	public class Jester : IPiece
	{
		public string Name => "Jester";
		public int ScoreValue => 3;
		public ETeam Team { get; set; }
		public EPieceType PieceType => EPieceType.Jester;

		public Jester(ETeam team)
		{
			Team = team;
		}

		public IEnumerator<Move> GetMoves(int x, int y, IPieceMap map)
		{
			yield break;
		}
	}
}
