using Autodesk.AutoCAD.Runtime;
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

namespace Autocad_Draw_3D_Polyline_26_04_2024
{
    /// <summary>
    /// Логика взаимодействия для Win1.xaml
    /// </summary>
    public partial class Win1 : Window
    {

        public Win1()
        {
            InitializeComponent();
            this.Closed += new EventHandler(MainWindow_Closed);
        }

        private void AddLay_Click(object sender, RoutedEventArgs e)
        {
            GetTextbox getTextbox = new GetTextbox();
            GetTextbox.stringsLay = (TextboxLayer.Text.ToString());
            GetTextbox.stringsCoorStart = (TextboxCoorStart.Text.ToString());
            GetTextbox.stringsCoorN = (TextboxCoorN.Text.ToString());
            GetTextbox.stringsCoorEnd = (TextboxCoorEnd.Text.ToString());
            ClassDraw3dPline classDraw3DPline = new ClassDraw3dPline();
            classDraw3DPline.Draw3dPline();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            TextboxCoorStart.Clear();
            TextboxCoorN.Clear();
            TextboxCoorEnd.Clear();
            TextboxLayer.Clear();
            GetTextbox getTextbox = new GetTextbox();

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            TextboxCoorStart.Clear();
            TextboxCoorN.Clear();
            TextboxCoorEnd.Clear();
            TextboxLayer.Clear();
            GetTextbox getTextbox = new GetTextbox();
        }

        void MainWindow_Closed(object sender, EventArgs e)
        {
            if (CountWin.Count == 1)
            {
                CountWin.Count = 0;
            }
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            string strhelp = HelpText.textHelp().ToString();
            MessageBox.Show(strhelp);
        }

        private void Clear_mass_Click(object sender, RoutedEventArgs e)
        {
           GetTextbox.massCoorEnd = new string [] { } ;
           GetTextbox.massCoorN =  new string[] { }; 
        GetTextbox.massLay = new string[] { };
            GetTextbox.massCoorStart = new string[] { };
        }
    }
}

