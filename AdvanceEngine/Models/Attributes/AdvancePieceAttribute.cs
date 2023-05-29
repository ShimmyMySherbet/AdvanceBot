using System;
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
		/// <summary>
		/// The piece score value
		/// </summary>
		public int Value { get; }

		/// <summary>
		/// The piece type
		/// </summary>
		public EPieceType Type { get; }

		/// <summary>
		/// Specifies if this piece can be converted to the other team
		/// </summary>
		public bool Convertable { get; }

		/// <summary>
		/// Decorates Pieces with piece metadata
		/// </summary>
		/// <param name="value">Piece score value</param>
		/// <param name="type">Piece Type</param>
		/// <param name="convertable">Flag specifying if the piece can be converted to the other team</param>
		public AdvancePieceAttribute(int value, EPieceType type, bool convertable = true)
		{
			Value = value;
			Type = type;
			Convertable = convertable;
		}
	}
}
