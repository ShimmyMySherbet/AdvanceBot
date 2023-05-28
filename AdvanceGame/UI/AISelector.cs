using AdvanceEngine.AI;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceGame.UI
{
	public partial class AISelector : Form
	{
		public Func<IAdvanceAI>? SelectedModel;
		private List<_AIModelInfo> Models { get; } = new List<_AIModelInfo>()
		{
			new _AIModelInfo("AI Level 4", "Advance AI bot implemented to level 4 (Pass).\r\nSelects a random valid move each turn. Does not employ any strategy or move planning or prediction.", "Low", () => new AILevel4()),
			new _AIModelInfo("AI Level 5", "Advance AI bot implemented to level 5 (Credit).\nTakes a winning move if presented, and prefers moves that place the enemy in check. For other moves it selects one at random. Does not employ any long term strategy.", "Low-Medium", () => new AILevel5()),
			new _AIModelInfo("AI Level 6", "Advance AI bot implemented to level 6 (Distinction).\nTakes winning moves if possible. Prefers moves with a higher score difference, then prefers moves that threaten the enemy General. Does not plan ahead or employ any long term strategy", "Medium", () => new AILevel6())
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

		private void Ct_OnModelSelected(Func<IAdvanceAI> factory)
		{
			DialogResult = DialogResult.OK;
			SelectedModel = factory;
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
