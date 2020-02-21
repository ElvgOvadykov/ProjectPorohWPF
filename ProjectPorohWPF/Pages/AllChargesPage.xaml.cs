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
    /// Логика взаимодействия для AllChargesPage.xaml
    /// </summary>
    public partial class AllChargesPage : UserControl
    {
        List<CZarad> zarads;
        List<CPoroh> porohs;

        public AllChargesPage()
        {
            zarads = new List<CZarad>(DataBaseController.GetZarads());
            porohs = new List<CPoroh>(DataBaseController.GetPorohs());
            InitializeComponent();
            ChargesDataGrid.ItemsSource = zarads;
        }

        private void UpdateCharges_Click(object sender, RoutedEventArgs e)
        {
            DataBaseController.UpdateAllCharges(zarads);
            ChargesDataGrid.ItemsSource = new List<CZarad>(DataBaseController.GetZarads());
        }

        private void DeleteCharge_Click(object sender, RoutedEventArgs e)
        {
            zarads.Remove(ChargesDataGrid.SelectedItem as CZarad);
            DataBaseController.DeleteCharge(ChargesDataGrid.SelectedItem as CZarad);
            ChargesDataGrid.ItemsSource = new List<CZarad>(DataBaseController.GetZarads());
        }
    }
}
