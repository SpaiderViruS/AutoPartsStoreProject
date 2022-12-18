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
using AutoPartsStore.Windows;
using AutoPartsStore.UserControls;

namespace AutoPartsStore.Windows.ManagerWindows
{
    /// <summary>
    /// Логика взаимодействия для UsersOrdersManagmentWindow.xaml
    /// </summary>
    public partial class UsersOrdersManagmentWindow : Window
    {
        public static db_autopartsstoreContext DbContext;
        User User;

        public UsersOrdersManagmentWindow(User user)
        {
            InitializeComponent();
            DbContext = new db_autopartsstoreContext();
            User = user;

            SNPLabel.Content = $"Менеджер {User.Name} {User.Surname} {User.Patronomyc}";

            UpdateListView();
        }

        private void UpdateListView()
        {
            OrdersListView.Items.Clear();
            List<Busket> displayBusket = new List<Busket>();
            displayBusket = DbContext.Busket.ToList();

            

            if (!string.IsNullOrEmpty(SearchTextBox.Text))
            {
                displayBusket = displayBusket.Where(d =>
                d.IdUserNavigation.Name.ToLower().Contains(SearchTextBox.Text.ToLower())
                || d.IdUserNavigation.Surname.ToLower().Contains(SearchTextBox.Text.ToLower())
                || d.IdUserNavigation.Patronomyc.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            }

            foreach (Busket busket in displayBusket)
            {
                if (busket.OrderStatus == "Комплектуется")
                {
                    OrdersListView.Items.Add(new ManagersOrdersUserControl(busket)
                    {
                        Width = GetOptimizedWidth()
                    });
                }
            }
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

        private void OrdersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrdersListView.SelectedItem != null)
            {
                Busket Busket = ((ManagersOrdersUserControl)OrdersListView.SelectedItem).Busket;

                UserOrderInfoWindow userOrderInfoWindow = new UserOrderInfoWindow(Busket);
                userOrderInfoWindow.ShowDialog();
            }
            UpdateListView();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateListView();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateListView();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
