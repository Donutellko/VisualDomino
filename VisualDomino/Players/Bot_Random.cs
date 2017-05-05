using System.Collections.Generic;

namespace DominoC {
	//========//
	//ВНИМАНИЕ//
	//========//

	//В модуле переименована часть переменных, это никак не влияет на игру.
	//Советую всё-таки переписать код самостоятельно, опять же попереименовывать переменные, 
	//подчистить комментарии, разобраться в нём.
	//Отсылка одинаковых ботов будет лютой подставой, товарищи :D
	internal class MSPlayer {
		public static string PlayerName = "Tutorial_bot";
		private static List<MTable.SBone> _lHand;
	
		//===============//
		//Готовые функции//
		//===============//

		//Инициализация игрока
		public static void Initialize () {
			_lHand = new List<MTable.SBone>();
		}

		//Вывод на экран
		public static void PrintAll () {
			MTable.PrintAll(_lHand);
		}

		//Возвращает количество костей домино в руке
		public static int GetCount () {
			return _lHand.Count;
		}

		//=======================//
		//Разрабатываемые функции//
		//=======================//

		//Добавляет кость домино в руку
		public static void AddItem (MTable.SBone sb) {
			_lHand.Add(sb);
		}

		//Возвращает сумму очков на костях в руке
		public static int GetScore () {
			//Количество очков
			int iScore = 0;
			//Если в руке дупль 0/0 возвращаем 25 очков
			if (_lHand.Count == 1) {
				//Проверка на нулики
				if (_lHand[0].First == 0 && _lHand[0].Second == 0)
					//Возвращение очков
					return 25;
			}

			//Подсчитываем сумму очков на костях
			foreach (MTable.SBone sbone in _lHand)
				iScore += sbone.First + sbone.Second;

			//Возвращение очков
			return iScore;
		}

		//Сделать ход

		//Используется максимально примитивная логика: мы проходим по всем доминошкам в нашей руке
		//и выставляем первую попавшуюся, которую можно пристроить
		public static bool MakeStep (out MTable.SBone sb, out bool end) {
			//Получаем состояние стола
			List<MTable.SBone> lTableCondition = MTable.GetGameCollection();
			//Кость на левом  конце цепочки
			MTable.SBone sLeft = lTableCondition[0];
			//Кость на правом конце цепочки
			MTable.SBone sRight = lTableCondition[lTableCondition.Count - 1];

			//Просматриваем все домино в руке
			for (int i = 0; i < _lHand.Count; ++i) {
				//Если её можно поставить слева
				if (_lHand[i].Second == sLeft.First || _lHand[i].First == sLeft.First) {
					//То говорим, что мы выбираем её
					sb = _lHand[i];
					//Удаляем её из руки
					_lHand.RemoveAt(i);
					//Указываем куда мы её ставим
					end = false;
					//Заканчиваем ход
					return true;
				}
				//Если её можно поставить справа
				if (_lHand[i].Second == sRight.Second || _lHand[i].First == sRight.Second) {
					//То говорим, что мы выбираем её
					sb = _lHand[i];
					//Удаляем её из руки
					_lHand.RemoveAt(i);
					//Указываем куда мы её ставим
					end = true;
					//Заканчиваем ход
					return true;
				}
			}

			//Добираем новые кости с базара, пока не сможем их поставить
			//Переменная для добираемой кости
			MTable.SBone sbNew;

			//Пока можно добирать из базара берём кость
			while (MTable.GetFromShop(out sbNew)) {
				//Если её можно поставить слева
				if (sbNew.Second == sLeft.First || sbNew.First == sLeft.First) {
					//То говорим, что мы выбираем её
					sb = sbNew;
					//Указываем куда мы её ставим
					end = false;
					//Заканчиваем ход
					return true;
				}
				//Если её можно поставить справа
				if (sbNew.Second == sRight.Second || sbNew.First == sRight.Second) {
					//То говорим, что мы выбираем её
					sb = sbNew;
					//Указываем куда мы её ставим
					end = true;
					//Заканчиваем ход
					return true;
				}
				//Если её нельзя положить на стол, то кладем её в руку
				_lHand.Add(sbNew);
			}

			//Если мы не можем сделать ход, то в данные параметры загоняем любую чушь, которую можно загнать
			sb.First = 322;
			sb.Second = 322;
			end = true;

			//И возвращаем, что ход сделать нельзя
			return false;
		}
	}
}