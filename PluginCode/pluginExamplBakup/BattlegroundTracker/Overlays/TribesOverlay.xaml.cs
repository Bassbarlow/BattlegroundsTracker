using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;

namespace BattlegroundTracker
{
    /// <summary>
    /// Interaktionslogik für TribesOverlay.xaml
    /// </summary>
    public partial class TribesOverlay : UserControl
    {   
        public TribesOverlay()
        {
            InitializeComponent();          
        }

        public void SetTribeImageSize(int index)
        {
            switch (index)
            {
                case 0:

                    imgTribe1.Width = 150;
                    imgTribe1.Height = 150;
                    imgTribe2.Width = 150;
                    imgTribe2.Height = 150;
                    imgTribe3.Width = 150;
                    imgTribe3.Height = 150;
                    break;
                case 1:

                    imgTribe1.Width = 200;
                    imgTribe1.Height = 200;
                    imgTribe2.Width = 200;
                    imgTribe2.Height = 200;
                    imgTribe3.Width = 200;
                    imgTribe3.Height = 200;
                    break;
                case 2:

                    imgTribe1.Width = 250;
                    imgTribe1.Height = 250;
                    imgTribe2.Width = 250;
                    imgTribe2.Height = 250;
                    imgTribe3.Width = 250;
                    imgTribe3.Height = 250;
                    break;
                case 3:

                    imgTribe1.Width = 300;
                    imgTribe1.Height = 300;
                    imgTribe2.Width = 300;
                    imgTribe2.Height = 300;
                    imgTribe3.Width = 300;
                    imgTribe3.Height = 300;
                    break;
            }
        }
    }
}
