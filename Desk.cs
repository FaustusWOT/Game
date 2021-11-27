using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class TDesk
    {
        public TMainColoda MainCards;
        public TGamers GM;
        public int iHigh;

        public TDesk()
        {
            MainCards = new TMainColoda();
            GM = new TGamers();
        }

        public void PrepareCards()
        {
            MainCards.Make();
            MainCards.Shuffle();
        }
        
        public void AddGamer(TGamer Data)
        {
            GM.AddGamer(Data);
        }

        public void GiveFirstCards()
        {
            iHigh = -1;
            for (int i = 0; i < GM.iGamersCount * 6;i++)           
            {
                if (MainCards.iCardCount == 1) iHigh = MainCards.GetHigh();

                GM.CurrentGamer.GetCard(MainCards.GetFrom(MainCards.iCardCount - 1));
                GM.GoNextGamer();
            }
            if (iHigh < 0) iHigh = MainCards.GetHigh();
        }

        public void AddCards()
        {
            TGamer Current;

            Current = GM.CurrentGamer;
            do
            {
                while (GM.CurrentGamer.NeedCard())
                {
                    if (!MainCards.isEmpty())
                    {
                        GM.CurrentGamer.GetCard(MainCards.GetLast());
                    }
                }
                GM.GoNextGamer();
            } while (GM.CurrentGamer != Current);

        }


        public void SetFirstCurrent()
        {
            GM.iCurrent = 0;
        }
        public bool GameIsDone()
        {
            if (!MainCards.isEmpty()) return false;
            return GM.GameIsDone();
        }

        public void DisplayPosition()
        {
            Console.WriteLine("Колода на столе:");
            MainCards.DisplayColoda();

            Console.WriteLine("Колоды у других игроков");
            GM.DisplayColoda(0);

            Console.WriteLine("Колода текущего игрока");
            GM.DisplayColoda(1);
        }
        public TCard getMove()
        {
            return GM.CurrentGamer.GetFirstMove();
        }
        public TCard getSecondMove(TCard CardOnDesk)
        {

            return GM.NGamer.GetSecondMove(CardOnDesk,iHigh);
        }
    }
}
