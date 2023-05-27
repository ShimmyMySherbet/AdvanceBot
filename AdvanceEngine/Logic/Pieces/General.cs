using AdvanceEngine.Models;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Logic.Pieces
{
	public class General : IPiece
	{
		public string Name => "General";
		public int ScoreValue => 999999;
		public ETeam Team { get; set; }
		public EPieceType PieceType => EPieceType.General;

		public General(ETeam team)
		{
			Team = team;
		}

		public IEnumerator<Move> GetMoves(int x, int y, IPieceMap map)
		{
			yield break;
		}
	}
}
