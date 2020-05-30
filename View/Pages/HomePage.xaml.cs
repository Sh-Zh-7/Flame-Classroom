using FlameClassroom.Pages.SubPages;
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
        public MainPage mainPage = new MainPage();
        public UserInfo userInfo = new UserInfo();
        public AboutUs aboutUs = new AboutUs();
        public Others others = new Others();

        public HomePage()
        {
            InitializeComponent();
            // Bind Events
            MainPageLBI.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(MainPageLBI_MouseLeftButtonDown), true);
            UserInfoLBI.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(UserInfoLBI_MouseLeftButtonDown), true);
            HomeworkLBI.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(HomeworkLBI_MouseLeftButtonDown), true);
            AboutUsLBI.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(AboutUsLBI_MouseLeftButtonDown), true);
            OthersLBI.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(OthersLBI_MouseLeftButtonDown), true);
            // Main page by default
            frame.Content = mainPage;
        }

        private void MainPageLBI_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frame.Content = mainPage;
        }

        private void UserInfoLBI_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frame.Content = userInfo;
        }

        private void AboutUsLBI_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frame.Content = aboutUs;
        }

        private void OthersLBI_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frame.Content = others;
        }

        private void HomeworkLBI_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
