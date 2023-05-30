using System;
using System.Collections.Generic;
using System.Text;
using AdvanceEngine.Logic.Pieces;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine
{
	/// <summary>
	/// Class providing helper extensions
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Gets the enemy for this team
		/// </summary>
		/// <param name="team">The playing team</param>
		/// <returns>The playing team's enemy</returns>
		public static ETeam Enemy(this ETeam team)
		{
			switch (team)
			{
				case ETeam.Black:
					return ETeam.White;
				case ETeam.White:
					return ETeam.Black;
				default:
					return ETeam.Neutral;
			}
		}

		/// <summary>
		/// Returns an iterable collection of hostile pieces on the board
		/// </summary>
		/// <param name="map">The board state</param>
		/// <param name="team">The playing team</param>
		/// <returns>Iterable list of hostile pieces</returns>
		public static IReadOnlyList<IPiece> EnemyPieces(this IPieceMap map, ETeam team) => FriendlyPieces(map, team.Enemy());

		/// <summary>
		/// Returns an iterable collection of firendly pieces on the board
		/// </summary>
		/// <param name="map">The board state</param>
		/// <param name="team">The playing team</param>
		/// <returns>Iterable list of friendly pieces</returns>
		public static IReadOnlyList<IPiece> FriendlyPieces(this IPieceMap map, ETeam team)
		{
			switch (team)
			{
				case ETeam.White:
					return map.WhitePieces;
				case ETeam.Black:
					return map.BlackPieces;
				default:
					return Array.Empty<IPiece>();
			}
		}

		/// <summary>
		/// Converts a piece map into string representation
		/// </summary>
		/// <param name="map">Piece map to save</param>
		/// <returns>
		/// String representation of the map that can be read using <seealso cref="Models.Mutators.LoadFromFile(string)"/> 
		/// or <seealso cref="Models.Mutators.LoadFromData(string[])"/> when split by newlines
		/// </returns>
		public static string Save(this IPieceMap map)
		{
			var sb = new StringBuilder();

			for (int y = 0; y < 9; y++)
			{
				for (int x = 0; x < 9; x++)
				{
					var piece = map.GetPieceAtPosition(x, y);

					if (piece == null)
					{
						sb.Append('.');
					}
					else if (piece is Wall)
					{
						sb.Append('#');
					}
					else
					{
						var name = piece.PieceType.ToString();
						var fc = name[0];

						if (piece.Team == ETeam.White)
						{
							fc = char.ToUpper(fc);
						}
						else
						{
							fc = char.ToLower(fc);
						}
						sb.Append(fc);
					}
				}

				sb.Append('\n');
			}

			return sb.ToString().Trim('\n');

		}
	}
}
