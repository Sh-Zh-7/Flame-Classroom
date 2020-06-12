using System.Windows;
using System.Windows.Controls;

namespace FlameClassroom.Pages
{
    /// <summary>
    /// Student.xaml 的交互逻辑
    /// </summary>
    public partial class SignIn : Page
    {
        public MainWindow parentWindow { set; get; }
        public SignIn()
        {
            InitializeComponent();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            parentWindow.homePage = new HomePage(parentWindow);
            parentWindow.mainFrame.Content = parentWindow.homePage;
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            parentWindow.mainFrame.Content = parentWindow.signUpPage;
        }

        private void Return_to_ID_Click(object sender, RoutedEventArgs e)
        {
            var newInputPage = new IPInput();
            newInputPage.parentWindow = parentWindow;
            parentWindow.mainFrame.Content = newInputPage;
        }

        private void ShowAccountError()
        {
            warning.Content = "Account doesn't exist!";
        }

        private void ShowPasswordError()
        {
            warning.Content = "Password Error!";
        }
    }
}
