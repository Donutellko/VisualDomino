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

		private void StartButton_Click (object sender, EventArgs e) {
			Program.Battle();
		}

		public Form1 () {
			InitializeComponent();

			PlayerSelecter1 = SelectPlayer1;
			PlayerSelecter2 = SelectPlayer2;


		}
		
	}
}
