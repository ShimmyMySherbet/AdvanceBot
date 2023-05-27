using AdvanceEngine.Models;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Logic.Pieces
{
	public class Catapult : IPiece
	{
		public string Name => "Catapult";
		public int ScoreValue => 6;
		public ETeam Team { get; set; }
		public EPieceType PieceType => EPieceType.Catapult;

		public Catapult(ETeam team)
		{
			Team = team;
		}

		public IEnumerator<Move> GetMoves(int x, int y, IPieceMap map)
		{
			yield break;
		}
	}
}
