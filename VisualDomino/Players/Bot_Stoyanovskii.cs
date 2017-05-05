using System;
using System.Collections.Generic;
using System.Linq;

namespace DominoC
{
    internal class MSPlayer
    {
        public static string PlayerName = "SU_Bot";
        private static List<MTable.SBone> _lHand;

        //Переменные
        private static List<MTable.SBone> _lsbUsed = new List<MTable.SBone>();

        private static bool _pInit;
        private static int _pEnemyBoneCount;
        private static MTable.SBone _pLeft;
        private static MTable.SBone _pRight;

        private static int[,] _iProbabilityTable =
        {
            {7,7,7,7,7,7,7},
            {7,7,7,7,7,7,7},
            {7,7,7,7,7,7,7},
            {7,7,7,7,7,7,7},
            {7,7,7,7,7,7,7},
            {7,7,7,7,7,7,7},
            {7,7,7,7,7,7,7},
        };

        private static byte[] vars = { 20, 10, 8, 6, 4, 2, 0, 0 };

        public struct TurnOption
        {
            public MTable.SBone Bone;
            public bool End;
            public int TakeChance;

            public int Score()
            {
                return Bone.First + Bone.Second + ((IsDouble()) ? vars[Bone.First] : 0);
            }

            public bool IsDouble()
            {
                return Bone.First == Bone.Second;
            }
        }
        //===============//
        //Готовые функции//
        //===============//

        //Инициализация игрока
        public static void Initialize()
        {
            _lHand = new List<MTable.SBone>();

            //Added
            _iProbabilityTable = new[,]
            {
                {7,7,7,7,7,7,7},
                {7,7,7,7,7,7,7},
                {7,7,7,7,7,7,7},
                {7,7,7,7,7,7,7},
                {7,7,7,7,7,7,7},
                {7,7,7,7,7,7,7},
                {7,7,7,7,7,7,7},
            };

            _lsbUsed.Clear();
            _pInit = false;
        }

        //Вывод на экран
        public static void PrintAll()
        {
            MTable.PrintAll(_lHand);
        }

        //Возвращает количество костей домино в руке
        public static int GetCount()
        {
            return _lHand.Count;
        }

        //=======================//
        //Разрабатываемые функции//
        //=======================//

        private static void Standardize(ref MTable.SBone domino)
        {
            if (domino.First > domino.Second)
                domino.Exchange();
        }
        private static void DominoIsntInEHand(MTable.SBone domino)
        {
            _iProbabilityTable[domino.First, domino.Second] = 0;
            _iProbabilityTable[domino.Second, domino.First] = 0;
        }
        private static void DominoIsUsed(MTable.SBone domino)
        {
            _iProbabilityTable[domino.First, domino.Second] = -1;
            _iProbabilityTable[domino.Second, domino.First] = -1;
            _lsbUsed.Add(domino);
        }


        private static void SaveTable(ref MTable.SBone domino, bool end, int iEnemyBoneCount, ushort number)
        {
            if (end)
            {
                _pRight = domino;
                if (number != _pRight.First) _pRight.Exchange();
            }
            else
            {
                _pLeft = domino;
                if (number != _pLeft.Second) _pLeft.Exchange();
            }

            _pEnemyBoneCount = iEnemyBoneCount;
        }

        private static List<TurnOption> GetUsefulDomino(List<MTable.SBone> lHand, MTable.SBone sLeft, MTable.SBone sRight)
        {
            var ltoUsefulDominoes = new List<TurnOption>();

            for (int i = 0; i < lHand.Count; i++)
            {
                TurnOption t = new TurnOption
                {
                    Bone =
                    {
                        First = lHand[i].First,
                        Second = lHand[i].Second
                    }
                };


                if (lHand[i].Second == sLeft.First || lHand[i].First == sLeft.First)
                {
                    int chance = 0;
                    t.End = false;
                    for (int j = 0; j < 7; j++)
                    {
                        if (_iProbabilityTable[sRight.Second, j] != -1)
                            chance += _iProbabilityTable[sRight.Second, j];

                        if (sRight.Second != sLeft.First && sRight.Second != j)
                        {
                            var temp = lHand[i].Second == sLeft.First ? _iProbabilityTable[lHand[i].First, j] : _iProbabilityTable[lHand[i].Second, j];
                            if (temp != -1)
                                chance += temp;
                        }
                    }
                    t.TakeChance = chance;
                    ltoUsefulDominoes.Add(t);
                }

                if (lHand[i].Second == sRight.Second || lHand[i].First == sRight.Second)
                {
                    int chance = 0;
                    t.End = true;
                    for (int j = 0; j < 7; j++)
                    {
                        if (_iProbabilityTable[sLeft.First, j] != -1)
                            chance += _iProbabilityTable[sLeft.First, j];

                        if (sRight.Second != sLeft.First && sLeft.First != j)
                        {
                            var temp = lHand[i].Second == sRight.Second ? _iProbabilityTable[lHand[i].First, j] : _iProbabilityTable[lHand[i].Second, j];
                            if (temp != -1)
                                chance += temp;
                        }
                    }
                    t.TakeChance = chance;
                    ltoUsefulDominoes.Add(t);
                }
            }

            return ltoUsefulDominoes;
        }

        public static TurnOption GetBt(List<TurnOption> ltoUsefulDominoes)
        {
            bool flag = false;
            double min = int.MaxValue;
            TurnOption bestTo = new TurnOption
            {
                Bone = new MTable.SBone(),
                End = false
            };

            foreach (var domino in ltoUsefulDominoes)
            {
                var timed = domino.TakeChance - (double)vars[7] / 10 * domino.Score();
                if (!flag)
                {
                    min = timed;
                    bestTo = domino;
                    flag = true;
                }
                else
                {
                    if (min > timed)
                    {
                        min = timed;
                        bestTo = domino;
                    }
                    else if (Math.Abs(min - timed) < 0.01)
                    {
                        if (domino.Score() >= bestTo.Score())
                        {
                            min = timed;
                            bestTo = domino;
                        }
                    }
                }
            }

            return bestTo;
        }

        //Добавляет кость домино в руку
        public static void AddItem(MTable.SBone sb)
        {
            _lHand.Add(sb);
        }

        //Возвращает сумму очков на костях в руке
        public static int GetScore()
        {
            if (_lHand.Count == 1 && _lHand[0].First == 0 && _lHand[0].Second == 0) return 25;
            return _lHand.Sum(sbone => sbone.First + sbone.Second);
        }

        //Сделать ход
        public static bool MakeStep(out MTable.SBone sb, out bool end)
        {
            #region TableLoad

            //Получаем состояние стола
            List<MTable.SBone> lTableCondition = MTable.GetGameCollection();

            //Кости на концах цепочки
            MTable.SBone sLeft = lTableCondition[0];
            MTable.SBone sRight = lTableCondition[lTableCondition.Count - 1];

            //Standardize(ref sLeft );
            //Standardize(ref sRight);

            //Получение числа костей на столе, в руке оппонента
            int iShopBoneCount = MTable.GetShopCount();
            int iEnemyBoneCount = 28 - _lHand.Count - iShopBoneCount - lTableCondition.Count;

            #endregion

            //Поиск изменений стола с предыдущего хода
            #region TableUpdate

            //Первичная инициализация
            if (!_pInit)
            {
                foreach (var item in lTableCondition)
                {
                    var t = item;
                    Standardize(ref t);
                    DominoIsUsed(t);
                }
                foreach (var item in _lHand)
                {
                    DominoIsUsed(item);
                }
            }

            //Подсчет количества новых домино в руке оппонента
            int iEnemyBoneCountDeltha = iEnemyBoneCount - _pEnemyBoneCount;

            DominoIsUsed(sLeft);
            DominoIsUsed(sRight);

            if (iEnemyBoneCountDeltha > 0)
            {
                for (ushort i = 0; i < 7; i++)
                    for (ushort j = 0; j < 7; j++)
                    {
                        if (j > i) break;
                        if (!_lsbUsed.Contains(new MTable.SBone { First = i, Second = j }) && !_lsbUsed.Contains(new MTable.SBone { First = j, Second = i }) && _pInit)
                        {
                            _iProbabilityTable[j, i] += iEnemyBoneCountDeltha;
                            if (i != j)
                                _iProbabilityTable[i, j] += iEnemyBoneCountDeltha;
                        }
                    }
            }
            else if (iEnemyBoneCountDeltha != 0)
            {
                for (ushort i = 0; i < 7; i++)
                    for (ushort j = 0; j < 7; j++)
                    {
                        if (j > i) break;
                        if (!_lsbUsed.Contains(new MTable.SBone { First = i, Second = j }) && !_lsbUsed.Contains(new MTable.SBone { First = j, Second = i }) && _iProbabilityTable[i, j] == _pEnemyBoneCount)
                        {
                            _iProbabilityTable[i, j] -= 1;
                            if (i != j)
                                _iProbabilityTable[j, i] -= 1;
                        }
                    }
            }

            if (!_pInit) _pInit = !_pInit;
            else if (iEnemyBoneCountDeltha >= 0)
            {
                if (Equals(_pLeft, sLeft))
                {
                    if (!Equals(_pRight, sRight))
                    {
                        DominoIsUsed(sRight);
                        for (ushort i = 0; i < 7; i++)
                        {
                            if (!_lsbUsed.Contains(new MTable.SBone { First = i, Second = _pLeft.First }) &&
                                !_lsbUsed.Contains(new MTable.SBone { First = _pLeft.First, Second = i }))
                            {
                                DominoIsntInEHand(new MTable.SBone { First = i, Second = _pLeft.First });

                            }
                            if (!_lsbUsed.Contains(new MTable.SBone { First = i, Second = _pRight.Second }) &&
                                !_lsbUsed.Contains(new MTable.SBone { First = _pRight.Second, Second = i }))
                            {
                                DominoIsntInEHand(new MTable.SBone { First = i, Second = _pRight.Second });
                            }
                        }
                    }
                }
                else if (!Equals(_pLeft, sLeft))
                {
                    DominoIsUsed(sLeft);
                    for (ushort i = 0; i < 7; i++)
                    {
                        if (!_lsbUsed.Contains(new MTable.SBone { First = i, Second = _pLeft.First }) &&
                            !_lsbUsed.Contains(new MTable.SBone { First = _pLeft.First, Second = i }))
                        {
                            DominoIsntInEHand(new MTable.SBone { First = i, Second = _pLeft.First });

                        }
                        if (!_lsbUsed.Contains(new MTable.SBone { First = i, Second = _pRight.Second }) &&
                            !_lsbUsed.Contains(new MTable.SBone { First = _pRight.Second, Second = i }))
                        {
                            DominoIsntInEHand(new MTable.SBone { First = i, Second = _pRight.Second });
                        }
                    }
                }
            }
            #endregion

            //Получаем все домино в руке, которые можно использовать на этот ход
            List<TurnOption> ltoUsefulDominoes = GetUsefulDomino(_lHand, sLeft, sRight);

            //Обновление сохраняемых параметров
            _pLeft = sLeft;
            _pRight = sRight;

            if (ltoUsefulDominoes.Count != 0)
            {
                TurnOption bestTo = GetBt(ltoUsefulDominoes);
                sb = bestTo.Bone;
                end = bestTo.End;

                SaveTable(ref sb, end, iEnemyBoneCount, end ? sRight.Second : sLeft.First);
                _lHand.Remove(sb);
                sb.Exchange();
                _lHand.Remove(sb);
                return true;
            }

            MTable.SBone sbNew;

            while (MTable.GetFromShop(out sbNew))
            {
                DominoIsUsed(sbNew);
                List<MTable.SBone> lNew = new List<MTable.SBone>
                {
                    sbNew
                };
                ltoUsefulDominoes = GetUsefulDomino(lNew, sLeft, sRight);

                if (ltoUsefulDominoes.Count != 0)
                {
                    TurnOption bestTo = GetBt(ltoUsefulDominoes);
                    sb = bestTo.Bone;
                    end = bestTo.End;

                    SaveTable(ref sb, end, iEnemyBoneCount, end ? sRight.Second : sLeft.First);
                    _lHand.Remove(sb);
                    sb.Exchange();
                    _lHand.Remove(sb);
                    return true;
                }

                _lHand.Add(sbNew);
            }

            //Нельзя сделать ход
            sb.First = 322;
            sb.Second = 322;
            end = true;

            _pRight = sRight;
            _pLeft = sLeft;

            return false;
        }
    }
}