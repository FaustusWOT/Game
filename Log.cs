using System;
using System.Collections.Generic;
using System.Text;
#nullable enable

namespace Game
{
    public enum TColodaType{
        IS_OPEN_COLODA,
        IS_CLOSE_COLODA,
        IS_DESK_COLODA
    }

public class TLog
    {
        private readonly int iDisplayLog;

        public TLog(int iSetDisplay)
        {
            iDisplayLog = iSetDisplay;
        }

        public void DisplayColoda(TColodaType iOpenCards, TColoda Cards)
        {
            if (iDisplayLog > 0)
            {
                string S = "";
                if ((iDisplayLog > 5) & (iOpenCards == TColodaType.IS_CLOSE_COLODA)) iOpenCards = TColodaType.IS_OPEN_COLODA;

                switch (iOpenCards)
                {
                    case TColodaType.IS_DESK_COLODA:
                        {
                            if (!Cards.IsEmpty())
                            {
                                Console.WriteLine("Козырь : " + Cards[0].ToString());

                                Console.WriteLine("Колода ");

                                switch (Cards.Count)
                                {
                                    case 1: Console.WriteLine("Одна карта "); break;
                                    case 2:
                                    case 3:
                                    case 4:
                                    case 5: Console.WriteLine("несколько карт "); break;
                                    default: Console.WriteLine("много карт "); break;
                                }

                            }
                            else Console.WriteLine("Колода пуста");
                            break;
                        }
                    case TColodaType.IS_CLOSE_COLODA:
                        {
                            if (!Cards.IsEmpty())
                            {
                                Console.WriteLine("Колода ");

                                foreach (TCard Card in Cards) S += "#";

                                Console.WriteLine(S);
                            }
                            else Console.WriteLine("Карт нет.");
                            break;
                        }
                    case TColodaType.IS_OPEN_COLODA:
                        {
                            if (!Cards.IsEmpty())
                            {
                                Console.WriteLine("Колода ");

                                for (int i = 0; i < Cards.Count; i++) Console.WriteLine((i + 1).ToString() + " " + Cards[i].ToString());

                                Console.WriteLine(S);
                            }
                            else Console.WriteLine("Карт нет.");
                            break;
                        }
                }
            }
        }
        public void DisplayFirst()
        {
            if (iDisplayLog > 0) Console.WriteLine("Раздаю!!!");
        }
        public void DisplayPosition(string sTitle, TColoda Cards, TColodaType iOpenCards)
        {
            if (iDisplayLog > 5)
                Console.WriteLine(sTitle);
            DisplayColoda(iOpenCards, Cards);
        }
        public void DisplayPosition(string sTitle, TGamer gamer)
        {
            if (iDisplayLog > 5)
                Console.WriteLine(sTitle);
            gamer.DisplayColoda();
        }
        public void DisplayMessage(int iLogLevel, string sMsg)
        {
            if (iLogLevel <= iDisplayLog)
                Console.WriteLine(sMsg);
        }
        public void DisplayMessage(string sMsg)
        {
            DisplayMessage(10, sMsg);
        }

        private void DisplayMove(string sMoveText,string sNULLText,string sGamerName,string? sCard)
        {
            if (sCard != null)
                DisplayMessage(5, String.Format(sMoveText, sGamerName, sCard));
            else
                DisplayMessage(5, String.Format(sNULLText, sGamerName));
        }

        public void SecondMove(string sGamerName, string? sCard)
        {
            DisplayMove("{0:s}: Подкидываю {1:s}", "{0:s}: Бито!!!", sGamerName, sCard);
        }

        public void AnsverMove(string sGamerName, string? sCard)
        {
            DisplayMove("{0:s}: Отбиваю {1:s}", "{0:s}: Принимаю!!!", sGamerName, sCard);
        }

        public void FirstMove(string sGamerName, string? sCard)
        {
            DisplayMove("{0:s}: Захожу {1:s}", "{0:s}: Бросил карты!!!", sGamerName, sCard);
        }

        public void DisplayCurrentGamer(string sGamerName)
        {
            DisplayMessage(5, String.Format("Теперь ходит {0:s}", sGamerName));
        }

        public void TakeOneCard (string sGamerName,string sCard)
        {
            DisplayMessage(5, String.Format("{0:s}: Взял {1:s}", sGamerName, sCard));
        }

        public int? ReadUserInput(string sMsg,int iMaxInput)
        {
            int i;
            do
            {
                DisplayMessage(0, sMsg);

                string S = Console.ReadLine();
                if ((S.ToUpper() == "QUIT") || (S.ToUpper() == "EXIT") || (S.ToUpper() == "STOP")) return null;

                try
                {
                    i = int.Parse(S);
                    if (i < 0)
                    {
                        DisplayMessage(0,"Чиско не должно быть отрицательным!");
                    }
                }
                catch (Exception e)
                {
                    DisplayMessage(0, String.Format("Error {0:S}. Try again!", e.Message));
                    i = -1;
                }

                if (i > 0)
                {
                    if (i > iMaxInput)
                    {
                        DisplayMessage(String.Format("Число должно быть от 0 (нечем ходить) до {0:d}", iMaxInput));
                        i = -1;
                    }
                }

            } while (i < 0);

            return i;

        }

        public void GamerStand(string sGamerName)
        {
            DisplayMessage(5, String.Format("Игрок {0:s} вышел!", sGamerName));
        }

        public void GamerSay(string sGamerName, string sCard)
        {
            DisplayMessage(5,String.Format("{0:s}: у меня {1:s}!", sGamerName, sCard));
        }

        public void FirstMoveGamer(string sGamerName, string sCard)
        {
            DisplayMessage(5,String.Format("Ходит {0:s}, у него {1:s}!", sGamerName, sCard));

        }
        public void DisplayResult(string? sGamerName)
        {
            if (sGamerName != null)
            {
                DisplayMessage(1, String.Format("{0:s} - проиграл!", sGamerName));
            } else
                DisplayMessage(1, "Розыгрыш!");
        }
    }
}
