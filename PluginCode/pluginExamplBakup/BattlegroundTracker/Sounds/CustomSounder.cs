using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;
using System.Windows.Controls;
using BattlegroundTracker;

namespace BoonwinsBattlegroundTracker.Sounds
{
    internal class CustomSounder
    {
        public static void TavernUp(Config _config)
        {
            if (_config.IsCustomSoundsEnabled)
            {
                if (File.Exists(_config._soundLocation + "tavernup.wav"))
                {
                    SoundPlayer player = new SoundPlayer(_config._soundLocation + "tavernup.wav");
                    player.Play();
                }
            }
        }
        public static void Reroll(Config _config)
        {
            if (_config.IsCustomSoundsEnabled)
            {
                if (File.Exists(_config._soundLocation + "reroll.wav"))
                {
                    SoundPlayer player = new SoundPlayer(_config._soundLocation + "reroll.wav");
                    player.Play();
                }
            }
        }
    }
}
