using System;
using System.Collections.Generic;
using System.Text;
#nullable enable

namespace Game
{
    public class TColoda
    {
        //        public TCard[] Data;
        //        public int iCardCount;
        public Random Rand = new Random();

        protected List<TCard> Cards;

        public TColoda()
        {
            //            Data = new TCard[52];
            //            iCardCount = 0;
            Cards = new List<TCard>();
        }

        public void PutIn (TCard Card)
        {
            //            Data[iCardCount++] = Card;
            Cards.Add(Card);
        }

        public TCard GetFrom(int iIndex)
        {
            /*            TCard Card = Data[iIndex];

                        for (int i = iIndex; i < iCardCount; i++) Data[i] = Data[i + 1];
                        iCardCount--;
                        return Card;*/
            TCard Card = Cards[iIndex];
            Cards.RemoveAt(iIndex);
            return Card;
        }
        public TCard? GetFrom(TCard? Card)
        {
            if (Card == null) return null;
            int iIndex = Cards.FindIndex(C => ((C.iMast == Card.iMast) && (C.iVal == Card.iVal)));
            if (iIndex >= 0)
            {
                Cards.RemoveAt(iIndex);
                return Card;
            }
            else return null;
        }

        public TCard? GetLast()
        {
            /*            if (iCardCount != 0)
                        {
                            return  Data[--iCardCount];
                        }
                        else return null;
            */
            return (Cards.Count > 0)?GetFrom(Cards.Count-1):null;
        }

        public TCard GetRandomCard ()
        {
            /*            Random Rand = new Random();

                        return GetFrom(Rand.Next(0, iCardCount));
            */
            return GetFrom(Rand.Next(0, Cards.Count));
        }

        public bool IsEmpty ()
        {
            //            return (iCardCount == 0);
            return (Cards.Count == 0);
        }

        public virtual void DisplayColoda(TLog MainLog) { }

        public bool IsCorrectFirstColoda()
        {
            int[] C;

            C = new int[4];
            //            for (int i= 0;i < iCardCount;i++) C[Data[i].iMast]++;
            foreach (TCard Card in Cards) C[Card.iMast]++;

            foreach (int D in C)
                if (D > 4) return false;
            return true;
        }
        public void DoEmpty()
        {
            //            iCardCount = 0;
            Cards.Clear();
        }
        public int GetMinHigh(int iHigh)
        {
            int iC = 15;
/*            for (int i=0;i < iCardCount;i++)
                if (Data[i].iMast == iHigh)
                {
                    if (Data[i].iVal < iC) iC = Data[i].iVal;
                }*/
            foreach (TCard Card in Cards) 
                if (Card.iMast == iHigh)
                {
                    if (Card.iVal < iC) iC = Card.iVal;
                }
            return iC;
        }
        public void GetCards(TColoda Coloda)
        {
            foreach(TCard Card in Coloda.Cards)
                Cards.Add(Card);

/*            for (int i = 0; i < Coloda.iCardCount; i++)
                PutIn(Coloda.Data[i]);*/
        }

        public bool CanSecond(TCard Card,int iHigh)
        {
            /*            for (int i = 0; i < iCardCount; i++)
                            if (Data[i].iVal == Card.iVal) return true;
                        return false;*/
            return Cards.Exists(C => C.iVal == Card.iVal);
            
        }

        public bool NeedCard()
        {
            return Cards.Count < 6;
        }
        public TColoda? GetSecondMoveCards(TColoda CardsOnDesk, int iHigh)
        {
            TColoda SelCards = new TColoda();

            foreach (TCard Card in Cards)
            {
                if (Card.CanSecond(CardsOnDesk, iHigh))
                {
                    SelCards.PutIn(Card);
                }
            }

            return (!SelCards.IsEmpty()) ? SelCards : null;

        }

        public TColoda? GetAnsverMoveCards(TCard CardOnDesk, int iHigh)
        {
            TColoda SelCards = new TColoda();

            foreach (TCard Card in Cards)
            {
                if (Card.CanTake(CardOnDesk, iHigh))
                {
                    SelCards.PutIn(Card);
                }
            }

            return (!SelCards.IsEmpty()) ? SelCards : null;

        }

        public int CardCount { get { return Cards.Count; } }


        public bool CanTake(int iIndex,TCard Card,int iHigh)
        {
            return Cards[iIndex].CanTake(Card, iHigh);
        }

        public TCard? SelectMinCard (int iHigh)
        {
            TCard? Card = null;
            foreach (TCard C in Cards)
            {
                if (Card == null)
                {
                    Card = C;
                }
                else
                {
                    if (!C.IsHigh(iHigh) & (Card.IsHigh(iHigh))) {
                        Card = C;
                    }
                    else if ((C.IsHigh(iHigh) & Card.IsHigh(iHigh)) | (!C.IsHigh(iHigh) & !Card.IsHigh(iHigh)))
                    {
                        if (C.iVal < Card.iVal)
                        {
                            Card = C;
                        }
                    }
                }
            }
            return Card;
        }
        public TCard this[int i]
        {
            get => Cards[i];
            set => Cards[i] = value; 
        }
        public int Count
        {
            get => Cards.Count;
        }

        public MyColodaEnumerator GetEnumerator()
        {  
            return new MyColodaEnumerator(this);
        }
    }

    public class MyColodaEnumerator
    {
        int nIndex;
        readonly TColoda Coloda;

        public MyColodaEnumerator(TColoda coll)
        {
            Coloda = coll;
            nIndex = 0;
        }

        public bool MoveNext()
        {
            nIndex++;
            return (nIndex < Coloda.Count);
        }

        public TCard Current => Coloda[nIndex];
    }



    class TMainColoda : TColoda
    {
        public override void DisplayColoda(TLog MainLog)
        {
            MainLog.DisplayColoda(TColodaType.IS_DESK_COLODA, this);
        }

        public int GetHigh()
        {
            return Cards[0].iMast;
        }

        public void Make()
        {
            Cards.Clear();
            for (int i = 0; i < 4; i++)
                for (int j = 6; j <= 14; j++)
                    PutIn(new TCard(i, j));
        }

        public void Shuffle()
        {
            //            Random rand = new Random();

            /*            for (int i=0;i < iCardCount;i++)
                        {
                            ChangeTwo(i, (int)(rand.NextDouble() * iCardCount));
                        }*/
            for (int i = 0; i < Cards.Count; i++)
            {
                ChangeTwo(i, (int)(Rand.Next(0,Cards.Count)));
            }

        }

        private void ChangeTwo(int i, int j)
        {
/*            TCard T;

            T = Data[i];
            Data[i] = Data[j];
            Data[j] = T;*/

            TCard T;

            T = Cards[i];
            Cards[i] = Cards[j];
            Cards[j] = T;
        }
    }

    class TManColoda : TColoda
    {
        public override void DisplayColoda(TLog MainLog)
        {
            MainLog.DisplayColoda(TColodaType.IS_CLOSE_COLODA, this);
        }
    }

    class THumanColoda : TColoda
    {
        public override void DisplayColoda(TLog MainLog)
        {
            MainLog.DisplayColoda(TColodaType.IS_OPEN_COLODA, this);
        }

    }
}
