using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public class TCard
    {
        public int iVal;
        public int iMast;

        public TCard(int Mast,int Val)
        {
            iVal = Val;
            iMast = Mast;
        }
        public override string ToString()
        {
            string S=new string("");

            switch (iMast)
            {
                case 0: S = " червей"; break;
                case 1: S = " пик"; break;
                case 2: S = " крестей"; break;
                case 3: S = " виней"; break;
            }
            S = (iVal switch
            {
                11 => "валет",
                12 => "дама",
                13 => "король",
                14 => "туз",
                _ => iVal.ToString(),
            }) + S;
            return S;
        }
        public bool CanTake(TCard D, int iHigh)
        {
            if ((iMast == iHigh) & !(D.iMast == iHigh)) return true;
            if (!(iMast == iHigh) & (D.iMast == iHigh)) return false;
            if (iMast != D.iMast) return false;
            return (iVal > D.iVal);

        }
        public bool CanSecond(TColoda CardsOnDesk, int iHigh)
        {
            return CardsOnDesk.CanSecond(this, iHigh);
        }
        public bool IsHigh(int iHigh) { return iHigh == iMast; }
    }
}
