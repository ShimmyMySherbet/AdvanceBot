using AdvanceEngine.Logic.PieceLogic;
using AdvanceEngine.Models.Enums;

namespace AdvanceEngine.Models.Interfaces
{
	public interface IPiece
	{
		public int ScoreValue { get; }

		public ETeam Team { get; set; }

		public EPieceType PieceType { get; }

		public IEnumerator<Move> GetMoves(int x, int y, IPieceMap map);
	}
}
