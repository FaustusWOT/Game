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

            Desk.AddGamer(new THuman());
            Desk.AddGamer(new TMan());

            Desk.PrepareCards();

            Desk.GiveFirstCards();

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
                Console.WriteLine("Захожу {0:s}", CardOnDesk.ToString());
                ACard = Desk.getSecondMove(CardOnDesk);
                if (Desk.GM.GamerStand())
                {
                    Console.WriteLine("Игрок бросил карты!");
                    break;
                }
                if (ACard == null)
                {
                    Console.WriteLine("Принимаю!");
                    Desk.GM.NGamer.GetCard(CardOnDesk);
                    Desk.AddCards();
                }
                else
                {
                    Console.WriteLine("Отбиваю {0:s}!", ACard.ToString());
                    Desk.AddCards();
                    Desk.GM.GoNextGamer();
                }

            } while (!Desk.GameIsDone());

            Console.WriteLine("Конец программы...");
        }
    }
}
