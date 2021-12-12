using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class TGamers
    {
        public int iCurrent;
        public TGamer[] G;
        public int iGamersCount;

        public TGamers()
        {
            G = new TGamer[6];
            iCurrent = 0;
            iGamersCount = 0;
        }

        public void AddGamer(TGamer D)
        {
            G[iGamersCount++] = D;
        }

        public void RemoveGamer(int iIndex)
        {
            if (iCurrent > iIndex) iCurrent--; 
            for (int i = iIndex; i < iGamersCount; i++) G[i] = G[i + 1];
            iGamersCount--;
            if (iCurrent > iGamersCount) iCurrent = 0;
        }

        public int NextGamer()
        {
            return ((iCurrent+1) < iGamersCount)? iCurrent+1:0;
        }

        public void GoNextGamer()
        {
            iCurrent = NextGamer();
//            Console.WriteLine("Теперь ходит {0:s}", CurrentGamer.sName);
        }

        public bool GameIsDone()
        {
            return (iGamersCount <= 1);
        }

        public void DisplayColoda(int iMode ) // 0 - других игроков, 1- текущего игрока
        {
            if (iMode == 0)
            {
                for(int i = 0; i < iGamersCount;i++)
                {
                    if (i != iCurrent)
                    {
                        Console.WriteLine("Игрок {0:s}", G[i].sName);
                        G[i].DisplayColoda();
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
            get { return G[iCurrent]; }
            set {; }
        }
        public TGamer NGamer
        {
            get { return G[NextGamer()]; }
            set {; }
        }
        public bool GamerStand ()
        {
            for (int i = 0; i < iGamersCount; i++) 
            {
                if (G[i].ThrowCards) return true;
            }
            return false;
        }
        public void DeleteGamersWhoGameIsDone()
        {
            for (int i = 0;i < iGamersCount;)
            {
                if (G[i].GameIsDone())
                {
                    Console.WriteLine("Игрок {0:s} вышел!",G[i].sName);
                    RemoveGamer(i);
                }
                else
                    i++;
            }
        }
        public bool IsCorrectFirstColoda()
        {
            for (int i = 0; i < iGamersCount; i++)
                if (!G[i].IsCorrectFirstColoda()) return false;
            return true;
        }
        public void DoEmptyHands()
        {
            for (int i=0;i<iGamersCount;i++) G[i].DoEmptyHands();
        }
        public void SelectFirstGamer(int iHigh)
        {
            int iC=-1, iV=15,iT;
            for (int i=0;i < iGamersCount;i++)
            {
                iT = G[i].getMinHigh(iHigh);
                Console.WriteLine("{0:s} сказал \"У меня {1:d}!", G[i].sName, iT);
                if (iT < iV)
                {
                    iC = i;
                    iV = iT;
                }
            }
            if (iV < 15)
            {
                iCurrent = iC;
                Console.WriteLine("Ходит {0:s}, у него {1:d}!", CurrentGamer.sName, iV);
            }

        }
    }
}
