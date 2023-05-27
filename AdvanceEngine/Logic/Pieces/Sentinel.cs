using AdvanceEngine.Models;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Logic.Pieces
{
	public class Sentinel : IPiece
	{
		public string Name => "Sentinel";
		public int ScoreValue => 5;
		public ETeam Team { get; set; }
		public EPieceType PieceType => EPieceType.Sentinel;
		public Sentinel(ETeam team)
		{
			Team = team;
		}

		public IEnumerator<Move> GetMoves(int x, int y, IPieceMap map)
		{
			yield break;
		}
	}
}
