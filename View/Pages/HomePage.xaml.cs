using FlameClassroom.Pages.SubPages;
using System.Windows.Controls;
using System.Windows.Input;

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
        public Undo undoPage = new Undo();
        public Others others = new Others();

        public MainWindow parentWindow { set; get; }

        public HomePage(MainWindow mainWindow)
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
            mainPage.parentWindow = mainWindow;
            userInfo.parentWindow = mainWindow;

            mainPage.SetAavatar();
            userInfo.SetAavatar();
        }

        public void SetParentWnd()
        {
            aboutUs.parentWindow = parentWindow;
            mainPage.parentWindow = parentWindow;
            others.parentWindow = parentWindow;
            userInfo.parentWindow = parentWindow;
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
            frame.Content = undoPage;
        }
    }
}
