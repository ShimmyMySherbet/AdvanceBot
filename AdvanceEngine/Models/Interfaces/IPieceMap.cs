using AdvanceEngine.Models.Enums;

namespace AdvanceEngine.Models.Interfaces
{
	public interface IPieceMap
	{
		bool IsValidCoordinate(int x, int y);

		bool IsOcupied(int x, int y);

		bool IsProtected(int x, int y, ETeam attacker);

		IPiece? GetPieceAtPosition(int x, int y);

		IPieceMap Mutate(Move move);

		IPieceMap Mutate(MapMutator mutator);

		IPiece? CheckForDanger(int x, int y, IPiece piece, Move? move = null);

		ECheckState CheckState(ETeam team);

		BoardInfo GetBoardInfo(ETeam team);

		int GetTeamValue(ETeam team);
		int GetTeamLead(ETeam team);
	}
}
