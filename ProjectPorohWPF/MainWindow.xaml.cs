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

        CLOADPARAMS BaseCalcParam;

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
            LoadChargesCombobox();
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

        private void LoadChargesCombobox()
        {
            List<CZarad> zarads = new List<CZarad>(DataBaseController.GetZarads());
            foreach(var item in zarads)
            {
                ChargeSelection.MainСharge.Items.Add(item);
                ChargeSelection.ActiveСharge.Items.Add(item);
            }
            List<CPoroh> porohs = new List<CPoroh>(DataBaseController.GetPorohs());
            foreach(var item in porohs)
            {
                ChargeSelection.ActiveСhargeType.Items.Add(item);
                ChargeSelection.MainСhargeType.Items.Add(item);
            }
        }

        private void Click_Calculation(object sender, RoutedEventArgs e)
        {

        }

        private bool CorrectData()
        {
            BaseCalcParam.NameWell = DataPage.WellNumber.Text;
            if(BaseCalcParam.NameWell == "")
            {
                MessageBox.Show("Введите название скважины"); //?????????????????????????????????????????????????????????
                throw (new IntException(0));
                return false;
            }
            BaseCalcParam.NameMestor = DataPage.FieldName.Text;
            if (BaseCalcParam.NameMestor == "") BaseCalcParam.NameMestor = "Без названия";
            try
            {
                BaseCalcParam.Zaboy = Convert.ToDouble(DataPage.SlaughterCurrent.Text);
                BaseCalcParam.Dvn = Convert.ToDouble(DataPage.CasingDiameter.Text); //как перевести внутренний и внешний  диаметр в диаметр и толщину обсадной калонны
                BaseCalcParam.GlubVoda = Convert.ToDouble(DataPage.CasingDiameter.Text);
                BaseCalcParam.DensVoda = ConvertToFloatF(StringGrid3->Cells[1][6]);
                BaseCalcParam.HPerf = ConvertToFloatF(StringGrid3->Cells[1][7]);
                BaseCalcParam.PodIntPerf = ConvertToFloatF(StringGrid3->Cells[1][8]);

                BaseCalcParam.DensPerf = ConvertToFloatF(StringGrid3->Cells[1][9]);
                BaseCalcParam.CountOsnZarad = ConvertToFloatF(StringGrid3->Cells[1][13]);
                BaseCalcParam.CountVospZarad = ConvertToFloatF(StringGrid3->Cells[1][11]);
                BaseCalcParam.GlubGen = ConvertToFloatF(StringGrid3->Cells[1][14]);
                BaseCalcParam.Pplast = ConvertToFloatF(StringGrid3->Cells[1][15]) / 10.0;
                BaseCalcParam.Tplast = ConvertToFloatF(StringGrid3->Cells[1][16]);
                BaseCalcParam.ModUnga = ConvertToFloatF(StringGrid3->Cells[1][17]);
                BaseCalcParam.KPuass = ConvertToFloatF(StringGrid3->Cells[1][18]);
                BaseCalcParam.TPvdolWell = 1.0;
                BaseCalcParam.dHFromGenToMan = 1.0;
            }
            catch
            {

            }
        }
    }
}
