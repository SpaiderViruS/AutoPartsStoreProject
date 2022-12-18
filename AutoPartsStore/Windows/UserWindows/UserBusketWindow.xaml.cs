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
using AutoPartsStore.UserControls;

namespace AutoPartsStore.Windows.UserWindows
{
    /// <summary>
    /// Логика взаимодействия для UserBusketWindow.xaml
    /// </summary>
    public partial class UserBusketWindow : Window
    {
        db_autopartsstoreContext DbContext;
        User User { get; set; }
        List<Busket> Busket;
        int totalCost = 0;
        public UserBusketWindow(User user)
        {
            InitializeComponent();
            User = user;
            DbContext = UserProfileWindow.DbContext;
            Busket = new List<Busket>();

            LoadBusket();
        }

        private void LoadBusket()
        {
            List<Busketautopart> displayBusket = new List<Busketautopart>();
            displayBusket = DbContext.Busketautopart.ToList();

            Busket busket = DbContext.Busket.Where(b =>
            b.IdUser == User.IdUser).FirstOrDefault();

            BusketListView.Items.Clear();

            if (busket != null)
            {
                foreach (Busketautopart bsk in displayBusket)
                {
                    if (bsk.IdBusket == busket.IdBusket)
                    {
                        if (busket.OrderStatus.Contains("Формируется"))
                        {
                            totalCost += bsk.IdAutopartNavigation.Cost;
                            BusketListView.Items.Add(new UserBusketUserControl(bsk)
                            {
                                Width = GetOptimizedWidth()
                            });
                        }
                    }
                }
                TotalCostLabel.Content = $"К оплате {totalCost} ₽";
                totalCost = 0;
            }

            if (BusketListView.Items.Count == 0)
            {
                NoOrdersLabel.Visibility = Visibility.Visible;
                TotalCostLabel.Visibility = Visibility.Collapsed;
                PlaceOrder.IsEnabled = false;
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

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadBusket();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            if (BusketListView.Items.Count != 0)
            {
                if (MessageBox.Show($"Вы уверены, что хотите оформить заказ в кол - ве {BusketListView.Items.Count} шт.",
                    "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    List<Busket> updateBusket = new List<Busket>();
                    updateBusket = DbContext.Busket.ToList();

                    foreach (Busket bsk in updateBusket)
                    {
                        if (bsk.IdUser == User.IdUser)
                        {
                            Busket tempBusket = DbContext.Busket.Where(b =>
                            b.IdBusket == bsk.IdBusket).FirstOrDefault();

                            tempBusket.OrderStatus = "Комплектуется";
                            DbContext.SaveChanges();
                        }
                    }

                    MessageBox.Show("Заказ оформлен\nОтследить заказ можно в `Мои заказы`", "Уведомление",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    LoadBusket();
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("У вас нету товаров в коризне, чтобы оформить заказ", "Уведомление",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LoadBusket();
        }
    }
}
