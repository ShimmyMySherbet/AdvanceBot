namespace AdvanceEngine.Models
{
	public readonly struct BoardInfo
	{
		public PieceInfo Self { get; }

		public PieceInfo Opponent { get; }

		public IReadOnlyCollection<PieceInfo> Friendly { get; }

		public IReadOnlyCollection<PieceInfo> Hostile { get; }

		public BoardInfo(PieceInfo self, PieceInfo opponent, IReadOnlyCollection<PieceInfo> friendly, IReadOnlyCollection<PieceInfo> hostile)
		{
			Self = self;
			Opponent = opponent;
			Friendly = friendly;
			Hostile = hostile;
		}
	}
}
