﻿using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using SMT_Material_Handling.Model;
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
using System.Windows.Shapes;

namespace SMT_Material_Handling
{
    /// <summary>
    /// Interaction logic for TowerWindow.xaml
    /// </summary>
    /// init
    public partial class TowerWindow : MetroWindow
    {
        public TowerWindow(string input)
        {
            InitializeComponent();

            Database db = new Database();


            string[] machineModule = input.Split('_');

            List<ActualMaterialInLine> materialInLines = db.GetActualMaterialInLines(machineModule[2], machineModule[3] + "01");


            for (int i = 0; i < materialInLines.Count; i++)
            {
                System.Windows.Controls.Button button = new Button();
                button.Name = "bt" + i;
                button.Content = materialInLines[i].Stage + " - " + materialInLines[i].Material;
                button.Click += Button_Click;
                button.FontSize = 48;

                sp.Children.Add(button);

            }
            Button BtBack = new Button();
            BtBack.Name = "BtBack";
            BtBack.Content = "Zpět";
            BtBack.Click += BtBack_Click;
            BtBack.FontSize = 48;
            BtBack.Background = Brushes.LightBlue;
            sp.Children.Add(BtBack);
        }


        private void BtBack_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Tower tower = new Tower();
            string towerData = tower.sendRequest((sender as Button).Content.ToString());

            await this.ShowMessageAsync("", towerData);
            this.Close();
        }
    }
}
