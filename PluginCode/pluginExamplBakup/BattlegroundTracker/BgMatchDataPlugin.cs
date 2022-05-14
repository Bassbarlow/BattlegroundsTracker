using System;
using System.IO;
using System.Windows.Controls;
using Hearthstone_Deck_Tracker.Plugins;
using Hearthstone_Deck_Tracker.API;
using Microsoft.Win32;
using Newtonsoft.Json;
using Hearthstone_Deck_Tracker.Utility.Logging;
using MahApps.Metro.Controls;
using System.Windows;
using Hearthstone_Deck_Tracker.HsReplay;
using System.Threading;
using AutoUpdaterDotNET;
using System.Threading.Tasks;
using System.Windows.Forms;
using BattlegroundTracker.Overlays;
//using Squirrel;
using MenuItem = System.Windows.Controls.MenuItem;
using MessageBox = System.Windows.MessageBox;
using Panel = System.Windows.Controls.Panel;


namespace BattlegroundTracker
{
    public class BgMatchDataPlugin : IPlugin
    {
        private Config _config;
        private BgMatchOverlay _overlay;
        private TribesOverlay _tribes;
        private OverlayManager _overlayManager;

        private TavernUpBttnManager _tavernUpManager;
        private RerollBttnManager _rerollManager;
        private TavernUpBttnArea _rerollArea;
        private TavernUpBttnArea _tavernUp;

        private TriverOverlayManager _tribeOverlayManager;
        private InGameDisconectorOverlay _inGameDisconectorOverlay;

        private View _view;
        private Flyout _settingsFlyout;
        private SettingsControl _settingsControl;
        private ConsoleOverlay _console;

        private SimpleOverlay _simpleOverlay;

        public void OnLoad()
        {
            // create overlay and insert into HDT overlay
            AutoUpdate();
            CreateDateFileEnviroment();
            _tavernUp = new TavernUpBttnArea();
            _rerollArea = new TavernUpBttnArea();
            _overlay = new BgMatchOverlay();
            _view = new View();
            _tribes = new TribesOverlay();
            _inGameDisconectorOverlay = new InGameDisconectorOverlay();

            _console = new ConsoleOverlay();
            _simpleOverlay = new SimpleOverlay();

            BgMatchData._tavernUp = _tavernUp;
            BgMatchData._rerollArea= _rerollArea;
            BgMatchData._tavernUpBttnInput = _tavernUpManager;
            BgMatchData._rerollInput = _rerollManager;
            BgMatchData._overlay = _overlay;
            BgMatchData._view = _view;
            BgMatchData._tribes = _tribes;
            BgMatchData._cheatButtonForNoobs = _inGameDisconectorOverlay;

            //BgMatchData._console = _console;
            BgMatchData._simpleOverlay = _simpleOverlay;

            // Triggered upon startup and when the user ticks the plugin on            
            GameEvents.OnGameStart.Add(BgMatchData.GameStart);
            GameEvents.OnInMenu.Add(BgMatchData.InMenu);
            GameEvents.OnTurnStart.Add(BgMatchData.TurnStart);
            GameEvents.OnGameEnd.Add(BgMatchData.GameEnd);


           

            BgMatchData.OnLoad(_config);

           


            if (_config.showStatsOverlay)
            {
                MountOverlay();
            }
            _overlayManager = new OverlayManager(_overlay, _config);
            _tavernUpManager = new(_tavernUp, _config);
            _rerollManager = new RerollBttnManager(_rerollArea, _config);
            _tribeOverlayManager = new TriverOverlayManager(_tribes, _config);

            BgMatchData._tavernUpBttnInput= _tavernUpManager;
            BgMatchData._rerollInput= _rerollManager;
            BgMatchData._input = _overlayManager;
            BgMatchData._tribeInput = _tribeOverlayManager;
            
            Canvas.SetLeft(_rerollArea,_config.rerollPosLeft);
            Canvas.SetTop(_rerollArea, _config.rerollPosTop);

            Canvas.SetLeft(_tavernUp,_config.tavernUpPosLeft);
            Canvas.SetTop(_tavernUp,_config.tavernUpPosTop);

            Canvas.SetTop(_overlay, _config.posTop);
            Canvas.SetLeft(_overlay, _config.posLeft);

            Canvas.SetTop(_inGameDisconectorOverlay, 50);
            Canvas.SetLeft(_inGameDisconectorOverlay, 300);

            Canvas.SetTop(_tribes, _config.tribePosTop);
            Canvas.SetLeft(_tribes, _config.tribePosLeft);

            if (_config.IsCustomSoundsEnabled)
            {
                BgMatchData._rerollInput.TriggerSound();
                BgMatchData._tavernUpBttnInput.TriggerSound();
            }


            _settingsFlyout = new Flyout();
            _settingsFlyout.Name = "BgSettingsFlyout";
            _settingsFlyout.Position = Position.Left;
            Panel.SetZIndex(_settingsFlyout, 100);
            _settingsFlyout.Header = "BattlegroundTracker Settings";
            _settingsControl = new SettingsControl(_config, MountOverlay, UnmountOverlay);
            _settingsFlyout.Content = _settingsControl;
            //_settingsFlyout.ClosingFinished += (sender, args) =>
            //{        
            //    config.save();
            //};
            Core.MainWindow.Flyouts.Items.Add(_settingsFlyout);

        }

        //private async void UpdatePlugin(UpdateManager manager)
        //{
        //    await manager.UpdateApp();
        //}
        private async void AutoUpdate()
        {
        //UpdateManager manager =
        //    await UpdateManager.GitHubUpdateManager(@"https://github.com/Bassbarlow/BattlegroundsTracker");
        //var updateinfo = await manager.CheckForUpdate();
        //    if (updateinfo.ReleasesToApply.Count > 0)
        //    {
        //       MessageBoxResult dialog= MessageBox.Show("Доступна новая версия, хотите установить?","Обновление",MessageBoxButton.YesNo);
        //       if (dialog == MessageBoxResult.Yes)
        //       {
        //           UpdatePlugin(manager);
        //       }
        //    }
            //AutoUpdater.InstalledVersion = Version;
            //AutoUpdater.AppTitle = "Battlegrounds Tracker";
            ////AutoUpdater.Start("https://boonwin.de/Downloads/version.xml");
            
            //AutoUpdater.DownloadPath = Hearthstone_Deck_Tracker.Config.AppDataPath + @"\Plugins\";
            //var currentDirectory = new DirectoryInfo(Hearthstone_Deck_Tracker.Config.AppDataPath + @"\Plugins\BattlegroundTracker\");
            //if (currentDirectory.Parent != null)
            //{
            //    AutoUpdater.InstallationPath = currentDirectory.Parent.FullName;
            //}

        }
        private void CreateDateFileEnviroment()
        {
            var pluginDateFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\BattlegroundTracker\data\";
            if (!Directory.Exists(pluginDateFolderPath)) { 
            Directory.CreateDirectory(pluginDateFolderPath);
            }
            if (File.Exists(Config._configLocation))
            {
                // load config from file, if available
                _config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Config._configLocation));
            }
            else
            { // create config file
                _config = new Config();
                _config.save();
            } 
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\BattlegroundTracker\data\baseTheme.png") && _config._themeLocation != Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\BattlegroundTracker\data\") 
            {
                _config._themeLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\BattlegroundTracker\data\";
                _config.save();
            }


            if (!File.Exists(_config._gameRecordPath))
            {
                using (File.Create(_config._gameRecordPath));
                
            }
        }

        public void MountOverlay()
        {
            StackPanel BgsTopBar = (StackPanel)Core.OverlayWindow.FindName("BgsTopBar");
            BgsTopBar.Children.Insert(1, _view);
        }


        public void UnmountOverlay()
        {
            StackPanel BgsTopBar = (StackPanel)Core.OverlayWindow.FindName("BgsTopBar");
            BgsTopBar.Children.Remove(_view);
        }

        public void OnUnload()
        {
            Core.OverlayCanvas.Children.Remove(_overlay);
            if (_config.showStatsOverlay) UnmountOverlay();
        }

        public void OnButtonPress()
        {
            // Triggered when the user clicks your button in the plugin list
            _settingsFlyout.IsOpen = true;

        }

        public void OnUpdate()
        {
            BgMatchData.Update();

            if (_settingsFlyout.IsOpen == false)
            {
                _settingsControl.mmrPlus.IsChecked = false;
                _settingsControl.mmrMinus.IsChecked = false;
            }
 
        }

        public string Name => "Battlegrounds Tracker ";

        public string Description => "Shows your reached Ranks, Banned Tribes and best Heroes. Soon there will be even more, or not UwU";

        public string ButtonText => "Settings";

        public string Author => "Bassbarlow";

        public System.Version Version => new System.Version(0, 0, 1, 5);

        public MenuItem MenuItem => CreateMenu();


        private MenuItem CreateMenu()
        {
            MenuItem m = new MenuItem { Header = "Battlegrounds Tracker Settings" };

            m.Click += (sender, args) =>
            {
                _settingsFlyout.IsOpen = true;
            };

            return m;
        }

    }

}
