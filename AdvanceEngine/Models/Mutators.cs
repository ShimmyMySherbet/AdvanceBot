using System.IO;
using AdvanceEngine.Logic.Pieces;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Exceptions;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Models
{
	/// <summary>
	/// Provides static mutators to manipulate the board state
	/// </summary>
	public static class Mutators
	{
		/// <summary>
		/// Board mutator to add initial pieces
		/// </summary>
		/// <param name="map">The board to mutate</param>
		public static void DefaultLayout(IPiece?[,] map)
		{
			map[0, 0] = new Miner(ETeam.Black, 0, 0);
			map[1, 0] = new Jester(ETeam.Black, 1, 0);
			map[2, 0] = new Dragon(ETeam.Black, 2, 0);
			map[3, 0] = new Sentinel(ETeam.Black, 3, 0);
			map[4, 0] = new General(ETeam.Black, 4, 0);
			map[5, 0] = new Sentinel(ETeam.Black, 5, 0);
			map[6, 0] = new Catapult(ETeam.Black, 6, 0);
			map[7, 0] = new Jester(ETeam.Black, 7, 0);
			map[8, 0] = new Miner(ETeam.Black, 8, 0);

			map[0, 1] = new Builder(ETeam.Black, 0, 1);
			for (int x = 1; x < 8; x++)
			{
				map[x, 1] = new Zombie(ETeam.Black, x, 1);
			}
			map[8, 1] = new Builder(ETeam.Black, 8, 1);

			map[0, 7] = new Builder(ETeam.White, 0, 7);
			for (int x = 1; x < 8; x++)
			{
				map[x, 7] = new Zombie(ETeam.White, x, 7);
			}
			map[8, 7] = new Builder(ETeam.White, 8, 7);

			map[0, 8] = new Miner(ETeam.White, 0, 8);
			map[1, 8] = new Jester(ETeam.White, 1, 8);

			map[2, 8] = new Catapult(ETeam.White, 2, 8);

			map[3, 8] = new Sentinel(ETeam.White, 3, 8);
			map[4, 8] = new General(ETeam.White, 4, 8);
			map[5, 8] = new Sentinel(ETeam.White, 5, 8);

			map[6, 8] = new Dragon(ETeam.White, 6, 8);

			map[7, 8] = new Jester(ETeam.White, 7, 8);
			map[8, 8] = new Miner(ETeam.White, 8, 8);

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

		/// <summary>
		/// Loads the contents of a file to the board
		/// </summary>
		/// <param name="filename">File name to read from</param>
		/// <returns>A mutator to apply to the board</returns>
		public static MapMutator LoadFromFile(string filename)
		{
			var lines = File.ReadAllLines(filename);

			return LoadFromData(lines);
		}

		/// <summary>
		/// Loads string line content to the board
		/// </summary>
		/// <param name="data">Lines representing rows on the board, of piece characters</param>
		/// <returns>A mutator to add the provided pieces to the board</returns>
		public static MapMutator LoadFromData(string[] data)
		{
			return (IPiece?[,] map) =>
			{
				for (int x = 0; x < 9; x++)
				{
					for (int y = 0; y < 9; y++)
					{
						map[x, y] = ConvertToPiece(data[y][x], x ,y);
					}
				}
			};
		}

		/// <summary>
		/// Removes all of a team's pieces except for it's general. Used for debugging
		/// </summary>
		/// <param name="team">Team to wipe</param>
		/// <returns>A mutatior to apply to a board</returns>
		public static MapMutator RemoveArmy(ETeam team)
		{
			return (IPiece?[,] map) =>
			{
				for (int x = 0; x < 9; x++)
				{
					for (int y = 0; y < 9; y++)
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

		/// <summary>
		/// Forcefully moves a piece to another position
		/// </summary>
		/// <param name="info">The peice to move</param>
		/// <param name="x">Target X Coordinate</param>
		/// <param name="y">Target Y Coordinate</param>
		/// <returns>A mutator to apply to a board</returns>
		public static MapMutator MovePiece(IPiece piece, int x, int y)
		{
			return (IPiece?[,] map) =>
			{
				map[piece.X, piece.Y] = null;
				map[x, y] = piece.Mutate(piece.Team, x, y);
			};
		}

		/// <summary>
		/// Creates a piece instance from a char
		/// </summary>
		/// <param name="pieceChar">Char representation</param>
		/// <returns>A new instance of the specified piece</returns>
		/// <exception cref="UnknownPieceException">Raised when an unknown/unexpected character is provided</exception>
		private static IPiece? ConvertToPiece(char pieceChar, int x, int y)
		{
			var team = char.IsUpper(pieceChar) ? ETeam.White : ETeam.Black;

			switch (char.ToLower(pieceChar))
			{
				case '.': return null;
				case '#': return new Wall(x, y);
				case 'm': return new Miner(team, x, y);
				case 'j': return new Jester(team, x, y);
				case 'd': return new Dragon(team, x , y);
				case 's': return new Sentinel(team, x, y);
				case 'g': return new General(team, x, y);
				case 'c': return new Catapult(team, x, y);
				case 'b': return new Builder(team, x, y);
				case 'z': return new Zombie(team, x, y);
			}

			throw new UnknownPieceException(pieceChar);
		}
	}
}
