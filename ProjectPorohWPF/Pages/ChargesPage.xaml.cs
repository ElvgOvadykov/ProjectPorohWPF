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
    /// Логика взаимодействия для ChargesPage.xaml
    /// </summary>
    public partial class ChargesPage : UserControl
    {
        List<CZarad> zarads = new List<CZarad>();
        List<CPoroh> porohs = new List<CPoroh>();

        public ChargesPage()
        {
            InitializeComponent();
            UpdateComboBox();
        }

        private void ActiveСharge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ActiveСharge.SelectedItem as CZarad != null)
            {
                foreach (var item in ActiveСhargeType.ItemsSource)
                {
                    if ((item as CPoroh).ID == (ActiveСharge.SelectedItem as CZarad).Poroh.ID)
                    {
                        ActiveСhargeType.SelectedItem = item;
                        ActiveСhargeType.Text = (item as CPoroh).Name;
                    }
                }
            }
        }

        private void MainСharge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(MainСharge.SelectedItem as CZarad != null)
            {
                foreach (var item in MainСhargeType.ItemsSource)
                {
                    if ((item as CPoroh).ID == (MainСharge.SelectedItem as CZarad).Poroh.ID)
                    {
                        MainСhargeType.SelectedItem = item;
                        MainСhargeType.Text = (item as CPoroh).Name;
                    }
                }
            }
        }

        private void Combobox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UpdateComboBox();
        }

        public void UpdateComboBox()
        {
            List<CZarad> zarads_new = new List<CZarad>(DataBaseController.GetZarads());
            List<CPoroh> porohs_new = new List<CPoroh>(DataBaseController.GetPorohs());

            bool isNewZarads = zarads.SequenceEqual(zarads_new);
            bool isNewPorohs = porohs.SequenceEqual(porohs_new);

            if (!isNewZarads||!isNewPorohs)
            {
                zarads = zarads_new;
                porohs = porohs_new;
                ActiveСharge.ItemsSource = zarads;
                MainСharge.ItemsSource = zarads;
                ActiveСhargeType.ItemsSource = porohs;
                MainСhargeType.ItemsSource = porohs;
            }
            
        }
    }
}
