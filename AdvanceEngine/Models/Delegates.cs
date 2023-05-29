using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Models
{
	/// <summary>
	/// A delegtae definition for a map mutator, that operates and modifies a map's data
	/// </summary>
	/// <param name="map">The internal board state</param>
	public delegate void MapMutator(IPiece?[,] map);
}
