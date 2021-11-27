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
            for (int i = iIndex; i < iGamersCount; i++) G[i] = G[i + 1];
            iGamersCount--;
        }

        public int NextGamer()
        {
            return ((iCurrent+1) < iGamersCount)? iCurrent+1:0;
        }

        public void GoNextGamer()
        {
            iCurrent = NextGamer();
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
                        Console.WriteLine("Игрок N {0:d}", i);
                        G[i].DisplayColoda();
                    } 
                }
            } else
            {
                Console.WriteLine("Текущий игрок:");
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
    }
}
