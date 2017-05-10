using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualDomino {
	public partial class Form1 : Form {
		public static ComboBox PlayerSelecter1, PlayerSelecter2;
		public static NumericUpDown TournamentsCounter, RoundsCounter;

		private int currentTournament, currentRound, currentStep = 1;

		private List<State> roundInfo;

		private int tileWidth, tileLength;
		private int FieldTilesCount = 10;

		public Form1 () {
			InitializeComponent();

			PlayerSelecter1 = SelectPlayer1;
			PlayerSelecter2 = SelectPlayer2;

			TournamentsCounter = TournamentsCount;
			RoundsCounter = RoundsCount;

			Program.setPlayersList();
			PlayerSelecter1.SelectedIndex = 1;
			PlayerSelecter2.SelectedIndex = 2;

			tileLength = FieldBox.Width / (FieldTilesCount + 2);
			tileWidth = tileLength / 2;
		}

		public void SetScores (int Score1, int Score2) {
			ScoresLabel.Text = "Счёт: " + Score1 + ":" + Score2;
			curStep.Text = "(  /  )";
			curRound.Text = "(  /  )";
			curTournament.Text = "(  /  )";
		}

		private void SwapPlayers_Click (object sender, EventArgs e) {
			int tmp = PlayerSelecter1.SelectedIndex;
			PlayerSelecter1.SelectedIndex = PlayerSelecter2.SelectedIndex;
			PlayerSelecter2.SelectedIndex = tmp;
		}


		private void ResetScores_Click (object sender, EventArgs e) {
			Program.ResetScore();
			ScoresLabel.Text = "Счёт: 0:0";
		}


		private void StartButton_Click (object sender, EventArgs e) {
			Program.SetBattle(PlayerSelecter1.SelectedIndex, PlayerSelecter2.SelectedIndex);

			if (ShowOnlyFinal.Checked) {
				for (int i = 1; i <= RoundsCounter.Value; i++) {
					roundInfo = Program.PlayRound(i % 2 == 1);
				}
			} else {
				roundInfo = Program.PlayRound(currentRound % 2 == 1);
				currentStep = ShowRoundEnd.Checked ? roundInfo.Count - 1 : 1;
				ShowState();
			}
			/*
			if (ShowOnlyFinal.Checked) {
				for (int i = 0; i < RoundsCounter.Value; i++) {
					roundInfo = Program.PlayRound(i % 2 == 0);
				}
			} else {
				currentTournament = 1;
				currentRound = 0;
				NextRound_Click(null, null);
			}
			*/
		}

		private void NextRound_Click (object sender, EventArgs e) {

			if (currentRound == RoundsCount.Value - 1) {
				NextTournament_Click(null, null);
			}
			currentRound++;

			roundInfo = Program.PlayRound(true);

			currentStep = ShowRoundEnd.Checked ? roundInfo.Count : 1;

			ShowState();
		}

		private void NextTournament_Click (object sender, EventArgs e) {
			if (currentTournament == TournamentsCount.Value - 1) return;

			for (int i = currentRound; i < RoundsCount.Value - 1; i++)
				NextRound_Click(null, null);

			currentTournament++;
			currentRound = 0;
			NextRound_Click(null, null);
		}

		private void ShowState () {
			FieldBox.Controls.Clear();

			State current = roundInfo[currentStep - 1];

			ScoresLabel.Text = "Счёт: " + current.FScore + " : " + current.SScore;

			curStep.Text = "( " + currentStep + " / " + roundInfo.Count + " )";
			curRound.Text = "( " + (currentRound + 1) + " / " + (int)RoundsCounter.Value + " )";
			curTournament.Text = "( " + (currentTournament + 1) + " / " + (int)TournamentsCounter.Value + " )";

			var table = roundInfo[currentStep - 1].table;
			int center = table.IndexOf(roundInfo[1].table[0]);

			List<Tile> tiles = new List<Tile>(29);

			int centerX = FieldBox.Width / 2 - tileLength;
			int centerY = FieldBox.Height / 2;

			int moveVert = 0;
			int direction = 1;

			double posX = 0, posY = 0;

			for (int i = center; i < table.Count; i++) {
				MTable.SBone b = table[i];
				Tile t = new Tile(b.First, b.Second);

				if (moveVert == 2) {
					moveVert = 0;
					direction *= -1;
					t.isVertical = false;
					t.X = (posX - 1) * tileWidth;
					t.Y = posY * tileWidth;
					posX += 2 * direction;
					posY += 1;
				} else if (moveVert == 1) {
					moveVert = 2;
					t.isVertical = true;
					t.X = posX * tileWidth;
					t.Y = posY * tileWidth;
					posY += 2;
				} else {
					bool needToTurn = (posX >= 12 && direction > 0 || posX <= -8 && direction < 0);
					if (needToTurn) {
						moveVert = 1;
						t.isVertical = false;
					}

					if (t.isVertical) {
						t.X = centerX + posX * tileWidth;
						t.Y = centerY + tileWidth * (posY - 0.5);
						posX += direction;
					} else {
						t.X = centerX + posX * tileWidth;
						t.Y = centerY + posY * tileWidth;
						posX += (needToTurn ? 1 : 2) * direction;
					}
				}
				tiles.Add(t);
			}

			moveVert = 0;
			direction = -1;

			posX = -2;
			posY = 0;

			for (int i = center - 1; i >= 0; i--) {
				MTable.SBone b = table[i];
				Tile t = new Tile(b.First, b.Second);

				if (moveVert == 2) {
					direction *= -1;
					moveVert = 0;
					t.isVertical = false;
					//if (direction < 0)
					t.X = (posX + direction) * tileWidth;
					//else t.X = posX * tileWidth;
					t.Y = posY * tileWidth;
					posX += 2 * direction;
					posY -= 1;
				} else if (moveVert == 1) {
					moveVert = 2;
					t.isVertical = true;
					t.X = posX * tileWidth;
					t.Y = posY * tileWidth;
					posY -= 2;
				} else {
					bool needToTurn = (posX >= 12 && direction > 0 || posX <= -8 && direction < 0);
					if (needToTurn) {
						moveVert = 1;
						t.isVertical = false;
					}
					if (t.isVertical) {
						t.X = centerX + (posX + 1) * tileWidth;
						t.Y = centerY + tileWidth * (posY - 0.5);
						posX += direction;
					} else {
						t.X = centerX + posX * tileWidth;
						t.Y = centerY + posY * tileWidth;
						posX += (needToTurn ? 1 : 2) * direction;
					}
				}

				tiles.Add(t);
			}
			
			var tilesToShow = new List<Rectangle>(tiles.Count);
			foreach (var t in tiles) {
				var pb = new PictureBox();
				if (t.isVertical) {
					pb.Width = tileWidth;
					pb.Height = tileLength;
				} else {
					pb.Width = tileLength;
					pb.Height = tileWidth;
				}

			pb.Left = (int) t.X;
				pb.Top =  (int) t.Y;
				if (t.first == t.second)
					pb.BackColor = Color.Aqua;
				
				pb.BorderStyle = BorderStyle.FixedSingle;
				FieldBox.Controls.Add(pb);
			}

		}


		private void PrevStep_Click (object sender, EventArgs e) {
			if (roundInfo != null && currentStep != 1) {
				currentStep--;
				ShowState();
			}
		}
		private void NextStep_Click (object sender, EventArgs e) {
			if (roundInfo != null && currentStep != roundInfo.Count) {
				currentStep++;
				ShowState();
			}
		}

		private struct Tile {
			public int first, second;
			public double X, Y;
			public bool isVertical;

			public Tile (ushort first, ushort second, double X, double Y) {
				this.first = first;
				this.second = second;
				this.X = (int) X;
				this.Y = (int) Y;
				isVertical = first == second;
			}

			public Tile (ushort first, ushort second) {
				this.first = first;
				this.second = second;
				this.X = -1;
				this.Y = -1;
				isVertical = first == second;
			}
		}
	}
}
