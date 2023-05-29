using AdvanceEngine.AI;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceGame.UI
{
	public partial class AISelector : Form
	{
		public Func<IAdvanceAI>? SelectedModel { get; private set; }
		public string SelectedModelName { get; private set; } = string.Empty;
		private List<_AIModelInfo> Models { get; } = new List<_AIModelInfo>()
		{
			new _AIModelInfo("AI Level 4", "Advance AI bot implemented to level 4 (Pass).\r\nSelects a random valid move each turn. Does not employ any strategy or move planning or prediction.", "Low", () => new AILevel4()),
			new _AIModelInfo("AI Level 5", "Advance AI bot implemented to level 5 (Credit).\r\nTakes a winning move if presented, and prefers moves that place the enemy in check. For other moves it selects one at random. Does not employ any long term strategy.", "Low-Medium", () => new AILevel5()),
			new _AIModelInfo("AI Level 6", "Advance AI bot implemented to level 6 (Distinction).\r\nTakes winning moves if possible. Prefers moves with a higher score difference, then prefers moves that threaten the enemy General. Does not plan ahead or employ any long term strategy", "Medium", () => new AILevel6()),
			new _AIModelInfo("AI Level 7", "Advance AI bot implemented to level 7 (High Distinction)\r\n" +
				"Takes winning moves if possible. Prefers moves with a higher score difference, and moves that theaten the enemy General. Can plan ahead 2 moves by predicting the opponent when multiple moves of equal immediate value are available.\n Uses AI level 6 as a predictor.", "Medium-Heavy", () => new AILevel7(predictor: new AILevel6())),
			new _AIModelInfo("AI Level 7x2", "Takes winning moves if possible. Plays to maximise immediate move score, then by predicting up to 4 moves into the future to optimize strategy. Can employ basic mid-term game strategy. Uses AI Level 7 as a self predictor and AI level 6 as an ememy predictor.", "Heavy", () => new AILevel7(new AILevel7(new AILevel6())))
		};

		public AISelector()
		{
			InitializeComponent();

			foreach (var model in Models)
			{
				var ct = new AIModel(model.Name, model.Description, model.Intensity, model.Factory);
				ct.Width = flowLayoutPanel1.Width - 30;
				ct.OnModelSelected += Ct_OnModelSelected;
				flowLayoutPanel1.Controls.Add(ct);
			}
		}

		private void Ct_OnModelSelected(Func<IAdvanceAI> factory, string name)
		{
			DialogResult = DialogResult.OK;
			SelectedModel = factory;
			SelectedModelName = name;
			Close();
		}

		private class _AIModelInfo
		{
			public string Name { get; }
			public string Description { get; }
			public string Intensity { get; }
			public Func<IAdvanceAI> Factory { get; }

			public _AIModelInfo(string name, string description, string intensity, Func<IAdvanceAI> factory)
			{
				Name = name;
				Description = description;
				Intensity = intensity;
				Factory = factory;
			}
		}
	}
}
