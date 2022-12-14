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
    /// Логика взаимодействия для ManagersOrdersUserControl.xaml
    /// </summary>
    public partial class ManagersOrdersUserControl : UserControl
    {
        public Busket Busket { get; set; }
        User User { get; set; }
        public ManagersOrdersUserControl(Busket busket)
        {
            InitializeComponent();
            Busket = busket;

            User = db_autopartsstoreContext.DbContext.User.Where(u =>
            u.IdUser == Busket.IdUser).FirstOrDefault();

            LoadLabels();
            LoadImage();
        }

        private void LoadLabels()
        {
            SNPUserLabel.Content = $"Покупатель {User.Name} {User.Surname} {User.Patronomyc}";
            StatusLabel.Content = $"Статус заказа: {Busket.OrderStatus}";
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
    }
}
