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

		IPiece? CheckForDanger(int x, int y, ETeam team);

		ECheckState CheckState(ETeam team);

		BoardInfo GetBoardInfo(ETeam team);
	}
}
