using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualDomino {
	static class Program {
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>

		// public static Player[] PLAYERS = new Player[] { new Bot_Shergalis(), new Bot_Kuchinskas()};

		public static Type[] PLAYER_TYPES = new Type[] { typeof(Bot_Shergalis), typeof(Bot_Kuchinskas), };
		public static String[] PLAYER_NAMES = { Bot_Shergalis.PlayerName, Bot_Kuchinskas.PlayerName, };

		[STAThread]
		static void Main () {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			setPlayersList();

			Application.Run(new Form1());
		}

		public static void Battle () {
			//Type MFP = AVAILABLE_PLAYERS[Form1.PlayerSelecter1.SelectedIndex].GetType();
			//Type MSP = AVAILABLE_PLAYERS[Form1.PlayerSelecter2.SelectedIndex].GetType();

			Type MFP = PLAYER_TYPES[Form1.PlayerSelecter1.SelectedIndex];
			Type MSP = PLAYER_TYPES[Form1.PlayerSelecter2.SelectedIndex];

			// MTable.MFPlayer = (Player) MFP.GetConstructor(new Type[] { }).Invoke(new Player[] { });
			// MTable.MSPlayer = (Player) MSP.GetConstructor(new Type[] { }).Invoke(new Player[] { });

			Player First = (Player)Activator.CreateInstance(MFP);
			Player Second = (Player)Activator.CreateInstance(MSP);

			MTable.StartBattle(First, Second);
		}

		private static void setPlayersList () { // засовывает в ComboBox'ы игроков для проведения турнира
			Form1.PlayerSelecter1.DataSource = PLAYER_NAMES;
			Form1.PlayerSelecter2.DataSource = PLAYER_NAMES;
		}
	}

	[SuppressMessage("ReSharper", "InconsistentNaming")]
	abstract class Player {
		public string PlayerName;
		private static List<MTable.SBone> lHand;
		public static void Initialize () { lHand = new List<MTable.SBone>(); }
		public static void PrintAll () { MTable.PrintAll(lHand); }
		public static int GetCount () { return lHand.Count; }

		public static void AddItem (MTable.SBone sb) { lHand.Add(sb); }

		public static int GetScore () { // Сумма очков на руке
			int n = 0;
			foreach (MTable.SBone bone in lHand)
				n += bone.First + bone.Second;

			return (lHand.Count == 1 && n == 0) ? 25 : n;
		}

		public abstract bool MakeStep (out MTable.SBone sb, out bool End);
	}
}
