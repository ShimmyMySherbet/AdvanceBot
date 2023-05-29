using System.IO;
using AdvanceEngine.Logic.Pieces;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Exceptions;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Models
{
	public static class Mutators
	{
		/// <summary>
		/// Board mutator to add initial pieces
		/// </summary>
		/// <param name="map">The board to mutate</param>
		public static void DefaultLayout(IPiece?[,] map)
		{
			map[0, 0] = new Miner(ETeam.Black);
			map[1, 0] = new Jester(ETeam.Black);
			map[2, 0] = new Dragon(ETeam.Black);
			map[3, 0] = new Sentinel(ETeam.Black);
			map[4, 0] = new General(ETeam.Black);
			map[5, 0] = new Sentinel(ETeam.Black);
			map[6, 0] = new Catapult(ETeam.Black);
			map[7, 0] = new Jester(ETeam.Black);
			map[8, 0] = new Miner(ETeam.Black);

			map[0, 1] = new Builder(ETeam.Black);
			for (int x = 1; x < 8; x++)
			{
				map[x, 1] = new Zombie(ETeam.Black);
			}
			map[8, 1] = new Builder(ETeam.Black);

			map[0, 7] = new Builder(ETeam.White);
			for (int x = 1; x < 8; x++)
			{
				map[x, 7] = new Zombie(ETeam.White);
			}
			map[8, 7] = new Builder(ETeam.White);

			map[0, 8] = new Miner(ETeam.White);
			map[1, 8] = new Jester(ETeam.White);

			map[2, 8] = new Catapult(ETeam.White);

			map[3, 8] = new Sentinel(ETeam.White);
			map[4, 8] = new General(ETeam.White);
			map[5, 8] = new Sentinel(ETeam.White);

			map[6, 8] = new Dragon(ETeam.White);

			map[7, 8] = new Jester(ETeam.White);
			map[8, 8] = new Miner(ETeam.White);

		}

		/// <summary>
		/// Configurable board mutator to add a single piece in the centre of the board.
		/// Used for testing piece logic.
		/// </summary>
		/// <param name="map">The board to mutate</param>
		public static MapMutator SinglePiece(IPiece piece)
		{
			return (IPiece?[,] map) =>
			{
				map[4, 4] = piece;
			};
		}
		public static MapMutator LoadFromFile(string filename)
		{
			var lines = File.ReadAllLines(filename);

			return LoadFromData(lines);
		}
		public static MapMutator LoadFromData(string[] data)
		{
			return (IPiece?[,] map) =>
			{
				for (int x = 0; x < 9; x++)
				{
					for (int y = 0; y < 9; y++)
					{
						map[x, y] = ConvertToPiece(data[y][x]);
					}
				}
			};
		}

		public static MapMutator RemoveArmy(ETeam team)
		{
			return (IPiece?[,] map) =>
			{
				for(int x = 0; x < 9; x++)
				{
					for(int y = 0; y < 9; y++)
					{
						var piece = map[x, y];
						
						if (piece != null && piece.Team == team && piece.PieceType != EPieceType.General)
						{
							map[x, y] = null;
						} 
					}
				}
			};
		}

		private static IPiece? ConvertToPiece(char c)
		{
			var team = char.IsUpper(c) ? ETeam.White : ETeam.Black;

			switch (char.ToLower(c))
			{
				case '.': return null;
				case '#': return new Wall();
				case 'm': return new Miner(team);
				case 'j': return new Jester(team);
				case 'd': return new Dragon(team);
				case 's': return new Sentinel(team);
				case 'g': return new General(team);
				case 'c': return new Catapult(team);
				case 'b': return new Builder(team);
				case 'z': return new Zombie(team);
			}

			throw new UnknownPieceException(c);
		}
	}
}
