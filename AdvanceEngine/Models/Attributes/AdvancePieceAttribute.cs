using AdvanceEngine.Models.Enums;

namespace AdvanceEngine.Models.Attributes
{
	/// <summary>
	/// Class decorator to specify piece type and value.
	/// Applied to classes inheriting <seealso cref="Piece"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public sealed class AdvancePieceAttribute : Attribute
	{
		public int Value { get; }
		public EPieceType Type { get; }
		public bool Convertable { get; }

		public AdvancePieceAttribute(int value, EPieceType type, bool convertable = true)
		{
			Value = value;
			Type = type;
			Convertable = convertable;
		}
	}
}
