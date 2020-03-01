using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.DocumentObjectModel.Shapes.Charts;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using OxyPlot;
using OxyPlot.Wpf;

namespace ProjectPorohWPF
{
    class ReportClass
    {
        bool isLoadParamsReady = false;
        bool isCombustionPressureReady = false;
        bool isTemperatureCombustionReady = false;
        bool isCrackLengthReady = false;
        bool isCrackWidthReady = false;
        bool isBarrelPressureDistributionReady = false;
        bool isGasAreaCoordinatesReady = false;
        bool isUpperFluidBoundaryReady = false;
        private CLOADPARAMS LOADPARAMS;

        private List<Tuple<string, string>> CombustionPressure_Parametrs;
        private PlotModel CombustionPressure_Model;
        private List<Tuple<string, string>> TemperatureCombustion_Parametrs;
        private PlotModel TemperatureCombustion_Model;
        private List<Tuple<string, string>> CrackWidth_Parametrs;
        private PlotModel CrackWidth_Model;
        private List<Tuple<string, string>> BarrelPressureDistribution_Parametrs;
        private PlotModel BarrelPressureDistribution_Model;
        private List<Tuple<string, string>> CrackLength_Parametrs;
        private PlotModel CrackLength_Model;
        private List<Tuple<string, string>> GasAreaCoordinates_Parametrs;
        private PlotModel GasAreaCoordinates_Model;
        private List<Tuple<string, string>> UpperFluidBoundary_Parametrs;
        private PlotModel UpperFluidBoundary_Model;

        List<string> TempFile = new List<string>();

        Document document;
        public void SetLoadParams(CLOADPARAMS loadparams)
        {
            LOADPARAMS = loadparams;
            isLoadParamsReady = true;
        }

        private bool IsReady()
        {
            if (isLoadParamsReady 
                && isCombustionPressureReady 
                && isTemperatureCombustionReady 
                && isCrackLengthReady 
                && isCrackWidthReady
                && isBarrelPressureDistributionReady
                && isGasAreaCoordinatesReady
                && isUpperFluidBoundaryReady)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetTemperatureCombustionReady(PlotModel model, List<Tuple<string, string>> parametrs)
        {
            TemperatureCombustion_Model = model;
            TemperatureCombustion_Parametrs = parametrs;
            isTemperatureCombustionReady = true;
        }

        public void SetCombustionPressureReady(PlotModel model, List<Tuple<string, string>> parametrs)
        {
            CombustionPressure_Model = model;
            CombustionPressure_Parametrs = parametrs;
            isCombustionPressureReady = true;
        }

        public void SetCrackLengthReady(PlotModel model, List<Tuple<string, string>> parametrs)
        {
            CrackLength_Model = model;
            CrackLength_Parametrs = parametrs;
            isCrackLengthReady = true;
        }

        public void SetCrackWidthReady(PlotModel model, List<Tuple<string, string>> parametrs)
        {
            CrackWidth_Model = model;
            CrackWidth_Parametrs = parametrs;
            isCrackWidthReady = true;
        }

        public void SetBarrelPressureDistributionReady(PlotModel model, List<Tuple<string, string>> parametrs)
        {
            BarrelPressureDistribution_Model = model;
            BarrelPressureDistribution_Parametrs = parametrs;
            isBarrelPressureDistributionReady = true;
        }

        public void SetGasAreaCoordinatesReady(PlotModel model, List<Tuple<string, string>> parametrs)
        {
            GasAreaCoordinates_Model = model;
            GasAreaCoordinates_Parametrs = parametrs;
            isGasAreaCoordinatesReady = true;
        }

        public void SetUpperFluidBoundaryReady(PlotModel model, List<Tuple<string, string>> parametrs)
        {
            UpperFluidBoundary_Model = model;
            UpperFluidBoundary_Parametrs = parametrs;
            isUpperFluidBoundaryReady = true;
        }

        public void CreateReport(string path)
        {
            if (IsReady())
            {
                SetPageSetupHeadersFooters();
                SetMainInformation();
                SetChartPage(CombustionPressure_Model, CombustionPressure_Parametrs);
                SetChartPage(TemperatureCombustion_Model, TemperatureCombustion_Parametrs);
                SetChartPage(CrackLength_Model, CrackLength_Parametrs);
                SetChartPage(CrackWidth_Model, CrackWidth_Parametrs);
                SetChartPage(BarrelPressureDistribution_Model, BarrelPressureDistribution_Parametrs);
                SetChartPage(GasAreaCoordinates_Model, GasAreaCoordinates_Parametrs);
                SetChartPage(UpperFluidBoundary_Model, UpperFluidBoundary_Parametrs);
                RenderPdf(path);
                DeleteTempFile();
            }
            else
                throw new Exception("Выполните расчет!");
        }

        private void SetPageSetupHeadersFooters()
        {
            document = new Document();
            Section section = document.AddSection();
            section.PageSetup.PageFormat = PageFormat.A4;//стандартный размер страницы
            section.PageSetup.Orientation = Orientation.Landscape;//ориентация
            section.PageSetup.FooterDistance = 30;
            section.PageSetup.TopMargin = 100;
            section.PageSetup.StartingNumber = 1;

            //шапка 
            Table Shapka = section.Headers.Primary.AddTable();
            Shapka.AddColumn(Unit.FromCentimeter(3.5));
            Shapka.AddColumn(Unit.FromCentimeter(18));
            Shapka.AddColumn(Unit.FromCentimeter(3.5));
            Shapka.AddRow();
            Shapka.Rows[0].Cells[0].AddImage("icon.jpg");
            Paragraph programmName = Shapka.Rows[0].Cells[1].AddParagraph("Расчет параметров порохового воздействия");
            programmName.Format.Alignment = ParagraphAlignment.Center;
            programmName.Format.Font.Size = 11;
            Shapka.Rows[0].Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
            programmName = Shapka.Rows[0].Cells[2].AddParagraph();
            programmName.AddImage("icon.jpg");
            programmName.Format.Alignment = ParagraphAlignment.Right;
            Shapka.AddRow();
            Paragraph paragraph = Shapka.Rows[1].Cells[0].AddParagraph();
            paragraph.Format.Font.Color = Colors.Black;
            paragraph.Format.Font.Size = 11;
            paragraph.AddText("Отчет");//добавление любого из перечисленых ниже
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            Shapka.Rows[1].Cells[0].MergeRight = 2;

            Table fottertable = section.Footers.Primary.AddTable();
            fottertable.AddColumn(Unit.FromCentimeter(12.5));
            fottertable.AddColumn(Unit.FromCentimeter(12.5));
            Row row = fottertable.AddRow();
            paragraph = row.Cells[0].AddParagraph();
            paragraph.AddText(DateTime.Now.ToString("dd.MM.yyyy"));
            paragraph = row.Cells[1].AddParagraph();
            paragraph.AddText("Страница ");
            paragraph.AddPageField();
            paragraph.AddText(" из 8");
            paragraph.Format.Alignment = ParagraphAlignment.Right;
        }

        private void SetMainInformation()
        {
            Section section = document.LastSection;
            //таблица информации
            Table FirstInfo = section.AddTable();
            FirstInfo.Borders.Visible = true;
            FirstInfo.Borders.Color = Colors.Black;
            FirstInfo.AddColumn(Unit.FromCentimeter(12.5));
            FirstInfo.AddColumn(Unit.FromCentimeter(12.5));
            Row row = FirstInfo.AddRow();
            row.Cells[0].AddParagraph("Дата расчета").Format.Font.Size = 11;
            row.Cells[1].AddParagraph(LOADPARAMS.Date.ToString("dd.MM.yyyy")).Format.Font.Size = 11;

            row = FirstInfo.AddRow();
            row.Cells[0].AddParagraph("Компания").Format.Font.Size = 11;
            row.Cells[1].AddParagraph(LOADPARAMS.CompanyName).Format.Font.Size = 11;

            row = FirstInfo.AddRow();
            row.Cells[0].AddParagraph("Исполнитель расчета").Format.Font.Size = 11;
            row.Cells[1].AddParagraph(LOADPARAMS.CalculationExecutor).Format.Font.Size = 11;

            row = FirstInfo.AddRow();
            row.Cells[0].AddParagraph("Выполнен для").Format.Font.Size = 11;
            row.Cells[1].AddParagraph(LOADPARAMS.MadeFor).Format.Font.Size = 11;

            Paragraph paragraph = section.AddParagraph();
            paragraph.AddText("");
            paragraph.Format.Font.Size = 12;

            //Подпись к таблице общая информация
            Table table = section.AddTable();
            table.AddColumn(Unit.FromCentimeter(12.5));
            table.AddColumn(Unit.FromCentimeter(12.5));
            row = table.AddRow();
            paragraph = row.Cells[0].AddParagraph("Общая информация");
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            row.Cells[0].MergeRight = 1;
            row.Cells[0].Borders.Visible = true;
            row.Cells[0].Shading.Color = Color.FromRgb(231, 208, 149);
            row.Cells[0].Shading.Visible = true;

            row = table.AddRow();
            paragraph = row.Cells[0].AddParagraph("");
            paragraph.Format.Font.Size = 11;

            //таблица общая информация
            Table GeneralIinformation = section.AddTable();
            GeneralIinformation.AddColumn(Unit.FromCentimeter(7));
            GeneralIinformation.AddColumn(Unit.FromCentimeter(5.5));
            GeneralIinformation.AddColumn(Unit.FromCentimeter(7));
            GeneralIinformation.AddColumn(Unit.FromCentimeter(5.5));
            row = GeneralIinformation.AddRow();
            paragraph = row.Cells[0].AddParagraph("Данные по скважине");
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            row.Cells[0].MergeRight = 1;
            paragraph = row.Cells[2].AddParagraph("Порода и пласт");
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            row.Cells[2].MergeRight = 1;
            row.Borders.Visible = true;

            row = GeneralIinformation.AddRow();
            row.Borders.Visible = true;
            paragraph = row.Cells[0].AddParagraph("Месторождение");
            paragraph.Format.Font.Size = 11;
            paragraph = row.Cells[1].AddParagraph(LOADPARAMS.NameMestor);
            paragraph.Format.Font.Size = 11;
            paragraph = row.Cells[2].AddParagraph("Пластовое давление , МПа");
            paragraph.Format.Font.Size = 11;
            paragraph = row.Cells[3].AddParagraph(LOADPARAMS.Pplast.ToString());
            paragraph.Format.Font.Size = 11;

            row = GeneralIinformation.AddRow();
            row.Borders.Visible = true;
            paragraph = row.Cells[0].AddParagraph("№ куста");
            paragraph.Format.Font.Size = 11;
            paragraph = row.Cells[1].AddParagraph(LOADPARAMS.BushNumber);
            paragraph.Format.Font.Size = 11;
            paragraph = row.Cells[2].AddParagraph("Температура пласта, С");
            paragraph.Format.Font.Size = 11;
            paragraph = row.Cells[3].AddParagraph(LOADPARAMS.Tplast.ToString());
            paragraph.Format.Font.Size = 11;

            row = GeneralIinformation.AddRow();
            row.Borders.Visible = true;
            paragraph = row.Cells[0].AddParagraph("№ скважины");
            paragraph.Format.Font.Size = 11;
            paragraph = row.Cells[1].AddParagraph(LOADPARAMS.NameWell);
            paragraph.Format.Font.Size = 11;
            paragraph = row.Cells[2].AddParagraph("Модуль Юнга породы, МПа");
            paragraph.Format.Font.Size = 11;
            paragraph = row.Cells[3].AddParagraph(LOADPARAMS.ModUnga.ToString());
            paragraph.Format.Font.Size = 11;

            row = GeneralIinformation.AddRow();
            row.Borders.Visible = true;
            paragraph = row.Cells[0].AddParagraph("Забой текущий, м");
            paragraph.Format.Font.Size = 11;
            paragraph = row.Cells[1].AddParagraph(LOADPARAMS.Zaboy.ToString());
            paragraph.Format.Font.Size = 11;
            paragraph = row.Cells[2].AddParagraph("Коэффициент Пуассона породы");
            paragraph.Format.Font.Size = 11;
            paragraph = row.Cells[3].AddParagraph(LOADPARAMS.KPuass.ToString());
            paragraph.Format.Font.Size = 11;

            row = GeneralIinformation.AddRow();
            row.Borders.Visible = true;
            paragraph = row.Cells[0].AddParagraph("Мощность интервала перфорации, м");
            paragraph.Format.Font.Size = 11;
            paragraph = row.Cells[1].AddParagraph(LOADPARAMS.HPerf.ToString());
            paragraph.Format.Font.Size = 11;
            paragraph = row.Cells[2].AddParagraph("Флюид");
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            row.Cells[2].MergeRight = 1;

            row = GeneralIinformation.AddRow();
            row.Borders.Visible = true;
            paragraph = row.Cells[0].AddParagraph("Подошва интервала перфорации, м");
            paragraph.Format.Font.Size = 11;
            paragraph = row.Cells[1].AddParagraph(LOADPARAMS.PodIntPerf.ToString());
            paragraph.Format.Font.Size = 11;
            paragraph = row.Cells[2].AddParagraph("Тип флюида в скважине");
            paragraph.Format.Font.Size = 11;
            paragraph = row.Cells[3].AddParagraph(LOADPARAMS.TypeFluid);
            paragraph.Format.Font.Size = 11;

            row = GeneralIinformation.AddRow();
            row.Borders.Visible = true;
            paragraph = row.Cells[0].AddParagraph("Глубина установки генератора, м");
            paragraph.Format.Font.Size = 11;
            paragraph = row.Cells[1].AddParagraph(LOADPARAMS.GlubGen.ToString());
            paragraph.Format.Font.Size = 11;
            paragraph = row.Cells[2].AddParagraph("Уровень флюида в скважине, м");
            paragraph.Format.Font.Size = 11;
            paragraph = row.Cells[3].AddParagraph(LOADPARAMS.GlubVoda.ToString());
            paragraph.Format.Font.Size = 11;

            row = GeneralIinformation.AddRow();
            row.Borders.Visible = true;
            paragraph = row.Cells[0].AddParagraph("Обсадная колонна");
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            row.Cells[0].MergeRight = 1;
            paragraph = row.Cells[2].AddParagraph("Плотность флюида, г/см3");
            paragraph.Format.Font.Size = 11;
            paragraph = row.Cells[3].AddParagraph(LOADPARAMS.DensVoda.ToString());
            paragraph.Format.Font.Size = 11;

            row = GeneralIinformation.AddRow();
            row.Borders.Visible = true;
            paragraph = row.Cells[0].AddParagraph("Диаметр обсадной колонны, мм");
            paragraph.Format.Font.Size = 11;
            paragraph = row.Cells[1].AddParagraph(LOADPARAMS.CasingDiameter.ToString());
            paragraph.Format.Font.Size = 11;

            row = GeneralIinformation.AddRow();
            row.Borders.Visible = true;
            paragraph = row.Cells[0].AddParagraph("Толщина обсадной колонны, мм");
            paragraph.Format.Font.Size = 11;
            paragraph = row.Cells[1].AddParagraph(LOADPARAMS.CasingThickness.ToString());
            paragraph.Format.Font.Size = 11;

            //Подпись к таблице заряды
            table = section.AddTable();
            table.AddColumn(Unit.FromCentimeter(12.5));
            table.AddColumn(Unit.FromCentimeter(12.5));
            row = table.AddRow();
            paragraph = row.Cells[0].AddParagraph("Заряды");
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            row.Cells[0].MergeRight = 1;
            row.Cells[0].Borders.Visible = true;
            row.Cells[0].Shading.Color = Color.FromRgb(231, 208, 149);
            row.Cells[0].Shading.Visible = true;

            row = table.AddRow();
            paragraph = row.Cells[0].AddParagraph("");
            paragraph.Format.Font.Size = 11;

            //Таблица заряды
            Table Zarads = section.AddTable();
            Zarads.Borders.Visible = true;
            Zarads.AddColumn(Unit.FromCentimeter(3.125));
            Zarads.AddColumn(Unit.FromCentimeter(3.125));
            Zarads.AddColumn(Unit.FromCentimeter(3.125));
            Zarads.AddColumn(Unit.FromCentimeter(3.125));
            Zarads.AddColumn(Unit.FromCentimeter(3.125));
            Zarads.AddColumn(Unit.FromCentimeter(3.125));
            Zarads.AddColumn(Unit.FromCentimeter(3.125));
            Zarads.AddColumn(Unit.FromCentimeter(3.125));
            row = Zarads.AddRow();
            paragraph = row.Cells[0].AddParagraph("№ варианта");
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph = row.Cells[1].AddParagraph("Наименование заряда");
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph = row.Cells[2].AddParagraph("Тип");
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph = row.Cells[3].AddParagraph("Количество");
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph = row.Cells[4].AddParagraph("Внешний диаметр, мм");
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph = row.Cells[5].AddParagraph("Внутренний диаметр, мм");
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph = row.Cells[6].AddParagraph("Длина заряда, мм");
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph = row.Cells[7].AddParagraph("Тип пороховой смеси");
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            row = Zarads.AddRow();
            paragraph = row.Cells[0].AddParagraph("1");
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            row.Cells[0].MergeDown = 1;
            row.Cells[0].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
            paragraph = row.Cells[1].AddParagraph(LOADPARAMS.vospZar.Name);
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph = row.Cells[2].AddParagraph("активные");
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph = row.Cells[3].AddParagraph(LOADPARAMS.CountVospZarad.ToString());
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph = row.Cells[4].AddParagraph(LOADPARAMS.vospZar.Dnar.ToString());
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph = row.Cells[5].AddParagraph(LOADPARAMS.vospZar.Dvnutr.ToString());
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph = row.Cells[6].AddParagraph(LOADPARAMS.vospZar.L.ToString());
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph = row.Cells[7].AddParagraph(LOADPARAMS.vospZar.Poroh.ToString());
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            row = Zarads.AddRow();
            paragraph = row.Cells[1].AddParagraph(LOADPARAMS.osnZar.Name);
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph = row.Cells[2].AddParagraph("основные");
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph = row.Cells[3].AddParagraph(LOADPARAMS.CountOsnZarad.ToString());
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph = row.Cells[4].AddParagraph(LOADPARAMS.osnZar.Dnar.ToString());
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph = row.Cells[5].AddParagraph(LOADPARAMS.osnZar.Dvnutr.ToString());
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph = row.Cells[6].AddParagraph(LOADPARAMS.osnZar.L.ToString());
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph = row.Cells[7].AddParagraph(LOADPARAMS.osnZar.Poroh.ToString());
            paragraph.Format.Font.Size = 11;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
        }

        private void SetChartPage(PlotModel model, List<Tuple<string, string>> parametrs)
        {
            Section section = document.LastSection;
            section.AddPageBreak();

            var pngExporter = new PngExporter
            {
                Width = 900,
                Height = 400,
                Background = OxyColors.White
            };
            pngExporter.ExportToFile(model, model.Title + "_temp.png");

            section.AddImage(model.Title + "_temp.png");

            TempFile.Add(model.Title + "_temp.png");

            Paragraph paragraph = section.AddParagraph("");
            paragraph.Format.Font.Size = 12;

            Table table = section.AddTable();
            table.Borders.Visible = true;
            table.AddColumn(Unit.FromCentimeter(12.5));
            table.AddColumn(Unit.FromCentimeter(12.5));
            foreach (var para in parametrs)
            {
                Row row = table.AddRow();
                row.VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
                Paragraph paragraph1 = row.Cells[0].AddParagraph(para.Item1);
                paragraph1.Format.Font.Size = 11;
                paragraph1.Format.Alignment = ParagraphAlignment.Center;
                Paragraph paragraph2 = row.Cells[1].AddParagraph(para.Item2);
                paragraph2.Format.Font.Size = 11;
                paragraph2.Format.Alignment = ParagraphAlignment.Center;
            }
        }

        private void RenderPdf(string path)
        {
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always);
            pdfRenderer.Document = document;
            pdfRenderer.RenderDocument();
            pdfRenderer.PdfDocument.Save(path + LOADPARAMS.CalculationName+"_" +LOADPARAMS.Date.ToString("dd_MM_yyyy")+".pdf");//сохраняем
        }

        public void DeleteTempFile()
        {
            foreach(var item in TempFile)
            {
                System.IO.File.Delete(item);
            }
        }
    }
}
