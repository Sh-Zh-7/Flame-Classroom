using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

        public void SetAavatar()
        {
            
            if (parentWindow.identification == "student")
            {
                ImageBrush myImageBrush = new ImageBrush();
                myImageBrush.ImageSource = new BitmapImage(new Uri("Images/student.png", UriKind.Relative));
                avatar.Fill = myImageBrush;
            }
            else if (parentWindow.identification == "teacher")
            {
                ImageBrush myImageBrush = new ImageBrush();
                myImageBrush.ImageSource = new BitmapImage(new Uri("Images/teacher.png", UriKind.Relative));
                avatar.Fill = myImageBrush;
            }
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
