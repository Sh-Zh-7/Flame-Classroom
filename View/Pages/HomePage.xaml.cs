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

namespace FlameClassroom.Pages
{
    /// <summary>
    /// StudentHomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : Page
    {
        private Dictionary<string, Uri> allViews = new Dictionary<string, Uri>();
        public HomePage()
        {
            InitializeComponent();
            // Bind Events
            MainPageLBI.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(MainPageLBI_MouseLeftButtonDown), true);
            UserInfoLBI.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(UserInfoLBI_MouseLeftButtonDown), true);
            FeedBackLBI.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(FeedBackLBI_MouseLeftButtonDown), true);
            AboutUsLBI.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(AboutUsLBI_MouseLeftButtonDown), true);
            OthersLBI.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(OthersLBI_MouseLeftButtonDown), true);
            // Add pages to dict
            allViews.Add("mainPage", new Uri("Pages/SubPages/MainPage.xaml", UriKind.Relative));
            // Main page by default
            frame.Navigate(allViews["mainPage"]);
        }

        private void MainPageLBI_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frame.Navigate(allViews["mainPage"]);
        }

        private void UserInfoLBI_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void FeedBackLBI_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void AboutUsLBI_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void OthersLBI_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
