using System.Diagnostics;
using System.Xml.XPath;
using AdvanceEngine.Logic.Pieces;
using AdvanceEngine.Models;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceGame.UI
{
	public partial class GameWindow : Form
	{
		private BoardRenderer m_Renderer;

		public IPieceMap Map { get; set; }

		public GameWindow()
		{
			InitializeComponent();

			var subMap = new IPiece?[9, 9];
			Map = PieceMap.Default.Mutate(Mutators.DefaultLayout);


			var set = PieceAssetSet.FromFolder("assets");
			m_Renderer = new BoardRenderer(set);

			Render();

			pngRender.MouseClick += PngRender_MouseClick;
		}

		private void PngRender_MouseClick(object? sender, MouseEventArgs e)
		{

			Debug.WriteLine($"x: {e.X}, Y: {e.Y}");
			var xPerc = e.X / (float)pngRender.Width;
			var yPerc = e.Y / (float)pngRender.Height;

			var x = xPerc * 1000f;
			var y = yPerc * 1000f;

			var blockSize = 1000 / 9f;

			var sqX = (int)Math.Floor(x / (blockSize));
			var sqY = (int)Math.Floor(y / (blockSize));



			var moves = m_Renderer.CurrentMoves.Where(x => (x.TargetPosition?.x ?? -1) == sqX && (x.TargetPosition?.y ?? -1) == sqY).ToArray();

			var move = moves.FirstOrDefault();


			if (moves.Length > 1 && e.Button == MouseButtons.Right)
			{
				move = moves[1];
			}



			if (move != null)
			{
				Map = Map.Mutate(move.Mutator);
				m_Renderer.SelectedPiece = (-1, -1);
				Render();
				return;
			}

			if (Map.IsOcupied(sqX, sqY))
			{
				m_Renderer.SelectedPiece = (sqX, sqY);
			} else
			{
				m_Renderer.SelectedPiece = (-1, -1);
			}

			Render();
		}

		public void Render()
		{
			pngRender.Image?.Dispose();
			pngRender.Image = m_Renderer.Render(Map);
		}
	}
}
