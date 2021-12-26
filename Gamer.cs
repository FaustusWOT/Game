using System;
using System.Collections.Generic;
using System.Text;
#nullable enable

namespace Game
{
    public abstract class TGamer
    {
        protected TLog MainLog;

        public TColoda? Cards;
        public bool ThrowCards;
        public string sName;

        public TGamer(ref TLog setMainLog,string pName)
        {
            sName = pName;
            ThrowCards = false;
            MainLog = setMainLog;
        }

        public void GetCard(TCard Card)
        {
            DisplayTakenCard(Card);
            Cards!.PutIn(Card);
        }

        public abstract void DisplayTakenCard(TCard? TakenCard);
        public abstract TCard? GetFirstMove(int iHigh);
        public abstract TCard? GetSecondMove(TColoda CardsOnDesk, int iHigh);
        public abstract TCard? GetAnsverMove(TCard DeskCard, int iHigh);


        public bool GameIsDone ()
        {
            return (Cards!.IsEmpty());
        }

        public bool NeedCard()
        {
            return Cards!.NeedCard();
        }

        public void DisplayColoda()
        {
            Cards!.DisplayColoda(MainLog);
        }
        public bool IsCorrectFirstColoda()
        {
            return Cards!.IsCorrectFirstColoda();
        }

        public void DoEmptyHands()
        {
            Cards!.DoEmpty();
        }
        public int GetMinHigh(int iHigh)
        {
            return Cards!.GetMinHigh(iHigh);
        }
        public void GetCards(TColoda Coloda)
        {
            Cards!.GetCards(Coloda);
        }
    }

    class TMan : TGamer
    {
        public TMan(ref TLog setMainLog,string pName) : base(ref setMainLog,pName)
        {
            Cards = new TManColoda();
        }
        public override TCard? GetFirstMove(int iHigh)
        {
            return Cards!.GetRandomCard();
        }

        public override TCard? GetSecondMove(TColoda CardsOnDesk,int iHigh)
        {
            /*            TColoda SelCards = new TColoda();
                        int iCount = 0;

                        for (int i = 0; i < Cards.iCardCount; i++)
                        {
                            if (Cards.Data[i].CanSecond(CardsOnDesk, iHigh))
                            {
                                SelCards[iCount++] = i;
                            }
                        }

                        if (iCount == 0)
                        {
                            return null;
                        }
                        else
                        {
                            Random rand = new Random();
                            return Cards.GetFrom(SelCards[rand.Next(0, iCount)]);
                        }*/

            TColoda? SelCards = Cards!.GetSecondMoveCards(CardsOnDesk, iHigh);

            TCard? SelCard = SelCards?.GetRandomCard();

            return Cards!.GetFrom(SelCard);
        }

        public override TCard? GetAnsverMove(TCard DeskCard, int iHigh)
        {
            /*            int[] SelCards = new int[52];
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
            */

            TColoda? SelCards = Cards!.GetAnsverMoveCards(DeskCard, iHigh);

            TCard? SelCard = SelCards?.GetRandomCard();

            return Cards.GetFrom(SelCard);

        }
        public override void DisplayTakenCard(TCard? TakenCard)
        {
            MainLog.TakeOneCard(sName,"1 карту");
        }
    }

    class THuman : TGamer
    {
        public THuman (ref TLog setMainLog,string pName) : base(ref setMainLog,pName)
        {
            Cards = new THumanColoda();
        }

        private int? GetHumanEnter()
        {
            Cards!.DisplayColoda(MainLog);

            return MainLog.ReadUserInput("Укажите номер карты : ", Cards.CardCount);
        }

        public override TCard? GetFirstMove(int iHigh)
        {
            int? i = GetHumanEnter();

            if (i == null)
            {
                ThrowCards = true;
                return null;
            }

            return (i > 0) ? Cards!.GetFrom(i.Value - 1) : null;
        }
        public override TCard? GetSecondMove(TColoda CardsOnDesk,int iHigh)
        {
            int? i = GetHumanEnter();

            if (i == null)
            {
                ThrowCards = true;
                return null;
            }
            TCard? ACard = (i > 0) ? Cards!.GetFrom(i.Value - 1) : null;
            if (ACard == null) return null;

            return (CardsOnDesk.CanSecond(ACard, iHigh)) ? ACard : null; 
        }

        public override TCard? GetAnsverMove(TCard DeskCard, int iHigh)
        {
            int? iIndex = GetHumanEnter();

            if (!iIndex.HasValue)
            {
                ThrowCards = true;
                return null;
            }
            if (iIndex == 0) return null;

            if (Cards!.CanTake(iIndex.Value,DeskCard, iHigh))
            {
                return Cards.GetFrom(iIndex.Value-1);
            }
            else
            {
                return null;
            }
        }

        public override void DisplayTakenCard(TCard? TakenCard)
        {
            MainLog.TakeOneCard(sName,TakenCard!.ToString());
        }
    }
}

