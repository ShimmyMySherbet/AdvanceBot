using AdvanceEngine.Models.Enums;

namespace AdvanceEngine.Models.Attributes
{
	public sealed class AdvancePieceAttribute : Attribute
	{
		public int Value { get; }
		public EPieceType Type { get; }

		public AdvancePieceAttribute(int value, EPieceType type)
		{
			Value = value;
			Type = type;
		}
	}
}
