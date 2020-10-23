using MahApps.Metro.Controls;
using SMT_Material_Handling.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
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

namespace SMT_Material_Handling
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, INotifyPropertyChanged
    {
        
        public MainWindow()
        { 
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;



        //private void TiLog_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{

        //}

        private void TiTower_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void TiOrder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        //private void SwSMT2_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show(SwSMT2.IsChecked.ToString(), "", MessageBoxButton.OK);
        //}

        //private void SwSMT3_Click(object sender, RoutedEventArgs e)
        //{
          
        //}
    

        private void BtTw1_1_Click(object sender, RoutedEventArgs e)
        {
            TowerWindow towerWindow = new TowerWindow((sender as Button).Name.ToString());
            towerWindow.ShowDialog();
        }

        private void BtOr0_1_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow orderWindow = new OrderWindow((sender as Button).Name.ToString());
            orderWindow.ShowDialog();
        }
        protected void propertyChange(string change)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(change));
        }
    }
}
