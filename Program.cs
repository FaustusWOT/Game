using System;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            int iMoveCount = 0;
            TCard ACard;

            Console.WriteLine("Игра в дурака!");
            TDesk Desk = new TDesk();

            Desk.AddGamer(new TMan("Джон"));
            Desk.AddGamer(new TMan("Билл"));
            Desk.AddGamer(new TSmartMan("Гарри"));
            Desk.AddGamer(new TMan("Гермиона"));

            if (!Desk.GM.GamerStand())
            {
                if (Desk.GM.isGameDone)
                    Console.WriteLine("{0:s} - проиграл!", Desk.GM.Gamers[0].sName);
                else
                    Console.WriteLine("Розыгрыш!!!");
            }
            Desk.GameCycle();
            Console.WriteLine("Конец программы...");
        }
    }
}
