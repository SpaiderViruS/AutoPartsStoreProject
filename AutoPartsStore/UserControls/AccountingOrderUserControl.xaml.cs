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

namespace AutoPartsStore.UserControls
{
    /// <summary>
    /// Логика взаимодействия для AccountingOrderUserControl.xaml
    /// </summary>
    public partial class AccountingOrderUserControl : UserControl
    {
        db_autopartsstoreContext DbContext;
        public Order Order { get; set; }
        public AccountingOrderUserControl(Order order)
        {
            InitializeComponent();
            DbContext = new db_autopartsstoreContext();
            Order = order;

            LoadLabels();
        }

        private void LoadLabels()
        {
            Busket busket = DbContext.Busket.Where(b =>
            b.IdBusket == Order.IdBusket).FirstOrDefault();

            User user = DbContext.User.Where(b =>
            b.IdUser == busket.IdUser).FirstOrDefault();

            UserSNPLabel.Content = $"ФИО покупателя: {user.Name} {user.Surname} {user.Patronomyc}";
            DateOrderLabel.Content = Order.DateOrder;
            TotalCostLabel.Content = $"Выручка: {Order.TotalCost} ₽";
        }
    }
}
