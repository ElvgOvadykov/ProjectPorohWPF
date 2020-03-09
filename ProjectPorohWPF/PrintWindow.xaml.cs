using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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

namespace ProjectPorohWPF
{
    /// <summary>
    /// Логика взаимодействия для PrintWindow.xaml
    /// </summary>
    public partial class PrintWindow : Window
    {
        internal ReportClass report;

        public PrintWindow()
        {
            InitializeComponent();
            PrinterComboBox.ItemsSource = PrinterSettings.InstalledPrinters;
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                if (new PrinterSettings() { PrinterName = printer }.IsDefaultPrinter)
                {
                    PrinterComboBox.SelectedItem = printer;
                }
            }
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        { 
            try
            {
                report.PrintPDF("temperfile.pdf", PrinterComboBox.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
