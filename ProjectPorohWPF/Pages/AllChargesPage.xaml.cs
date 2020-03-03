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
        List<CPoroh> porohs;

        public AllChargesPage()
        {
            porohs = new List<CPoroh>(DataBaseController.GetPorohs());
            InitializeComponent();
            ChargesDataGrid.ItemsSource = new List<CZarad>(DataBaseController.GetZarads());
        }

        private void UpdateCharges_Click(object sender, RoutedEventArgs e)
        {
            bool isOk = true;
            foreach (var item in ChargesDataGrid.ItemsSource)
            {
                if ((item as CZarad).Name == "")
                    isOk = false;
                if ((item as CZarad).Dnar == 0)
                    isOk = false;
                if ((item as CZarad).Dvnutr == 0)
                    isOk = false;
                if ((item as CZarad).L == 0)
                    isOk = false;
                if ((item as CZarad).Poroh == null)
                    isOk = false;
            }
            if (!isOk)
            {
                MessageBox.Show("Проверьте заполнение всех полей!");
                return;
            }
            DataBaseController.UpdateAllCharges(new List<CZarad>(ChargesDataGrid.ItemsSource as List<CZarad>));
            ChargesDataGrid.ItemsSource = new List<CZarad>(DataBaseController.GetZarads());
        }

        private void DeleteCharge_Click(object sender, RoutedEventArgs e)
        {
            DataBaseController.DeleteCharge(ChargesDataGrid.SelectedItem as CZarad);
            ChargesDataGrid.ItemsSource = new List<CZarad>(DataBaseController.GetZarads());
        }

        public void UpdateGrid()
        { 
            ChargesDataGrid.ItemsSource = new List<CZarad>(DataBaseController.GetZarads());
        }
    }
}
