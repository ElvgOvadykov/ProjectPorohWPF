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
        CBase BaseRasch;
        CZarad osnZar, vospZar;
        //CReference Reference;
        List<double> T;
        List<double> P;
        List<double> Temper;
        List<double> DlinTrech;
        List<double> ShirTrech;
        List<double> Coord1Gaz;
        List<double> Coord2Gaz;
        List<double> Davl;
        List<double> Voda;
        List<double> WellData;

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
            foreach (var item in Pages)
            {
                if (item == page)
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
            foreach (var item in zarads)
            {
                ChargeSelection.MainСharge.Items.Add(item);
                ChargeSelection.ActiveСharge.Items.Add(item);
            }
            List<CPoroh> porohs = new List<CPoroh>(DataBaseController.GetPorohs());
            foreach (var item in porohs)
            {
                ChargeSelection.ActiveСhargeType.Items.Add(item);
                ChargeSelection.MainСhargeType.Items.Add(item);
            }
        }

        private void Click_Calculation(object sender, RoutedEventArgs e)
        {
            //TTabSheet* ts;

            //for (int i = 2; i < 23; i++)
            //{
            //    ts = (TTabSheet*)FindComponent("TabSheet" + IntToStr(i));
            //    if (ts != NULL) ts->TabVisible = true;
            //}

            BaseRasch = new CBase();
            ClearArray();
            ClearCharts();


            BaseRasch.SetZarad(ref osnZar,ref vospZar);
            BaseRasch.LoadBaseParams(ref BaseCalcParam);
            BaseRasch.SetCountCalcPoint(1000);
            BaseRasch.SetCalcInterval(DataBaseController.GetDataTimeIntervals().First());// Магическое число

            double t1, t2;
            try
            {
                t1 = Convert.ToDouble(Edit1->Text);
                t2 = Convert.ToDouble(Edit8->Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Проверьте правильность введенных расстояния до генератора и времени после воздействия!");
                return;
            }

            if (t2 > Reference->DataTimeInterval[0])
            {
                t2 = Reference->DataTimeInterval[0];
                Edit8->Text = RoundS(t2, 2);
            }

            BaseRasch->SetdHFromGenToMan(t1);
            BaseRasch->SetTPvdolWell(t2);

            int countiter = 0;

            while (1)
            {
                countiter++;
                try
                {
                    BaseRasch->Calc();
                    break;
                    if (countiter > 20) break;
                }
                catch (...)
		{
                    BaseRasch->PlusK++;
                    continue;
                }
                }

                BaseRasch->GetCalcTimes(&T);
                BaseRasch->GetCalcP(&P);
                BaseRasch->GetCalcTemper(&Temper);
                BaseRasch->GetDlinTrech(&DlinTrech);
                BaseRasch->GetShirTrech(&ShirTrech);
                BaseRasch->Get1CoorGaz(&Coord1Gaz);
                BaseRasch->Get2CoorGaz(&Coord2Gaz);
                BaseRasch->GetDavlOfWell(&Davl);
                BaseRasch->GetCoordVoda(&Voda);
                BaseRasch->GetWellCoord(&WellData);

                GetDataToTables();
                InsertDataToCharts();

                TabSheet1->Enabled = false;
                Excel1->Enabled = true;

            }
        }
        private bool CorrectData()
        {
            BaseCalcParam.NameWell = DataPage.WellNumber.Text;
            if (BaseCalcParam.NameWell == "")
            {
                MessageBox.Show("Введите номер скважины"); //?????????????????????????????????????????????????????????
                throw (new IntException(0));
                return false;
            }
            BaseCalcParam.NameMestor = DataPage.FieldName.Text;
            if (BaseCalcParam.NameMestor == "") BaseCalcParam.NameMestor = "Без названия";
            try
            {
                BaseCalcParam.Zaboy = Convert.ToDouble(DataPage.SlaughterCurrent.Text);
                BaseCalcParam.Dvn = Convert.ToDouble(DataPage.CasingDiameter.Text)
                    - (Convert.ToDouble(DataPage.CasingThickness) * 2);
                BaseCalcParam.GlubVoda = Convert.ToDouble(DataPage.FluidLevel.Text);
                BaseCalcParam.DensVoda = Convert.ToDouble(DataPage.FluidDensity.Text);
                BaseCalcParam.HPerf = Convert.ToDouble(DataPage.PunchIntervalPower.Text);
                BaseCalcParam.PodIntPerf = Convert.ToDouble(DataPage.SolePerforationInterval.Text);

                BaseCalcParam.DensPerf = Convert.ToInt32(DataPage.PerforationDensity.Text);
                BaseCalcParam.CountOsnZarad = Convert.ToInt32(ChargeSelection.MainCount.Text);
                BaseCalcParam.CountVospZarad = Convert.ToInt32(ChargeSelection.ActiveCount.Text);
                BaseCalcParam.GlubGen = Convert.ToDouble(DataPage.GeneratorDepth.Text);
                BaseCalcParam.Pplast = Convert.ToDouble(DataPage.ReservoirPressure.Text); // давление в Мп
                BaseCalcParam.Tplast = Convert.ToDouble(DataPage.ReservoirTemperature.Text);
                BaseCalcParam.ModUnga = Convert.ToDouble(DataPage.YoungModulus.Text);
                BaseCalcParam.KPuass = Convert.ToDouble(DataPage.PoissonRatio.Text);
                BaseCalcParam.TPvdolWell = 1.0;
                BaseCalcParam.dHFromGenToMan = 1.0;

                BaseCalcParam.NameOsnZarad = (ChargeSelection.MainСharge.SelectedItem as CZarad).Name;
                if (BaseCalcParam.NameOsnZarad == "")
                {
                    MessageBox.Show("Выберите основные заряды");
                    throw (new IntException(0));
                    return false;
                }

                osnZar = ChargeSelection.MainСharge.SelectedItem as CZarad;

                BaseCalcParam.NameVospZarad = (ChargeSelection.ActiveСharge.SelectedItem as CZarad).Name;

                if (BaseCalcParam.NameVospZarad == "")
                {
                    MessageBox.Show("Выберите активные заряды");
                    throw (new IntException(0));
                    return false;
                }

                vospZar = ChargeSelection.ActiveСharge.SelectedItem as CZarad;

                return true;
                //N2->Enabled = true;
                //Button5->Enabled = false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show("Проверьте правильность введенных данных!");
                throw (new IntException(0));
            }
        }

        private void ClearArray()
        {
            T.Clear();
            P.Clear();
            Temper.Clear();
            DlinTrech.Clear();
            ShirTrech.Clear();
            Coord1Gaz.Clear();
            Coord2Gaz.Clear();
            Davl.Clear();
            Voda.Clear();
            WellData.Clear();
        }

        private void ClearCharts() //Очистка графиков!!!!!
        {

        }
    }
}
