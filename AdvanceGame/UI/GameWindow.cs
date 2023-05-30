using System.Diagnostics;
using System.Text;
using AdvanceEngine.Logic.Pieces;
using AdvanceEngine.Models;
using AdvanceEngine.Models.Enums;
using AdvanceEngine.Models.Exceptions;
using AdvanceEngine.Models.Interfaces;
using Newtonsoft.Json;

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
		public string WhiteAIName { get; set; } = string.Empty;
		public string BlackAIName { get; set; } = string.Empty;

		public int WhiteMoves { get; set; } = 0;
		public int BlackMoves { get; set; } = 0;

		public ETeam Winner { get; set; } = ETeam.Neutral;

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

			if (Map.IsOccupied(sqX, sqY))
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

				Winner = winner;

				if (cbAutoPlay.Checked)
				{
					Reset();
					PromptForMove(ETeam.White);
					return;
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

		public void PromptForMove(ETeam team, bool ovr = false)
		{
			if (team == ETeam.Black)
			{
				if (BlackAI != null && (ovr || BlackAIEnabled))
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
								Winner = enemy;
								lblBlackStatus.Text = $"Status: Moved (took {Math.Round(sw.ElapsedTicks / 10000f, 3)}ms)";

								if (cbAutoPlay.Checked)
								{
									Reset();
									PromptForMove(ETeam.White);
									return;
								}

								MessageBox.Show($"{enemy} Wins!");

							});
						}
					});
				}
			}
			else if (team == ETeam.White)
			{
				if (WhiteAI != null && (WhiteAIEnabled || ovr))
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
						catch (AdvanceEngine.Models.Exceptions.CheckmatedException)
						{
							sw.Stop();
							Invoke(() =>
							{
								var enemy = team == ETeam.White ? ETeam.Black : ETeam.White;
								lblWinner.Text = $"Winner: {enemy}";
								lblWhiteStatus.Text = $"Status: Moved (took {Math.Round(sw.ElapsedTicks / 10000f, 3)}ms)";
								Winner = enemy;

								if (cbAutoPlay.Checked)
								{
									Reset();
									PromptForMove(ETeam.White);
									return;
								}

								MessageBox.Show($"{enemy} Wins!");
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
						lblWhiteAI.Text = $"Selected AI: {selector.SelectedModelName}";
						WhiteAIName = selector.SelectedModelName;
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
						lblBlack.Text = $"Selected AI: {selector.SelectedModelName}";
						BlackAIName = selector.SelectedModelName;

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
			Reset();
		}

		public void Reset()
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
			lblWinner.Text = $"Previous Winner: {Winner}";
			Render();
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			using (var ofd = new OpenFileDialog() { Title = "Load board from file", Filter = "Text Files|*.txt" })
			{
				if (ofd.ShowDialog() == DialogResult.OK)
				{
					Text = $"Game Window ({ofd.SafeFileName})";
					Map = PieceMap.Default
						.Mutate(Mutators.LoadFromFile(ofd.FileName));
					Render();
				}
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			using (var sfd = new SaveFileDialog() { Filter = "Advance Bot Recording|*.adr", Title = "Save game recording" })
			{
				if (sfd.ShowDialog() == DialogResult.OK)
				{
					var saves = new List<_Save>();
					m_History.Add((Map, CurrentTeam, BlackMoves, WhiteMoves));
					foreach (var record in m_History)
					{
						var strings = new List<string>();
						for (int y = 0; y < 9; y++)
						{
							var b = new StringBuilder();

							for (int x = 0; x < 9; x++)
							{
								var piece = record.board.GetPieceAtPosition(x, y);

								if (piece == null)
								{
									b.Append('.');
								}
								else if (piece is Wall)
								{
									b.Append('#');
								}
								else
								{
									var name = piece.PieceType.ToString();
									var fc = name[0];

									if (piece.Team == ETeam.White)
									{
										fc = char.ToUpper(fc);
									}
									else
									{
										fc = char.ToLower(fc);
									}
									b.Append(fc);
								}
							}
							strings.Add(b.ToString());
						}

						saves.Add(new _Save()
						{
							Black = record.black,
							White = record.white,
							CurrentTeam = record.team,
							State = strings
						});
					}

					var wrapper = new _SaveWrapper()
					{
						Saves = saves,
						Winner = Winner,
						WhiteAI = WhiteAI != null ? WhiteAIName : null,
						BlackAI = BlackAI != null ? BlackAIName : null,
					};

					var data = JsonConvert.SerializeObject(wrapper);
					File.WriteAllText(sfd.FileName, data);
				}
			}
		}

		private class _SaveWrapper
		{
			public ETeam Winner { get; set; }
			public List<_Save> Saves { get; set; } = new List<_Save>();
			public string? BlackAI { get; set; }
			public string? WhiteAI { get; set; }
		}
		private class _Save
		{
			public ETeam CurrentTeam { get; set; }
			public int Black { get; set; }
			public int White { get; set; }
			public List<string> State { get; set; } = new List<string>();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			using (var sfd = new OpenFileDialog() { Filter = "Advance Bot Recording|*.adr", Title = "Open game recording" })
			{
				if (sfd.ShowDialog() == DialogResult.OK)
				{
					var data = JsonConvert.DeserializeObject<_SaveWrapper>(File.ReadAllText(sfd.FileName));

					if (data == null)
					{
						MessageBox.Show("Invalid recording file");
						return;
					}

					lblWhiteAI.Text = $"Selecetd AI: {data.WhiteAI ?? "N/A"}";
					lblBlack.Text = $"Selecetd AI: {data.BlackAI ?? "N/A"}";

					cbBlackAI.Checked = data.BlackAI != null;
					cbWhiteAI.Checked = data.WhiteAI != null;

					Task.Run(async () =>
					{
						foreach (var save in data.Saves)
						{
							Invoke(() =>
							{
								Map = PieceMap.Default.Mutate(Mutators.LoadFromData(save.State.ToArray()));
								CurrentTeam = save.CurrentTeam;
								BlackMoves = save.Black;
								WhiteMoves = save.White;
								lblWhiteMoves.Text = $"Moves: {WhiteMoves}";
								lblBlackMoves.Text = $"Moves: {BlackMoves}";
								lblTurn.Text = $"Current Turn: {CurrentTeam}";
								Render();
							});

							await Task.Delay(100);
						}

						await Task.Delay(200);
						Invoke(() =>
						{
							lblWinner.Text = $"Winner: {data.Winner}";
							MessageBox.Show($"{data.Winner} Wins!");
						});

					});

				}
			}
		}

		private void btnRunMove_Click(object sender, EventArgs e)
		{
			PromptForMove(ETeam.White, true);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			PromptForMove(ETeam.Black, true);

		}

		private void btnDebug_Click(object sender, EventArgs e)
		{
			var whiteState = Map.CheckState(ETeam.White);

			Debug.WriteLine($"White: {whiteState}");
		}

		private void btnDebugBlack_Click(object sender, EventArgs e)
		{
			var blackState = Map.CheckState(ETeam.Black);

			Debug.WriteLine($"Black: {blackState}");
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{

		}
	}
}
