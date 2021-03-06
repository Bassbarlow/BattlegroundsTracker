using HearthDb.Enums;

using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Hearthstone.Entities;
using Hearthstone_Deck_Tracker.Utility;
using Hearthstone_Deck_Tracker.Utility.Logging;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;




namespace BattlegroundTracker
{
    public partial class SettingsControl : UserControl
    {

        BgMatchOverlay _overlay = new BgMatchOverlay();
        TribesOverlay _tribes = new TribesOverlay();
        ConsoleOverlay _console = new ConsoleOverlay();
        SimpleOverlay _simpleOverlay = new SimpleOverlay();
        TavernUpBttnArea _tavernUp = new();

        private Action _mount;
        private Action _unmount;
        private Config _config;
        public string bgFileName;
        public SettingsControl(Config c, Action mount, Action unmount)
        {
            InitializeComponent();
            //cbLeaderboardName.TextChanged += cbLeaderboardName_TextChanged;
            _config = c;
            UpdateConfig(c);
            _mount = mount;
            _unmount = unmount;

            IsBigMenuEnabled();
        }

 

        public void IsBigMenuEnabled()
        {
            if (_config.menuOverlayEnabled == true)
            {
                cbIsBigEmanled.IsChecked = true;
            }
            else { cbIsBigEmanled.IsChecked = false; }
            //if (_config.showTribeColors == true)
            //{
            //    cbEnableBannedTribeColors.IsChecked = true;
            //}
            //else { cbEnableBannedTribeColors.IsChecked = false; }
            if (_config.showTribeImages == true)
            {
                cbEnableBannedTribeImages.IsChecked = true;
                ImageSizeSlider.Value = _config.tribeSize;
                SizeBox.Text = _config.tribeSize.ToString();
                ImageSizeSlider.IsEnabled = true;
                SizeBox.IsEnabled = true;
            }
            else
            {
                cbEnableBannedTribeImages.IsChecked = false;
                ImageSizeSlider.IsEnabled = false;
                SizeBox.IsEnabled = false;
            }
            if (_config.ingameOverlayEnabled == true)
            {
                cbIsInGameEnabled.IsChecked = true;
                //cbBannedTribeImagesSizes.IsEnabled = true;
                ImageSizeSlider.IsEnabled = true;
                SizeBox.IsEnabled = true;
            }
            else { cbIsInGameEnabled.IsChecked = false;
                //cbBannedTribeImagesSizes.IsEnabled = false;
                ImageSizeSlider.IsEnabled = false;
                SizeBox.IsEnabled = false;
            }

            //if (_config.isSoundChecked == true)
            //{
            //    if (_config.IsMeanBobChecked == true)
            //    {
            //        cbSounds.IsChecked = false;
            //        _config.isSoundChecked = false;
            //    }else { 
            //    cbSounds.IsChecked = true;
            //    }
            //}
            //else
            //{
            //    cbSounds.IsChecked = false;
            //}

            if (_config.IsCustomSoundsEnabled)
            {
                cbCustomSounds.IsChecked = _config.IsCustomSoundsEnabled;
                BgMatchData._tavernUpBttnInput.TriggerSound();
                BgMatchData._rerollInput.TriggerSound();
            }

                

            //if (_config.IsMeanBobChecked == true)
            //{
            //    cbMeanBob.IsChecked = true;
            //    _config.isSoundChecked = false;
            //    cbSounds.IsChecked = false;
            //}
            //else
            //{
            //    cbMeanBob.IsChecked = false;
            //}

            if (_config.showSimpleOverlay == true) cbSimpleOverlay.IsChecked = true;
            else cbSimpleOverlay.IsChecked = false;

            //if (_config.isLeaderboardActivated)
            //{
            //    cbLeaderboard.IsChecked = true;
            //    LeaderboardNamePanel.IsEnabled = true;
                
            //}
            //else { cbLeaderboard.IsChecked = false;
            //    LeaderboardNamePanel.IsEnabled = false;

            //}
            //if (!String.IsNullOrEmpty(_config.leaderboardName))
            //{
            //    cbLeaderboardName.Text = _config.leaderboardName;
            //}
            if(_config.IsAdmin && _config.UseDisconect)
            {
                //cbDisconector.IsChecked = true;
                InGameDisconectorOverlay._window.Show();
            }

        }

        public void UpdateConfig(Config c)
        {


        }

        private void Mount(object sender, RoutedEventArgs e)
        {
            _mount();
            _config.showStatsOverlay = true;

        }

        private void Unmount(object sender, RoutedEventArgs e)
        {

            _unmount();
            _config.showStatsOverlay = false;

        }

        private void cpPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {

            BgMatchData._overlay.tbAvgRankText.Foreground = new SolidColorBrush(cpPickerTextColor.SelectedColor.Value);
            BgMatchData._overlay.tbMmrText.Foreground = new SolidColorBrush(cpPickerTextColor.SelectedColor.Value);
            BgMatchData._overlay.tbTotalGames.Foreground = new SolidColorBrush(cpPickerTextColor.SelectedColor.Value);
            BgMatchData._overlay.tbMmrValueText.Foreground = new SolidColorBrush(cpPickerTextColor.SelectedColor.Value);

            _config.TrackerFontColor = cpPickerTextColor.SelectedColor.Value.ToString();
            _config.save();

        }

        private void cpPickerPlusMMR_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            BgMatchData._overlay.tbMmrValueCangeText.Foreground = new SolidColorBrush(cpPickerPlusMMR.SelectedColor.Value);

            _config.MmrPlus = cpPickerPlusMMR.SelectedColor.Value.ToString();
            _config.save();

        }

        private void cpPickerMinusMMR_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            BgMatchData._overlay.tbMmrValueNegativeCange.Foreground = new SolidColorBrush(cpPickerMinusMMR.SelectedColor.Value);

            _config.MmrMinus = cpPickerMinusMMR.SelectedColor.Value.ToString();
            _config.save();

        }

        private void mmrPlus_Checked(object sender, RoutedEventArgs e)
        {
            BgMatchData._overlay.tbMmrValueCangeText.Visibility = Visibility.Visible;
        }

        private void mmrPlus_Unchecked(object sender, RoutedEventArgs e)
        {
            BgMatchData._overlay.tbMmrValueCangeText.Visibility = Visibility.Hidden;
        }

        private void mmrMinus_Checked(object sender, RoutedEventArgs e)
        {
            BgMatchData._overlay.tbMmrValueNegativeCange.Visibility = Visibility.Visible;
        }

        private void mmrMinus_Unchecked(object sender, RoutedEventArgs e)
        {
            BgMatchData._overlay.tbMmrValueNegativeCange.Visibility = Visibility.Hidden;
        }


        private void cbIsMenuOverlay_Checked(object sender, RoutedEventArgs e)
        {
           _config.menuOverlayEnabled = true;
           _config.save();

           if (_config.IsCustomSoundsEnabled)
           {
               CbCustomSounds_OnUnchecked(sender, e);
               CbCustomSounds_OnChecked(sender, e);
           }
           else
           {
               CbCustomSounds_OnChecked(sender, e);
               CbCustomSounds_OnUnchecked(sender, e);
           }

            //if (Core.Game.CurrentMode == Hearthstone_Deck_Tracker.Enums.Hearthstone.Mode.BACON)
            //{
            //    if (_config.menuOverlayEnabled == true)
            //    {

            //        if (!Core.OverlayCanvas.Children.Contains(_overlay))
            //        {
            //            Core.OverlayCanvas.Children.Add(_overlay);
            //        }

            //    }
            //    else
            //    {
            //        if (Core.OverlayCanvas.Children.Contains(_overlay)) { 
            //        Core.OverlayCanvas.Children.Remove(_overlay);
            //        }
            //    }

            //}


        }

        private void cbIsMenuOverlay_Unchecked(object sender, RoutedEventArgs e)
        {
            _config.menuOverlayEnabled = false;
            _config.save();

            if (_config.IsCustomSoundsEnabled)
            {
                CbCustomSounds_OnUnchecked(sender, e);
                CbCustomSounds_OnChecked(sender, e);
            }
            else
            {
                CbCustomSounds_OnChecked(sender, e);
                CbCustomSounds_OnUnchecked(sender, e);
            }
            //if (Core.Game.CurrentMode == Hearthstone_Deck_Tracker.Enums.Hearthstone.Mode.BACON)
            //{
            //    if (_config.menuOverlayEnabled == false)
            //    {


            //        if (Core.OverlayCanvas.Children.Contains(_overlay))
            //        {
            //            Core.OverlayCanvas.Children.Remove(_overlay);
            //        }

            //    }

            //}

        }

        private void cbIsInGameOverlay_Checked(object sender, RoutedEventArgs e)
        {
            _config.ingameOverlayEnabled = true;
            _config.save();
        }

        private void cbIsInGameOverlay_Unchecked(object sender, RoutedEventArgs e)
        {
            _config.ingameOverlayEnabled = false;
            _config.save();
        }

        private void BtnUnlockOverlay_Click(object sender, RoutedEventArgs e)
        {
            btnUnlock.Content = BgMatchData._input.Toggle() ? "Lock Ranks" : "Unlock Ranks";
        }

        private void cbEnableBannedTribeColors_Checked(object sender, RoutedEventArgs e)
        {
            _config.showTribeColors = true;
            _config.save();
        }

        private void cbEnableBannedTribeColors_Unchecked(object sender, RoutedEventArgs e)
        {
            _config.showTribeColors = false;
            _config.save();
        }

        private void cbEnableBannedTribeImages_Checked(object sender, RoutedEventArgs e)
        {
            _config.showTribeImages = true;
            ImageSizeSlider.IsEnabled = true;
            SizeBox.IsEnabled = true;
            //cbBannedTribeImagesSizes.IsEnabled = true;
            _config.save();
        }

        private void cbEnableBannedTribeImages_Unchecked(object sender, RoutedEventArgs e)
        {
            _config.showTribeImages = false;
            ImageSizeSlider.IsEnabled = false;
            SizeBox.IsEnabled = false;
            //cbBannedTribeImagesSizes.IsEnabled = false;
            _config.save();
        }

        private void btntribesUnlock_Click(object sender, RoutedEventArgs e)
        {
            if (_config.showTribeImages)
            btntribesUnlock.Content = BgMatchData._tribeInput.Toggle() ? "Lock Tribes" : "Unlock Tribes";
        }

        private void cbBannedTribeImagesSizes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var comboBox = sender as ComboBox;


            _tribes.SetTribeImageSize(comboBox.SelectedIndex);
            switch(comboBox.SelectedIndex)
            {
                case 0:
                    _config.tribeSize = 0;
                    _config.save();
                    break;
                case 1:
                    _config.tribeSize = 1;
                    _config.save();
                    break;
                case 2:
                    _config.tribeSize = 2;
                    _config.save();
                    break;
                case 3:
                    _config.tribeSize = 3;
                    _config.save();
                    break;
            }
            

        }

        private void cbBannedTribeImagesSizes_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            data.Add("150x150");
            data.Add("200x200");
            data.Add("250x250");
            data.Add("300x300");

            // ... Get the ComboBox reference.
            var comboBox = sender as ComboBox;


            // ... Assign the ItemsSource to the List.
            comboBox.ItemsSource = data;

            // ... Make the first item selected.
            if(_config.tribeSize != 0)
            {
                comboBox.SelectedIndex = _config.tribeSize;
            }else comboBox.SelectedIndex = 0;
        }

        //private void btnHideConsole_Click(object sender, RoutedEventArgs e)
        //{
        //    _config.showConsole = false;
        //    _config.save();
        //    ShowHideMenu("sbHideTopMenu", btnTopMenuHide, btnTopMenuShow);
        //}

        //private void btnShowConsole_Click(object sender, RoutedEventArgs e)
        //{
        //    _config.showConsole = true;
        //    _config.save();
        //    ShowHideMenu("sbShowTopMenu", btnTopMenuHide, btnTopMenuShow);
        //}

        public void ShowHideMenu(string Storyboard, Button btnHide, Button btnShow)
        {
            

            if (Storyboard.Contains("Show"))
            {
                btnHide.Visibility = System.Windows.Visibility.Visible;
                btnShow.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (Storyboard.Contains("Hide"))
            {
                btnHide.Visibility = System.Windows.Visibility.Hidden;
                btnShow.Visibility = System.Windows.Visibility.Visible;
            }
        }

     
        //private void cbLeaderboard_Checked(object sender, RoutedEventArgs e)
        //{
        //   _config.isLeaderboardActivated = true;
        //   LeaderboardNamePanel.IsEnabled = true;
        //   _config.save();
        //}

        //private void cbLeaderboard_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    _config.isLeaderboardActivated = false;
        //    LeaderboardNamePanel.IsEnabled = false;
        //    _config.save();
        //}       
      

        //private void cbSounds_Checked(object sender, RoutedEventArgs e)
        //{
        //    _config.isSoundChecked = true;
        //    _config.IsMeanBobChecked = false;
        //    cbMeanBob.IsChecked = false;
        //    _config.save();
        //}

        private void cbSounds_Unchecked(object sender, RoutedEventArgs e)
        {
            _config.isSoundChecked = false;
            _config.save();
        }

        private void cbSimpleOverlay_Checked(object sender, RoutedEventArgs e)
        {
            _config.showSimpleOverlay = true;
            _config.save();
        }

        private void cbSimpleOverlay_Unchecked(object sender, RoutedEventArgs e)
        {
            _config.showSimpleOverlay = false;
            _config.save();
        }

   

        private void btnStatsShow_Click(object sender, RoutedEventArgs e)
        {
            Window gameHistoryOverlay = new Window()
            {
                Title = " Battlegrounds MMR per Game Stats",
                Content = new GameHistoryOverlay(),

                ResizeMode = ResizeMode.NoResize
            };

            gameHistoryOverlay.Show();
        
        }
        private void cbLeaderboardName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = sender as TextBox;
            _config.leaderboardName = text.Text;
            _config.save();
        }



        public static bool IsValidUri(string uri)
        {
            if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                return false;
            Uri tmp;
            if (!Uri.TryCreate(uri, UriKind.Absolute, out tmp))
                return false;
            return tmp.Scheme == Uri.UriSchemeHttp || tmp.Scheme == Uri.UriSchemeHttps;
        }

        public static bool OpenUri(string uri)
        {
            if (!IsValidUri(uri))
                return false;
            System.Diagnostics.Process.Start(uri);
            return true;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenUri(@"https://ko-fi.com/boonwin");
        }

        //private void cbMeanBob_Checked(object sender, RoutedEventArgs e)
        //{
        //    _config.IsMeanBobChecked = true;
        //    _config.isSoundChecked = false;
        //    cbSounds.IsChecked = false;
        //    _config.save();
        //}

        private void cbMeanBob_Unchecked(object sender, RoutedEventArgs e)
        {
            _config.IsMeanBobChecked = false;
            _config.save();
        }

        private void btnHeroShow_Click(object sender, RoutedEventArgs e)
        {
            Window HeroStats = new Window()
            {
                Title = " Battlegrounds Hero Stats",
                Content = new HeroStats(),

                ResizeMode = ResizeMode.NoResize
            };

            HeroStats.Show();

        }

        private void btnDisconecterCreate_Click(object sender, RoutedEventArgs e)
        {
            Disconnector.CheckAndCreateRule();
            
        }

        //private void cbDisconector_Checked(object sender, RoutedEventArgs e)
        //{
        //    if (_config.IsAdmin) {

        //        if (!String.IsNullOrEmpty(_config.GamePath))
        //        {
        //            btnDisconecterCreate.IsEnabled = true;
        //            _config.UseDisconect = true;
        //            _config.save();
        //        }
        //        else
        //        {
        //            OpenFileDialog fileDialog = new OpenFileDialog();
        //            fileDialog.Filter = "Hearthstone.exe (Hearthstone.exe)|Hearthstone.exe";
        //            fileDialog.FilterIndex = 1;
        //            fileDialog.Multiselect = false;
        //            Nullable<bool> result = fileDialog.ShowDialog();
        //            if (result == true)
        //            {
        //                _config.GamePath = Path.GetFullPath(fileDialog.FileName);
        //                btnDisconecterCreate.IsEnabled = true;
        //                _config.UseDisconect = true;
        //                _config.save();
        //            }
                        
        //        }
        //    } else
        //    {
        //        MessageBox.Show("DOOOD IF YOU WANT TO USE THIS, YOU NEED TO BE ADMIN, CANT YOU READ? RESTART DECK TRACKER AS ADMIN....", "Boomer Settings");
        //        cbDisconector.IsChecked = false;
        //    }
        //}

        //private void cbDisconector_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    btnDisconecterCreate.IsEnabled = false;
        //    _config.UseDisconect = false;
        //    _config.save();
        //}

        private void btnOpenDisconectDialog_Click(object sender, RoutedEventArgs e)
        {
            if (_config.UseDisconect)
            {


                Window DisconectButtonWindow = new Window()
                {
                    Title = "Boomer Button",
                    Content = new InGameDisconectorOverlay(),
                    Height = 113.861,
                    Width = 211.646,

                    ResizeMode = ResizeMode.NoResize
                };
                if (!_config.DisconectWindowOpen)
                {
                    _config.DisconectWindowOpen = true;
                    _config.save();
                    InGameDisconectorOverlay.GetWindowName(DisconectButtonWindow);
                    DisconectButtonWindow.Show();
                }


            }

        }

        private void CbCustomSounds_OnChecked(object sender, RoutedEventArgs e)
        {
            _config.IsCustomSoundsEnabled = true;
            BgMatchData._tavernUpBttnInput.TriggerSound();
            BgMatchData._rerollInput.TriggerSound();
            bttnUnlockTavernUpLocation.IsEnabled = true;
            bttnUnlockRerollLocation.IsEnabled= true;
            _config.save();
        }

        private void CbCustomSounds_OnUnchecked(object sender, RoutedEventArgs e)
        {
            _config.IsCustomSoundsEnabled = false;
            bttnUnlockTavernUpLocation.IsEnabled = false;
            bttnUnlockRerollLocation.IsEnabled= false;
            BgMatchData._rerollInput.TriggerSound();
            BgMatchData._tavernUpBttnInput.TriggerSound();
            _config.save();
        }

        private void BttnUnlockTavernUpLocation_OnClick(object sender, RoutedEventArgs e)
        {
            bttnUnlockTavernUpLocation.Content = BgMatchData._tavernUpBttnInput.Toggle() ? "Lock" : "Unlock";
            //BgMatchData._rerollInput.TriggerSound();
        }

        private void BttnUnlockRerollLocation_OnClick(object sender, RoutedEventArgs e)
        {
            bttnUnlockRerollLocation.Content = BgMatchData._rerollInput.Toggle() ? "Lock" : "Unlock";
            //BgMatchData._tavernUpBttnInput.TriggerSound();
        }

        private void SizeBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Exception ex = new();
            try
            { 
                if (Convert.ToInt32(SizeBox.Text) < 150 || Convert.ToInt32(SizeBox.Text) > 350) return;
                //ImageSizeSlider.Value = Convert.ToDouble(SizeBox.Text);
                _config.tribeSize = Convert.ToInt32(Math.Round(ImageSizeSlider.Value));
                _config.save();
                _tribes.SetTribeSize(_config);
            }
            catch (NullReferenceException)
            {
                //MessageBox.Show("NullRefSizeBox");
                return;
            }
        }

        private void ImageSizeSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                //SizeBox.Text = Convert.ToInt32(ImageSizeSlider.Value).ToString();
                _config.tribeSize = Convert.ToInt32(Math.Round(e.NewValue));
                _config.save();
                _tribes.SetTribeSize(_config);
            }
            catch (NullReferenceException)
            {
                //MessageBox.Show("NullRef");
                return;
            }
        }
    }
}
