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
    /// Логика взаимодействия для InitialDataPage.xaml
    /// </summary>
    public partial class InitialDataPage : UserControl
    {
        public InitialDataPage()
        {
            InitializeComponent();
            Test();
        }

        void Test()
        {
            CalculationDate.SelectedDate = DateTime.Now.Date;
            CalculationName.Text = "1 расчет";
            CompanyName.Text = "ентертеймент";
            CalculationExecutor.Text = "Пупкин";
            MadeFor.Text = "assd";
            FieldName.Text = "Восточно-сургутское";
            BushNumber.Text = "111";
            WellNumber.Text = "8070";
            SlaughterCurrent.Text = "3300";
            PunchIntervalPower.Text = "19,2";
            SolePerforationInterval.Text = "3286";
            GeneratorDepth.Text = "6867";
            PerforationDensity.Text = "20";
            CasingDiameter.Text = "150";
            CasingThickness.Text = "10";
            ReservoirPressure.Text = "98,4";
            ReservoirTemperature.Text = "93";
            YoungModulus.Text = "3000";
            PoissonRatio.Text = "0,3";
            TypeFluid.Text = "Нефть";
            FluidLevel.Text = "50";
            FluidDensity.Text = "1,18";
            SimulationDuration.Text = "10";
        }
    }
}
