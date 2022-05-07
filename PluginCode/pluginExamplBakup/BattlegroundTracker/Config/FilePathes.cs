using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearthstone_Deck_Tracker;

namespace BattlegroundTracker
{
    
        public class FilePathes
        {

            public string writePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\hstracker\data\";
            public string gameResultPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\hstracker\data\gameresults.dat";
            public string underlordsGameResultPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\hstracker\data\ungameresults.dat";
            public string skinConfigPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\hstracker\data\skinConfig.dat";
            public string mmrPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\hstracker\data\mmr.dat";
            public string changeLogPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\hstracker\data\changeLogPath.dat";
           

    }
    
}
