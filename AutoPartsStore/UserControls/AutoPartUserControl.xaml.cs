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
    /// Логика взаимодействия для AutoPartUserControl.xaml
    /// </summary>
    public partial class AutoPartUserControl : UserControl
    {
        public Autopart Autopart { get; set; }

        public AutoPartUserControl(Autopart autopart)
        {
            InitializeComponent();
            Autopart = autopart;
            DataContext = db_autopartsstoreContext.DbContext;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLabels();
            LoadImage();
        }

        private void LoadLabels()
        {
            AutoPartNameLabel.Content = Autopart.AutoPartName;
            CostLabel.Content = Autopart.Cost;
            int statudID = Autopart.IdStatusAutoPart;
            Status statusTest = db_autopartsstoreContext.DbContext.Status.Where(u => u.IdStatus == Autopart.IdStatusAutoPart).FirstOrDefault();
            IDStatusAutoPartLabel.Content = statusTest.StatusName;

        }

        private void LoadImage()
        {
            if (Autopart.AutoPartImage != null && Autopart.AutoPartImage.Length > 0)
            {
                Uri resUri = new Uri(Environment.CurrentDirectory + Autopart.AutoPartImage);
                ImageAutoPart.Source = new BitmapImage(resUri);
            }
            else
            {
                ImageAutoPart.Source = new BitmapImage(new Uri("pack://application:,,,/Images/picture.png"));
            }

        }
    }
}
