using System;
using System.Collections.Generic;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;


namespace FlameClassroom.UserControls
{
    /// <summary>
    /// JudgePlot.xaml 的交互逻辑
    /// </summary>
    public partial class JudgePlot : UserControl
    {
        public JudgePlot(List<int> list)
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    //Title = "2015",
                    Values = new ChartValues<int> { list[0], list[1], list[2]}
                }
            };


            Labels = new[] { "True count", "False count", "Not select" };
            Formatter = value => value.ToString("N");

            DataContext = this;
        }

        public void SetValues(List<int> list)
        {
            SeriesCollection[0] = new ColumnSeries
            {
                Values = new ChartValues<int> { list[0], list[1], list[2] }
            };
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

    }
}
