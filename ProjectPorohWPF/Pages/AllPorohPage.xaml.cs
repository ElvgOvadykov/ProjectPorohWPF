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
    /// Логика взаимодействия для AllPorohPage.xaml
    /// </summary>
    public partial class AllPorohPage : UserControl
    {

        public AllPorohPage()
        {
            InitializeComponent();
            PorohsDataGrid.ItemsSource = new List<CPoroh>(DataBaseController.GetPorohs());
        }

        private void UpdatePorohs_Click(object sender, RoutedEventArgs e)
        {
            bool isOk = true;
            List<CPoroh> porohs = new List<CPoroh>();
            foreach(var item in PorohsDataGrid.ItemsSource)
            {
                if ((item as CPoroh).Name == "")
                    isOk = false;
                if ((item as CPoroh).Power == 0)
                    isOk = false;
                if ((item as CPoroh).Temper == 0)
                    isOk = false;
                if ((item as CPoroh).UdGaz == 0)
                    isOk = false;
                if ((item as CPoroh).Dens == 0)
                    isOk = false;
                if (!isOk)
                {
                    MessageBox.Show("Проверьте запонение всех полей!");
                    return;
                }
                porohs.Add(item as CPoroh);
            }
            DataBaseController.UpdateAllPorohs(porohs);
            PorohsDataGrid.ItemsSource = new List<CPoroh>(DataBaseController.GetPorohs());
        }

        private void DeletePoroh_Click(object sender, RoutedEventArgs e)
        {
 
            DataBaseController.DeletePoroh(PorohsDataGrid.SelectedItem as CPoroh);
            PorohsDataGrid.ItemsSource = new List<CPoroh>(DataBaseController.GetPorohs());
        }

        public void UpdateGrid()
        {
            PorohsDataGrid.ItemsSource = new List<CPoroh>(DataBaseController.GetPorohs()); 
        }
    }
}
