using System;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            int iMoveCount = 0;
            TCard CardOnDesk;
            TCard ACard;

            Console.WriteLine("Игра в дурака!");
            TDesk Desk = new TDesk();

            Desk.AddGamer(new TMan("Джон"));
            Desk.AddGamer(new TMan("Билл"));
            Desk.AddGamer(new TMan("Гарри"));
            Desk.AddGamer(new TMan("Гермиона"));

            do
            {
                Desk.PrepareCards();


                Desk.GiveFirstCards();

            } while (!Desk.isCorrectFirstColoda());

            Desk.SelectFirstGamer();
            // Начало игрового цикла
            do
            {
                Console.WriteLine("Ход номер {0:d}", ++iMoveCount);

                Desk.DisplayPosition();

                CardOnDesk = Desk.getMove();
                if (Desk.GM.GamerStand())
                {
                    Console.WriteLine("Игрок бросил карты!");
                    break;
                }
                Console.WriteLine("{0:s}: Захожу {1:s}", Desk.GM.CurrentGamer.sName, CardOnDesk.ToString());
                ACard = Desk.getSecondMove(CardOnDesk);
                if (Desk.GM.GamerStand())
                {
                    Console.WriteLine("{0:s} бросил карты!", Desk.GM.CurrentGamer.sName);
                    break;
                }
                if (ACard == null)
                {
                    Console.WriteLine("{0:s}: Принимаю!", Desk.GM.NGamer.sName);
                    Desk.GM.NGamer.GetCard(CardOnDesk);
                    Desk.AddCards();
                }
                else
                {
                    Console.WriteLine("{0:s}: Отбиваю {1:s}!", Desk.GM.NGamer.sName, ACard.ToString());
                    Desk.AddCards();
                    Desk.GM.GoNextGamer();
                }
                Desk.GM.DeleteGamersWhoGameIsDone();
            } while (!Desk.GameIsDone());
            if (!Desk.GM.GamerStand())
            {
                if (Desk.GM.iGamersCount > 0)
                    Console.WriteLine("{0:s} - проиграл!", Desk.GM.G[0].sName);
                else
                    Console.WriteLine("Розыгрыш!!!");
            }

            Console.WriteLine("Конец программы...");
        }
    }
}
