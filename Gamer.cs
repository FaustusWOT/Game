using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    abstract class TGamer
    {
        public TColoda Cards;
        public bool ThrowCards;

        public TGamer()
        {
            ThrowCards = false;
        }

        public void GetCard(TCard Card)
        {
            DisplayTakenCard(Card);
            Cards.PutIn(Card);
        }

        public abstract void DisplayTakenCard(TCard TakenCard);
        public abstract TCard GetFirstMove();
        public abstract TCard GetSecondMove(TCard DeskCard, int iHigh);

        public bool GameIsDone ()
        {
            return (Cards.isEmpty());
        }

        public bool NeedCard()
        {
            return Cards.iCardCount < 6;
        }

        public void DisplayColoda()
        {
            Cards.DisplayColoda();
        }
    }

    class TMan : TGamer
    {
        public TMan()
        {
            Cards = new TManColoda();
        }
        public override TCard GetFirstMove()
        {
            return Cards.GetRandomCard();
        }
        public override TCard GetSecondMove(TCard DeskCard, int iHigh)
        {
            int[] SelCards = new int[52];
            int iCount = 0;

            for (int i = 0;i < Cards.iCardCount; i++)
            {
                if (Cards.Data[i].CanTake(DeskCard,iHigh))
                {
                    SelCards[iCount++] = i;
                }
            }
            
            if (iCount == 0)
            {
                return null;
            } else
            {
                Random rand = new Random();
                return Cards.GetFrom(SelCards[rand.Next(0, iCount)]);
            }

        }
        public override void DisplayTakenCard(TCard TakenCard)
        {
            Console.WriteLine("Взял 1 карту...");
        }
    }

    class THuman : TGamer
    {
        public THuman ()
        {
            Cards = new THumanColoda();
        }

        private int? GetHumanEnter()
        {
            int i;
            do
            {
                string S = Console.ReadLine();
                if ((S.ToUpper() == "QUIT") || (S.ToUpper() == "EXIT") || (S.ToUpper() == "STOP")) return null;

                try
                {
                    i = int.Parse(S);
                    if (i < 0)
                    {
                        Console.WriteLine("Чиско не должно быть отрицательным!");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error {0:S}. Try again!", e.Message);
                    i = -1;
                }

                if (i > 0)
                {
                    if (i > Cards.iCardCount)
                    {
                        Console.WriteLine("Число должно быть от 0 (нечем ходить) до {0:d}", Cards.iCardCount);
                        i = -1;
                    }
                }

            } while (i < 0);

            return i;
        }

        public override TCard GetFirstMove()
        {
            int? i = GetHumanEnter();

            if (i == null)
            {
                ThrowCards = true;
                return null;
            }

            return (i > 0)?Cards.GetFrom(i.Value - 1):null;
            }
        public override TCard GetSecondMove(TCard DeskCard, int iHigh)
        {
            int? iIndex = GetHumanEnter();

            if (!iIndex.HasValue)
            {
                ThrowCards = true;
                return null;
            }
            if (iIndex == 0) return null;

            if (Cards.Data[iIndex.Value].CanTake(DeskCard, iHigh))
            {
                return Cards.GetFrom(iIndex.Value-1);
            }
            else
            {
                return null;
            }
        }

        public override void DisplayTakenCard(TCard TakenCard)
        {
            Console.WriteLine("Взял {0:s}",TakenCard.ToString());
        }
    }
}

