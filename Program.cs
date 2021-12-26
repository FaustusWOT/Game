using System;

namespace Game
{
    class Program
    {
        static void Main()
        {
            TLog MainLog = new TLog(0);

            TStat Stat = new TStat();

            Stat.AddGamerToStat("Джон");
            Stat.AddGamerToStat("Билл");
            Stat.AddGamerToStat("Гарри");
            Stat.AddGamerToStat("Гермиона");

            for (int iMath = 1;iMath < 1000;iMath++)
            {
//                int iMoveCount = 0;
//                TCard ACard;

                MainLog.DisplayMessage("Игра в дурака!");

                TDesk Desk = new TDesk(ref MainLog);

                Desk.AddGamer(new TMan(ref MainLog, "Джон"));
                Desk.AddGamer(new TMan(ref MainLog, "Билл"));
                Desk.AddGamer(new TSmartMan(ref MainLog, "Гарри"));
                Desk.AddGamer(new TSmartMan(ref MainLog, "Гермиона"));

                Desk.GameCycle();
                if (!Desk.GM.GamerStand())
                {
                    if (!Desk.GM.IsEmpty())
                    {
                        MainLog.DisplayResult(Desk.GM.Gamers[0].sName);
                        Stat.GamerInc(Desk.GM.Gamers[0].sName);
                    }
                    else
                        MainLog.DisplayResult(null);
                }
                else
                {
                    MainLog.DisplayMessage("Игра прервана!");
                }
            }

            MainLog.DisplayMessage(0, Stat.GetStringStat());
            MainLog.DisplayMessage(0, "Конец программы");
        }
    }
}
