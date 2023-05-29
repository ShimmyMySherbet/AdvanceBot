using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Exceptions;
using System;

namespace AdvanceEngine.Models.Interfaces
{
	/// <summary>
	/// Interface representing an Advance AI model
	/// </summary>
	public interface IAdvanceAI
	{
		/// <summary>
		/// The human-readable name of the model
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Determines what move to make next, if possible.
		/// </summary>
		/// <param name="pieceMap">Current board state</param>
		/// <param name="team">Playing side</param>
		/// <returns>The next move to make, if possible</returns>
		/// <exception cref="CheckmatedException">Raised when the current player is checkmated</exception>
		/// <exception cref="InvalidOperationException">Raised when the AI model is provided with an invalid board state</exception>
		Move? DetermineMove(IPieceMap pieceMap, ETeam team);
	}
}
