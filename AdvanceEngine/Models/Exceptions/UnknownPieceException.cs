using System;

namespace AdvanceEngine.Models.Exceptions
{
	public class UnknownPieceException : Exception
	{
		public UnknownPieceException(char c) : base($"Unknown piece type for character '{c}'")
		{
		}
	}
}
