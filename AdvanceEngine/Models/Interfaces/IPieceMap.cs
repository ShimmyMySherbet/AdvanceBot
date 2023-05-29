using AdvanceEngine.Models.Enums;

namespace AdvanceEngine.Models.Interfaces
{
	/// <summary>
	/// Wraps a 2D array of <seealso cref="IPiece?"/>, providing helper methods
	/// </summary>
	public interface IPieceMap
	{
		/// <summary>
		/// Tests if a coordinate is valid on the board
		/// </summary>
		/// <param name="x">X Coordinate</param>
		/// <param name="y">Y Coordinate</param>
		/// <returns><see langword="true"/> if the position is valid</returns>
		bool IsValidCoordinate(int x, int y);

		/// <summary>
		/// Tests if the specified position is occupied by a peice
		/// </summary>
		/// <param name="x">X Corodinate</param>
		/// <param name="y">Y Coordinate</param>
		/// <returns><see langword="true"/> if the location is occupied</returns>
		bool IsOccupied(int x, int y);

		/// <summary>
		/// Tests if the specified position is Protected by a sentinel
		/// </summary>
		/// <param name="x">X Coordinate</param>
		/// <param name="y">Y Coordinate</param>
		/// <param name="attacker">The attacking team</param>
		/// <returns><see langword="true"> when the piece at the specified position cannot be attacked</returns>
		bool IsProtected(int x, int y, ETeam attacker);

		/// <summary>
		/// Gets a piece at the specified location, or null if it is empty
		/// </summary>
		/// <param name="x">X Coordinate</param>
		/// <param name="y">Y Coordinate</param>
		/// <returns>The piece occupying the specified location, or null</returns>
		IPiece? GetPieceAtPosition(int x, int y);

		/// <summary>
		/// Mutates the map with the specified move
		/// </summary>
		/// <param name="move">The move to make</param>
		/// <returns>A new instance of the map after the move has been made</returns>
		IPieceMap Mutate(Move move);

		/// <summary>
		/// Mutates the map with the specified mutator
		/// </summary>
		/// <param name="mutator">The mutator to use</param>
		/// <returns>A new instance of the map with the changes from the mutator</returns>
		IPieceMap Mutate(MapMutator mutator);

		/// <summary>
		/// Checks if a piece at a position is in danger
		/// </summary>
		/// <param name="x">X Coordinate to test</param>
		/// <param name="y">Y Coordinate to test</param>
		/// <param name="piece">The piece at the coordinates</param>
		/// <param name="move">Optionally, a move to simulate before testing safety</param>
		/// <returns>The piece threatening the location, or <see langword="null"/> if it is safe</returns>
		IPiece? CheckForDanger(int x, int y, IPiece piece, Move? move = null);

		/// <summary>
		/// Checks the check-state for a team
		/// </summary>
		/// <param name="team">The team to test</param>
		/// <returns>The check-state for the specified team</returns>
		ECheckState CheckState(ETeam team);

		/// <summary>
		/// Finds all pieces on the board and provides the information in a side-biased structure
		/// </summary>
		/// <param name="team">The point-of-view team</param>
		/// <returns>Board info for this board</returns>
		BoardInfo GetBoardInfo(ETeam team);

		/// <summary>
		/// Gets the total piece value for a team
		/// </summary>
		/// <param name="team">Team to check</param>
		/// <returns>The total score value of a team</returns>
		int GetTeamValue(ETeam team);

		/// <summary>
		/// Gets the score difference between both teams.
		/// </summary>
		/// <param name="team">Team to check</param>
		/// <returns>The score difference. Positive indicating the team is winning</returns>
		int GetTeamLead(ETeam team);
	}
}
