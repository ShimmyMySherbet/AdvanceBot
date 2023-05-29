using System;
using System.IO;
using AdvanceEngine;
using AdvanceEngine.AI;
using AdvanceEngine.Models;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Exceptions;

namespace AdvanceBot
{
	internal class Program
	{
		/// <summary>
		/// Entrance for the Advance Bot
		/// </summary>
		/// <param name="args">Command-Line arguments</param>
		static void Main(string[] args)
		{
			if (args.Length == 1 && args[0].Equals("Name", StringComparison.InvariantCultureIgnoreCase))
			{
				Console.WriteLine("Tactical Retreat");
				return;
			}

			if (args.Length < 3)
			{
				Console.WriteLine("Insufficient Arguments");
				return;
			}

			if (!Enum.TryParse<ETeam>(args[0], true, out var team))
			{
				Console.WriteLine("Invalid Team.");
				return;
			}

			var currentState = args[1];
			var outState = args[2];

			if (!File.Exists(currentState))
			{
				Console.WriteLine("Input board state file doesn't exist");
				return;
			}

			try
			{
				var map = PieceMap.Default
					.Mutate(Mutators.LoadFromFile(currentState));

				var ai = new AILevel7(predictor: new AILevel6());


				var move = ai.DetermineMove(map, team);
				if (move != null)
				{
					map = map.Mutate(move);
				}

				File.WriteAllText(outState, map.Save());
			}
			catch (UnknownPieceException piece)
			{
				Console.WriteLine($"Unknown piece in input file: '{piece}'");
			}
			catch (InvalidOperationException)
			{
				Console.WriteLine("Invalid board state in input file");
			}
			catch (CheckmatedException checkmated)
			{
				Console.WriteLine($"Team {checkmated.Team} is already checkmated, and cannot play");
			}
		}
	}
}