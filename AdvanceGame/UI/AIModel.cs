﻿using AdvanceEngine.Models.Interfaces;

namespace AdvanceGame.UI
{
	public delegate void ModelSelectedEvent(Func<IAdvanceAI> factory, string name);
	public partial class AIModel : UserControl
	{
		public Func<IAdvanceAI>? Factory { get; }

		public event ModelSelectedEvent? OnModelSelected;

		public string ModelName { get; } = string.Empty;

		public AIModel()
		{
			InitializeComponent();
		}

		public AIModel(string name, string descr, string intensity, Func<IAdvanceAI> factory)
		{
			InitializeComponent();
			lblTitle.Text = name;
			txtDescription.Text = descr;
			lblIntensity.Text = $"Computational Intensity: {intensity}";
			Factory = factory;
			ModelName = name;
		}

		private void btnSelect_Click(object sender, EventArgs e)
		{
			if (Factory != null)
			{
				OnModelSelected?.Invoke(Factory, ModelName);
			}
		}
	}
}
