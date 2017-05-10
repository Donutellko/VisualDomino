namespace VisualDomino {
	partial class Form1 {
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose (bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent () {
			this.SelectPlayer2 = new System.Windows.Forms.ComboBox();
			this.SelectPlayer1 = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.StartButton = new System.Windows.Forms.Button();
			this.SwapPlayers = new System.Windows.Forms.Button();
			this.TournamentsCount = new System.Windows.Forms.NumericUpDown();
			this.RoundsCount = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.ResetScores = new System.Windows.Forms.Button();
			this.ScoresLabel = new System.Windows.Forms.Label();
			this.FieldBox = new System.Windows.Forms.GroupBox();
			this.NextRound = new System.Windows.Forms.Button();
			this.NextStep = new System.Windows.Forms.Button();
			this.NextTournament = new System.Windows.Forms.Button();
			this.curStep = new System.Windows.Forms.Label();
			this.curRound = new System.Windows.Forms.Label();
			this.curTournament = new System.Windows.Forms.Label();
			this.PrevStep = new System.Windows.Forms.Button();
			this.Finish = new System.Windows.Forms.Button();
			this.ShowRoundEnd = new System.Windows.Forms.CheckBox();
			this.ShowOnlyFinal = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.TournamentsCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RoundsCount)).BeginInit();
			this.SuspendLayout();
			// 
			// SelectPlayer2
			// 
			this.SelectPlayer2.FormattingEnabled = true;
			this.SelectPlayer2.Location = new System.Drawing.Point(12, 65);
			this.SelectPlayer2.Name = "SelectPlayer2";
			this.SelectPlayer2.Size = new System.Drawing.Size(183, 21);
			this.SelectPlayer2.TabIndex = 1;
			// 
			// SelectPlayer1
			// 
			this.SelectPlayer1.FormattingEnabled = true;
			this.SelectPlayer1.Location = new System.Drawing.Point(12, 25);
			this.SelectPlayer1.Name = "SelectPlayer1";
			this.SelectPlayer1.Size = new System.Drawing.Size(183, 21);
			this.SelectPlayer1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Первый игрок:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 49);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(78, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Второй игрок:";
			// 
			// StartButton
			// 
			this.StartButton.Location = new System.Drawing.Point(12, 180);
			this.StartButton.Name = "StartButton";
			this.StartButton.Size = new System.Drawing.Size(75, 23);
			this.StartButton.TabIndex = 0;
			this.StartButton.Text = "Запуск";
			this.StartButton.UseVisualStyleBackColor = true;
			this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
			// 
			// SwapPlayers
			// 
			this.SwapPlayers.Location = new System.Drawing.Point(201, 44);
			this.SwapPlayers.Name = "SwapPlayers";
			this.SwapPlayers.Size = new System.Drawing.Size(75, 23);
			this.SwapPlayers.TabIndex = 5;
			this.SwapPlayers.Text = "Поменять";
			this.SwapPlayers.UseVisualStyleBackColor = true;
			this.SwapPlayers.Click += new System.EventHandler(this.SwapPlayers_Click);
			// 
			// TournamentsCount
			// 
			this.TournamentsCount.Location = new System.Drawing.Point(136, 92);
			this.TournamentsCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.TournamentsCount.Name = "TournamentsCount";
			this.TournamentsCount.Size = new System.Drawing.Size(59, 20);
			this.TournamentsCount.TabIndex = 6;
			this.TournamentsCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// RoundsCount
			// 
			this.RoundsCount.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.RoundsCount.Location = new System.Drawing.Point(136, 118);
			this.RoundsCount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.RoundsCount.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.RoundsCount.Name = "RoundsCount";
			this.RoundsCount.Size = new System.Drawing.Size(59, 20);
			this.RoundsCount.TabIndex = 7;
			this.RoundsCount.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 94);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(121, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "Количество турниров: ";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(9, 120);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(104, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Раундов в турнире:";
			// 
			// ResetScores
			// 
			this.ResetScores.Location = new System.Drawing.Point(12, 488);
			this.ResetScores.Name = "ResetScores";
			this.ResetScores.Size = new System.Drawing.Size(126, 23);
			this.ResetScores.TabIndex = 10;
			this.ResetScores.Text = "Сброс";
			this.ResetScores.UseVisualStyleBackColor = true;
			this.ResetScores.Click += new System.EventHandler(this.ResetScores_Click);
			// 
			// ScoresLabel
			// 
			this.ScoresLabel.AutoSize = true;
			this.ScoresLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
			this.ScoresLabel.Location = new System.Drawing.Point(623, 11);
			this.ScoresLabel.Name = "ScoresLabel";
			this.ScoresLabel.Size = new System.Drawing.Size(84, 20);
			this.ScoresLabel.TabIndex = 11;
			this.ScoresLabel.Text = "Счёт: 0:0";
			// 
			// FieldBox
			// 
			this.FieldBox.BackgroundImage = global::VisualDomino.Properties.Resources.tableBG2;
			this.FieldBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.FieldBox.Location = new System.Drawing.Point(282, 63);
			this.FieldBox.Name = "FieldBox";
			this.FieldBox.Size = new System.Drawing.Size(969, 652);
			this.FieldBox.TabIndex = 12;
			this.FieldBox.TabStop = false;
			// 
			// NextRound
			// 
			this.NextRound.Location = new System.Drawing.Point(12, 373);
			this.NextRound.Name = "NextRound";
			this.NextRound.Size = new System.Drawing.Size(126, 23);
			this.NextRound.TabIndex = 13;
			this.NextRound.Text = "Следующий раунд";
			this.NextRound.UseVisualStyleBackColor = true;
			this.NextRound.Click += new System.EventHandler(this.NextRound_Click);
			// 
			// NextStep
			// 
			this.NextStep.Location = new System.Drawing.Point(12, 327);
			this.NextStep.Name = "NextStep";
			this.NextStep.Size = new System.Drawing.Size(126, 23);
			this.NextStep.TabIndex = 14;
			this.NextStep.Text = "Следующий ход";
			this.NextStep.UseVisualStyleBackColor = true;
			this.NextStep.Click += new System.EventHandler(this.NextStep_Click);
			// 
			// NextTournament
			// 
			this.NextTournament.Location = new System.Drawing.Point(12, 418);
			this.NextTournament.Name = "NextTournament";
			this.NextTournament.Size = new System.Drawing.Size(126, 23);
			this.NextTournament.TabIndex = 15;
			this.NextTournament.Text = "Следующий турнир";
			this.NextTournament.UseVisualStyleBackColor = true;
			this.NextTournament.Click += new System.EventHandler(this.NextTournament_Click);
			// 
			// curStep
			// 
			this.curStep.AutoSize = true;
			this.curStep.Location = new System.Drawing.Point(144, 318);
			this.curStep.Name = "curStep";
			this.curStep.Size = new System.Drawing.Size(12, 13);
			this.curStep.TabIndex = 16;
			this.curStep.Text = "/";
			// 
			// curRound
			// 
			this.curRound.AutoSize = true;
			this.curRound.Location = new System.Drawing.Point(144, 378);
			this.curRound.Name = "curRound";
			this.curRound.Size = new System.Drawing.Size(12, 13);
			this.curRound.TabIndex = 17;
			this.curRound.Text = "/";
			// 
			// curTournament
			// 
			this.curTournament.AutoSize = true;
			this.curTournament.Location = new System.Drawing.Point(144, 438);
			this.curTournament.Name = "curTournament";
			this.curTournament.Size = new System.Drawing.Size(12, 13);
			this.curTournament.TabIndex = 18;
			this.curTournament.Text = "/";
			// 
			// PrevStep
			// 
			this.PrevStep.Location = new System.Drawing.Point(12, 298);
			this.PrevStep.Name = "PrevStep";
			this.PrevStep.Size = new System.Drawing.Size(126, 23);
			this.PrevStep.TabIndex = 19;
			this.PrevStep.Text = "Предыдущий ход";
			this.PrevStep.UseVisualStyleBackColor = true;
			this.PrevStep.Click += new System.EventHandler(this.PrevStep_Click);
			// 
			// Finish
			// 
			this.Finish.Location = new System.Drawing.Point(12, 447);
			this.Finish.Name = "Finish";
			this.Finish.Size = new System.Drawing.Size(126, 23);
			this.Finish.TabIndex = 20;
			this.Finish.Text = "Доиграть всё";
			this.Finish.UseVisualStyleBackColor = true;
			// 
			// ShowRoundEnd
			// 
			this.ShowRoundEnd.AutoSize = true;
			this.ShowRoundEnd.Checked = true;
			this.ShowRoundEnd.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ShowRoundEnd.Location = new System.Drawing.Point(12, 264);
			this.ShowRoundEnd.Name = "ShowRoundEnd";
			this.ShowRoundEnd.Size = new System.Drawing.Size(159, 17);
			this.ShowRoundEnd.TabIndex = 21;
			this.ShowRoundEnd.Text = "Отображать конец раунда";
			this.ShowRoundEnd.UseVisualStyleBackColor = true;
			// 
			// ShowOnlyFinal
			// 
			this.ShowOnlyFinal.AutoSize = true;
			this.ShowOnlyFinal.Location = new System.Drawing.Point(12, 241);
			this.ShowOnlyFinal.Name = "ShowOnlyFinal";
			this.ShowOnlyFinal.Size = new System.Drawing.Size(180, 17);
			this.ShowOnlyFinal.TabIndex = 22;
			this.ShowOnlyFinal.Text = "Отображать только результат";
			this.ShowOnlyFinal.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1264, 729);
			this.Controls.Add(this.ShowOnlyFinal);
			this.Controls.Add(this.ShowRoundEnd);
			this.Controls.Add(this.Finish);
			this.Controls.Add(this.PrevStep);
			this.Controls.Add(this.curTournament);
			this.Controls.Add(this.curRound);
			this.Controls.Add(this.curStep);
			this.Controls.Add(this.NextTournament);
			this.Controls.Add(this.NextStep);
			this.Controls.Add(this.NextRound);
			this.Controls.Add(this.FieldBox);
			this.Controls.Add(this.ScoresLabel);
			this.Controls.Add(this.ResetScores);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.RoundsCount);
			this.Controls.Add(this.TournamentsCount);
			this.Controls.Add(this.SwapPlayers);
			this.Controls.Add(this.StartButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.SelectPlayer1);
			this.Controls.Add(this.SelectPlayer2);
			this.HelpButton = true;
			this.Name = "Form1";
			this.Text = "Турнир по домино";
			((System.ComponentModel.ISupportInitialize)(this.TournamentsCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RoundsCount)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox SelectPlayer1;
		private System.Windows.Forms.ComboBox SelectPlayer2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button StartButton;
		private System.Windows.Forms.Button SwapPlayers;
		private System.Windows.Forms.NumericUpDown TournamentsCount;
		private System.Windows.Forms.NumericUpDown RoundsCount;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button ResetScores;
		private System.Windows.Forms.Label ScoresLabel;
		private System.Windows.Forms.GroupBox FieldBox;
		private System.Windows.Forms.Button NextRound;
		private System.Windows.Forms.Button NextStep;
		private System.Windows.Forms.Button NextTournament;
		private System.Windows.Forms.Label curStep;
		private System.Windows.Forms.Label curRound;
		private System.Windows.Forms.Label curTournament;
		private System.Windows.Forms.Button PrevStep;
		private System.Windows.Forms.Button Finish;
		private System.Windows.Forms.CheckBox ShowRoundEnd;
		private System.Windows.Forms.CheckBox ShowOnlyFinal;
	}
}

