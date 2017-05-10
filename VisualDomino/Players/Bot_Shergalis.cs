using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace VisualDomino {
	[SuppressMessage("ReSharper", "InconsistentNaming")]

	class Bot_Shergalis : Player {
		public new const string PlayerName = "Джефф Дин";

		private int[] probs;

		public static double KOEFF_FOR_ENEMIES_MOVE_POSSIB = 1.0;    // Для количество доминошек, миеющих такое количество точек
		public static double KOEFF_FOR_ASSURED_MOVES_COUNT = 0.5;   // Для количества ходов, которые можно сделать
		public static double KOEFF_FOR_OWN_SCORES = 0.3;    // Для количество точек доминошек

		public static double KOEFF_FOR_NEXT_ENEMY_MOVE_POSSIB = 0.2;     // Для влияния остающегося снаружи при одном из следующих продуманных ходов 
		public static double KOEFF_FOR_NEXT_MOVE_WEIGHT_DECREASE = 0.3;     // Для веса просчитываемого следующего и послеследующего хода
		public static double EMPTY_WEIGHT = 10;      // Вес пустой доминошки, определяющий скорость избавления от неё

		private int leftest, rightest;   // Содержат два возможных хода


		// Сделать ход: возвращает false, если ход сделать нельзя; sb - чем ходить, End: true = направо
		public override bool MakeStep (out MTable.SBone sb, out bool End) {
			var table = MTable.GetGameCollection();
			int myScore = GetScore(),
				tableScore = GetSBoneListScore(table);

			bool FishIsAnOption = myScore < 168 - myScore - tableScore; // Разумно ли будет походить для рыбы (меньше ли очков, чем будет у противника при рыбе)

			leftest = table[0].First;   // точки, находящиеся на краях
			rightest = table[table.Count - 1].Second;

			List<MTable.SBone> Variants;

			// Получение необходимых доминошек из базара
			while ((Variants = GetVariantsForStep()).Count == 0) {
				MTable.SBone tile;
				if (!MTable.GetFromShop(out tile)) {    // если базар пуст, а ход не сделать
					sb = new MTable.SBone();
					End = false;
					return false;
				}
				lHand.Add(tile);
			}

			// Получаем вероятности неналичия у соперника кости с данными точками.
			probs = CalculateReversedProbabilitiesOfEnemyHavingEachCountsOfSpotsOnTilesInHand();

			// Засовываем кости в обёртку
			var sTiles = new List<STiles>();
			foreach (var tile in Variants)
				sTiles.Add(new STiles(tile, probs, leftest, rightest));

			var cst = new CompSTiles<STiles>(); // Позволяет сортировать обёрнутые доминошки по их весу


			// Пытаемся сделать рыбу:
			if (FishIsAnOption && probs[leftest] == 7) // Если левыый из концов заблокирован для соперника
				foreach (var tile in Variants) {
					if (tile.First == rightest && probs[tile.Second] == 7 || tile.Second == rightest && probs[tile.First] == 7) {
						sb = tile;
						lHand.Remove(sb);
						End = true;
						return true;
					}
				} else if (FishIsAnOption && probs[rightest] == 7) // Если правый из концов заблокирован для соперника и выгодно сделать рыбу
				foreach (var tile in Variants)
					if (tile.First == leftest && probs[tile.Second] == 7 || tile.First == leftest && probs[tile.Second] == 7) {
						sb = tile;
						lHand.Remove(sb);
						End = false;
						return true;
					}

			foreach (var item in sTiles)
				item.wNextPoss = CheckPOSSIBOfNextMoves(ListWithout(lHand, item.tile), leftest, rightest, 0, 0);

			sTiles.Sort(cst); //сортировка по весу доминошек

			// Выбор самая выгодная по весу
			if (FishIsAnOption || sTiles[0].wEnemyHasnt > KOEFF_FOR_ASSURED_MOVES_COUNT)    // чтобы не зажать самого себя
				sb = sTiles[0].tile;
			else if (sTiles.Count > 1)
				sb = sTiles[(sTiles[0].wSpots > sTiles[1].wSpots) ? 0 : 1].tile;
			else
				sb = sTiles[0].tile;


			lHand.Remove(sb); // Остаётся только убрать выбранную доминошку из руки и походить ей

			End = DecideSide(sb, leftest, rightest, probs);
			return true;
		}

		// Проверка возможности сделать ещё несколько ходов.
		public double CheckPOSSIBOfNextMoves (List<MTable.SBone> hand, int _leftest, int _rightest, double Weight, int N) {
			Weight += 7;
			N++;
			if (N == 2) return Weight; // Больше двух не имеет смысла

			double max = 0;
			foreach (var item in hand) {
				if (item.First == _leftest || item.Second == _leftest) {
					double m = CheckPOSSIBOfNextMoves(ListWithout(hand, item), item.First == _leftest ? item.Second : item.First, _rightest, Weight, N);
					int localEnemyPoss = probs[(item.First == _leftest ? item.Second : item.First)];

					if (m > max) max = m + localEnemyPoss * KOEFF_FOR_NEXT_ENEMY_MOVE_POSSIB;
				}
				if (item.First == _rightest || item.Second == _rightest) {
					double m = CheckPOSSIBOfNextMoves(ListWithout(hand, item), _leftest, item.First == _rightest ? item.Second : item.First, Weight, N);
					int localEnemyPoss = probs[(item.First == _leftest ? item.Second : item.First)];
					if (m > max) max = m + localEnemyPoss * KOEFF_FOR_NEXT_ENEMY_MOVE_POSSIB;
				}
			}
			return Weight + KOEFF_FOR_NEXT_MOVE_WEIGHT_DECREASE * max;
		}

		public List<MTable.SBone> ListWithout (List<MTable.SBone> list, MTable.SBone b) {
			List<MTable.SBone> tmp = new List<MTable.SBone>();
			tmp.AddRange(list);
			tmp.Remove(b);
			return tmp;
		}

		public bool DecideSide (MTable.SBone sb, int leftest, int rightest, int[] probs) {
			if (sb.First == leftest && sb.Second == rightest)   // Если можно и так, и так
				return probs[sb.First] >= probs[sb.Second]; // Чтобы у наружней был больше шанс не оказаться в руке соперника
			else if (sb.First == rightest && sb.Second == leftest) // то же, но наоборот
				return probs[sb.First] <= probs[sb.Second];
			else if (sb.First == sb.Second) // Если они равны
				return sb.First == rightest;
			else if (sb.First == leftest || sb.First == rightest) // если только первую
				return sb.First == rightest;
			else // if (sb.Second == leftest || sb.Second == rightest) // Если только вторую
				return sb.Second == rightest;
		}

		// На основе вывода этого метода делается вывод о том, какой костью соперник с наименьшей вероятностью обладает. Чем больше число, тем меньше вероятность.
		private int[] CalculateReversedProbabilitiesOfEnemyHavingEachCountsOfSpotsOnTilesInHand () {
			int[] reversedProbabilities = new int[7];

			CountSpots(ref reversedProbabilities, MTable.GetGameCollection());
			CountSpots(ref reversedProbabilities, lHand);

			return reversedProbabilities;
		}

		public void CountSpots (ref int[] reversedProbabilities, List<MTable.SBone> list) {
			foreach (var tile in list) {
				int a = tile.First, b = tile.Second;
				reversedProbabilities[a]++;
				if (a != b) reversedProbabilities[b]++;
			}
		}

		// Получение количества очков по костям, лежащим на столе.
		private int GetSBoneListScore (List<MTable.SBone> table) {
			int score = 0;
			foreach (var tile in table) {
				score += TileScore(tile);
			}
			return score;
		}

		// получение суммы очков у переданной кости
		private int TileScore (MTable.SBone tile) {
			return tile.First + tile.Second;
		}

		// Возвращает список всех костей, которыми можно походить на данном ходе, заодно поворачивая их.
		private List<MTable.SBone> GetVariantsForStep () {
			var s = new List<MTable.SBone>();

			foreach (var tile in lHand) {
				if (tile.First == leftest || tile.Second == rightest || tile.First == rightest || tile.Second == leftest)
					s.Add(tile);
			}
			return s;
		}

		// Возвращает количество костей на руке, содержащих переданное количество точек
		private int CountOfTilesWith (int n) {
			int count = 0;
			foreach (var tile in lHand) {
				if (tile.First == n || tile.Second == n) {
					count++;
				}
			}
			return count;
		}

		private List<MTable.SBone> GetTilesWith (int n) {
			var s = new List<MTable.SBone>();
			foreach (var tile in lHand) {
				if (tile.First == n || tile.Second == n) {
					s.Add(tile);
				}
			}
			return s;
		}

		private void RemoveTile (MTable.SBone tile) { // Удаляет переданную кость из руки
			for (int i = 0; i < lHand.Count; i++) {
				if (lHand[i].First == tile.First && lHand[i].Second == tile.Second) {
					lHand.Remove(lHand[i]);
					return;
				}
			}
		}

		private class STiles {   // Обёртка для SBone, хранящая вес доминошки и позволяющая сортировать список из STiles по нему.
			public double
				wSpots,    // количество точек на доминошке (или 6, если 0)
				wEnemyHasnt,    // вероятность того, что соперник должен будет добрать доминошки
				wNextPoss;    // балл для возможности и результативности следующих ходов
			public MTable.SBone tile;

			public STiles (MTable.SBone tile, int[] vals, int leftest, int rightest) {
				this.tile = tile;
				int sc1 = vals[tile.First],
					sc2 = vals[tile.Second];

				if (tile.First == leftest && tile.First == rightest)  // Если левая подходит в оба
					wEnemyHasnt = sc2;
				else if (tile.Second == leftest && tile.Second == rightest)  // Если правая подходит в оба
					wEnemyHasnt = sc1;
				else if (tile.First == leftest && tile.Second == rightest || tile.Second == leftest && tile.Second == rightest) //  Если подходят обе
					wEnemyHasnt = Math.Max(sc1, sc2);
				else if (tile.Second == leftest || tile.Second == rightest) // Если подходит только вторая
					wEnemyHasnt = sc1;
				else if (tile.First == leftest || tile.First == rightest)  // Если подходит только первая
					wEnemyHasnt = sc2;

				wSpots = tile.First + tile.Second;
				if (wSpots == 0) wSpots = EMPTY_WEIGHT;
				else if (tile.First == tile.Second) wSpots += 5;
			}

			public bool hasN (int n) {
				return tile.First == n || tile.Second == n;
			}

			public int getScores () {
				return tile.First + tile.Second;
			}
		}

		public class STilesForBruteForce {   // Обёртка для SBone, хранящая вес доминошки и позволяющая сортировать список из STiles по нему.
			public double scoresLeft, scoresRight;
			public MTable.SBone tile;

			public STilesForBruteForce (MTable.SBone tile) {
				this.tile = tile;
			}

			public bool hasN (int n) {
				return tile.First == n || tile.Second == n;
			}

			public void increment (bool End) {
				if (End) scoresRight++;
				else scoresLeft++;
			}
		}

		class CompSTiles<T> : IComparer<T>
			where T : STiles {
			public int Compare (T x, T y) {
				return (
					x.wEnemyHasnt * KOEFF_FOR_ENEMIES_MOVE_POSSIB + x.wNextPoss * KOEFF_FOR_ASSURED_MOVES_COUNT + x.wSpots * KOEFF_FOR_OWN_SCORES >=
					y.wEnemyHasnt * KOEFF_FOR_ENEMIES_MOVE_POSSIB + y.wNextPoss * KOEFF_FOR_ASSURED_MOVES_COUNT + y.wSpots * KOEFF_FOR_OWN_SCORES)
					 ? -1 : 1;
			}
		}
	}
}
