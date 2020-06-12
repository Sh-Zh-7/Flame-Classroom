using LiveCharts;
using LiveCharts.Wpf;
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

namespace FlameClassroom.UserControls
{
    /// <summary>
    /// SelectionPlot.xaml 的交互逻辑
    /// </summary>
    public partial class SelectionPlot : UserControl
    {
        public SelectionPlot(List<int> list)
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    //Title = "2015",
                    Values = new ChartValues<int> { list[0], list[1], list[2], list[3], list[4]}
                }
            };


            Labels = new[] { "A", "B", "C", "D", "Not select" };
            Formatter = value => value.ToString("N");

            DataContext = this;
        }

        public void SetValues(List<int> list)
        {
            SeriesCollection[0] = new ColumnSeries
            {
                Values = new ChartValues<int> { list[0], list[1], list[2], list[3], list[4] }
            };
        }


        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

    }
}
