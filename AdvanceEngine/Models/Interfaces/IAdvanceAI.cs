using AdvanceEngine.Models.Enums;

namespace AdvanceEngine.Models.Interfaces
{
	public interface IAdvanceAI
	{
		string Name { get; }
		Move? DetermineMove(IPieceMap pieceMap, ETeam team);
	}
}
