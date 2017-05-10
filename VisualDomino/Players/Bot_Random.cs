using System;
using System.Collections.Generic;

namespace VisualDomino{
	//========//
	//ВНИМАНИЕ//
	//========//

	//В модуле переименована часть переменных, это никак не влияет на игру.
	//Советую всё-таки переписать код самостоятельно, опять же попереименовывать переменные, 
	//подчистить комментарии, разобраться в нём.
	//Отсылка одинаковых ботов будет лютой подставой, товарищи :D
	internal class Bot_Random : Player {
		public new static string PlayerName = "Tutorial_bot";
		
		//Используется максимально примитивная логика: мы проходим по всем доминошкам в нашей руке
		//и выставляем первую попавшуюся, которую можно пристроить
		public override bool MakeStep (out MTable.SBone sb, out bool end) {
			//Получаем состояние стола
			List<MTable.SBone> lTableCondition = MTable.GetGameCollection();
			//Кость на левом  конце цепочки
			MTable.SBone sLeft = lTableCondition[0];
			//Кость на правом конце цепочки
			MTable.SBone sRight = lTableCondition[lTableCondition.Count - 1];

			//Просматриваем все домино в руке
			for (int i = 0; i < lHand.Count; ++i) {
				//Если её можно поставить слева
				if (lHand[i].Second == sLeft.First || lHand[i].First == sLeft.First) {
					//То говорим, что мы выбираем её
					sb = lHand[i];
					//Удаляем её из руки
					lHand.RemoveAt(i);
					//Указываем куда мы её ставим
					end = false;
					//Заканчиваем ход
					return true;
				}
				//Если её можно поставить справа
				if (lHand[i].Second == sRight.Second || lHand[i].First == sRight.Second) {
					//То говорим, что мы выбираем её
					sb = lHand[i];
					//Удаляем её из руки
					lHand.RemoveAt(i);
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
				lHand.Add(sbNew);
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