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
			this.SelectPlayer2.TabIndex = 0;
			// 
			// SelectPlayer1
			// 
			this.SelectPlayer1.FormattingEnabled = true;
			this.SelectPlayer1.Location = new System.Drawing.Point(12, 25);
			this.SelectPlayer1.Name = "SelectPlayer1";
			this.SelectPlayer1.Size = new System.Drawing.Size(183, 21);
			this.SelectPlayer1.TabIndex = 1;
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
			this.StartButton.TabIndex = 4;
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
			this.RoundsCount.Location = new System.Drawing.Point(136, 118);
			this.RoundsCount.Minimum = new decimal(new int[] {
            1,
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
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1264, 729);
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
	}
}

