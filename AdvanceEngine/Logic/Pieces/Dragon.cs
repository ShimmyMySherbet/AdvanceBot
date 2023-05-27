using AdvanceEngine.Models;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Logic.Pieces
{
	public class Dragon : IPiece
	{
		public string Name => "Dragon";
		public int ScoreValue => 7;
		public ETeam Team { get; set; }
		public EPieceType PieceType => EPieceType.Dragon;

		public Dragon(ETeam team)
		{
			Team = team;
		}

		public IEnumerator<Move> GetMoves(int x, int y, IPieceMap map)
		{
			yield break;
		}
	}
}
