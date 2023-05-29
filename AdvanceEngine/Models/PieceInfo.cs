using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Models
{
	public struct PieceInfo
	{
		public int X { get; }

		public int Y { get; }

		public IPiece Piece { get; }

		public PieceInfo(int x, int y, IPiece piece)
		{
			X = x;
			Y = y;
			Piece = piece;
		}

		public override string ToString()
		{
			return $"[{Piece} ({X}, {Y})]";
		}
	}
}
