﻿using FlameClassroom.Backend;
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
using FlameClassroom;

namespace FlameClassroom.Pages
{
    /// <summary>
    /// IPInput.xaml 的交互逻辑
    /// </summary>
    public partial class IPInput : Page
    {
        public MainWindow parentWindow { set; get; }

        public IPInput()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (parentWindow.identification == "student")
            {
                App.student = new StudentSide(StudentIpInput.Text);
                parentWindow.mainFrame.Content = parentWindow.signInPage;
            }
            else if (parentWindow.identification == "teacher")
            {
                App.teacher = new TeacherSide(TeacherIpInput.Text);
                App.teacher.Name = TeacherNameInput.Text;
                parentWindow.homePage = new HomePage(parentWindow);
                parentWindow.mainFrame.Content = parentWindow.homePage;

            }
        }

        private void TeacherBtn_Click(object sender, RoutedEventArgs e)
        {
            Height = 500;
            parentWindow.identification = "teacher";
            studentSubmit.Visibility = Visibility.Hidden;
            teacherSubmit.Visibility = Visibility.Visible;
        }

        private void StudentBtn_Click(object sender, RoutedEventArgs e)
        {
            Height = 500;
            parentWindow.identification = "student";
            teacherSubmit.Visibility = Visibility.Hidden;
            studentSubmit.Visibility = Visibility.Visible;
            //StudentIpInput.Text = "123";
        }
    }
}
