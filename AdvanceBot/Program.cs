using System;
using System.IO;
using AdvanceEngine;
using AdvanceEngine.AI;
using AdvanceEngine.Models;
using AdvanceEngine.Models.Enums;

namespace AdvanceBot
{
	internal class Program
	{
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

			var map = PieceMap.Default.Mutate(Mutators.LoadFromFile(currentState));

			var ai = new AILevel7(predictor: new AILevel6());

			var move = ai.DetermineMove(map, team);

			if (move != null)
			{
				map = map.Mutate(move);
			}

			var result = map.Save();

			File.WriteAllText(outState, result);
		}
	}
}