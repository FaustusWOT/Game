using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class TGamers
    {
        public int iCurrent;
        public List<TGamer> Gamers;
//        public int iGamersCount;

        public TGamers()
        {
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
//            Console.WriteLine("Теперь ходит {0:s}", CurrentGamer.sName);
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
                        Console.WriteLine("Игрок {0:s}", Gamers[i].sName);
                        Gamers[i].DisplayColoda();
                    } 
                }
            } else
            {
                Console.WriteLine("Текущий игрок ({0:s}):",CurrentGamer.sName);
                CurrentGamer.DisplayColoda();
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
                Console.WriteLine("Игрок {0:s} вышел!", Gamers[iIndex].sName);
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
                iT = Gamer.getMinHigh(iHigh);
                Console.WriteLine("{0:s} сказал \"У меня {1:d}!", Gamer.sName, iT);

                if (iT < iV)
                {
                    iC = Gamer;
                    iV = iT;
                }
            }

            if (iV < 15)
            {
                iCurrent = Gamers.FindIndex(x => (x.sName == iC.sName));
                Console.WriteLine("Ходит {0:s}, у него {1:d}!", CurrentGamer.sName, iV);
            }

        }
        public bool isGameDone
        {
            get { return (Gamers.Count <= 1); }
        }

        public void GetNeedCards(TColoda Coloda)
        {
            if (Gamers.Count > 0)
            {
                for (int i = 0; i < Gamers.Count; i++)
                {
                    while (CurrentGamer.NeedCard() && !(Coloda.isEmpty())) CurrentGamer.GetCard(Coloda.GetLast());

                    GoNextGamer();
                }
            }

        }
    }
}
