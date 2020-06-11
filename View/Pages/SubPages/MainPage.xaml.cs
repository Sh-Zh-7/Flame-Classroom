using FlameClassroom.Windows;
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
            StudentWnd studentWnd = new StudentWnd();
            studentWnd.Show();
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            TeacherWnd teacherWnd = new TeacherWnd();
            teacherWnd.Show();
        }

        private void Return_to_SignIn_Click(object sender, RoutedEventArgs e)
        {
            parentWindow.mainFrame.Content = parentWindow.signInPage;
        }
    }
}
