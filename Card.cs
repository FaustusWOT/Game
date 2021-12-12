using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class TCard
    {
        public int iVal;
        public int iMast;

        public TCard(int Mast,int Val)
        {
            iVal = Val;
            iMast = Mast;
        }
        public string ToString()
        {
            string S=new string("");

            switch (iMast)
            {
                case 0: S = " червей"; break;
                case 1: S = " пик"; break;
                case 2: S = " крестей"; break;
                case 3: S = " виней"; break;
            }
            switch (iVal)
            {
                case 11: S = "валет" + S; break;
                case 12: S = "дама" + S; break;
                case 13: S = "король" + S; break;
                case 14: S = "туз" + S; break;
                default: S = iVal.ToString() + S; break;
            }
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
    }
}
