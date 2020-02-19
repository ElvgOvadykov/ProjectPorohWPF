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
        List<CPoroh> porohs;

        public AllPorohPage()
        {
            porohs = new List<CPoroh>(DataBaseController.GetPorohs());
            InitializeComponent();
            PorohsDataGrid.ItemsSource = porohs;
        }

        private void UpdatePorohs_Click(object sender, RoutedEventArgs e)
        {
            DataBaseController.UpdateAllPorohs(porohs);
            PorohsDataGrid.ItemsSource = new List<CPoroh>(DataBaseController.GetPorohs());
        }
    }
}
