using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class TDesk
    {
        private readonly TLog MainLog;

        public TMainColoda MainCards;
        public TGamers GM;
        public int iHigh;

        public TColoda CardsOnDesk;
        public TCard CardOnMove;

        public TDesk(ref TLog setMainLog)
        {
            MainLog = setMainLog;
            MainCards = new TMainColoda();
            GM = new TGamers(ref MainLog);
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
            MainLog.DisplayFirst();

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
                    if (!MainCards.IsEmpty())
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
            if (!MainCards.IsEmpty()) return false;
            return GM.GameIsDone();
        }

        public void DisplayPosition()
        {
            MainLog.DisplayPosition("Колода на столе:",MainCards,TColodaType.IS_DESK_COLODA);

            MainLog.DisplayMessage("Колоды у других игроков");
            GM.DisplayColoda(0);

            MainLog.DisplayMessage("Колода текущего игрока");
            GM.DisplayColoda(1);
        }

        public void GetFirstMove()
        {
            CardsOnDesk.DoEmpty();
            CardOnMove =  GM.CurrentGamer.GetFirstMove(iHigh);
        }
        public TCard GetSecondMove()
        {
            int iTempStore;

            TCard ACard;

            iTempStore = GM.iCurrent;

            do
            {
                if (iTempStore != GM.NextGamer())
                {
                    ACard = GM.Gamers[iTempStore].GetSecondMove(CardsOnDesk,iHigh);
                    if (ACard != null)
                    {
                        MainLog.SecondMove(GM.Gamers[iTempStore].sName, ACard.ToString());
                        return ACard;
                    }
                }
                iTempStore++;
                if (iTempStore >= GM.Gamers.Count) iTempStore = 0;
            } while (iTempStore != GM.iCurrent);

            MainLog.SecondMove(GM.CurrentGamer.sName,null);

            return null;
        }
        public bool GetAnsverMove()
        {
            TCard ACard;
            ACard = GM.NGamer.GetAnsverMove(CardOnMove,iHigh);
            if (ACard != null)
            {
                MainLog.AnsverMove(GM.NGamer.sName, ACard.ToString());
                CardsOnDesk.PutIn(CardOnMove);
                CardsOnDesk.PutIn(ACard);
                return true;
            } else
            {
                MainLog.AnsverMove(GM.NGamer.sName, null);
                GM.NGamer.GetCard(CardOnMove);
                GM.NGamer.GetCards(CardsOnDesk);
                CardsOnDesk.DoEmpty();

                return false;
            }
        }
        public bool IsCorrectFirstColoda()
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

            } while (!IsCorrectFirstColoda());

            SelectFirstGamer();
            // Начало игрового цикла
            do
            {
                MainLog.DisplayMessage(String.Format("Ход номер {0:d}", ++iMoveCount));

                DisplayPosition();

                GetFirstMove();
                if (GM.GamerStand())
                {
                    MainLog.FirstMove("", null);
                    break;
                }
                MainLog.FirstMove( GM.CurrentGamer.sName, CardOnMove.ToString());

                TCard ACard;
                bool isMoveDone = false;

                while (GetAnsverMove())
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
                    MainLog.FirstMove(GM.CurrentGamer.sName, null);
                    break;
                }

                AddCards();
                GM.DeleteGamersWhoGameIsDone();
                if (GameIsDone()) break;
                MainLog.DisplayCurrentGamer(GM.CurrentGamer.sName);

            } while (!GameIsDone());

        }
    }
}
