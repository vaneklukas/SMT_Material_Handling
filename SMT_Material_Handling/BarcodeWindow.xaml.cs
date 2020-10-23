using MahApps.Metro.Controls;
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
    /// Interaction logic for Barcode.xaml
    /// </summary>
    public partial class BarcodeWindow : MetroWindow
    {
        public BarcodeWindow(string input)
        {
            InitializeComponent();

            string[] recived = input.Split(' ');
            // Barcode Text Block
            TbBarcode.Text = '*'+recived[2]+'*';
            TbBarcode.FontFamily = new FontFamily("3 of 9 Barcode");
            TbBarcode.FontSize = 65;
            Tbtext.Text = input;
            Tbtext.FontSize = 65;
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
