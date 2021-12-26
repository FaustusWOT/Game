using System;
using System.Collections.Generic;
using System.Text;
#nullable enable

namespace Game
{
    class TSmartMan : TMan
    {
        public TSmartMan(ref TLog setMainLog,string pName) : base(ref setMainLog,pName) { }

        public override TCard? GetFirstMove(int iHigh)
        {
            return Cards!.GetFrom(Cards.SelectMinCard(iHigh));
        }

        public override TCard? GetSecondMove(TColoda CardsOnDesk, int iHigh)
        {

            TColoda? SelCards = Cards!.GetSecondMoveCards(CardsOnDesk, iHigh);

            TCard? SelCard = SelCards?.SelectMinCard(iHigh);

            return Cards.GetFrom(SelCard);
        }

        public override TCard? GetAnsverMove(TCard DeskCard, int iHigh)
        {

            TColoda? SelCards = Cards!.GetAnsverMoveCards(DeskCard, iHigh);

            TCard? SelCard = SelCards?.SelectMinCard(iHigh);

            return Cards.GetFrom(SelCard);

        }

    }
}
