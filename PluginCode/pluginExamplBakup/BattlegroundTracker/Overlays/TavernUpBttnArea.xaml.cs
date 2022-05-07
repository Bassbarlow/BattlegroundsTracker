using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Hearthstone_Deck_Tracker.API;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BoonwinsBattlegroundTracker.Sounds;
using Hearthstone_Deck_Tracker;
using Core = Hearthstone_Deck_Tracker.API.Core;

namespace BattlegroundTracker
{
    /// <summary>
    /// Interaktionslogik für View.xaml
    /// </summary>
    public partial class TavernUpBttnArea : UserControl
    {
        private User32.MouseInput _mouseInput;
        private TavernUpBttnArea _tavernUp;
        private Config _config;
        private Point mousePos0;

        public TavernUpBttnArea()
        {
            _mouseInput = new User32.MouseInput();
            _mouseInput.LmbDown += MouseInputOnLmbDownSound;
            InitializeComponent();
            
        }
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Config conf = new();
            //CustomSounder.TavernUp(conf);
            //meanbob.meanBobLines(1,conf);
        }

        private void MouseInputOnLmbDownSound(object sender, EventArgs eventArgs)
        {
            var position = User32.GetMousePos();
            mousePos0 = new Point(position.X, position.Y);

            if (PointInsideControl(mousePos0, _tavernUp))
            {
                //CustomSounder.TavernUp(_config);
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
