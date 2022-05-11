using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserControl = System.Windows.Controls.UserControl;

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

        public void SetTribeSize(Config _config)
        {
            int pos = _config.tribeSize;
            imgTribe1.Width = _config.tribeSize;
            imgTribe1.Height = _config.tribeSize;
            imgTribe1.Margin = new(0, -2*pos, 0, 0);
            imgTribe2.Width = _config.tribeSize;
            imgTribe2.Height = _config.tribeSize;
            imgTribe2.Margin = new(0, -pos+pos/3, 0, 0);
            imgTribe3.Width = _config.tribeSize;
            imgTribe3.Height = _config.tribeSize;
            imgTribe3.Margin = new(0, pos-pos/3, 0, 0);
            imgTribe4.Width = _config.tribeSize;
            imgTribe4.Height = _config.tribeSize;
            imgTribe4.Margin = new(0, 2*pos, 0, 0);
        }
        public void SetTribeImageSize(int index)
        {
            switch (index)
            {
                case 0:
                    
                    imgTribe1.Width = 150;
                    imgTribe1.Height = 150;
                    imgTribe1.Margin = new(-150, -150, 0, 0);
                    imgTribe2.Width = 150;
                    imgTribe2.Height = 150;
                    imgTribe2.Margin= new(-150, 150, 0, 0);
                    imgTribe3.Width = 150;
                    imgTribe3.Height = 150;
                    imgTribe3.Margin = new(150, -150, 0, 0);
                    imgTribe4.Width = 150;
                    imgTribe4.Height = 150;
                    imgTribe4.Margin = new(150, 150, 0, 0);
                    break;
                case 1:

                    imgTribe1.Width = 200;
                    imgTribe1.Height = 200;
                    imgTribe1.Margin = new(-200, -200, 0, 0);
                    imgTribe2.Width = 200;
                    imgTribe2.Height = 200;
                    imgTribe2.Margin = new(-200, 200, 0, 0);
                    imgTribe3.Width = 200;
                    imgTribe3.Height = 200;
                    imgTribe3.Margin = new(200, -200, 0, 0);
                    imgTribe4.Width = 200;
                    imgTribe4.Height = 200;
                    imgTribe4.Margin = new(200, 200, 0, 0);
                    break;
                case 2:

                    imgTribe1.Width = 250;
                    imgTribe1.Height = 250;
                    imgTribe4.Margin = new(-250, -250, 0, 0);
                    imgTribe2.Width = 250;
                    imgTribe2.Height = 250;
                    imgTribe4.Margin = new(-250, 250, 0, 0);
                    imgTribe3.Width = 250;
                    imgTribe3.Height = 250;
                    imgTribe4.Margin = new(250, -250, 0, 0);
                    imgTribe4.Width = 250;
                    imgTribe4.Height = 250;
                    imgTribe4.Margin = new(250, 250, 0, 0);
                    break;
                case 3:

                    imgTribe1.Width = 300;
                    imgTribe1.Height = 300;
                    imgTribe1.Margin = new(-300, -300, 0, 0);
                    imgTribe2.Width = 300;
                    imgTribe2.Height = 300;
                    imgTribe2.Margin = new(-300, 300, 0, 0);
                    imgTribe3.Width = 300;
                    imgTribe3.Height = 300;
                    imgTribe3.Margin = new(300, -300, 0, 0);
                    imgTribe4.Width = 300;
                    imgTribe4.Height = 300;
                    imgTribe4.Margin = new(300, 300, 0, 0);
                    break;
            }
        }
    }
}
