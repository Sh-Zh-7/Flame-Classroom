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
    /// UserInfo.xaml 的交互逻辑
    /// </summary>
    public partial class UserInfo : Page
    {
        public MainWindow parentWindow { set; get; }
        public UserInfo()
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(parentWindow.identification == "student")
            {
                MessageBox.Show("修改信息成功（学生端）");
                //TODO:messagebox
                App.student.ChangeInfo(UserInfoSchool.Text, UserInfoID.Text, UserInfoName.Text, UserInfoDescription.Text);
                this.parentWindow.homePage.mainPage.ChangedInfo(UserInfoName.Text, UserInfoDescription.Text, UserInfoSchool.Text, UserInfoID.Text);
            }
            else if(parentWindow.identification == "teacher")
            {
                this.parentWindow.homePage.mainPage.ChangedInfo(UserInfoName.Text, UserInfoDescription.Text, UserInfoSchool.Text, UserInfoID.Text);
            }
        }
    }
}
