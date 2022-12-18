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
using AutoPartsStore.UserControls;

namespace AutoPartsStore.Windows.ManagerWindows
{
    /// <summary>
    /// Логика взаимодействия для UserOrderInfoWindow.xaml
    /// </summary>
    public partial class UserOrderInfoWindow : Window
    {
        db_autopartsstoreContext DbContext;
        Busket Busket;
        User User;
        int totalCost;
        public UserOrderInfoWindow(Busket busket)
        {
            InitializeComponent();
            DbContext = UsersOrdersManagmentWindow.DbContext;
            Busket = busket;

            //Autopart = DbContext.Autopart.Where(a =>
            //a.IdAutoPart == Busket.IdAutoPart).FirstOrDefault();

            User = DbContext.User.Where(a =>
            a.IdUser == Busket.IdUser).FirstOrDefault();

            SNPLabel.Content = $"Заказ покупателя {User.Name} {User.Surname} {User.Patronomyc}";

            UpdateListView();
        }

        private void UpdateListView()
        {
            UserBusketListView.Items.Clear();
            List<Busketautopart> displayBusketautoparts = new List<Busketautopart>();
            displayBusketautoparts = DbContext.Busketautopart.ToList();

            foreach (Busketautopart bskautopart in displayBusketautoparts)
            {
                if (Busket.IdBusket == bskautopart.IdBusket)
                {
                    Autopart costAutopart = DbContext.Autopart.Where(a => a.IdAutoPart == bskautopart.IdAutopart).FirstOrDefault();
                    totalCost += costAutopart.Cost;
                    UserBusketListView.Items.Add(new ManagersBusketAutopartsUserControl(bskautopart)
                    {
                        Width = GetOptimizedWidth()
                    });
                }
            }
            TotalCostLabel.Content = $"Прибыль {totalCost} ₽";
        }

        private double GetOptimizedWidth()
        {
            if (WindowState == WindowState.Maximized)
            {
                return (RenderSize.Width - 55) / 1.5;
            }
            else
            {
                return (Width - 55) / 1.5;
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

                Order order = new Order();
                order.IdBusket = Busket.IdBusket;
                order.TotalCost = totalCost;
                order.DateOrder = DateTime.Now;

                UserBusketListView.Items.Clear();

                DbContext.Order.Add(order);
                DbContext.SaveChanges();

                MessageBox.Show("Заказ успешно подтвержден", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                return;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateListView();
        }
    }
}
