using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media;
using BoonwinsBattlegroundTracker.Sounds;
using Hearthstone_Deck_Tracker;
using Core = Hearthstone_Deck_Tracker.API.Core;


namespace BattlegroundTracker.Overlays
{
    public class TavernUpBttnManager
    {
        private User32.MouseInput _mouseInput;
        private User32.MouseInput _mouseSounder;
        private TavernUpBttnArea _tavernUp;
        private Config _config;
        private Point mousePos0;
        private Point overlayPos0;
        private String _selected;
        public TavernUpBttnManager(TavernUpBttnArea tavernUpArea, Config c)
        {
            _tavernUp = tavernUpArea;
            _config = c;
            UpdateConfig(c);
            
        }

        public void UpdateConfig(Config c)
        {
            c.tavernUpPosLeft= _config.tavernUpPosLeft;
            c.tavernUpPosTop= _config.tavernUpPosTop;
            c.save();
        }

        public bool TriggerSound()
        {
            if (Hearthstone_Deck_Tracker.Core.Game.IsRunning && _mouseInput == null)
            {
                _mouseSounder = new User32.MouseInput();
                _mouseSounder.LmbDown += MouseInputOnLmbDownSound;
                return true;
            }
            Dispose();
            return false;
        }
        public bool Toggle()
        {
            if (Hearthstone_Deck_Tracker.Core.Game.IsRunning && _mouseInput == null)
            {
                _tavernUp.Background = new SolidColorBrush(Color.FromArgb(50,0,255,0));
                _mouseInput = new User32.MouseInput();
                _mouseSounder = new User32.MouseInput();
                _mouseInput.LmbDown += MouseInputOnLmbDown;
                _mouseInput.LmbUp += MouseInputOnLmbUp;
                _mouseInput.MouseMoved += MouseInputOnMouseMoved;
                //_mouseSounder.LmbDown += MouseInputOnLmbDownSound;
                return true;
            }
            Dispose();
            return false;
        }

        public void Dispose()
        {
            _tavernUp.Background = new SolidColorBrush(Colors.Transparent);
            _mouseInput?.Dispose();
            _mouseInput = null;

        }

        private void MouseInputOnLmbDown(object sender, EventArgs eventArgs)
        {
            var position = User32.GetMousePos();
            mousePos0 = new Point(position.X, position.Y);

            if (PointInsideControl(mousePos0, _tavernUp))
            {
                _selected = "tavernup";
                //CustomSounder.TavernUp(_config);
            }

            _config.save();
        }
        private async void MouseInputOnLmbDownSound(object sender, EventArgs eventArgs)
        {
            var position = User32.GetMousePos();
            mousePos0 = new Point(position.X, position.Y);

            if (PointInsideControl(mousePos0, _tavernUp))
            {
                //CustomSounder.TavernUp(_config);
                
                CustomSounder.TavernUp(_config);
            }
        }

        private void MouseInputOnLmbUp(object sender, EventArgs eventArgs)
        {
            var pos = User32.GetMousePos();


            if (_selected == "tavernup")
            {
                _config.tavernUpPosTop = pos.Y;
                _config.tavernUpPosLeft = pos.X;
            }

            _selected = null;
            _config.save();
        }

        private void MouseInputOnMouseMoved(object sender, EventArgs eventArgs)
        {
            if (_selected == null)
            {
                return;
            }

            var pos = User32.GetMousePos();


            if (_selected == "tavernup")
            {
                Canvas.SetTop(_tavernUp, pos.Y);
                Canvas.SetLeft(_tavernUp, pos.X );
            }

        }

        private bool PointInsideControl(Point p, FrameworkElement control)
        {
            try
            {
                var position = control.PointFromScreen(p);
                return position.X > 0 && position.X < control.ActualWidth && position.Y > 0 && position.Y < control.ActualHeight;
            }
            catch { return false; }
        }
    }
}

