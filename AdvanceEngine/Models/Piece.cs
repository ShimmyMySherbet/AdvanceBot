using System;
using System.Reflection;
using AdvanceEngine.Models.Attributes;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceEngine.Models
{
	/// <summary>
	/// Base class providing metadata for a piece
	/// </summary>
	public abstract partial class Piece : IPiece
	{
		/// <summary>
		/// Piece score value
		/// </summary>
		public int ScoreValue { get; }

		/// <summary>
		/// The piece type
		/// </summary>
		public EPieceType PieceType { get; }

		/// <summary>
		/// The team the piece is on
		/// </summary>
		public ETeam Team { get; }

		/// <summary>
		/// The piece X-Coordinate
		/// </summary>
		public int X { get; }

		/// <summary>
		/// The piece Y-Coordinate
		/// </summary>
		public int Y { get; }

		/// <summary>
		/// A flag specifying if the piece can be converted to another team
		/// </summary>
		public bool Convertable { get; } = true;

		/// <summary>
		/// Constructor using <seealso cref="AdvancePieceAttribute"/> for metadata.
		/// </summary>
		/// <param name="x">Piece X Cooridnate</param>
		/// <param name="y">Piece Y Cooridnate</param>
		/// <exception cref="InvalidOperationException">Raised when the base class doesn't posess the <see cref="AdvancePieceAttribute"/> attribute</exception>
		protected Piece(int x, int y)
		{
			var info = GetType().GetCustomAttribute<AdvancePieceAttribute>();
			if (info == null)
			{
				throw new InvalidOperationException($"Piece {GetType().FullName} requires the AdvancePieceAttribute");
			}
			ScoreValue = info.Value;
			PieceType = info.Type;
			Convertable = info.Convertable;
			X = x;
			Y = y;
		}

		/// <summary>
		/// Proxy constructor for <see cref="Piece.Piece"/>
		/// </summary>
		/// <param name="team">The team the piece is assigned to</param>
		/// <param name="x">Piece X Cooridnate</param>
		/// <param name="y">Piece Y Cooridnate</param>
		public Piece(ETeam team, int x, int y) : this(x, y)
		{
			Team = team;
		}

		/// <summary>
		/// Verbose constructor that doesn't use <see cref="AdvancePieceAttribute"/>
		/// </summary>
		/// <param name="scoreValue">The score value of the piece</param>
		/// <param name="pieceType">The type of the piece</param>
		/// <param name="team">The team the piece is assigned to</param>
		/// <param name="x">Piece X Cooridnate</param>
		/// <param name="y">Piece Y Cooridnate</param>
		public Piece(int scoreValue, EPieceType pieceType, ETeam team, int x, int y) : this(team,x, y)
		{
			ScoreValue = scoreValue;
			PieceType = pieceType;
			Team = team;
		}

		/// <summary>
		/// Converts the piece to another team
		/// </summary>
		/// <param name="team">Team to convert to</param>
		/// <param name="x">New X Coordinate</param>
		/// <param name="y">New Y Coordinate</param>
		/// <returns>A new instance of the piece as the specified team</returns>
		public abstract IPiece Mutate(ETeam team, int x, int y);

		/// <summary>
		/// Provides a human-readable view of the piece for debugging
		/// </summary>
		/// <returns>Human readable representation</returns>
		public override string ToString()
		{
			return $"{Team} {PieceType}";
		}
	}
}
