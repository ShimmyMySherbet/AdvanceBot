using AdvanceEngine.Logic.Pieces;
using AdvanceEngine.Models;
using AdvanceEngine.Models.Interfaces;

namespace AdvanceGame
{
	public class BoardRenderer
	{
		public static readonly Brush Dark = new SolidBrush(Color.FromArgb(96, 96, 96));
		public static readonly Brush Light = new SolidBrush(Color.FromArgb(160, 160, 160));

		public PieceAssetSet Set { get; set; }

		public (int x, int y) SelectedPiece { get; set; } = (-1, -1);

		public List<Move> CurrentMoves { get; private set; } = new List<Move>();

		public BoardRenderer(PieceAssetSet set)
		{
			Set = set;
		}

		public Image Render(IPieceMap map)
		{
			var bmp = new Bitmap(1000, 1000);

			using (var g = Graphics.FromImage(bmp))
			{
				var blockSize = 1000 / 9f;

				var checker = false;
				for (int y = 0; y < 9; y++)
				{
					for (int x = 0; x < 9; x++)
					{
						checker = !checker;

						var rect = new RectangleF(x * blockSize, y * blockSize, blockSize, blockSize);
						var brush = checker ? Dark : Light;

						g.FillRectangle(brush, rect);


						if (x == SelectedPiece.x && y == SelectedPiece.y)
						{
							g.FillRectangle(Brushes.Blue, rect);
						}


						var piece = map.GetPieceAtPosition(x, y);
						if (piece != null)
						{
							var asset = Set.GetAsset(piece.PieceType, piece.Team);
							if (asset != null)
							{
								g.DrawImage(asset, rect);
							}
						}
					}
				}

				CurrentMoves.Clear();

				if (map.IsValidCoordinate(SelectedPiece.x, SelectedPiece.y))
				{
					var highlightTarget = map.GetPieceAtPosition(SelectedPiece.x, SelectedPiece.y);

					if (highlightTarget != null)
					{
						using (var moves = highlightTarget.GetMoves(SelectedPiece.x, SelectedPiece.y, map))
						{
							while (moves.MoveNext())
							{
								var move = moves.Current;

								if (move.TargetPosition != null)
								{
									var targetX = move.TargetPosition.Value.x * blockSize;
									var targetY = move.TargetPosition.Value.y * blockSize;

									var rect = new RectangleF(targetX, targetY, blockSize, blockSize);

									Image? icon = null;
									if (move.IsAttack)
									{
										icon = Set.Attack;
									}
									else
									{
										icon = Set.Move;
									}

									if (icon != null)
										g.DrawImage(icon, rect);


									CurrentMoves.Add(move);
								}
							}
						}

					}

					if (highlightTarget is Sentinel)
					{
						var positions = new (int x, int y)[] { (SelectedPiece.x - 1, SelectedPiece.y), (SelectedPiece.x + 1, SelectedPiece.y), (SelectedPiece.x, SelectedPiece.y - 1), (SelectedPiece.x, SelectedPiece.y + 1) };

						foreach(var p in positions)
						{
							if (map.IsValidCoordinate(p.x, p.y))
							{
								var P = map.GetPieceAtPosition(p.x, p.y);
								if (P != null && P.Team == highlightTarget.Team)
								{
									var rect = new RectangleF(p.x * blockSize, p.y * blockSize, blockSize, blockSize);

									if (Set.Protect != null)
									{
										g.DrawImage(Set.Protect, rect);
									}


								}
							}
						}


					}


				}


				g.Save();
			}

			return bmp;
		}
	}
}
