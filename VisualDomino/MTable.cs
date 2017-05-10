using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualDomino {
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	[SuppressMessage("ReSharper", "JoinDeclarationAndInitializer")]
	[SuppressMessage("ReSharper", "TooWideLocalVariableScope")]

	public class MTable {
		public static Player first, second;

		public static double Score1 = 0, Score2 = 0;

		public const int conStartCount = 7;// количество доминошек в руке в начале игры

		private static List<SBone> lBoneyard;// "Базар" доминошек
		private static List<SBone> lGame;//Текущий расклад на столе
		private static int intGameStep = 1;// Номер хода игры
		private static int intLastTaken, intTaken;// Количество взятых доминушек игроком за прошлый \ текущий ход 
		private static Random rnd = new Random();// Генератор случайных чисел

		private static bool prevStepIsDone;

		private static int stepsCount = 0;

		public enum EFinish { Play = 0, First, Second, Lockdown };  // Состояние игры // играем, выиграл первый, выиграл второй, рыба

		public static List<State> states = new List<State>(29);
		

		public static List<State> RunRound (bool firstIsFirst) {
			Initialize();

			states = new List<State>(29);
			
			GetHands();

			Score1 = first.GetScore();
			Score2 = second.GetScore();

			while (MakeNextStep(!(firstIsFirst = !firstIsFirst))) ;

			return states;
		}

		private static bool MakeNextStep (bool isFirstsStep) {
			Player current = isFirstsStep ? first : second;

			EFinish efFinish = EFinish.Play; // признак окончания игры

			int intBoneyard = 0; // количество доминушек в базаре, нужно для определения корректности хода игрока
			SBone sb; // Чем ходить
			bool blnEnd; // куда ходить

			if (stepsCount == 0) {
				int intN = rnd.Next(lBoneyard.Count - 1);
				lGame.Add(lBoneyard[intN]);
				lBoneyard.RemoveAt(intN);
			}
			stepsCount++;

			intBoneyard = lBoneyard.Count;

			intLastTaken = intTaken;
			intTaken = 0;
			// ход первого игрока
			intBoneyard = lBoneyard.Count;
			bool stepIsDone = current.MakeStep(out sb, out blnEnd);
			
			states.Add(new State(lGame, first.lHand, second.lHand, (int)Score1, (int)Score2));

			// если ход сделан
			if (stepIsDone) {
				// пристраиваем доминушку
				if (SetBone(sb, blnEnd) == false) {
					PrintAll(lGame);
					Console.WriteLine(
						"!!!!!!!!Нельзя пристроить кость {0}:{1} ({3})!!!!!! {2}", sb.First, sb.Second, first.PlayerName,
						!blnEnd ? "слева" + lGame[0].First + ":" + lGame[0].Second : "справа " + lGame[lGame.Count - 1].First + ":" + lGame[lGame.Count - 1].Second);
					Console.Beep(2000, 1000);
					Console.ReadLine();
					return false;
				}
			} else if (intBoneyard == lBoneyard.Count && intBoneyard > 0) { // если ход не сделан
				Console.WriteLine("!!!!!!!!Надо было добрать!!!!!! " + first.PlayerName);
				Console.ReadLine();
				return false;
			}

			if (!stepIsDone && !prevStepIsDone) { // рыба
				efFinish = EFinish.Lockdown;
				return false;
			} else if (stepIsDone && first.GetCount() == 0) efFinish = EFinish.First;

			prevStepIsDone = stepIsDone;

			Score1 = first.GetScore();
			Score2 = second.GetScore();

			return true;
		}



		//***********************************************************************
		// Инициализация игры
		//***********************************************************************
		private static void Initialize () {
			SBone sb;

			stepsCount = 0;
			Score1 = 0;
			Score2 = 0;

			prevStepIsDone = true;


			// Очищаем коллекции в этом модуле
			lBoneyard = new List<SBone>();
			lGame = new List<SBone>();

			// Формирование базара
			for (ushort shrC = 0; shrC <= 6; shrC++)
				for (ushort shrB = shrC; shrB <= 6; shrB++) {
					sb.First = shrC;
					sb.Second = shrB;
					lBoneyard.Add(sb);
				}

			// Инициализация игроков
			first.Initialize();
			second.Initialize();
		}

		public struct SBone {
			public ushort First;
			public ushort Second;

			public void Exchange () {
				ushort shrTemp = First;
				First = Second;
				Second = shrTemp;
			}

			public bool hasSpot (int s) {
				return First == s || Second == s;
			}
		}

		//***********************************************************************
		// Получение случайной доминошки (sb) из базара
		// Возвращает FALSE, если базар пустой
		//***********************************************************************
		public static bool GetFromShop (out SBone sb) {
			int intN;
			sb.First = 7; sb.Second = 7;

			if (lBoneyard.Count == 0) return false;

			// Подсчет количества взятых доминушек одним игроком за текущий ход
			intTaken += 1;
			// определяем случайным образом доминушку из базара
			intN = rnd.Next(lBoneyard.Count - 1);
			sb = lBoneyard[intN];

			lBoneyard.RemoveAt(intN);// удаляем ее из базара
			return true;
		}

		//***********************************************************************
		// Возвращает количество оставшихся доминошек в базаре
		//***********************************************************************
		public static int GetShopCount () {
			return lBoneyard.Count;
		}

		//***********************************************************************
		// Возвращает количество взятых игроком доминушек за текущий ход
		//***********************************************************************
		public static int GetTaken () { return intLastTaken; }

		//***********************************************************************
		// Возвращает информацию о текущем раскладе на столе
		//***********************************************************************
		public static List<SBone> GetGameCollection () { return lGame.ToList(); }

		//***********************************************************************
		// Раздача доминошек обоим игрокам в начале игры
		//***********************************************************************
		public static void GetHands () {
			SBone sb;
			for (int intC = 0; intC < conStartCount; intC++) {
				if (GetFromShop(out sb))
					first.AddItem(sb);
				intTaken = 0;

				if (GetFromShop(out sb))
					second.AddItem(sb);
				intTaken = 0;
			}
		}

		//***********************************************************************
		// Вывод на экран всех элементов коллекции colX
		//***********************************************************************
		public static void PrintAll (List<SBone> lItems) {
			foreach (SBone sb in lItems)
				Console.Write(sb.First + ":" + sb.Second + "  ");
			Console.WriteLine();
		}

		//***********************************************************************
		// Положить доминушку на стол
		//***********************************************************************
		private static bool SetBone (SBone sb, bool blnEnd) {
			SBone sbT;
			if (blnEnd) {
				sbT = lGame[lGame.Count - 1];
				if (sbT.Second == sb.First) {
					lGame.Add(sb);
					return true;
				}
				if (sbT.Second == sb.Second) {
					sb.Exchange();
					lGame.Add(sb);
					return true;
				}
				return false;
			}

			sbT = lGame[0];
			if (sbT.First == sb.Second) {
				lGame.Insert(0, sb);
				return true;
			}
			if (sbT.First == sb.First) {
				sb.Exchange();
				lGame.Insert(0, sb);
				return true;
			}
			return false;
		}


	}

	public class State {
		public List<MTable.SBone> table;
		public List<MTable.SBone> FHand;
		public List<MTable.SBone> SHand;
		
		public int FScore, SScore, BoneyardCount;

		public State (List<MTable.SBone> lGame, List<MTable.SBone> lHand1, List<MTable.SBone> lHand2, int Score1, int Score2) {
			this.BoneyardCount = 29 - lGame.Count - lHand1.Count - lHand2.Count;

			this.table = CloneList(lGame);

			this.FHand = CloneList(lHand1);
			this.SHand = CloneList(lHand2);

			this.FScore = Score1;
			this.SScore = Score2;
		}

		private List<MTable.SBone> CloneList(List<MTable.SBone> list) {
			var result = new List<MTable.SBone>(list.Count);
			foreach(var tile in list)
				result.Add(tile);
			return result;
		}
	}
}
