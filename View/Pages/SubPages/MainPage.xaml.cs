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
                if(App.student.account.Init==false)
                {
                    this.Dispatcher.BeginInvoke((Action)delegate ()
                    {
                        MainPageName1.Content = App.student.account.Name;
                        MainPageDescription.Content = App.student.account.Description;
                        MainPageSchool.Text = App.student.account.School;
                        MainPageID.Text = App.student.account.ID;
                        MainPageName.Text = App.student.account.Name;
                    });
                }
            }
            else if (parentWindow.identification == "teacher")
            {
                ImageBrush myImageBrush = new ImageBrush();
                myImageBrush.ImageSource = new BitmapImage(new Uri("Images/teacher.png", UriKind.Relative));
                avatar.Fill = myImageBrush;
                MainPageName1.Content = App.teacher.Name;
                MainPageSchool.Text = "WHU";
                MainPageName.Text = App.teacher.Name;
            }
        }

        private void Listen_Click(object sender, RoutedEventArgs e)
        {
            if (parentWindow.identification == "student")
            {
                parentWindow.mainFrame.Content = parentWindow.studentLivePage;
            } else if (parentWindow.identification == "teacher")
            {
                parentWindow.mainFrame.Content = parentWindow.teacherLivePage;
                parentWindow.teacherLivePage.Init();
            }


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

        public void ChangedInfo(string Name,string Description,string School,string ID)
        {
            this.MainPageName1.Content = Name;
            this.MainPageDescription.Content = Description;
            this.MainPageSchool.Text = School;
            this.MainPageName.Text = Name;
            this.MainPageID.Text = ID;
        }
    }
}
