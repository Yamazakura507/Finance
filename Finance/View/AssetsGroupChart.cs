
using Finance.Classes;
using Finance.Classes.AppSettings;
using Microcharts;
using SkiaSharp;
using System.Data;

namespace Finance.View
{
    public class AssetsGroupChart
    {
        private int id;

        public int Id
        {
            get => id;
            set
            { 
                id = value;

                ChartData = LoadCharts(value).Result;

                #if ANDROID || IOS
                    WidthChart = 300;
                    HeightChart = 300;
                #else
                    WidthChart = 500;
                    HeightChart = 300;
                #endif

                BackBord = Color.Parse(ChartData.BackgroundColor.ToString());
                StrokeBord = Color.FromRgba(BackBord.Red*0.8, BackBord.Green*0.8, BackBord.Blue*0.8, BackBord.Alpha);
            }
        }

        public string Name
        {
            get;
            set;
        }

        public byte[] Icon
        {
            get;
            set;
        }

        public int WidthChart { get; set; }
        public int HeightChart { get; set; }
        public Color StrokeBord { get; set; }
        public Color BackBord { get; set; }

        public Chart ChartData 
        {
            get;
            private set;
        }

        async private Task<Chart> LoadCharts(int NewId)
        {
            string sql = $@"SELECT ag.`Id`,ag.`IdUser`, COALESCE(SUM(case WHEN a.`IsAsset` then a.`Sum` end),0)-COALESCE(SUM(case WHEN not a.`IsAsset` then a.`Sum` end),0) SumDohod, a.`IsAsset`, concat(d.Year, ' ', month_name(d.Month)) DateStr FROM `AssetsGroup` ag 
                            INNER JOIN `GroupingAssets` ga ON ga.`IdGroupAssets` = ag.`Id` 
                            INNER JOIN `Assets` a ON (a.`Id`,a.`IdUser`) = (ga.`IdAssets`,ag.`IdUser`) OR (a.`Id`,a.`IdUser`) = (ga.`IdAssets`,ag.IdUser)
                            INNER JOIN `DateJournal` d ON d.`Id` = ga.`IdDate`
                            WHERE ag.`IdUser` = '{InfoAccount.IdUser}' OR ag.`IdUser` is NULL
                            GROUP BY ag.`Id`,ag.`IdUser`, d.`Id`
                            HAVING ag.`Id` = '{NewId}'
                            ORDER BY d.Year ASC, d.Month ASC;";

            DataTable dataGrChart;

            using (var ms = new Mysql())
            {
                dataGrChart = await ms.GetTableAsync(sql);
            }

            if (dataGrChart is null)
            {
                return new LineChart();
            }

            Random rnd = new Random();
            Chart[] chartType = new Chart[]
            {
                new LineChart(),new DonutChart(),new BarChart(), new PointChart(), new RadarChart(), new RadialGaugeChart(), new PieChart()
            };

            Chart chart = chartType[rnd.Next(0, 6)];

            var color = SKColor.Parse(String.Format("#80{0:X6}", rnd.Next(0x1000000)));

            List<ChartEntry> entries = new List<ChartEntry>();

            foreach (DataRow item in dataGrChart.Rows)
            {
                if (chart.GetType() == typeof(DonutChart) || chart.GetType() == typeof(RadarChart) || chart.GetType() == typeof(PieChart) || chart.GetType() == typeof(RadialGaugeChart))
                {
                    color = SKColor.Parse(String.Format("#{0:X6}", rnd.Next(0x1000000)));
                }

                entries.Add(new ChartEntry(Convert.ToSingle(item["SumDohod"]))
                {
                    ValueLabel = item["SumDohod"].ToString(),
                    Label = item["DateStr"].ToString(),
                    TextColor = color,
                    ValueLabelColor = color,
                    Color = color
                });
            }

            switch (chart.GetType().ToString())
            {
                case "Microcharts." + nameof(LineChart):
                    ((LineChart)chart).Entries = entries;
                    break;
                case "Microcharts." + nameof(DonutChart):
                    ((DonutChart)chart).Entries = entries;
                    break;
                case "Microcharts." + nameof(BarChart):
                    ((BarChart)chart).Entries = entries;
                    break;
                case "Microcharts." + nameof(PointChart):
                    ((PointChart)chart).Entries = entries;
                    break;
                case "Microcharts." + nameof(RadarChart):
                    ((RadarChart)chart).Entries = entries;
                    break;
                case "Microcharts." + nameof(RadialGaugeChart):
                    ((RadialGaugeChart)chart).Entries = entries;
                    break;
                case "Microcharts." + nameof(PieChart):
                    ((PieChart)chart).Entries = entries;
                    break;
            }

            chart.BackgroundColor = this.Icon.RaschetColor();

            #if ANDROID || IOS
                chart.LabelTextSize = 30;
            #endif

            return chart;
        }
    }
}
