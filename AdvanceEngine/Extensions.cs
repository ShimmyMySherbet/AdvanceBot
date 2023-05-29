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
			if (team == ETeam.Black)
			{
				return ETeam.White;
			}
			else if (team == ETeam.White)
			{
				return ETeam.Black;
			}
			return ETeam.Neutral;
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
