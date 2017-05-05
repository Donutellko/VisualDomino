using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualDomino {
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	[SuppressMessage("ReSharper", "JoinDeclarationAndInitializer")]
	[SuppressMessage("ReSharper", "TooWideLocalVariableScope")]

	class MTable {
		public static Player MFPlayer, MSPlayer;

		public const bool PrintOutput = false; // Для ускорения тестирования
		public static int COUNT_OF_MATCHES_FOR_TEST = 10000;

		static double sum1 = 0, sum2 = 0;

		public const int conStartCount = 7;// количество доминошек в руке в начале игры

		private static List<SBone> lBoneyard;// "Базар" доминошек
		private static List<SBone> lGame;//Текущий расклад на столе
		private static int intGameStep = 1;// Номер хода игры
		private static int intLastTaken, intTaken;// Количество взятых доминушек игроком за прошлый \ текущий ход 
		private static Random rnd;// Генератор случайных чисел
		public enum EFinish { Play = 0, First, Second, Lockdown };  // Состояние игры // играем, выиграл первый, выиграл второй, рыба

		static void MMain () {
			rnd = new Random();
			Console.Write("Что-нибудь, чтобы задать значения вручную: ");

			if (Console.ReadLine().Length > 0) {
				Console.Write("Количество турниров = "); String tmp0 = Console.ReadLine();
				int t = 0;
				if (Int32.TryParse(tmp0, out t)) COUNT_OF_MATCHES_FOR_TEST = t * 20;
			}


			double wins1 = 0, wins2 = 0;

			for (int i = 0; i <= COUNT_OF_MATCHES_FOR_TEST; i++) {
				if (i % 20 == 0 && i > 0) {
					if (sum1 < sum2) wins1++;
					else if (sum1 > sum2) wins2++;

					sum1 = 0;
					sum2 = 0;

					Console.WriteLine("Турнир " + (i / 20) + ": " + Math.Round(100 * wins1 / (wins1 + wins2), 1));
				}


				lBoneyard?.Clear();
				lGame?.Clear();
				intLastTaken = 0;
				intTaken = 0;


				bool blnFirst; // кто сейчас ходит
				bool blnFRes, blnSRes; // результат текущего хода игроков =TRUE, если ход состоялся

				EFinish efFinish = EFinish.Play; // признак окончания игры
				string[] arrFinishMsg = { "---", "Победил первый игрок!", "Победил второй игрок!", "Рыба!" }; // сообщения о результате игры

				int intBoneyard = 0; // количество доминушек в базаре, нужно для определения корректности хода игрока
				SBone sb; // Чем ходить
				bool blnEnd; // куда ходить

				// Инициализация игры
				Initialize();
				// Раздача доминошек в начале игры
				GetHands();
				// первая доминушка - первая из базара
				// определяем случайным образом доминушку из базара
				int intN = rnd.Next(lBoneyard.Count - 1);
				lGame.Add(lBoneyard[intN]);
				lBoneyard.RemoveAt(intN);


				// вывод на экран начального состояния игры
				if (PrintOutput) {
					Console.WriteLine("*************ИГРА НАЧАЛАСЬ*********************");
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine();
					Console.WriteLine("*************Шаг #0");
					Console.ForegroundColor = ConsoleColor.White;
					PrintAll(lGame);
					Console.ForegroundColor = ConsoleColor.Cyan;
					Console.WriteLine("ИГРОК " + MFPlayer.PlayerName);
					MFPlayer.PrintAll();
					Console.ForegroundColor = ConsoleColor.Magenta;
					Console.WriteLine("ИГРОК " + MSPlayer.PlayerName);
					MSPlayer.PrintAll();
					//Console.ReadKey();
				}

				blnFRes = true;
				blnSRes = true;
				// Первым ходит первый игрок
				blnFirst = i % 20 < 10; // COUNT_OF_MATCHES_FOR_TEST % 20;

				intBoneyard = lBoneyard.Count;
				//-----------------------------------------------------------------
				// ИГРА
				do {
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.ForegroundColor = ConsoleColor.White;

					// кто ходит? ---- Ходит первый игрок
					if (blnFirst) {
						if (PrintOutput) {
							Console.ForegroundColor = ConsoleColor.Yellow;
							Console.WriteLine();
							Console.WriteLine("*************Шаг #" + intGameStep + " " + MFPlayer.PlayerName);
							Console.ForegroundColor = ConsoleColor.White;
						}

						// количество взятых доминушек
						intLastTaken = intTaken;
						intTaken = 0;
						// ход первого игрока
						intBoneyard = lBoneyard.Count;
						blnFRes = MFPlayer.MakeStep(out sb, out blnEnd);

						// если ход сделан
						if (blnFRes) {
							// пристраиваем доминушку
							if (SetBone(sb, blnEnd) == false) {
								PrintAll(lGame);
								Console.WriteLine(
									"!!!!!!!!Нельзя пристроить кость {0}:{1} ({3})!!!!!! {2}", sb.First, sb.Second, MFPlayer.PlayerName,
									!blnEnd ? "слева" + lGame[0].First + ":" + lGame[0].Second : "справа " + lGame[lGame.Count - 1].First + ":" + lGame[lGame.Count - 1].Second);
								Console.Beep(2000, 1000);
								Console.ReadLine();
								return;
							}
						} else if (intBoneyard == lBoneyard.Count && intBoneyard > 0) { // если ход не сделан
							Console.WriteLine("!!!!!!!!Надо было добрать!!!!!! " + MFPlayer.PlayerName);
							Console.ReadLine();
							return;
						}

						if (!blnFRes && !blnSRes) // рыба
							efFinish = EFinish.Lockdown;
						else if (blnFRes) // если нет домино, то я выиграл
							if (MFPlayer.GetCount() == 0) efFinish = EFinish.First;


					} else { // кто ходит? ---- Ходит вторый игрок
						if (PrintOutput) {
							Console.ForegroundColor = ConsoleColor.Yellow;
							Console.WriteLine();
							Console.WriteLine("*************Шаг #" + intGameStep + " " + MSPlayer.PlayerName);
							Console.ForegroundColor = ConsoleColor.White;
						}

						// количество взятых доминушек
						intLastTaken = intTaken;
						intTaken = 0;
						// ход первого игрока
						intBoneyard = lBoneyard.Count;
						blnSRes = MSPlayer.MakeStep(out sb, out blnEnd);
						// если ход сделан
						if (blnSRes) {
							// пристраиваем доминушку
							if (SetBone(sb, blnEnd) == false) {
								Console.WriteLine("!!!!!!!!Нельзя пристроить кость {0}:{1}!!!!!! {2}", sb.First, sb.Second, MSPlayer.PlayerName);
								Console.ReadLine();
								return;
							}
						}
						// если ход не сделан
						else if (intBoneyard == lBoneyard.Count && intBoneyard > 0) {
							Console.WriteLine("!!!!!!!!Надо было добрать!!!!!! " + MSPlayer.PlayerName);
							Console.ReadLine();
							return;
						}

						if (!blnFRes && !blnSRes)
							// рыба
							efFinish = EFinish.Lockdown;
						else if (blnSRes)
							// если нет домино, то я выиграл
							if (MSPlayer.GetCount() == 0) efFinish = EFinish.Second;
					}
					// после хода вывести данные на столе--------------------------------------------------------
					if (PrintOutput) {
						PrintAll(lGame);
						Console.ForegroundColor = ConsoleColor.Cyan;
						Console.WriteLine("ИГРОК " + MFPlayer.PlayerName);
						MFPlayer.PrintAll();
						Console.ForegroundColor = ConsoleColor.Magenta;
						Console.WriteLine("ИГРОК " + MSPlayer.PlayerName);
						MSPlayer.PrintAll();
						Console.ReadKey();
					}

					// будет ходить другой игрок
					blnFirst = !blnFirst;
					intBoneyard = lBoneyard.Count;
					intGameStep += 1;
				} while (efFinish == EFinish.Play);
				// результат текущей игры

				if (PrintOutput) Console.WriteLine(arrFinishMsg[(int)efFinish]);
				//Console.WriteLine(MFPlayer.GetScore() + "\t:\t" + MSPlayer.GetScore());
				//Console.ReadLine();
				sum1 += MFPlayer.GetScore();
				sum2 += MSPlayer.GetScore();
			}

			Console.WriteLine("\n\nТурниров сыграно: " + COUNT_OF_MATCHES_FOR_TEST / 20.0);

			Console.WriteLine("\nResult: \n{0}:\t{1}\t=\t{2}", wins1, wins2, Math.Round(wins1 / (wins1 + wins2), 4) * 100);
			Console.Beep(200, 500);
			Console.ReadLine();
		}

		internal static double[] StartBattle (Player first, Player second, int RoundsCount) {
			MFPlayer = first;
			MSPlayer = second;

			MMain();

			return new double[] { sum1, sum2 };
		}

		//***********************************************************************
		// Инициализация игры
		//***********************************************************************
		private static void Initialize () {
			SBone sb;

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
			MFPlayer.Initialize();
			MSPlayer.Initialize();
		}
		public struct SBone {
			public ushort First;
			public ushort Second;

			public void Exchange () {
				ushort shrTemp = First;
				First = Second;
				Second = shrTemp;
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
			if (PrintOutput)
				Console.WriteLine("Взяли из базара: " + sb.First + ":" + sb.Second + " ");
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
				if (GetFromShop(out sb)) MFPlayer.AddItem(sb);
				intTaken = 0;

				if (GetFromShop(out sb)) MSPlayer.AddItem(sb);
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
}
