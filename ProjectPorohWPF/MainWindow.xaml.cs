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

namespace ProjectPorohWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IPage CurrentFrame { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            CurrentFrame = new InitialDataPage();
        }

        private void TreeViewItem_Selected_Initial_Data(object sender, RoutedEventArgs e)
        {
            Panel.SetZIndex(DataPage, 1);
            Panel.SetZIndex(ChargeSelection, 0);
        }

        private void TreeViewItem_Selected_Charge_Selection(object sender, RoutedEventArgs e)
        {
            Panel.SetZIndex(DataPage, 0);
            Panel.SetZIndex(ChargeSelection, 1);
        }
    }
}
