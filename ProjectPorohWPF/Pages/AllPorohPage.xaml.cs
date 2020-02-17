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
        }

        private void PorohsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            List<CPoroh> porohs = new List<CPoroh>(DataBaseController.GetPorohs());
            PorohsDataGrid.ItemsSource = porohs;
            PorohsDataGrid.Columns[0].Header = "№";
            PorohsDataGrid.Columns[1].Header = "Наименование";
            PorohsDataGrid.Columns[2].Header = "Сила пороха, Дж/кг";
            PorohsDataGrid.Columns[3].Header = "Расчетная температура, К";
            PorohsDataGrid.Columns[4].Header = "Удельная газопроизводительность, л/кг";
            PorohsDataGrid.Columns[5].Header = "Плотность, г/см3";
        }
    }
}
