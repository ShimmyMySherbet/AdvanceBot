using System.Windows.Markup;

namespace AdvanceBot
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");

			var map = new int[12, 12];
			map[0, 0] = 20;
			map[1, 1] = 30;
			map[11,11] = 100;

			C(map);


			var n = new int[12, 12];

			Array.Copy(map, n, map.Length);

			n[1, 2] = 22;

			Console.WriteLine();
			Console.WriteLine();
			C(n);



		}
		public static void C(int[,] d)
		{
			for (int x = 0; x < d.GetLength(0); x++)
			{
				for (int y = 0; y < d.GetLength(1); y++)
				{
					Console.Write($"{d[x, y]} ");
				}
				Console.WriteLine();
			}
		}
		public readonly struct rd
		{
			public readonly int[,] Values = new int[2, 2];
			public rd(int[,] values) { Values = values; }



			public rd Mutate()
			{
				Values[0, 0] = 1;
				return this;
			}

		}
	}
}