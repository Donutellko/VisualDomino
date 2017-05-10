using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace VisualDomino {

	[SuppressMessage("ReSharper", "InconsistentNaming")]
	internal class Bot_Kuchinskas : Player {
		public new const string PlayerName = "Medium_bot";
		
		public void GenerateDomino (List<MTable.SBone> NullDomino) {
			List<MTable.SBone> AllDomino = new List<MTable.SBone>();

			for (ushort i = 1; i <= 6; i++) {
				for (ushort j = 1; j <= 6; j++) {
					MTable.SBone Domino = new MTable.SBone();

					Domino.First = i;

					Domino.Second = j;
					AllDomino.Add(Domino);
				}

			}
		}
		//Сделать ход
		public int _i = 0;

		//Список всех доминошек, существующих в игре
		public List<MTable.SBone> AllDomino = new List<MTable.SBone>();

		public override bool MakeStep (out MTable.SBone sb, out bool end) {
			_i++;
			if (_i == 1) {
				GenerateDomino(AllDomino);

			}

			//Получаем состояние стола
			List<MTable.SBone> lTableCondition = MTable.GetGameCollection();
			//Кость на левом  конце цепочки
			MTable.SBone sLeft = lTableCondition[0];
			if (lTableCondition.Contains(sLeft)) {
				AllDomino.Remove(sLeft);
			}


			MTable.SBone sRight = lTableCondition[lTableCondition.Count - 1];
			if (lTableCondition.Contains(sRight)) {
				AllDomino.Remove(sRight);
			}


			List<Tuple<MTable.SBone, bool, int>> HandDomino = new List<Tuple<MTable.SBone, bool, int>>();
			//Просматриваем все домино в руке

			for (int i = 0; i < lHand.Count; i++) {
				//Если её можно поставить слева
				if (lHand[i].Second == sLeft.First || lHand[i].First == sLeft.First) {
					end = false;
					int k = 0;
					for (int j = 0; j < AllDomino.Count; j++) {
						if ((AllDomino[j].First == lHand[i].First) || (AllDomino[j].Second == lHand[i].Second)) {
							k++;
						}
					}
					HandDomino.Add(Tuple.Create(lHand[i], end, k));
					//    end = false; слева

				}
				//Если её можно поставить справа
				if (lHand[i].Second == sRight.Second || lHand[i].First == sRight.Second) {
					int k = 0;
					for (int j = 0; j < AllDomino.Count; j++) {
						if ((AllDomino[j].First == lHand[i].First) || (AllDomino[j].Second == lHand[i].Second)) {
							k++;
						}
					}
					end = true;
					HandDomino.Add(Tuple.Create(lHand[i], end, k));
					// end = true; справа
				}
			}

			// анализ руки и выставление доминошки
			// взять минимальную доминошку из нашей руки и она минмальная в общей куче
			if (HandDomino.Count != 0) {
				int min = 10;
				sb = HandDomino[0].Item1;
				end = false;
				int b = 0;
				for (int i = 0; i < HandDomino.Count; i++) {

					if (HandDomino[i].Item3 < min) {
						min = HandDomino[i].Item1.Second;
						sb = HandDomino[i].Item1;
						b = i;
						end = HandDomino[i].Item2;
					}


				}
				if (min != 10) {
					HandDomino.RemoveAt(b);
					lHand.Remove(sb);
					return true;
				}
			}

			//Добираем новые кости, пока не сможем их поставить
			MTable.SBone sbNew;


			while (MTable.GetFromShop(out sbNew)) {
				//Если её можно поставить слева
				if (sbNew.Second == sLeft.First || sbNew.First == sLeft.First) {
					sb = sbNew;
					end = false;
					return true;
				}
				//Если её можно поставить справа
				if (sbNew.Second == sRight.Second || sbNew.First == sRight.Second) {

					sb = sbNew;

					end = true;
					//Заканчиваем ход
					return true;
				}
				//Если её нельзя положить на стол, то кладем её в руку
				lHand.Add(sbNew);
			}


			sb.First = 400;
			sb.Second = 400;
			end = true;

			// ход сделать нельзя
			return false;
		}
	}
}