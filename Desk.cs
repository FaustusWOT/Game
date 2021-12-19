using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class TDesk
    {
        public TMainColoda MainCards;
        public TGamers GM;
        public int iHigh;

        public TColoda CardsOnDesk;
        public TCard CardOnMove;

        public TDesk()
        {
            MainCards = new TMainColoda();
            GM = new TGamers();
            CardsOnDesk = new TColoda();
        }

        public void PrepareCards()
        {
            MainCards.Make();
            MainCards.Shuffle();
        }
        
        public void AddGamer(TGamer Data)
        {
            GM.AddGamer(Data);
        }

        public void GiveFirstCards()
        {
            Console.WriteLine("Раздаю!!!");

            iHigh = MainCards.GetHigh();
            GM.DoEmptyHands();
            GM.GetNeedCards(MainCards);

        }

        public void AddCards()
        {
            TGamer Current;

            Current = GM.CurrentGamer;
            do
            {
                while (GM.CurrentGamer.NeedCard())
                {
                    if (!MainCards.isEmpty())
                    {
                        GM.CurrentGamer.GetCard(MainCards.GetLast());
                    }
                    else break;
                }
                GM.GoNextGamer();
            } while (GM.CurrentGamer != Current);

        }


        public void SetFirstCurrent()
        {
            GM.iCurrent = 0;
        }
        public bool GameIsDone()
        {
            if (!MainCards.isEmpty()) return false;
            return GM.GameIsDone();
        }

        public void DisplayPosition()
        {
            Console.WriteLine("Колода на столе:");
            MainCards.DisplayColoda();

            Console.WriteLine("Колоды у других игроков");
            GM.DisplayColoda(0);

            Console.WriteLine("Колода текущего игрока");
            GM.DisplayColoda(1);
        }
        public void getFirstMove()
        {
            CardsOnDesk.DoEmpty();
            CardOnMove =  GM.CurrentGamer.GetFirstMove(iHigh);
        }
        public TCard GetSecondMove()
        {
            int iTempStore;

            TCard ACard=null;

            iTempStore = GM.iCurrent;

            do
            {
                if (iTempStore != GM.NextGamer())
                {
                    ACard = GM.Gamers[iTempStore].GetSecondMove(CardsOnDesk,iHigh);
                    if (ACard != null)
                    {
                        Console.WriteLine("{0:s}: Подкидываю {1:s}", GM.Gamers[iTempStore].sName, ACard.ToString());
                        return ACard;
                    }
                }
                iTempStore++;
                if (iTempStore >= GM.Gamers.Count) iTempStore = 0;
            } while (iTempStore != GM.iCurrent);

            Console.WriteLine("{0:s}: Бито!!!", GM.CurrentGamer.sName);

            return null;
        }
        public bool getAnsverMove()
        {
            TCard ACard;
            ACard = GM.NGamer.GetAnsverMove(CardOnMove,iHigh);
            if (ACard != null)
            {
                Console.WriteLine("{0:s}: Отбиваю {1:s}!", GM.NGamer.sName, ACard.ToString());
                CardsOnDesk.PutIn(CardOnMove);
                CardsOnDesk.PutIn(ACard);
                return true;
            } else
            {
                Console.WriteLine("{0:s}: Принимаю!", GM.NGamer.sName);
                GM.NGamer.GetCard(CardOnMove);
                GM.NGamer.GetCards(CardsOnDesk);
                CardsOnDesk.DoEmpty();

                return false;
            }
        }
        public bool isCorrectFirstColoda()
        {
            return GM.IsCorrectFirstColoda();
        }
        public void SelectFirstGamer()
        {
            GM.SelectFirstGamer(iHigh);
        }

        public void GameCycle()
        {
            int iMoveCount = 0;

            do
            {
                PrepareCards();


                GiveFirstCards();

            } while (!isCorrectFirstColoda());

            SelectFirstGamer();
            // Начало игрового цикла
            do
            {
                Console.WriteLine("Ход номер {0:d}", ++iMoveCount);

                DisplayPosition();

                getFirstMove();
                if (GM.GamerStand())
                {
                    Console.WriteLine("Игрок бросил карты!");
                    break;
                }
                Console.WriteLine("{0:s}: Захожу {1:s}", GM.CurrentGamer.sName, CardOnMove.ToString());

                TCard ACard = null;
                bool isMoveDone = false;

                while (getAnsverMove())
                {
                    ACard = GetSecondMove();
                    if (ACard != null)
                    {
                        CardOnMove = ACard;
                    }
                    else
                    {
                        isMoveDone = true;
                        break;
                    }
                }
                if (!isMoveDone)
                {
                    GM.GoNextGamer();
                }
                GM.GoNextGamer();

                if (GM.GamerStand())
                {
                    Console.WriteLine("{0:s} бросил карты!", GM.CurrentGamer.sName);
                    break;
                }
                /*                if (ACard == null)
                                {
                                    Console.WriteLine("{0:s}: Принимаю!", Desk.GM.NGamer.sName);
                                    Desk.GM.NGamer.GetCard(CardOnDesk);
                                    Desk.AddCards();
                                }
                                else
                                {
                                    Console.WriteLine("{0:s}: Отбиваю {1:s}!", Desk.GM.NGamer.sName, ACard.ToString());
                                    Desk.AddCards();
                                    Desk.GM.GoNextGamer();
                                }*/
                AddCards();
                GM.DeleteGamersWhoGameIsDone();
                Console.WriteLine("Теперь ходит {0:s}", GM.CurrentGamer.sName);
            } while (!GameIsDone());

        }
    }
}
