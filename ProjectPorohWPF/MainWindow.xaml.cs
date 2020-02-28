using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
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

        ReportClass report = new ReportClass();

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
            Pages.Add(CrackWidth);
            Pages.Add(BarrelPressureDistribution);
            Pages.Add(CrackLength);
            Pages.Add(GasAreaCoordinates);
            Pages.Add(UpperFluidBoundary);
            Archive.parent = this;
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
            AllCharges.UpdateGrid();
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
            if (CorrectData())
            {
                Calculation();
                NameCalculation.Header = BaseCalcParam.CalculationName;
                DataBaseController.SaveCalculationToArchive(BaseCalcParam);
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

            if(CombustionPressure.Distance.Text == "")
            {
                result = false;
                CombustionPressure.Distance.BorderBrush = System.Windows.Media.Brushes.Red;
            }

            if (BarrelPressureDistribution.TimeAfterExposure.Text == "")
            {
                result = false;
                BarrelPressureDistribution.TimeAfterExposure.BorderBrush = System.Windows.Media.Brushes.Red;
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
            try
            {
                Convert.ToDouble(CombustionPressure.Distance.Text);
            }
            catch
            {
                CombustionPressure.Distance.BorderBrush = System.Windows.Media.Brushes.Red;
                result = false;
            }
            try
            {
                Convert.ToDouble(BarrelPressureDistribution.TimeAfterExposure.Text);
            }
            catch
            {
                BarrelPressureDistribution.TimeAfterExposure.BorderBrush = System.Windows.Media.Brushes.Red;
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
            if(Convert.ToDouble(CombustionPressure.Distance.Text) < 0)
            {
                result = false;
                CombustionPressure.Distance.BorderBrush = Brushes.Red;
            }
            if(Convert.ToDouble(BarrelPressureDistribution.TimeAfterExposure.Text) < 0)
            {
                result = false;
                BarrelPressureDistribution.TimeAfterExposure.BorderBrush = Brushes.Red;
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
            CombustionPressure.Distance.BorderBrush = Brushes.LightGray;
            BarrelPressureDistribution.TimeAfterExposure.BorderBrush = Brushes.LightGray;
        }



        private bool CorrectData()
        {
            SetDefaultBorderOnDataPages();

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

        private void Calculation() 
        {
            report.SetLoadParams(BaseCalcParam);
            
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
                t2 = Convert.ToDouble(BarrelPressureDistribution.TimeAfterExposure.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Проверьте правильность введенных расстояния до генератора и времени после воздействия!");
                return;
            }

            if (t2 > BaseCalcParam.TimeInterval)
            {
                t2 = BaseCalcParam.TimeInterval;
                BarrelPressureDistribution.TimeAfterExposure.Text = Func.RoundS(t2, 2);
            }

            BaseRasch.SetdHFromGenToMan(t1);
            //BaseRasch.SetTPvdolWell(1); // Тестовое время воздействия 1 секунда
            BaseRasch.SetTPvdolWell(t2);

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

            GetDataToTables();
            InsertDataToCharts();

            //TabSheet1->Enabled = false;
            //Excel1->Enabled = true;
            CalculationResult.Visibility = Visibility.Visible;
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
            InsertDataToChart(CrackWidth.Chart, T, ShirTrech);
            InsertDataToChart(BarrelPressureDistribution.Chart, WellData, Davl);
            InsertDataToChart(CrackLength.Chart, T, DlinTrech);
            InsertDataToChart(GasAreaCoordinates.Chart, T, Coord1Gaz, Coord2Gaz);
            InsertDataToChart(UpperFluidBoundary.Chart, T, Voda);
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
            AllPoroh.UpdateGrid();
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

        private void TreeViewItem_Selected_Crack_Width(object sender, RoutedEventArgs e)
        {
            ViewPage(CrackWidth);
        }

        private void TreeViewItem_Selected_Barrel_Pressure_Distribution(object sender, RoutedEventArgs e)
        {
            ViewPage(BarrelPressureDistribution);
        }

        private void TreeViewItem_Selected_Crack_Length(object sender, RoutedEventArgs e)
        {
            ViewPage(CrackLength);
        }

        private void TreeViewItem_Selected_Gas_Area_Coordinates(object sender, RoutedEventArgs e)
        {
            ViewPage(GasAreaCoordinates);
        }

        private void TreeViewItem_Selected_Upper_Fluid_Boundary(object sender, RoutedEventArgs e)
        {
            ViewPage(UpperFluidBoundary);
        }

        internal void ViewCalculation(CLOADPARAMS calc)
        {
            DataPage.CalculationDate.SelectedDate = calc.Date;
            DataPage.CalculationName.Text = calc.CalculationName;
            DataPage.CompanyName.Text = calc.CompanyName;
            DataPage.CalculationExecutor.Text = calc.CalculationExecutor;
            DataPage.MadeFor.Text = calc.MadeFor;
            DataPage.FieldName.Text = calc.NameMestor;
            DataPage.BushNumber.Text = calc.BushNumber;
            DataPage.WellNumber.Text = calc.NameWell;
            DataPage.SlaughterCurrent.Text = calc.Zaboy.ToString();
            DataPage.PunchIntervalPower.Text = calc.HPerf.ToString();
            DataPage.SolePerforationInterval.Text = calc.PodIntPerf.ToString();
            DataPage.GeneratorDepth.Text = calc.GlubGen.ToString();
            DataPage.PerforationDensity.Text = calc.DensPerf.ToString();
            DataPage.CasingDiameter.Text = calc.CasingDiameter.ToString();
            DataPage.CasingThickness.Text = calc.CasingThickness.ToString();
            DataPage.ReservoirPressure.Text = calc.Pplast.ToString();
            DataPage.ReservoirTemperature.Text = calc.Tplast.ToString();
            DataPage.YoungModulus.Text = calc.ModUnga.ToString();
            DataPage.PoissonRatio.Text = calc.KPuass.ToString();
            DataPage.TypeFluid.Text = calc.TypeFluid;
            DataPage.FluidLevel.Text = calc.GlubVoda.ToString();
            DataPage.FluidDensity.Text = calc.DensVoda.ToString();

            ChargeSelection.SimulationDuration.Text = calc.TimeInterval.ToString();

            bool haveOsnPoroh = false;
            bool haveOsnZarad = false;

            List<CPoroh> porohs = new List<CPoroh>(DataBaseController.GetPorohs());
            List<CZarad> zarads = new List<CZarad>(DataBaseController.GetZarads());

            foreach(var item in porohs)
            {
                if (calc.osnZar.Poroh.Equals(item))
                {
                    calc.osnZar.Poroh = item;
                    haveOsnPoroh = true;
                }
            }

            if (!haveOsnPoroh)
            {
                calc.osnZar.Poroh = DataBaseController.AddPoroh(calc.osnZar.Poroh);
            }

            foreach (var item in zarads)
            {
                if (calc.osnZar.Equals(item))
                {
                    if(item.Poroh == null)
                        item.Poroh = calc.osnZar.Poroh;
                    calc.osnZar.ID = item.ID;
                    haveOsnZarad = true;
                }
            }

            if (!haveOsnZarad)
            {
                calc.osnZar = DataBaseController.AddCharge(calc.osnZar);
            }
            else
            {
                DataBaseController.UpdateAllCharges(zarads);
            }

            porohs = new List<CPoroh>(DataBaseController.GetPorohs());
            zarads = new List<CZarad>(DataBaseController.GetZarads());

            bool haveVospPoroh = false;
            bool haveVospZarad = false;

            foreach (var item in porohs)
            {
                if (calc.vospZar.Poroh.Equals(item))
                {
                    calc.vospZar.Poroh = item;
                    haveVospPoroh = true;
                }
            }

            if (!haveVospPoroh)
            {
                calc.vospZar.Poroh = DataBaseController.AddPoroh(calc.vospZar.Poroh);
            }

            foreach (var item in zarads)
            {
                if (calc.vospZar.Equals(item))
                {
                    if (item.Poroh == null)
                        item.Poroh = calc.vospZar.Poroh;
                    calc.vospZar.ID = item.ID;
                    haveVospZarad = true;
                }
            }

            if (!haveVospZarad)
            {
                calc.vospZar = DataBaseController.AddCharge(calc.vospZar);
            }

            ChargeSelection.UpdateComboBox();

            ChargeSelection.MainСharge.SelectedItem = calc.osnZar;
            ChargeSelection.MainСhargeType.SelectedItem = calc.osnZar.Poroh;
            ChargeSelection.MainCount.Text = calc.CountOsnZarad.ToString();

            ChargeSelection.ActiveСharge.SelectedItem = calc.vospZar;
            ChargeSelection.ActiveСhargeType.SelectedItem = calc.vospZar.Poroh;
            ChargeSelection.ActiveCount.Text = calc.CountVospZarad.ToString();

            CombustionPressure.Distance.Text = calc.dHFromGenToMan.ToString();
            BarrelPressureDistribution.TimeAfterExposure.Text = calc.TPvdolWell.ToString();

            if (CorrectData())
            {
                Calculation();
                NameCalculation.Header = BaseCalcParam.CalculationName;
            }

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Click_New_Calculation(object sender, RoutedEventArgs e)
        {
            DataPage.CalculationDate.SelectedDate = DateTime.Now;
            DataPage.CalculationName.Text = "";
            DataPage.CompanyName.Text = "";
            DataPage.CalculationExecutor.Text = "";
            DataPage.MadeFor.Text = "";
            DataPage.FieldName.Text = "";
            DataPage.BushNumber.Text = "";
            DataPage.WellNumber.Text = "";
            DataPage.SlaughterCurrent.Text = "";
            DataPage.PunchIntervalPower.Text = "";
            DataPage.SolePerforationInterval.Text = "";
            DataPage.GeneratorDepth.Text = "";
            DataPage.PerforationDensity.Text = "";
            DataPage.CasingDiameter.Text = "";
            DataPage.CasingThickness.Text = "";
            DataPage.ReservoirPressure.Text = "";
            DataPage.ReservoirTemperature.Text = "";
            DataPage.YoungModulus.Text = "";
            DataPage.PoissonRatio.Text = "";
            DataPage.TypeFluid.SelectedIndex = 0;
            DataPage.FluidLevel.Text = "";
            DataPage.FluidDensity.Text = "";

            ChargeSelection.SimulationDuration.Text = "10";

            ChargeSelection.MainСharge.SelectedItem = null;
            ChargeSelection.MainСhargeType.SelectedItem = null;
            ChargeSelection.MainCount.Text = "";

            ChargeSelection.ActiveСharge.SelectedItem = null;
            ChargeSelection.ActiveСhargeType.SelectedItem = null;
            ChargeSelection.ActiveCount.Text = "";

            CombustionPressure.Distance.Text = "30";
            BarrelPressureDistribution.TimeAfterExposure.Text = "1";

            NameCalculation.Header = "Расчет";

            CalculationResult.Visibility = Visibility.Collapsed;
        }

        private void GetDataToTables()
        {
            int imin = 0, imax = 0;
            CombustionPressure.Distance.Text = BaseCalcParam.dHFromGenToMan.ToString();

            //по давлению
            double gd;
            double a1, a2;
            InsertDataToTable(ref CombustionPressure.data, T,  P, 2, "Время, сек", "P, атм",ref imin, ref imax);
            CombustionPressure.MaxPressure.Text = Func.RoundS(P.Max(x=>x), 2) + "атм";
            CombustionPressure.MinPressure.Text = Func.RoundS(P.Min(x=>x), 2) + "атм";
            gd = BaseRasch.Hidrost;
            CombustionPressure.HydrostaticPressure.Text = Func.RoundS(gd / 100000.0, 2) + "атм";
            CombustionPressure.DepressionDepth.Text = Func.RoundS(gd / 100000.0 - P.Min(x=>x), 2) + "атм";
            gd = BaseRasch.gorn;
            CombustionPressure.RockPressure.Text = Func.RoundS(gd / 100000.0, 2) + "атм";

            InsertDataToTable(ref TemperatureCombustion.data,T, Temper, 2, "Время, сек", "T, C", ref imin,ref imax);
            TemperatureCombustion.MaxTemperature.Text = Func.RoundS(Temper.Max(x=>x), 2) + " C";
            TemperatureCombustion.MinTemperature.Text = Func.RoundS(Temper.Min(x=>x), 2) + " C";

            InsertDataToTable(ref CrackLength.data,T, DlinTrech, 2, "Время, сек", "L, м", ref imin, ref imax);
            CrackLength.MaxLength.Text = Func.RoundS(DlinTrech.Max(x=>x), 2) + " м";
            //CrackLength.MinLength.Text = Func.RoundS(DlinTrech.Min(x=>x), 2) + " м";

            InsertDataToTable(ref CrackWidth.data, T, ShirTrech, 2, "Время, сек", "W, мм", ref imin, ref imax);
            CrackWidth.MaxWidth.Text = Func.RoundS(ShirTrech.Max(x=>x), 2) + " мм";
            //CrackWidth.MinWidth.Text = Func.RoundS(ShirTrech.Min(x=>x), 2) + " мм";

            InsertDataToTable(ref GasAreaCoordinates.data,T,Coord1Gaz, 3, "Время, сек", "X1, м",ref imin,ref imax, Coord2Gaz, "X2, м");
            GasAreaCoordinates.MinDepth.Text = Func.RoundS(Coord2Gaz.Min(x=>x), 2) + " м";
            GasAreaCoordinates.MaxDepth.Text = Func.RoundS(Coord1Gaz.Max(x=>x), 2) + " м";
            GasAreaCoordinates.MaxAmplitude.Text = Func.RoundS(Coord1Gaz.Max(x=>x) - Coord2Gaz.Min(x=>x), 2) + " м";

            InsertDataToTable(ref BarrelPressureDistribution.data, WellData, Davl, 2, "Глубина, м", "Давление, атм", ref imin, ref imax);
            BarrelPressureDistribution.MaxPressure.Text = Func.RoundS(Davl.Max(x=>x), 2) + " атм";
            BarrelPressureDistribution.MinPressure.Text = Func.RoundS(Davl.Min(x=>x), 2) + " атм";

            InsertDataToTable(ref UpperFluidBoundary.data,T,Voda, 2, "Время, сек", "X, м", ref imin,ref imax);
            UpperFluidBoundary.MaxValue.Text = Func.RoundS(Voda.Max(x=>x), 2) + " м";
            UpperFluidBoundary.MinValue.Text = Func.RoundS(Voda.Min(x=>x), 2) + " м";
            UpperFluidBoundary.Amplitude.Text = Func.RoundS(Voda.Max(x=>x) - Voda.Min(x=>x), 2) + " м";

        }

        void InsertDataToTable(ref DataGrid dg, List<double> data, List<double> data1,
        int cnt, string s, string s1, ref int indMin, ref int indMax, List<double> data2 = null, string s2 = null)
        {
            dg.ItemsSource = null;
            dg.Items.Clear();
            dg.Columns.Clear();
            int rz;
            float min = 99999999;
            float max = -9999999;
            int imin, imax;
            rz = data.Count();
            DataTable dt = new DataTable();

            if (data2 == null && s2 == null)
            {
                dt.Columns.Add("№");
                dt.Columns.Add(s);
                dt.Columns.Add(s1);
            }
            else
            {
                dt.Columns.Add("№");
                dt.Columns.Add(s);
                dt.Columns.Add(s1);
                dt.Columns.Add(s2);
            }

            for (int i = 0; i < rz; i++)
            {
                if(data2 == null&&s2 == null)
                {
                    dt.Rows.Add(i, data[i], data1[i]);
                }
                else
                {
                    dt.Rows.Add(i, data[i], data1[i], data2[i]);
                }
            }

            dg.ItemsSource = dt.DefaultView;
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                report.CreateReport("");
            }    
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
