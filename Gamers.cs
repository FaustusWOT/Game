using System;
using System.Collections.Generic;
using System.Text;
#nullable enable

namespace Game
{
    class TGamers
    {
        private readonly TLog MainLog;

        public int iCurrent;
        public List<TGamer> Gamers;
//        public int iGamersCount;

        public TGamers(ref TLog setMainLog)
        {
            MainLog = setMainLog;

            Gamers = new List<TGamer>();
            iCurrent = 0;
        }

        public void AddGamer(TGamer D)
        {
            Gamers.Add(D);
        }

        public void RemoveGamer(int iIndex)
        {
            if (iCurrent > iIndex) iCurrent--;

            Gamers.RemoveAt(iIndex);

            if (iCurrent >= Gamers.Count) iCurrent = 0;
        }

        public int NextGamer()
        {
            return ((iCurrent+1) < Gamers.Count)?iCurrent+1:0;
        }

        public void GoNextGamer()
        {
            iCurrent = NextGamer();
        }

        public bool GameIsDone()
        {
            return (Gamers.Count <= 1);
        }

        public void DisplayColoda(int iMode ) // 0 - других игроков, 1- текущего игрока
        {
            if (iMode == 0)
            {
                for(int i = 0; i < Gamers.Count;i++)
                {
                    if (i != iCurrent)
                    {
                        MainLog.DisplayPosition(String.Format("Игрок {0:s}", Gamers[i].sName),Gamers[i]);
                    } 
                }
            } else
            {
                MainLog.DisplayPosition(String.Format("Текущий игрок ({0:s}):",CurrentGamer.sName),CurrentGamer);
//                CurrentGamer.DisplayColoda();
            }
        }

        public TGamer CurrentGamer
        {
            get { return Gamers[iCurrent]; }
            set {; }
        }
        public TGamer NGamer
        {
            get { return Gamers[NextGamer()]; }
            set {; }
        }
        public bool GamerStand ()
        {
            return Gamers.Exists(x => x.ThrowCards);
        }

        public void DeleteGamersWhoGameIsDone()
        {
            int iIndex;
            while ((iIndex = Gamers.FindIndex(x => x.GameIsDone())) >= 0) 
            {
                MainLog.GamerStand(Gamers[iIndex].sName);
                RemoveGamer(iIndex);
            }

        }
        public bool IsCorrectFirstColoda()
        {
            return !Gamers.Exists(x => !(x.IsCorrectFirstColoda()));
        }

        public void DoEmptyHands()
        {
            foreach (TGamer Gamer in Gamers) Gamer.DoEmptyHands();
        }

        public void SelectFirstGamer(int iHigh)
        {
            int iV=15,iT;
            TGamer? iC=null;

            foreach (TGamer Gamer in Gamers) 
            {
                iT = Gamer.GetMinHigh(iHigh);
                MainLog.GamerSay(Gamer.sName, iT.ToString());

                if (iT < iV)
                {
                    iC = Gamer;
                    iV = iT;
                }
            }

            if (iV < 15)
            {
                iCurrent = Gamers.FindIndex(x => (x.sName == iC!.sName));
                MainLog.FirstMoveGamer(CurrentGamer.sName, iV.ToString());
            }

        }
        public bool IsGameDone
        {
            get { return (Gamers.Count <= 1); }
        }
        public bool IsEmpty()
        {
            return Gamers.Count == 0;
        }

        public void GetNeedCards(TColoda Coloda)
        {
            if (Gamers.Count > 0)
            {
                for (int i = 0; i < Gamers.Count; i++)
                {
                    while (CurrentGamer.NeedCard() && !(Coloda.IsEmpty()))
                    {
                        TCard? ACard = Coloda.GetLast();
                        if (ACard != null)
                            CurrentGamer.GetCard(ACard);
                    }

                    GoNextGamer();
                }
            }

        }
    }
}
