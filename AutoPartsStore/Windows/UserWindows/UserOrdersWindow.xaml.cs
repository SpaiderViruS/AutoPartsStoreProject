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
    /// Логика взаимодействия для UserOrdersWindow.xaml
    /// </summary>
    public partial class UserOrdersWindow : Window
    {
        db_autopartsstoreContext DbContext;
        User User { get; set; }
        public UserOrdersWindow(User user)
        {
            InitializeComponent();
            User = user;
            DbContext = UserProfileWindow.DbContext;

            LoadListView();
        }

        private void LoadListView()
        {
            List<Busket> displayBusket = new List<Busket>();
            displayBusket = DbContext.Busket.ToList();

            OrdersListView.Items.Clear();
            foreach (Busket bsk in displayBusket)
            {
                if (bsk.IdUser == User.IdUser)
                {
                    if (bsk.OrderStatus != "Формируется")
                    {
                        OrdersListView.Items.Add(new UserBusketUserControl(bsk)
                        {
                            Width = GetOptimizedWidth()
                        });
                    }
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

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
