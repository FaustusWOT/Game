using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class TColoda
    {
        public TCard[] Data;
        public int iCardCount;

        public TColoda()
        {
            Data = new TCard[52];
            iCardCount = 0;
        }

        public void PutIn (TCard Card)
        {
            Data[iCardCount++] = Card;
        }

        public TCard GetFrom(int iIndex)
        {
            TCard Card = Data[iIndex];

            for (int i = iIndex; i < iCardCount; i++) Data[i] = Data[i + 1];
            iCardCount--;
            return Card;
        }

        public TCard GetLast()
        {
            if (iCardCount != 0)
            {
                return  Data[--iCardCount];
            }
            else return null;

        }

        public TCard GetRandomCard ()
        {
            Random Rand = new Random();

            return GetFrom(Rand.Next(0, iCardCount));
        }

        public bool isEmpty ()
        {
            return (iCardCount == 0);
        }
        public virtual void DisplayColoda() { }

    }

    class TMainColoda : TColoda
    {
        public override void DisplayColoda()
        {
            base.DisplayColoda();
            
            if (iCardCount > 0)
            {
                Console.WriteLine("Козырь : " + Data[0].ToString());

                Console.WriteLine("Колода ");

                switch (iCardCount)
                {
                    case 1: Console.WriteLine("Одна карта "); break;
                    case 2:
                    case 3:
                    case 4:
                    case 5: Console.WriteLine("несколько карт "); break;
                    default: Console.WriteLine("много карт "); break;
                }

            } else Console.WriteLine("Колода пуста");

        }
        public int GetHigh()
        {
            return Data[0].iMast;
        }

        public void Make()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 6; j <= 14; j++)
                    PutIn(new TCard(i, j));
        }

        public void Shuffle()
        {
            Random rand = new Random();

            for (int i=0;i < iCardCount;i++)
            {
                ChangeTwo(i, (int)(rand.NextDouble() * iCardCount));
            }

        }

        private void ChangeTwo(int i, int j)
        {
            TCard T;

            T = Data[i];
            Data[i] = Data[j];
            Data[j] = T;
        }
    }

    class TManColoda : TColoda
    {
        public override void DisplayColoda()
        {
            base.DisplayColoda();

            if (iCardCount > 0)
            {
                Console.WriteLine("Колода ");

                string S = "";

                for (int i = 0; i < iCardCount; i++) S += "#";

                Console.WriteLine(S);
            }
            else Console.WriteLine("Карт нет.");

        }

    }

    class THumanColoda : TColoda
    {
        public override void DisplayColoda()
        {
            base.DisplayColoda();

            if (iCardCount > 0)
            {
                Console.WriteLine("Колода ");

                for (int i = 0; i < iCardCount; i++) Console.WriteLine((i+1).ToString() + " " + Data[i].ToString());

            }
            else Console.WriteLine("Карт нет.");

        }

    }
}
