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
using AutoPartsStore.Models;
using AutoPartsStore.Windows;

namespace AutoPartsStore.UserControls
{
    /// <summary>
    /// Логика взаимодействия для OrderUserControl.xaml
    /// </summary>
    public partial class OrderUserControl : UserControl
    {
        public Order Order { get; set; }
        public Busketautopart Busketautopart { get; set; }
        Busket Busket { get; set; }
        Autopart Autopart;
        public OrderUserControl(Order order)
        {
            InitializeComponent();
            Order = order;

            Busket = db_autopartsstoreContext.DbContext.Busket.Where(b =>
            b.IdBusket == Order.IdBusket).FirstOrDefault();

            Busketautopart = db_autopartsstoreContext.DbContext.Busketautopart.Where(b =>
            b.IdBusket == Busket.IdBusket).FirstOrDefault();

            Autopart = db_autopartsstoreContext.DbContext.Autopart.Where(a =>
            a.IdAutoPart == Busketautopart.IdAutopart).FirstOrDefault();

            // нужна ли пользователю выводить аву

            LoadLabels();
            LoadImage();
        }

        private void LoadLabels()
        {
            User user = db_autopartsstoreContext.DbContext.User.Where(u =>
            u.IdUser == Busket.IdUser).FirstOrDefault();

            OrderAutoPartNameLabel.Content = $"Наименоввание запчасти: {Autopart.AutoPartName}";
            OrderStatusLabel.Content = $"Статус заказа: {Busket.OrderStatus}";
            OrderUserLabel.Content = $"Покупатель: {user.Name} {user.Surname} {user.Patronomyc}";
        }

        private void LoadImage()
        {
            if (Autopart.AutoPartImage != null && Autopart.AutoPartImage.Length > 0)
            {
                Uri resUri = new Uri(Environment.CurrentDirectory + Autopart.AutoPartImage);
                AutoPartOrderImage.Source = new BitmapImage(resUri);
            }
            else
            {
                AutoPartOrderImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/picture.png"));
            }
        }
    }
}
