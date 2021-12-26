using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    public class TStat
    {
        readonly Dictionary<string,int> Data;

        public TStat()
        {
            Data = new Dictionary<string, int>();
        }

        public void AddGamerToStat(string sName)
        {
            Data.Add(sName, 0);
        }

        public void GamerInc(string sName)
        {
            Data[sName]++;
        }

        public string GetStringStat()
        {
            StringBuilder S = new StringBuilder();
            
            foreach (string sName in Data.Keys)
            {
                S.AppendLine(sName + " - " + Data[sName]);
            }
            return S.ToString();
        }
    }
}
