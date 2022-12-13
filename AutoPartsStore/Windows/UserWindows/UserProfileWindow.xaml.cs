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
using System.IO;
using AutoPartsStore.Models;
using Microsoft.Win32;

namespace AutoPartsStore.Windows.UserWindows
{
    /// <summary>
    /// Логика взаимодействия для UserProfileWindow.xaml
    /// </summary>
    public partial class UserProfileWindow : Window
    {
        public static db_autopartsstoreContext DbContext;
        OpenFileDialog openFileDialog = new OpenFileDialog();
        string photoPath = null;
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
            if (User.Image != null && User.Image.Length > 0)
            {
                Uri resUri = new Uri(Environment.CurrentDirectory + User.Image);
                UserImage.Source = new BitmapImage(resUri);
            }
            else
            {
                UserImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/UserPicture.png"));
            }
        }

        private void ToBusketButton_Click(object sender, RoutedEventArgs e)
        {
            UserBusketWindow userBusketWindow = new UserBusketWindow(User);
            userBusketWindow.ShowDialog();
        }

        private void ToOrders_Click(object sender, RoutedEventArgs e)
        {
            UserOrdersWindow userOrdersWindow = new UserOrdersWindow(User);
            userOrdersWindow.ShowDialog();
        }

        private void ChangeUserImage_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                photoPath = openFileDialog.FileName;
                UserImage.Source = new BitmapImage(new Uri(photoPath));
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (photoPath != null)
            {
                if (MessageBox.Show("Сохранить новое изображение?", "Вопрос",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    string photoName = "\\Images\\" + System.IO.Path.GetRandomFileName() + ".jpg";
                    File.Copy(photoPath, Environment.CurrentDirectory + photoName, true);
                    User.Image = photoName;
#warning Проверить работу смены картинки
                    DbContext.SaveChanges();
                }                
            }
        }
    }
}
