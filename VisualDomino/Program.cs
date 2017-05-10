using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualDomino {
	static class Program {
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		
		public static Type[] PLAYER_TYPES = new Type[] { typeof(Player), typeof(Bot_Random), typeof(Bot_Shergalis), typeof(Bot_Stoyanovskii), typeof(Bot_Kuchinskas), typeof(Bot_Random)};

		public static String[] PLAYER_NAMES = new string[PLAYER_TYPES.Length]; //{ "[НЕДОСТУПНО] Играть самому", Bot_Shergalis.PlayerName, Bot_Stoyanovskii.PlayerName, Bot_Kuchinskas.PlayerName, Bot_Random.PlayerName};
		
		//public static String[] PLAYER_NAMES = { "[НЕДОСТУПНО] Играть самому", "Шергалис", "Кучинскас", };

		[STAThread]
		static void Main () {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			PLAYER_NAMES[0] = "[НЕДОСТУПНО] Играть самому";

			for(int i = 1; i < PLAYER_TYPES.Length; i++) {
				Type t = PLAYER_TYPES[i];
				PLAYER_NAMES[i] = t.Name;

				//PLAYER_NAMES[i] = t.GetProperty("PlayerName").ToString();
				//FieldInfo f = t.GetField("PlayerName");
				//PLAYER_NAMES[i] = (String) f.GetCustomAttributes(false)[0];
			}
			
			Application.Run(new Form1());
		}

		public static void SetBattle (int f, int s) {
			Type MFP = PLAYER_TYPES[f];
			Type MSP = PLAYER_TYPES[s];
			
			MTable.first  = (Player) Activator.CreateInstance(MFP);
			MTable.second = (Player) Activator.CreateInstance(MSP);
		}

		public static List<State> PlayRound(bool firstIsFirst) {
			return MTable.RunRound(firstIsFirst);
		}

		public static void setPlayersList () { // засовывает в ComboBox'ы игроков для проведения турнира
			Form1.PlayerSelecter1.Items.AddRange(PLAYER_NAMES);
			Form1.PlayerSelecter2.Items.AddRange(PLAYER_NAMES);
		}

		public static void ResetScore() {
			
		}
	}

	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public abstract class Player {
		public string PlayerName = "Your Name";
		public List<MTable.SBone> lHand;
		
		public void Initialize () { lHand = new List<MTable.SBone>(); }
		public void PrintAll () { MTable.PrintAll(lHand); }
		public int GetCount () { return lHand.Count; }

		public void AddItem (MTable.SBone sb) { lHand.Add(sb); }

		public int GetScore () { // Сумма очков на руке
			int n = 0;
			foreach (MTable.SBone bone in lHand)
				n += bone.First + bone.Second;

			return (lHand.Count == 1 && n == 0) ? 25 : n;
		}

		public abstract bool MakeStep (out MTable.SBone sb, out bool End);
	}
}
