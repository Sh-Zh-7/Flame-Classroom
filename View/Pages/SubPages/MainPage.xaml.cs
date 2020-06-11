using System.Windows;
using System.Windows.Controls;

namespace FlameClassroom.Pages.SubPages
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page
    {
        public MainWindow parentWindow { set; get; }
        public MainPage()
        {
            InitializeComponent();
        }

        private void Listen_Click(object sender, RoutedEventArgs e)
        {
            parentWindow.mainFrame.Content = parentWindow.livePage;

        }

        private void Return_to_SignIn_Click(object sender, RoutedEventArgs e)
        {
            if (parentWindow.identification == "student")
            {
                parentWindow.mainFrame.Content = parentWindow.signInPage;
            }
            else if (parentWindow.identification == "teacher")
            {
                var newInputPage = new IPInput();
                newInputPage.parentWindow = parentWindow;
                parentWindow.mainFrame.Content = newInputPage;
            }
            
        }
    }
}
