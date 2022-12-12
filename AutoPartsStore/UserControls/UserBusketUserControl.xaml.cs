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
    /// Логика взаимодействия для UserBusketUserControl.xaml
    /// </summary>
    public partial class UserBusketUserControl : UserControl
    {
        Busket Busket { get; set; }
        public UserBusketUserControl(Busket busket)
        {
            InitializeComponent();
            Busket = busket;

            LoadLabels();
        }

        private void LoadLabels()
        {
            Autopart currentAutoPart = db_autopartsstoreContext.DbContext.Autopart.Where(p =>
            p.IdAutoPart == Busket.IdAutoPart).FirstOrDefault();

            AutoPartNameLabel.Content = $"Наименование: {currentAutoPart.AutoPartName}";
            StatusLabel.Content = $"Статус: {Busket.OrderStatus}";
        }

        private void LoadImage()
        {

        }

    }
}
