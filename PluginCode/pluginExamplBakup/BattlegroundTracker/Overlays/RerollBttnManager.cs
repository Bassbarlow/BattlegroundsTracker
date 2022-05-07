using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BoonwinsBattlegroundTracker.Sounds;
using Hearthstone_Deck_Tracker;
using Core = Hearthstone_Deck_Tracker.API.Core;

namespace BattlegroundTracker.Overlays
{
    public class RerollBttnManager
    {
        private User32.MouseInput _mouseInput;
        private User32.MouseInput _mouseSounder;
        private TavernUpBttnArea _reroll;
        private Config _config;
        private Point mousePos0;
        private Point overlayPos0;
        private String _selected;
        private MediaPlayer _player= new MediaPlayer();

        public RerollBttnManager(TavernUpBttnArea rerollArea, Config c)
        {
            _reroll = rerollArea;
            _config = c;
            UpdateConfig(c);
        }

        public void UpdateConfig(Config c)
        {
            c.rerollPosLeft = _config.rerollPosLeft;
            c.rerollPosTop = _config.rerollPosTop;
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
                _reroll.Background = new SolidColorBrush(Color.FromArgb(50, 255, 0, 0));
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
            _reroll.Background = new SolidColorBrush(Colors.Transparent);
            _mouseInput?.Dispose();
            _mouseInput = null;

        }

        private void MouseInputOnLmbDown(object sender, EventArgs eventArgs)
        {
            var position = User32.GetMousePos();
            mousePos0 = new Point(position.X, position.Y);

            if (PointInsideControl(mousePos0, _reroll))
            {
                _selected = "reroll";
                //CustomSounder.Reroll(_config);
            }

            _config.save();
        }
        private void MouseInputOnLmbDownSound(object sender, EventArgs eventArgs)
        {
            var position = User32.GetMousePos();
            mousePos0 = new Point(position.X, position.Y);

            if (PointInsideControl(mousePos0, _reroll))
            {
                CustomSounder.Reroll(_config);
            }
        }

        private void MouseInputOnLmbUp(object sender, EventArgs eventArgs)
        {
            var pos = User32.GetMousePos();


            if (_selected == "reroll")
            {
                _config.rerollPosTop = pos.Y;
                _config.rerollPosLeft = pos.X;
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


            if (_selected == "reroll")
            {
                Canvas.SetTop(_reroll, pos.Y);
                Canvas.SetLeft(_reroll, pos.X);
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
