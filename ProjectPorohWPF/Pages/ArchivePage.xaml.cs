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

namespace ProjectPorohWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для ArchivePage.xaml
    /// </summary>
    public partial class ArchivePage : UserControl
    {
        public MainWindow parent;

        List<CLOADPARAMS> archive;

        public ArchivePage()
        {
            archive = new List<CLOADPARAMS>(DataBaseController.GetArchive());
            InitializeComponent();
            ArchiveGrid.ItemsSource = archive;
        }

        public void UpdateGrid()
        {
            archive = new List<CLOADPARAMS>(DataBaseController.GetArchive());
            ArchiveGrid.ItemsSource = archive;
        }

        private void CheckCalculation(object sender, RoutedEventArgs e)
        {
            if(ArchiveGrid.SelectedItem != null)
            {
                parent.ViewCalculation(ArchiveGrid.SelectedItem as CLOADPARAMS);
            }
            
        }

        private void DeleteArchive_Click(object sender, RoutedEventArgs e)
        {
            archive.Remove(ArchiveGrid.SelectedItem as CLOADPARAMS);
            DataBaseController.DeleteArchive(ArchiveGrid.SelectedItem as CLOADPARAMS);
            ArchiveGrid.ItemsSource = new List<CLOADPARAMS>(DataBaseController.GetArchive());
        }
    }
}
