using AdvanceEngine.Logic.PieceLogic;
using AdvanceEngine.Models.Enums;

namespace AdvanceEngine.Models.Interfaces
{
	public interface IPiece
	{
		public int ScoreValue { get; }

		public ETeam Team { get; }

		public EPieceType PieceType { get; }

		public bool Convertable { get; }

		public IEnumerator<Move> GetMoves(int x, int y, IPieceMap map, int filterX = -1, int filyerY = -1);

		public IPiece Convert(ETeam team);
	}
}
