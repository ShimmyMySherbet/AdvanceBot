using AdvanceEngine.Models.Enums;

namespace AdvanceGame
{
	public class PieceAssetSet
	{
		public Dictionary<(EPieceType type, ETeam team), Image> Assets { get; } = new();



		public Image? Move { get; set; }
		public Image? Attack { get; set; }
		public Image? Build { get; set; }
		public Image? Protect { get; set; }
		public Image? Convert { get; set; }

		public static PieceAssetSet FromFolder(string folder)
		{
			var set = new PieceAssetSet();

			foreach (var file in Directory.GetFiles(folder, "*.png"))
			{
				var fName = Path.GetFileNameWithoutExtension(file);

				if (fName.Contains('_'))
				{
					var type = fName.Split('_')[0];
					var team = fName.Substring(type.Length + 1);

					var eType = Enum.Parse<EPieceType>(type, true);
					var eTeam = Enum.Parse<ETeam>(team, true);

					set.Assets[(eType, eTeam)] = Image.FromFile(file);
					continue;
				}

				switch (fName.ToLowerInvariant())
				{
					case "attack":
						set.Attack = Image.FromFile(file);
						break;
					case "build":
						set.Build = Image.FromFile(file);
						break;
					case "protect":
						set.Protect = Image.FromFile(file);
						break;
					case "move":
						set.Move = Image.FromFile(file);
						break;
					case "convert":
						set.Convert = Image.FromFile(file);
						break;
				}
			}

			return set;
		}

		public Image? GetAsset(EPieceType type, ETeam team)
		{
			var key = (type, team);
			if (Assets.ContainsKey(key))
			{
				return Assets[key];
			}
			return null;
		}
	}
}
