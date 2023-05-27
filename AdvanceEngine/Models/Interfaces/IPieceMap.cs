using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		
	}
}
