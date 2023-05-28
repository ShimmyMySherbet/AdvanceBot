using System.Diagnostics;
using AdvanceEngine.Models;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Exceptions;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceGame.UI
{
	public partial class GameWindow : Form
	{
		private List<(IPieceMap board, ETeam team, int black, int white)> m_History = new();

		private BoardRenderer m_Renderer;

		public IPieceMap Map { get; set; }

		public bool AIActive { get; set; } = false;

		private ETeam _CurrentTeam;
		public ETeam CurrentTeam
		{
			get => _CurrentTeam;
			set
			{
				_CurrentTeam = value;
				Invoke(() =>
				{
					lblTurn.Text = $"Current Turn: {value}";
				});
			}
		}

		public bool WhiteAIEnabled { get; set; } = false;
		public bool BlackAIEnabled { get; set; } = false;
		public IAdvanceAI? WhiteAI { get; set; }
		public IAdvanceAI? BlackAI { get; set; }

		public int WhiteMoves { get; set; } = 0;
		public int BlackMoves { get; set; } = 0;

		public GameWindow()
		{
			InitializeComponent();

			Map = PieceMap.Default
				.Mutate(Mutators.DefaultLayout);

			var set = PieceAssetSet.FromFolder("assets");
			m_Renderer = new BoardRenderer(set);
			m_History.Add((Map, CurrentTeam, 0, 0));
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
				m_History.Add((Map, CurrentTeam, BlackMoves, WhiteMoves));
				Map = Map.Mutate(move.Mutator);
				m_Renderer.SelectedPiece = (-1, -1);
				Render();
				OnMoveMade(move.Self?.Team ?? ETeam.Neutral);
				return;
			}

			if (Map.IsOcupied(sqX, sqY))
			{
				m_Renderer.SelectedPiece = (sqX, sqY);
			}
			else
			{
				m_Renderer.SelectedPiece = (-1, -1);
			}

			Render();
		}

		public void OnMoveMade(ETeam byTeam)
		{
			if (BlackMoves == 100 && WhiteMoves == 100)
			{
				// determine winner

				var blackValue = 0;
				var whiteValue = 0;

				for (int x = 0; x < 9; x++)
				{
					for (int y = 0; y < 9; y++)
					{
						var piece = Map.GetPieceAtPosition(x, y);

						if (piece != null)
						{
							if (piece.Team == ETeam.White)
							{
								whiteValue += piece.ScoreValue;
							}
							else if (piece.Team == ETeam.Black)
							{
								blackValue += piece.ScoreValue;
							}
						}
					}
				}

				ETeam winner;
				if (blackValue > whiteValue)
				{
					winner = ETeam.Black;
				}
				else if (whiteValue > blackValue)
				{
					winner = ETeam.White;
				}
				else
				{
					winner = ETeam.Neutral;
				}

				if (winner == ETeam.Neutral)
				{
					lblWinner.Text = $"Draw!";
					MessageBox.Show("Games over: Draw!");
					return;
				}
				else if (winner == ETeam.Black)
				{
					lblWinner.Text = $"Winner: {winner} ({blackValue} vs {whiteValue})";
					MessageBox.Show($"Games over: Black wins with score of {blackValue}");
				}
				else
				{
					lblWinner.Text = $"Winner: {winner} ({whiteValue} vs {blackValue})";
					MessageBox.Show($"Games over: White wins with score of {whiteValue}");
				}

				return;
			}

			if (byTeam == ETeam.Black)
			{
				BlackMoves++;
				lblBlackMoves.Text = $"Moves: {BlackMoves}";
				CurrentTeam = ETeam.White;
			}
			else
			{
				WhiteMoves++;
				lblWhiteMoves.Text = $"Moves: {BlackMoves}";
				CurrentTeam = ETeam.Black;
			}

			Debug.WriteLine("Prompting for move");
			PromptForMove(CurrentTeam);

		}

		public void Render()
		{
			pngRender.Image?.Dispose();
			pngRender.Image = m_Renderer.Render(Map);
		}

		public void PromptForMove(ETeam team)
		{
			if (team == ETeam.Black)
			{
				if (BlackAIEnabled && BlackAI != null)
				{
					lblBlackStatus.Text = "Status: Thinking...";

					Task.Run(async () =>
					{
						Debug.WriteLine("Running AI for Black");

						await Task.Delay(50);
						var sw = new Stopwatch();

						try
						{
							sw.Start();
							var move = BlackAI.DetermineMove(Map, team);
							sw.Stop();
							if (move != null)
							{
								var newMap = Map.Mutate(move.Mutator);
								Invoke(() =>
								{
									m_History.Add((Map, CurrentTeam, BlackMoves, WhiteMoves));
									Map = newMap;
									lblBlackStatus.Text = $"Status: Moved (took {Math.Round(sw.ElapsedTicks / 10000f, 3)}ms)";

									Render();
									OnMoveMade(ETeam.Black);

								});
							}
						}
						catch (CheckmatedException)
						{
							Invoke(() =>
							{
								var enemy = team == ETeam.White ? ETeam.Black : ETeam.White;
								lblWinner.Text = $"Winner: {enemy}";
								MessageBox.Show($"{enemy} Wins!");
								lblBlackStatus.Text = $"Status: Moved (took {Math.Round(sw.ElapsedTicks / 10000f, 3)}ms)";

							});
						}
					});
				}
			}
			else if (team == ETeam.White)
			{
				if (WhiteAIEnabled && WhiteAI != null)
				{
					lblWhiteStatus.Text = "Status: Thinking...";
					Task.Run(async () =>
					{
						await Task.Delay(50);

						Debug.WriteLine("Running AI for White");
						var sw = new Stopwatch();
						try
						{
							sw.Start();
							var move = WhiteAI.DetermineMove(Map, team);
							sw.Stop();

							if (move != null)
							{
								var newMap = Map.Mutate(move.Mutator);
								Invoke(() =>
								{
									m_History.Add((Map, CurrentTeam, BlackMoves, WhiteMoves));
									Map = newMap;
									lblWhiteStatus.Text = $"Status: Moved (took {Math.Round(sw.ElapsedTicks / 10000f, 2)}ms)";
									Render();
									OnMoveMade(ETeam.White);
								});
							}
						}
						catch (CheckmatedException)
						{
							sw.Stop();
							Invoke(() =>
							{
								var enemy = team == ETeam.White ? ETeam.Black : ETeam.White;
								lblWinner.Text = $"Winner: {enemy}";
								MessageBox.Show($"{enemy} Wins!");
								lblWhiteStatus.Text = $"Status: Moved (took {Math.Round(sw.ElapsedTicks / 10000f, 3)}ms)";
							});
						}
					});
				}
			}
		}

		private void btnSelectWhite_Click(object sender, EventArgs e)
		{
			using (var selector = new AISelector())
			{
				if (selector.ShowDialog() == DialogResult.OK)
				{
					if (selector.SelectedModel != null)
					{
						WhiteAI = selector.SelectedModel();
						lblWhiteAI.Text = $"Selected AI: {WhiteAI.Name}";
						if (CurrentTeam == ETeam.White)
						{
							PromptForMove(CurrentTeam);
						}
					}
				}
			}
		}

		private void btnSelectBlack_Click(object sender, EventArgs e)
		{
			using (var selector = new AISelector())
			{
				if (selector.ShowDialog() == DialogResult.OK)
				{
					if (selector.SelectedModel != null)
					{
						BlackAI = selector.SelectedModel();
						lblBlack.Text = $"Selected AI: {BlackAI.Name}";
						if (CurrentTeam == ETeam.Black)
						{
							PromptForMove(CurrentTeam);
						}
					}
				}
			}
		}

		private void cbWhiteAI_CheckedChanged(object sender, EventArgs e)
		{
			WhiteAIEnabled = cbWhiteAI.Checked;
			if (CurrentTeam == ETeam.White)
			{
				PromptForMove(CurrentTeam);
			}
		}

		private void cbBlackAI_CheckedChanged(object sender, EventArgs e)
		{
			BlackAIEnabled = cbBlackAI.Checked;
			if (CurrentTeam == ETeam.Black)
			{
				PromptForMove(CurrentTeam);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var last = m_History.Last();
			Map = last.board;
			CurrentTeam = last.team;
			WhiteMoves = last.white;
			BlackMoves = last.black;
			lblWhiteMoves.Text = $"Moves: {WhiteMoves}";
			lblBlackMoves.Text = $"Moves: {BlackMoves}";
			lblTurn.Text = $"Current Turn: {CurrentTeam}";
			m_History.RemoveAt(m_History.Count - 1);
			Render();
			PromptForMove(CurrentTeam);
		}

		private void btnReset_Click(object sender, EventArgs e)
		{
			m_History.Add((Map, CurrentTeam, BlackMoves, WhiteMoves));
			Map = PieceMap.Default
				.Mutate(Mutators.DefaultLayout);
			BlackMoves = 0;
			WhiteMoves = 0;
			CurrentTeam = ETeam.White;
			lblWhiteMoves.Text = $"Moves: {WhiteMoves}";
			lblBlackMoves.Text = $"Moves: {BlackMoves}";
			lblTurn.Text = $"Current Turn: {CurrentTeam}";
			Render();
		}
	}
}
