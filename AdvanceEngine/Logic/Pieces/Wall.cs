using AdvanceEngine.Models;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Logic.Pieces
{
	public class Wall : IPiece
	{
		public string Name => "Wall";
		public int ScoreValue => 0;
		public ETeam Team { get; set; }
		public EPieceType PieceType => EPieceType.Wall;

		public IEnumerator<Move> GetMoves(int x, int y, IPieceMap map)
		{
			yield break;
		}
	}
}
