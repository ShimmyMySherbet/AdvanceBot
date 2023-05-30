using System.Collections.Generic;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Models
{
	/// <summary>
	/// Structure representing the board state from a team's point of view
	/// </summary>
	public readonly struct BoardInfo
	{
		/// <summary>
		/// The current player's General
		/// </summary>
		public IPiece Self { get; }

		/// <summary>
		/// The opponent's General
		/// </summary>
		public IPiece Opponent { get; }

		/// <summary>
		/// Iterable collection of the player's own pieces
		/// </summary>
		public IReadOnlyList<IPiece> Friendly { get; }

		/// <summary>
		/// Iterable collection of the opponent's pieces
		/// </summary>
		public IReadOnlyList<IPiece> Hostile { get; }

		/// <summary>
		/// Board Info
		/// </summary>
		/// <param name="self">The current player's General</param>
		/// <param name="opponent">The enemy's general</param>
		/// <param name="friendly">Friendly pieces</param>
		/// <param name="hostile">Hostile pieces</param>
		public BoardInfo(IPiece self, IPiece opponent, IReadOnlyList<IPiece> friendly, IReadOnlyList<IPiece> hostile)
		{
			Self = self;
			Opponent = opponent;
			Friendly = friendly;
			Hostile = hostile;
		}
	}
}
