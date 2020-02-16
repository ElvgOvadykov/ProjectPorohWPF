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
        double TimeInterval;
        CLOADPARAMS BaseCalcParam;
        CBase BaseRasch;
        CZarad osnZar, vospZar;
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

        private void LoadAllCharges()
        {
            List<CZarad> zarads = new List<CZarad>(DataBaseController.GetZarads());
            DataTable dt = new DataTable();
            dt.Columns.Add("№");
            dt.Columns.Add("Наименование");
            dt.Columns.Add("Внешний диаметр, мм");
            dt.Columns.Add("Внутренний диаметр, мм");
            dt.Columns.Add("Длина заряда, мм");
            dt.Columns.Add("Тип пороховой смеси");
            foreach (var item in zarads)
            {
                dt.Rows.Add(item.ID, item.Name, item.Dnar, item.Dvnutr, item.L, item.Poroh);
            }
            AllCharges.ChargesDataGrid.ItemsSource = dt.DefaultView;
        }

        private void LoadAllPorohs()
        {
            List<CPoroh> porohs = new List<CPoroh>(DataBaseController.GetPorohs());
            DataTable dt = new DataTable();
            dt.Columns.Add(@"№");
            dt.Columns.Add(@"Наименование");
            dt.Columns.Add(@"Сила пороха, Джкг");
            dt.Columns.Add(@"Расчетная температура горения, С");
            dt.Columns.Add(@"Удельная газопроизводительность, лкг");
            dt.Columns.Add(@"Плотность, гсм3");
            foreach (var item in porohs)
            {
                dt.Rows.Add(item.ID, item.Name, item.Power, item.Temper, item.UdGaz, item.Dens);
            }
            //MessageBox.Show(dt.Rows[0].ItemArray[4].ToString());
            AllPoroh.PorohsDataGrid.ItemsSource = dt.DefaultView;
        }

        private void LoadChargesCombobox()
        {
            ChargeSelection.MainСharge.Items.Clear();
            ChargeSelection.ActiveСharge.Items.Clear();
            ChargeSelection.ActiveСhargeType.Items.Clear();
            ChargeSelection.MainСhargeType.Items.Clear();
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

            if (CorrectData())
            {
                BaseRasch = new CBase();
                ClearArray();
                ClearCharts();

                BaseRasch.SetZarad(ref osnZar, ref vospZar);
                BaseRasch.LoadBaseParams(ref BaseCalcParam);
                BaseRasch.SetCountCalcPoint(1000);
                BaseRasch.SetCalcInterval(TimeInterval);// Магическое число

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
            }
            else
            {
                MessageBox.Show("Проверьте введенные да");
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
                    - (Convert.ToDouble(DataPage.CasingThickness.Text) * 2);
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

                TimeInterval = Convert.ToDouble(DataPage.SimulationDuration.Text);

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
            if(data2 != null)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    ls.Points.Add(new DataPoint(data[i], data1[i]));
                    if (data2 != null)
                    {
                        z = data2[i];
                        ls1.Points.Add(new DataPoint(data[i],data2[i]));
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
            LoadAllPorohs();
            ViewPage(AllPoroh);
        }

        private void ClearChart(PlotView plot)
        {
            plot.Model = new PlotModel();
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
