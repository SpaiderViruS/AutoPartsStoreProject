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
using AutoPartsStore.Windows.ManagerWindows;

namespace AutoPartsStore.Windows.ManagerWindows
{
    /// <summary>
    /// Логика взаимодействия для UserOrderInfoWindow.xaml
    /// </summary>
    public partial class UserOrderInfoWindow : Window
    {
        db_autopartsstoreContext DbContext;
        Busket Busket;
        Autopart Autopart;
        User User;
        public UserOrderInfoWindow(Busket busket)
        {
            InitializeComponent();
            DbContext = UsersOrdersManagmentWindow.DbContext;
            Busket = busket;

            Autopart = DbContext.Autopart.Where(a =>
            a.IdAutoPart == Busket.IdAutoPart).FirstOrDefault();

            User = DbContext.User.Where(a =>
            a.IdUser == Busket.IdUser).FirstOrDefault();

            LoadLabels();
            LoadImage();
        }

        private void LoadLabels()
        {
            AutoPartNameLabel.Content = $"Наименование запчасти: {Autopart.AutoPartName}";
            SNPLabel.Content = $"Покупатель: {User.Name} {User.Surname} {User.Patronomyc}";
            StatusLabel.Content = $"Статус заказа: {Busket.OrderStatus}";
            CostLabel.Content = $"Цена заказа: {Autopart.Cost}";
        }

        private void LoadImage()
        {
            if (Autopart.AutoPartImage != null && Autopart.AutoPartImage.Length > 0)
            {
                Uri resUri = new Uri(Environment.CurrentDirectory + Autopart.AutoPartImage);
                ImageAutoPartImageBox.Source = new BitmapImage(resUri);
            }
            else
            {
                ImageAutoPartImageBox.Source = new BitmapImage(new Uri("pack://application:,,,/Images/picture.png"));
            }
        }

        private void ConfirmOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите подтвердить заказ", "Вопрос",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Busket.OrderStatus = "Готов";
                StatusLabel.Content = "Готов";
                ConfirmOrderButton.IsEnabled = false;
                DbContext.SaveChanges();
            }
            else
            {
                return;
            }
        }
    }
}
