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
using System.Windows.Shapes;
using AutoPartsStore.Models;

namespace AutoPartsStore.Windows.UserWindows
{
    /// <summary>
    /// Логика взаимодействия для UserProfileWindow.xaml
    /// </summary>
    public partial class UserProfileWindow : Window
    {
        User User { get; set; }
        public UserProfileWindow(User user)
        {
            InitializeComponent();
            User = user;

            LoadLabels();
            LoadImage();
        }

        private void LoadLabels()
        {
            NameUserLabel.Content = User.Name;
            SurnameUserLabel.Content = User.Surname;
            PatronomicUserLabel.Content = User.Patronomyc;
        }
        private void LoadImage()
        {

        }
    }
}
