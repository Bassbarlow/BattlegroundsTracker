using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Hearthstone_Deck_Tracker.Hearthstone;
using HearthDb.Enums;
using System.Windows;

namespace BattlegroundTracker
{
    public class Tribes
    {

        public static HashSet<Race> GetTribes(Guid? gameID, View _view, Config _config, TribesOverlay _tribes)
        {
            var gameTribes = BattlegroundsUtils.GetAvailableRaces(gameID);
            return gameTribes;
        }

        public static void SetBannedTribes(Guid? gameID, View _view, Config _config,  TribesOverlay _tribes)
        {
            Dictionary<Race, string> races = new Dictionary<Race, string>();
            races.Add(Race.BEAST, "Beasts");
            races.Add(Race.DEMON, "Demons");
            races.Add(Race.DRAGON, "Dragon");
            races.Add(Race.ELEMENTAL, "Elementals");
            races.Add(Race.MECHANICAL, "Mechs");
            races.Add(Race.MURLOC, "Murlocs");
            races.Add(Race.PIRATE, "Pirates");
            races.Add(Race.QUILBOAR, "Quilboars");
            races.Add(Race.NAGA, "Nagas");
            Dictionary<Race, string> imgways = new Dictionary<Race, string>();
            imgways.Add(Race.BEAST, "beast.png");
            imgways.Add(Race.DEMON, "demon.png");
            imgways.Add(Race.DRAGON, "dragon.png");
            imgways.Add(Race.ELEMENTAL, "elemental.png");
            imgways.Add(Race.MECHANICAL, "mech.png");
            imgways.Add(Race.MURLOC, "murloc.png");
            imgways.Add(Race.PIRATE, "pirate.png");
            imgways.Add(Race.QUILBOAR, "quilboar.png");
            imgways.Add(Race.NAGA, "nagas.png");
            var gameTribes = BattlegroundsUtils.GetAvailableRaces(gameID);

            List<Race>bans =new ();
            if (gameTribes?.Count > 0)
            {
                if (!gameTribes.Contains(Race.BEAST))
                    bans.Add(Race.BEAST);
                if (!gameTribes.Contains(Race.DEMON))
                    bans.Add(Race.DEMON);
                if (!gameTribes.Contains(Race.DRAGON))
                    bans.Add(Race.DRAGON);
                if (!gameTribes.Contains(Race.ELEMENTAL))
                    bans.Add(Race.ELEMENTAL);
                if (!gameTribes.Contains(Race.MECHANICAL))
                    bans.Add(Race.MECHANICAL);
                if (!gameTribes.Contains(Race.MURLOC))
                    bans.Add(Race.MURLOC);
                if (!gameTribes.Contains(Race.PIRATE))
                    bans.Add(Race.PIRATE);
                if (!gameTribes.Contains(Race.QUILBOAR))
                    bans.Add(Race.QUILBOAR);
                if (!gameTribes.Contains(Race.NAGA))     //ждем когда начнут банить наг
                    bans.Add(Race.NAGA);
            }

            _view.SetisBanned("N/A", "", "","");
            if (_config.showTribeColors == true)
            {

                LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(0, 0, 0), Color.FromRgb(0, 0, 0), new Point(0.5, 0), new Point(0.5, 1));
                _view.spBanned.Background = gradientBrush;
            }

            if (gameTribes?.Count > 0)
            {
                //MessageBox.Show(races[bans[0]] + races[bans[1]] + races[bans[2]] + races[bans[3]]);
                _view.SetisBanned(races[bans[0]], races[bans[1]], races[bans[2]],races[bans[3]]);
                if (_config.showTribeImages == true)
                {
                    if (File.Exists(Config._tribesImageLocation + imgways[bans[0]]))
                    {
                        _tribes.imgTribe1.Source = new BitmapImage(new Uri(Config._tribesImageLocation + imgways[bans[0]]));
                    }
                    else
                    {
                        _tribes.imgTribe1.Source = new BitmapImage(new Uri(Config._tribesImageLocation + @"na.png"));
                    }
                    if (File.Exists(Config._tribesImageLocation + imgways[bans[1]]))
                    {
                        _tribes.imgTribe2.Source = new BitmapImage(new Uri(Config._tribesImageLocation + imgways[bans[1]]));
                    }
                    else
                    {
                        _tribes.imgTribe2.Source = new BitmapImage(new Uri(Config._tribesImageLocation + @"na.png"));
                    }
                    if (File.Exists(Config._tribesImageLocation + imgways[bans[2]]))
                    {
                        _tribes.imgTribe3.Source = new BitmapImage(new Uri(Config._tribesImageLocation + imgways[bans[2]]));
                    }
                    else
                    {
                        _tribes.imgTribe2.Source = new BitmapImage(new Uri(Config._tribesImageLocation + @"na.png"));
                    }
                    if (File.Exists(Config._tribesImageLocation + imgways[bans[3]]))
                    {
                        _tribes.imgTribe4.Source = new BitmapImage(new Uri(Config._tribesImageLocation + imgways[bans[3]]));  //если начнут банить по 4 архетипа
                    }
                    else
                    {
                        _tribes.imgTribe4.Source = new BitmapImage(new Uri(Config._tribesImageLocation + @"na.png"));
                    }
                }

                
            }


            if (gameTribes?.Count > 0) {



            //if (!gameTribes.Contains(Race.PIRATE) && !gameTribes.Contains(Race.MURLOC))
            //{
            //    _view.SetisBanned("Pirates", "Murlocs");
            //    if (_config.showTribeColors == true)
            //    {
            //        LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(89, 0, 0), Color.FromRgb(4, 133, 25), new Point(0.5, 0), new Point(0.5, 1));
            //        _view.spBanned.Background = gradientBrush;
            //    }
            //    if (_config.showTribeImages == true)
            //    {
            //        if (File.Exists(Config._tribesImageLocation + @"piratemurloc.png"))
            //        {
            //            _tribes.imgTribes.Source = new BitmapImage(new Uri(Config._tribesImageLocation + @"piratemurloc.png"));
            //        }
            //    }

            //}
            //    else if (!gameTribes.Contains(Race.PIRATE) && !gameTribes.Contains(Race.DEMON))
            //    {
            //        _view.SetisBanned("Pirates", "Demons");
            //        if (_config.showTribeColors == true)
            //        {
            //            LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(89, 0, 0), Color.FromRgb(82, 0, 113), new Point(0.5, 0), new Point(0.5, 1));
            //            _view.spBanned.Background = gradientBrush;
            //        }
            //        if (_config.showTribeImages == true)
            //        {
            //            if (File.Exists(Config._tribesImageLocation + @"piratedemon.png"))
            //            {
            //                _tribes.imgTribes.Source = new BitmapImage(new Uri(Config._tribesImageLocation + @"piratedemon.png"));
            //            }
            //        }
            //    }
            //    else if (!gameTribes.Contains(Race.PIRATE) && !gameTribes.Contains(Race.MECHANICAL))
            //    {
            //        _view.SetisBanned("Pirates", "Mechs");
            //        if (_config.showTribeColors == true)
            //        {
            //            LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(89, 0, 0), Color.FromRgb(0, 139, 137), new Point(0.5, 0), new Point(0.5, 1));
            //            _view.spBanned.Background = gradientBrush;
            //        }
            //        if (_config.showTribeImages == true)
            //        {
            //            if (File.Exists(Config._tribesImageLocation + @"piratemech.png"))
            //            {
            //                _tribes.imgTribes.Source = new BitmapImage(new Uri(Config._tribesImageLocation + @"piratemech.png"));
            //            }
            //        }
            //    }

            //    else if (!gameTribes.Contains(Race.PIRATE) && !gameTribes.Contains(Race.ELEMENTAL))
            //    {
            //        _view.SetisBanned("Pirates", "Elementals");
            //        if (_config.showTribeColors == true)
            //        {
            //            LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(89, 0, 0), Color.FromRgb(218, 205, 1), new Point(0.5, 0), new Point(0.5, 1));
            //            _view.spBanned.Background = gradientBrush;
            //        }
            //        if (_config.showTribeImages == true)
            //        {
            //            if (File.Exists(Config._tribesImageLocation + @"pirateelemental.png"))
            //            {
            //                _tribes.imgTribes.Source = new BitmapImage(new Uri(Config._tribesImageLocation + @"pirateelemental.png"));
            //            }
            //        }
            //    }


            //    else if (!gameTribes.Contains(Race.PIRATE) && !gameTribes.Contains(Race.BEAST))
            //    {
            //        _view.SetisBanned("Pirates", "Beasts");
            //        if (_config.showTribeColors == true)
            //        {
            //            LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(89, 0, 0), Color.FromRgb(113, 72, 0), new Point(0.5, 0), new Point(0.5, 1));
            //            _view.spBanned.Background = gradientBrush;
            //        }
            //        if (_config.showTribeImages == true)
            //        {
            //            if (File.Exists(Config._tribesImageLocation + @"piratebeast.png"))
            //            {
            //                _tribes.imgTribes.Source = new BitmapImage(new Uri(Config._tribesImageLocation + @"piratebeast.png"));
            //            }
            //        }
            //    }
            //    else if (!gameTribes.Contains(Race.PIRATE) && !gameTribes.Contains(Race.DRAGON))
            //    {
            //        _view.SetisBanned("Pirates", "Dragons");
            //        if (_config.showTribeColors == true)
            //        {
            //            LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(89, 0, 0), Color.FromRgb(0, 0, 101), new Point(0.5, 0), new Point(0.5, 1));
            //            _view.spBanned.Background = gradientBrush;
            //        }
            //        if (_config.showTribeImages == true)
            //        {
            //            if (File.Exists(Config._tribesImageLocation + @"piratedragon.png"))
            //            {
            //                _tribes.imgTribes.Source = new BitmapImage(new Uri(Config._tribesImageLocation + @"piratedragon.png"));
            //            }
            //        }
            //    }

            //    else if (!gameTribes.Contains(Race.BEAST) && !gameTribes.Contains(Race.MURLOC))
            //    {
            //        _view.SetisBanned("Beasts", "Murlocs");
            //        if (_config.showTribeColors == true)
            //        {
            //            LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(113, 72, 0), Color.FromRgb(4, 133, 25), new Point(0.5, 0), new Point(0.5, 1));
            //            _view.spBanned.Background = gradientBrush;
            //        }
            //        if (_config.showTribeImages == true)
            //        {
            //            if (File.Exists(Config._tribesImageLocation + @"beastmurloc.png"))
            //            {
            //                _tribes.imgTribes.Source = new BitmapImage(new Uri(Config._tribesImageLocation + @"beastmurloc.png"));
            //            }
            //        }
            //    }
            //    else if (!gameTribes.Contains(Race.BEAST) && !gameTribes.Contains(Race.DEMON))
            //    {
            //        _view.SetisBanned("Beasts", "Demons");
            //        if (_config.showTribeColors == true)
            //        {
            //            LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(113, 72, 0), Color.FromRgb(82, 0, 113), new Point(0.5, 0), new Point(0.5, 1));
            //            _view.spBanned.Background = gradientBrush;
            //        }
            //        if (_config.showTribeImages == true)
            //        {
            //            if (File.Exists(Config._tribesImageLocation + @"beastdemon.png"))
            //            {
            //                _tribes.imgTribes.Source = new BitmapImage(new Uri(Config._tribesImageLocation + @"beastdemon.png"));
            //            }
            //        }
            //    }
            //    else if (!gameTribes.Contains(Race.BEAST) && !gameTribes.Contains(Race.MECHANICAL))
            //    {
            //        _view.SetisBanned("Beasts", "Mechs");
            //        if (_config.showTribeColors == true)
            //        {
            //            LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(113, 72, 0), Color.FromRgb(0, 139, 137), new Point(0.5, 0), new Point(0.5, 1));
            //            _view.spBanned.Background = gradientBrush;
            //        }
            //        if (_config.showTribeImages == true)
            //        {
            //            if (File.Exists(Config._tribesImageLocation + @"beastmech.png"))
            //            {
            //                _tribes.imgTribes.Source = new BitmapImage(new Uri(Config._tribesImageLocation + @"beastmech.png"));
            //            }
            //        }
            //    }
            //    else if (!gameTribes.Contains(Race.BEAST) && !gameTribes.Contains(Race.ELEMENTAL))
            //    {
            //        _view.SetisBanned("Beasts", "Elementals");
            //        if (_config.showTribeColors == true)
            //        {
            //            LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(113, 72, 0), Color.FromRgb(218, 205, 1), new Point(0.5, 0), new Point(0.5, 1));
            //            _view.spBanned.Background = gradientBrush;
            //        }
            //        if (_config.showTribeImages == true)
            //        {
            //            if (File.Exists(Config._tribesImageLocation + @"beastelemental.png"))
            //            {
            //                _tribes.imgTribes.Source = new BitmapImage(new Uri(Config._tribesImageLocation + @"beastelemental.png"));
            //            }
            //        }
            //    }
            //    else if (!gameTribes.Contains(Race.BEAST) && !gameTribes.Contains(Race.DRAGON))
            //    {
            //        _view.SetisBanned("Beasts", "Dragons");
            //        if (_config.showTribeColors == true)
            //        {
            //            LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(113, 72, 0), Color.FromRgb(0, 0, 101), new Point(0.5, 0), new Point(0.5, 1));
            //            _view.spBanned.Background = gradientBrush;
            //        }
            //        if (_config.showTribeImages == true)
            //        {
            //            if (File.Exists(Config._tribesImageLocation + @"beastdragon.png"))
            //            {
            //                _tribes.imgTribes.Source = new BitmapImage(new Uri(Config._tribesImageLocation + @"beastdragon.png"));
            //            }
            //        }
            //    }

            //    else if (!gameTribes.Contains(Race.DEMON) && !gameTribes.Contains(Race.MURLOC))
            //    {
            //        _view.SetisBanned("Demons", "Murlocs");
            //        if (_config.showTribeColors == true)
            //        {
            //            LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(82, 0, 113), Color.FromRgb(4, 133, 25), new Point(0.5, 0), new Point(0.5, 1));
            //            _view.spBanned.Background = gradientBrush;
            //        }
            //        if (_config.showTribeImages == true)
            //        {
            //            if (File.Exists(Config._tribesImageLocation + @"demonmurloc.png"))
            //            {
            //                _tribes.imgTribes.Source = new BitmapImage(new Uri(Config._tribesImageLocation + @"demonmurloc.png"));
            //            }
            //        }
            //    }

            //    else if (!gameTribes.Contains(Race.DEMON) && !gameTribes.Contains(Race.MECHANICAL))
            //    {
            //        _view.SetisBanned("Demons", "Mechs");
            //        if (_config.showTribeColors == true)
            //        {
            //            LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(82, 0, 113), Color.FromRgb(0, 139, 137), new Point(0.5, 0), new Point(0.5, 1));
            //            _view.spBanned.Background = gradientBrush;
            //        }
            //        if (_config.showTribeImages == true)
            //        {
            //            if (File.Exists(Config._tribesImageLocation + @"demonmech.png"))
            //            {
            //                _tribes.imgTribes.Source = new BitmapImage(new Uri(Config._tribesImageLocation + @"demonmech.png"));
            //            }
            //        }
            //    }

            //    else if (!gameTribes.Contains(Race.DEMON) && !gameTribes.Contains(Race.ELEMENTAL))
            //    {
            //        _view.SetisBanned("Demons", "Elementals");
            //        if (_config.showTribeColors == true)
            //        {
            //            LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(82, 0, 113), Color.FromRgb(218, 205, 1), new Point(0.5, 0), new Point(0.5, 1));
            //            _view.spBanned.Background = gradientBrush;
            //        }
            //        if (_config.showTribeImages == true)
            //        {
            //            if (File.Exists(Config._tribesImageLocation + @"demonelemental.png"))
            //            {
            //                _tribes.imgTribes.Source = new BitmapImage(new Uri(Config._tribesImageLocation + @"demonelemental.png"));
            //            }
            //        }
            //    }

            //    else if (!gameTribes.Contains(Race.DEMON) && !gameTribes.Contains(Race.DRAGON))
            //    {
            //        _view.SetisBanned("Demons", "Dragons");
            //        if (_config.showTribeColors == true)
            //        {
            //            LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(82, 0, 113), Color.FromRgb(0, 0, 101), new Point(0.5, 0), new Point(0.5, 1));
            //            _view.spBanned.Background = gradientBrush;
            //        }
            //        if (_config.showTribeImages == true)
            //        {
            //            if (File.Exists(Config._tribesImageLocation + @"demondragon.png"))
            //            {
            //                _tribes.imgTribes.Source = new BitmapImage(new Uri(Config._tribesImageLocation + @"demondragon.png"));
            //            }
            //        }
            //    }

            //    else if (!gameTribes.Contains(Race.DRAGON) && !gameTribes.Contains(Race.MURLOC))
            //    {
            //        _view.SetisBanned("Dragons", "Murlocs");
            //        if (_config.showTribeColors == true)
            //        {
            //            LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(0, 0, 101), Color.FromRgb(4, 133, 25), new Point(0.5, 0), new Point(0.5, 1));
            //            _view.spBanned.Background = gradientBrush;
            //        }
            //        if (_config.showTribeImages == true)
            //        {
            //            if (File.Exists(Config._tribesImageLocation + @"dragonmurloc.png"))
            //            {
            //                _tribes.imgTribes.Source = new BitmapImage(new Uri(Config._tribesImageLocation + @"dragonmurloc.png"));
            //            }
            //        }
            //    }

            //    else if (!gameTribes.Contains(Race.DRAGON) && !gameTribes.Contains(Race.MECHANICAL))
            //    {
            //        _view.SetisBanned("Dragons", "Mechs");
            //        if (_config.showTribeColors == true)
            //        {
            //            LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(0, 0, 101), Color.FromRgb(0, 139, 137), new Point(0.5, 0), new Point(0.5, 1));
            //            _view.spBanned.Background = gradientBrush;
            //        }
            //        if (_config.showTribeImages == true)
            //        {
            //            if (File.Exists(Config._tribesImageLocation + @"dragonmech.png"))
            //            {
            //                _tribes.imgTribes.Source = new BitmapImage(new Uri(Config._tribesImageLocation + @"dragonmech.png"));
            //            }
            //        }
            //    }


            //    else if (!gameTribes.Contains(Race.DRAGON) && !gameTribes.Contains(Race.ELEMENTAL))
            //    {
            //        _view.SetisBanned("Dragons", "Elementals");
            //        if (_config.showTribeColors == true)
            //        {
            //            LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(0, 0, 101), Color.FromRgb(218, 205, 1), new Point(0.5, 0), new Point(0.5, 1));
            //            _view.spBanned.Background = gradientBrush;
            //        }
            //        if (_config.showTribeImages == true)
            //        {
            //            if (File.Exists(Config._tribesImageLocation + @"dragonelemental.png"))
            //            {
            //                _tribes.imgTribes.Source = new BitmapImage(new Uri(Config._tribesImageLocation + @"dragonelemental.png"));
            //            }
            //        }
            //    }


            //    else if (!gameTribes.Contains(Race.ELEMENTAL) && !gameTribes.Contains(Race.MURLOC))
            //    {
            //        _view.SetisBanned("Elementals", "Murlocs");
            //        if (_config.showTribeColors == true)
            //        {
            //            LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(218, 205, 1), Color.FromRgb(4, 133, 25), new Point(0.5, 0), new Point(0.5, 1));
            //            _view.spBanned.Background = gradientBrush;
            //        }
            //        if (_config.showTribeImages == true)
            //        {
            //            if (File.Exists(Config._tribesImageLocation + @"elementalmurloc.png"))
            //            {
            //                _tribes.imgTribes.Source = new BitmapImage(new Uri(Config._tribesImageLocation + @"elementalmurloc.png"));
            //            }
            //        }
            //    }

            //    else if (!gameTribes.Contains(Race.ELEMENTAL) && !gameTribes.Contains(Race.MECHANICAL))
            //    {
            //        _view.SetisBanned("Elementals", "Mechs");
            //        if (_config.showTribeColors == true)
            //        {
            //            LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(218, 205, 1), Color.FromRgb(0, 139, 137), new Point(0.5, 0), new Point(0.5, 1));
            //            _view.spBanned.Background = gradientBrush;
            //        }
            //        if (_config.showTribeImages == true)
            //        {
            //            if (File.Exists(Config._tribesImageLocation + @"elementalmech.png"))
            //            {
            //                _tribes.imgTribes.Source = new BitmapImage(new Uri(Config._tribesImageLocation + @"elementalmech.png"));
            //            }
            //        }
            //    }


            //    else if (!gameTribes.Contains(Race.MECHANICAL) && !gameTribes.Contains(Race.MURLOC))
            //    {
            //        _view.SetisBanned("Mechs", "Murlocs");
            //        if (_config.showTribeColors == true)
            //        {

            //            LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(0, 139, 137), Color.FromRgb(4, 133, 25), new Point(0.5, 0), new Point(0.5, 1));
            //            _view.spBanned.Background = gradientBrush;

            //        }
            //        if (_config.showTribeImages == true)
            //        {
            //            if (File.Exists(Config._tribesImageLocation + @"mechmurloc.png"))
            //            {
            //                _tribes.imgTribes.Source = new BitmapImage(new Uri(Config._tribesImageLocation + @"mechmurloc.png"));
            //            }
            //        }
            //    }


            //    else if (gameTribes.Contains(Race.INVALID))
            //    {
            //        _view.SetisBanned("N/A", "");
            //        if (_config.showTribeColors == true)
            //        {
            //            LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(0, 0, 0), Color.FromRgb(0, 0, 0), new Point(0.5, 0), new Point(0.5, 1));
            //            _view.spBanned.Background = gradientBrush;
            //        }
            //        if (_config.showTribeImages == true)
            //        {
            //            if (File.Exists(Config._tribesImageLocation + @"na.png"))
            //            {
            //                _tribes.imgTribes.Source = new BitmapImage(new Uri(Config._tribesImageLocation + @"na.png"));
            //            }
            //        }
            }


        }
          

    }
}
