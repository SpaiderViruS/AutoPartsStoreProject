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
        public Busketautopart Busketautopart { get; set; }
        Busket Busket { get; set; }
        Autopart Autopart;
        public UserBusketUserControl(Busketautopart busketautopart)
        {
            InitializeComponent();
            Busketautopart = busketautopart;

            Busket = db_autopartsstoreContext.DbContext.Busket.Where(b =>
            b.IdBusket == Busketautopart.IdBusket).FirstOrDefault();

            Autopart = db_autopartsstoreContext.DbContext.Autopart.Where(p =>
            p.IdAutoPart == Busketautopart.IdAutopart).FirstOrDefault();

            LoadLabels();
            LoadImage();
        }

        private void LoadLabels()
        {           
            AutoPartNameLabel.Content = $"Наименование: {Autopart.AutoPartName}";
            StatusLabel.Content = $"Статус: {Busket.OrderStatus}";
        }

        private void LoadImage()
        {
            if (Autopart.AutoPartImage != null && Autopart.AutoPartImage.Length > 0)
            {
                Uri resUri = new Uri(Environment.CurrentDirectory + Autopart.AutoPartImage);
                AutoPartImage.Source = new BitmapImage(resUri);
            }
            else
            {
                AutoPartImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/picture.png"));
            }
        }

    }
}
