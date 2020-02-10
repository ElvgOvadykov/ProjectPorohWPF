using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        List<UserControl> Pages = new List<UserControl>();

        public MainWindow()
        {
            InitializeComponent();
            Pages.Add(DataPage);
            Pages.Add(ChargeSelection);
            Pages.Add(AllCharges);
        }

        private void TreeViewItem_Selected_Initial_Data(object sender, RoutedEventArgs e)
        {
            ViewPage(DataPage);
        }

        private void TreeViewItem_Selected_Charge_Selection(object sender, RoutedEventArgs e)
        {
            ViewPage(ChargeSelection);
        }

        private void TreeViewItem_Selected_All_Charges(object sender, RoutedEventArgs e)
        {
            LoadAllCharges();
            ViewPage(AllCharges);
        }

        private void ViewPage(UserControl page)
        {
            foreach(var item in Pages)
            {
                if(item == page)
                {
                    Panel.SetZIndex(item, 1);
                }
                else
                {
                    Panel.SetZIndex(item, 0);
                }
            }
        }

        private void LoadAllCharges()
        {
            List<CZarad> zarads = new List<CZarad>(DataBaseController.GetZarads());
            DataTable dt = new DataTable();
            dt.Columns.Add("Наименование");
            dt.Columns.Add("Внешний диаметр, мм");
            dt.Columns.Add("Внутренний диаметр, мм");
            dt.Columns.Add("Длина заряда, мм");
            dt.Columns.Add("Тип пороховой смеси");
            foreach (var item in zarads)
            {
                dt.Rows.Add(item.Name, item.Dnar, item.Dvnutr, item.L, item.Poroh);
            }
            AllCharges.ChargesDataGrid.ItemsSource = dt.DefaultView;
        }
    }
}
