namespace AdvanceEngine.Models
{
	/// <summary>
	/// Encapulates a position on the chess board
	/// </summary>
	public struct Position
	{
		public int X { get; set; }
		public int Y { get; set; }

		public Position(int x, int y)
		{
			X = x;
			Y = y;
		}
	}
}
