using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class TSmartMan : TMan
    {
        public TSmartMan(string pName) : base(pName) { }

        public override TCard GetFirstMove(int iHigh)
        {
            return Cards.GetFrom(Cards.SelectMinCard(iHigh));
        }

        public override TCard GetSecondMove(TColoda CardsOnDesk, int iHigh)
        {
            Random rand = new Random();

            TColoda? SelCards = Cards.GetSecondMoveCards(CardsOnDesk, iHigh);

            TCard? SelCard = (SelCards != null) ? SelCards.SelectMinCard(iHigh) : null;

            return Cards.GetFrom(SelCard);
        }

        public override TCard GetAnsverMove(TCard DeskCard, int iHigh)
        {
            Random rand = new Random();

            TColoda? SelCards = Cards.GetAnsverMoveCards(DeskCard, iHigh);

            TCard? SelCard = (SelCards != null) ? SelCards.SelectMinCard(iHigh) : null;

            return Cards.GetFrom(SelCard);

        }

    }
}
