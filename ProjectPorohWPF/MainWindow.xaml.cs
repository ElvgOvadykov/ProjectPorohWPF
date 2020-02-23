using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Wpf;
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
        //double TimeInterval;
        CLOADPARAMS BaseCalcParam = new CLOADPARAMS();
        CBase BaseRasch = new CBase();
        //CReference Reference;
        List<double> T = new List<double>();
        List<double> P = new List<double>();
        List<double> Temper = new List<double>();
        List<double> DlinTrech = new List<double>();
        List<double> ShirTrech = new List<double>();
        List<double> Coord1Gaz = new List<double>();
        List<double> Coord2Gaz = new List<double>();
        List<double> Davl = new List<double>();
        List<double> Voda = new List<double>();
        List<double> WellData = new List<double>();

        List<UserControl> Pages = new List<UserControl>();

        public MainWindow()
        {
            InitializeComponent();
            Pages.Add(DataPage);
            Pages.Add(ChargeSelection);
            Pages.Add(AllCharges);
            Pages.Add(CombustionPressure);
            Pages.Add(AllPoroh);
            Pages.Add(Archive);
            Pages.Add(TemperatureCombustion);
        }

        private void TreeViewItem_Selected_Initial_Data(object sender, RoutedEventArgs e)
        {
            ViewPage(DataPage);
        }

        private void TreeViewItem_Selected_Charge_Selection(object sender, RoutedEventArgs e)
        {
            ChargeSelection.UpdateComboBox();
            ViewPage(ChargeSelection);
        }

        private void TreeViewItem_Selected_All_Charges(object sender, RoutedEventArgs e)
        {
            ViewPage(AllCharges);
        }

        private void TreeViewItem_Selected_Combustion_Pressure(object sender, RoutedEventArgs e)
        {
            ViewPage(CombustionPressure);
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

        private void Click_Calculation(object sender, RoutedEventArgs e)
        {
            //TTabSheet* ts;

            //for (int i = 2; i < 23; i++)
            //{
            //    ts = (TTabSheet*)FindComponent("TabSheet" + IntToStr(i));
            //    if (ts != NULL) ts->TabVisible = true;
            //}

            if (CorrectData())
            {
                BaseRasch = new CBase();
                ClearArray();
                ClearCharts();

                BaseRasch.SetZarad(ref BaseCalcParam.osnZar, ref BaseCalcParam.vospZar);
                BaseRasch.LoadBaseParams(ref BaseCalcParam);
                BaseRasch.SetCountCalcPoint(1000);
                BaseRasch.SetCalcInterval(BaseCalcParam.TimeInterval);// Магическое число

                double t1, t2;
                try
                {
                    t1 = Convert.ToDouble(CombustionPressure.Distance.Text);
                    //t2 = Convert.ToDouble(Edit8->Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show("Проверьте правильность введенных расстояния до генератора и времени после воздействия!");
                    return;
                }

                //if (t2 > Reference->DataTimeInterval[0])
                //{
                //    t2 = Reference->DataTimeInterval[0];
                //    Edit8->Text = RoundS(t2, 2);
                //}

                BaseRasch.SetdHFromGenToMan(t1);
                BaseRasch.SetTPvdolWell(1); // Тестовое время воздействия 1 секунда
                //BaseRasch.SetTPvdolWell(t2);

                int countiter = 0;

                while (true)
                {
                    countiter++;
                    try
                    {
                        if (countiter > 20) break;
                        BaseRasch.Calc();//?????????????????????????????
                        break;
                    }
                    catch (Exception ex)
                    {
                        BaseRasch.PlusK++;
                        continue;
                    }
                }

                BaseRasch.GetCalcTimes(ref T);
                BaseRasch.GetCalcP(ref P);
                BaseRasch.GetCalcTemper(ref Temper);
                BaseRasch.GetDlinTrech(ref DlinTrech);
                BaseRasch.GetShirTrech(ref ShirTrech);
                BaseRasch.Get1CoorGaz(ref Coord1Gaz);
                BaseRasch.Get2CoorGaz(ref Coord2Gaz);
                BaseRasch.GetDavlOfWell(ref Davl);
                BaseRasch.GetCoordVoda(ref Voda);
                BaseRasch.GetWellCoord(ref WellData);

                //GetDataToTables();
                InsertDataToCharts();

                //TabSheet1->Enabled = false;
                //Excel1->Enabled = true;
                CalculationResult.Visibility = Visibility.Visible;

                DataBaseController.SaveCalculationToArchive(BaseCalcParam);
                NameCalculation.Header = BaseCalcParam.CalculationName;
            }
        }

        /// <summary>
        /// Контроль заполнения всех полей.
        /// </summary>
        /// <returns>Результат проверки</returns>
        private bool CheckFields()
        {
            bool result = true;

            //проверка всех текстбоксов на экране ввода общей информации!
            foreach (System.Windows.UIElement item in DataPage.MainGrid.Children)
            {
                if ((item as TextBox) != null)
                {
                    if ((item as TextBox).Text == "")
                    {
                        result = false;
                        (item as TextBox).BorderBrush = System.Windows.Media.Brushes.Red;
                    }
                }
            }

            if (DataPage.TypeFluid.Text == "")
            {
                result = false;
                DataPage.TypeFluid.BorderBrush = System.Windows.Media.Brushes.Red;
            }

            //проверка всех на экране выбора зарядов!
            foreach (System.Windows.UIElement item in ChargeSelection.MainGrid.Children)
            {
                if ((item as TextBox) != null)
                {
                    if ((item as TextBox).Text == "")
                    {
                        result = false;
                        (item as TextBox).BorderBrush = System.Windows.Media.Brushes.Red;
                    }
                }
                if ((item as ComboBox) != null)
                {
                    if ((item as ComboBox).SelectedItem == null)
                    {
                        result = false;
                        (item as ComboBox).BorderBrush = System.Windows.Media.Brushes.Red;
                    }
                }
            }

            if (result == false)
            {
                MessageBox.Show("Заполните все поля!");
            }

            return result;
        }

        /// <summary>
        /// Контроль ввода числовых значений в поля, где необходимы числовые значения
        /// </summary>
        private bool ControlInputNumericalValues()
        {
            bool result = true;
            try
            {
                Convert.ToDouble(DataPage.SlaughterCurrent.Text);
            }
            catch
            {
                DataPage.SlaughterCurrent.BorderBrush = System.Windows.Media.Brushes.Red;
                result = false;
            }
            try
            {
                Convert.ToDouble(DataPage.CasingDiameter.Text);
            }
            catch
            {
                DataPage.CasingDiameter.BorderBrush = System.Windows.Media.Brushes.Red;
                result = false;
            }
            try
            {
                Convert.ToDouble(DataPage.CasingThickness.Text);
            }
            catch
            {
                DataPage.CasingThickness.BorderBrush = System.Windows.Media.Brushes.Red;
                result = false;
            }
            try
            {
                Convert.ToDouble(DataPage.FluidLevel.Text);
            }
            catch
            {
                DataPage.FluidLevel.BorderBrush = System.Windows.Media.Brushes.Red;
                result = false;
            }
            try
            {
                Convert.ToDouble(DataPage.FluidDensity.Text);
            }
            catch
            {
                DataPage.FluidDensity.BorderBrush = System.Windows.Media.Brushes.Red;
                result = false;
            }
            try
            {
                Convert.ToDouble(DataPage.PunchIntervalPower.Text);
            }
            catch
            {
                DataPage.PunchIntervalPower.BorderBrush = System.Windows.Media.Brushes.Red;
                result = false;
            }
            try
            {
                Convert.ToDouble(DataPage.SolePerforationInterval.Text);
            }
            catch
            {
                DataPage.SolePerforationInterval.BorderBrush = System.Windows.Media.Brushes.Red;
                result = false;
            }
            try
            {
                Convert.ToInt32(DataPage.PerforationDensity.Text);
            }
            catch
            {
                DataPage.PerforationDensity.BorderBrush = System.Windows.Media.Brushes.Red;
                result = false;
            }
            try
            {
                Convert.ToInt32(ChargeSelection.MainCount.Text);
            }
            catch
            {
                ChargeSelection.MainCount.BorderBrush = System.Windows.Media.Brushes.Red;
                result = false;
            }
            try
            {
                Convert.ToInt32(ChargeSelection.ActiveCount.Text);
            }
            catch
            {
                ChargeSelection.ActiveCount.BorderBrush = System.Windows.Media.Brushes.Red;
                result = false;
            }
            try
            {
                Convert.ToDouble(DataPage.GeneratorDepth.Text);
            }
            catch
            {
                DataPage.GeneratorDepth.BorderBrush = System.Windows.Media.Brushes.Red;
                result = false;
            }
            try
            {
                Convert.ToDouble(DataPage.ReservoirPressure.Text); // давление в Мп
            }
            catch
            {
                DataPage.ReservoirPressure.BorderBrush = System.Windows.Media.Brushes.Red;
                result = false;
            }
            try
            {
                Convert.ToDouble(DataPage.ReservoirTemperature.Text);
            }
            catch
            {
                DataPage.ReservoirTemperature.BorderBrush = System.Windows.Media.Brushes.Red;
                result = false;
            }
            try
            {
                Convert.ToDouble(DataPage.YoungModulus.Text);
            }
            catch
            {
                DataPage.YoungModulus.BorderBrush = System.Windows.Media.Brushes.Red;
                result = false;
            }
            try
            {
                Convert.ToDouble(DataPage.PoissonRatio.Text);
            }
            catch
            {
                DataPage.PoissonRatio.BorderBrush = System.Windows.Media.Brushes.Red;
                result = false;
            }
            try
            {
                Convert.ToDouble(ChargeSelection.SimulationDuration.Text);
            }
            catch
            {
                ChargeSelection.SimulationDuration.BorderBrush = System.Windows.Media.Brushes.Red;
                result = false;
            }

            if (result == false)
            {
                MessageBox.Show("Проверьте ввод числовых данных!");
            }
            return result;
        }

        private bool ControlNonNegativityParameters()
        {
            bool result = true;
            if (Convert.ToDouble(DataPage.PunchIntervalPower.Text) < 0)
            {
                result = false;
                DataPage.PunchIntervalPower.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (Convert.ToDouble(DataPage.SlaughterCurrent.Text) < 0)
            {
                result = false;
                DataPage.SlaughterCurrent.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (Convert.ToDouble(DataPage.GeneratorDepth.Text) < 0)
            {
                result = false;
                DataPage.GeneratorDepth.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (Convert.ToDouble(DataPage.PerforationDensity.Text) < 0)
            {
                result = false;
                DataPage.PerforationDensity.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (Convert.ToDouble(DataPage.SolePerforationInterval.Text) < 0)
            {
                result = false;
                DataPage.SolePerforationInterval.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (Convert.ToDouble(DataPage.CasingDiameter.Text) < 0)
            {
                result = false;
                DataPage.CasingDiameter.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (Convert.ToDouble(DataPage.CasingThickness.Text) < 0)
            {
                result = false;
                DataPage.CasingThickness.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (Convert.ToDouble(DataPage.ReservoirPressure.Text) < 0)
            {
                result = false;
                DataPage.ReservoirPressure.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (Convert.ToDouble(DataPage.ReservoirTemperature.Text) < 0)
            {
                result = false;
                DataPage.ReservoirTemperature.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (Convert.ToDouble(DataPage.YoungModulus.Text) < 0)
            {
                result = false;
                DataPage.YoungModulus.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (Convert.ToDouble(DataPage.PoissonRatio.Text) < 0)
            {
                result = false;
                DataPage.PoissonRatio.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (Convert.ToDouble(DataPage.FluidLevel.Text) < 0)
            {
                result = false;
                DataPage.FluidLevel.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (Convert.ToDouble(DataPage.FluidDensity.Text) < 0)
            {
                result = false;
                DataPage.FluidDensity.BorderBrush = System.Windows.Media.Brushes.Red;
            }

            if(result == false)
            {
                MessageBox.Show("Проверьте данные на отрицательность!");
            }

            return result;
        }

        /// <summary>
        /// Задает стандартный цвет границ заполнения данных
        /// </summary>
        private void SetDefaultBorderOnDataPages()
        {
            foreach (System.Windows.UIElement item in DataPage.MainGrid.Children)
            {
                if ((item as TextBox) != null)
                {
                    (item as TextBox).BorderBrush = Brushes.LightGray;
                }
                if ((item as ComboBox) != null)
                {
                    (item as ComboBox).BorderBrush = Brushes.LightGray;
                }
            }
            foreach (System.Windows.UIElement item in ChargeSelection.MainGrid.Children)
            {
                if ((item as TextBox) != null)
                {
                    (item as TextBox).BorderBrush = Brushes.LightGray;
                }
                if ((item as ComboBox) != null)
                {
                    (item as ComboBox).BorderBrush = Brushes.LightGray;
                }
            }
        }



        private bool CorrectData()
        {
            if (!CheckFields())
            {
                return false;
            }

            if (!ControlInputNumericalValues())
            {
                return false;
            }

            if (!ControlNonNegativityParameters())
            {
                return false;
            }

            SetDefaultBorderOnDataPages();

            try
            {
                BaseCalcParam.Date = DataPage.CalculationDate.SelectedDate.Value;
                BaseCalcParam.CalculationName = DataPage.CalculationName.Text;
                BaseCalcParam.CompanyName = DataPage.CompanyName.Text;
                BaseCalcParam.CalculationExecutor = DataPage.CalculationExecutor.Text;
                BaseCalcParam.MadeFor = DataPage.MadeFor.Text;
                BaseCalcParam.BushNumber = DataPage.BushNumber.Text;
                BaseCalcParam.TypeFluid = DataPage.TypeFluid.Text;

                BaseCalcParam.NameMestor = DataPage.FieldName.Text;
                BaseCalcParam.NameWell = DataPage.WellNumber.Text;
                BaseCalcParam.Zaboy = Convert.ToDouble(DataPage.SlaughterCurrent.Text);
                BaseCalcParam.CasingDiameter = Convert.ToDouble(DataPage.CasingDiameter.Text);
                BaseCalcParam.CasingThickness = Convert.ToDouble(DataPage.CasingThickness.Text);
                //BaseCalcParam.Dvn = Convert.ToDouble(DataPage.CasingDiameter.Text)
                //    - (Convert.ToDouble(DataPage.CasingThickness.Text) * 2);
                BaseCalcParam.GlubVoda = Convert.ToDouble(DataPage.FluidLevel.Text);
                BaseCalcParam.DensVoda = Convert.ToDouble(DataPage.FluidDensity.Text);
                BaseCalcParam.HPerf = Convert.ToDouble(DataPage.PunchIntervalPower.Text);
                BaseCalcParam.PodIntPerf = Convert.ToDouble(DataPage.SolePerforationInterval.Text);

                BaseCalcParam.DensPerf = Convert.ToInt32(DataPage.PerforationDensity.Text);
                BaseCalcParam.CountOsnZarad = Convert.ToInt32(ChargeSelection.ActiveCount.Text);
                BaseCalcParam.CountVospZarad = Convert.ToInt32(ChargeSelection.MainCount.Text);
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

                BaseCalcParam.osnZar = ChargeSelection.MainСharge.SelectedItem as CZarad;
                BaseCalcParam.osnZar.Poroh = ChargeSelection.MainСhargeType.SelectedItem as CPoroh;


                BaseCalcParam.NameVospZarad = (ChargeSelection.ActiveСharge.SelectedItem as CZarad).Name;
                if (BaseCalcParam.NameVospZarad == "")
                {
                    MessageBox.Show("Выберите активные заряды");
                    throw (new IntException(0));
                    return false;
                }

                BaseCalcParam.vospZar = ChargeSelection.ActiveСharge.SelectedItem as CZarad;
                BaseCalcParam.vospZar.Poroh = ChargeSelection.ActiveСhargeType.SelectedItem as CPoroh;

                BaseCalcParam.TimeInterval = Convert.ToDouble(ChargeSelection.SimulationDuration.Text);

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

        private void InsertDataToCharts()
        {
            InsertDataToChart(CombustionPressure.Chart, T, P);
            InsertDataToChart(TemperatureCombustion.Chart, T, Temper);
        }

        private void InsertDataToChart(PlotView plot, List<double> data, List<double> data1, List<double> data2 = null)
        {
            double x;
            double y;
            double z;

            //List<DataPoint> points = new List<DataPoint>();

            //List<DataPoint> points1 = new List<DataPoint>();

            OxyPlot.Series.LineSeries ls = new OxyPlot.Series.LineSeries();
            OxyPlot.Series.LineSeries ls1 = new OxyPlot.Series.LineSeries();

            PlotModel model = new PlotModel();//???????
            if (data2 != null)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    ls.Points.Add(new DataPoint(data[i], data1[i]));
                    if (data2 != null)
                    {
                        z = data2[i];
                        ls1.Points.Add(new DataPoint(data[i], data2[i]));
                    }
                }
                model.Series.Add(ls);
                model.Series.Add(ls1);
            }
            else
            {
                for (int i = 0; i < data.Count; i++)
                {
                    ls.Points.Add(new DataPoint(data[i], data1[i]));
                }
                model.Series.Add(ls);
            }

            ClearChart(plot);

            plot.Model = model;
        }

        private void TreeViewItem_Selected_All_Porohs(object sender, RoutedEventArgs e)
        {
            ViewPage(AllPoroh);
        }

        private void ClearChart(PlotView plot)
        {
            plot.Model = new PlotModel();
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void TreeViewItem_Selected_Archive(object sender, RoutedEventArgs e)
        {
            Archive.UpdateGrid();
            ViewPage(Archive);
        }

        private void TreeViewItem_Selected_Temperature_Combustion(object sender, RoutedEventArgs e)
        {
            ViewPage(TemperatureCombustion);
        }

        //private void GetDataToTables()
        //{
        //    int imin, imax;
        //    CombustionPressure.Distance.Text = BaseCalcParam.dHFromGenToMan.ToString();

        //    //по давлению
        //    double gd;
        //    double a1, a2;
        //    InsertDataToTable(Form2->StringGrid1, ref T, &P, 2, "Время, сек", "P, атм", &imin, &imax);
        //    Form2->Label4->Caption = "Максимальное давление = " + RoundS(P[imax], 2) + "атм";
        //    Form2->Label5->Caption = "Минимальние давление = " + RoundS(P[imin], 2) + "атм";
        //    gd = BaseRasch->Hidrost;
        //    Form2->Label6->Caption = "Гидростатическое давление = " + RoundS(gd / 100000.0, 2) + "атм";
        //    Form2->Label8->Caption = "Глубина депрессионной воронки = " + RoundS(gd / 100000.0 - P[imin], 2) + "атм";
        //    gd = BaseRasch->gorn;
        //    Form2->Label7->Caption = "Горное давление = " + RoundS(gd / 100000.0, 2) + "атм";

        //    InsertDataToTable(Form2->StringGrid2, &T, &Temper, 2, "Время, сек", "T, C", &imin, &imax);
        //    Form2->Label14->Caption = "Максимальная температура = " + RoundS(Temper[imax], 2) + " C";
        //    Form2->Label15->Caption = "Минимальная температура = " + RoundS(Temper[imin], 2) + " C";

        //    InsertDataToTable(Form2->StringGrid4, &T, &DlinTrech, 2, "Время, сек", "L, м", &imin, &imax);
        //    Form2->Label18->Caption = "Максимальная длина = " + RoundS(DlinTrech[imax], 2) + " м";
        //    Form2->Label19->Caption = "Минимальная длина = " + RoundS(DlinTrech[imin], 2) + " м";

        //    InsertDataToTable(Form2->StringGrid5, &T, &ShirTrech, 2, "Время, сек", "W, мм", &imin, &imax);
        //    Form2->Label21->Caption = "Максимальная ширина = " + RoundS(ShirTrech[imax], 2) + " мм";
        //    Form2->Label20->Caption = "Минимальная ширина = " + RoundS(ShirTrech[imin], 2) + " мм";

        //    InsertDataToTable(Form2->StringGrid6, &T, &Coord1Gaz, 3, "Время, сек", "X1, м", &imin, &imax, &Coord2Gaz, "X2, м");
        //    Form2->Label32->Caption = "Минимальная глубина = " + RoundS(Coord2Gaz[imin], 2) + " м";
        //    Form2->Label33->Caption = "Максимальная глубина = " + RoundS(Coord1Gaz[imax], 2) + " м";
        //    Form2->Label34->Caption = "Максимальная амплитуда = " + RoundS(Coord1Gaz[imax] - Coord2Gaz[imin], 2) + " м";

        //    InsertDataToTable(Form2->StringGrid7, &WellData, &Davl, 2, "Глубина, м", "Давление, атм", &imin, &imax);
        //    Form2->Label27->Caption = "Максимальное давление = " + RoundS(Davl[imax], 2) + " атм";
        //    Form2->Label26->Caption = "Минимальное давление = " + RoundS(Davl[imin], 2) + " атм";

        //    InsertDataToTable(Form2->StringGrid8, &T, &Voda, 2, "Время, сек", "X, м", &imin, &imax);
        //    Form2->Label31->Caption = "Максимальное значение = " + RoundS(Voda[imax], 2) + " м";
        //    Form2->Label30->Caption = "Минимальное значение = " + RoundS(Voda[imin], 2) + " м";
        //    Form2->Label38->Caption = "Амплитуда = " + RoundS(Voda[imax] - Voda[imin], 2) + " м";

        //}

        //void ClearRezSG(TStringGrid* sg, string s1, string s2, string s3)
        //{
        //    int cont = 4;
        //    if (s3 == "") cont = 3;

        //    for (int i = 0; i < cont; i++)
        //        sg->Cols[i]->Clear();

        //    sg->RowCount = 2;
        //    sg->Cells[0][0] = "№";
        //    sg->Cells[1][0] = s1;
        //    sg->Cells[2][0] = s2;
        //    if (cont == 4) sg->Cells[3][0] = s3;
        //}

        //     void InsertDataToTable(ref string sg, ref List<double> data, ref List<double> data1,
        //     int cnt, string s, string s1, ref int indMin,ref int indMax, ref List<double> data2, string s2)
        //     {
        //         //ClearRezSG(sg, s, s1, s2);
        //         int rz;
        //         float min = 99999999;
        //         float max = -9999999;
        //         int imin, imax;
        //         rz = data.Count();
        //         for (int i = 0; i < rz; i++)
        //         {
        //             sg->Cells[0][i + 1] = IntToStr(i + 1);
        //             sg->Cells[1][i + 1] = RoundS(data->operator [](i),5);
        //         sg->Cells[2][i + 1] = RoundS(data1->operator [](i),2);
        //         if (cnt == 3) sg->Cells[3][i + 1] = RoundS(data2->operator [](i),2);

        //         if (cnt == 2)
        //         {
        //             if (min > data1->operator [] (i)) {min=data1->operator [] (i); imin=i;}
        //		if (max<data1->operator [](i)) {max=data1->operator [](i); imax=i;}
        // }
        // else
        // {
        //	 if (min>data2->operator [](i)) {min=data2->operator [](i); imin=i;}
        //	 if (max<data1->operator [](i)) {max=data1->operator [](i); imax=i;}
        // }
        // sg->RowCount+=1;
        //}
    }
}
