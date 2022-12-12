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
        public static db_autopartsstoreContext DbContext;
        User User { get; set; }
        public UserProfileWindow(User user)
        {
            InitializeComponent();
            User = user;
            DbContext = new db_autopartsstoreContext();

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

        private void ToBusketButton_Click(object sender, RoutedEventArgs e)
        {
            UserBusketWindow userBusketWindow = new UserBusketWindow(User);
            userBusketWindow.ShowDialog();
        }

        private void ToOrders_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
