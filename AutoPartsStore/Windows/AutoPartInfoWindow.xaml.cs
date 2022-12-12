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

namespace AutoPartsStore.Windows
{
    /// <summary>
    /// Логика взаимодействия для AutoPartInfoWindow.xaml
    /// </summary>
    public partial class AutoPartInfoWindow : Window
    {
        public static db_autopartsstoreContext DbContext;
        User currentUser { get; set; }
        Autopart Autopart { get; set; }
        public AutoPartInfoWindow(Autopart autopart, User user)
        {
            InitializeComponent();
            currentUser = user;
            Autopart = autopart;
            DataContext = db_autopartsstoreContext.DbContext;
            DbContext = MainWindow.context;

            LoadLabels();
            LoadReviews();
            LoadImage();
        }

        private void LoadLabels()
        {
            Status statusTest = db_autopartsstoreContext.DbContext.Status.Where(u =>
            u.IdStatus == Autopart.IdStatusAutoPart).FirstOrDefault();

            Manufracturer manufracturer = db_autopartsstoreContext.DbContext.Manufracturer.Where(m =>
            m.IdManufracturer == Autopart.IdManufracturer).FirstOrDefault();

            Characteristik characteristik = db_autopartsstoreContext.DbContext.Characteristik.Where(m =>
            m.IdCharacteristik == Autopart.IdCharacteristik).FirstOrDefault();

            AutoPartNameLabel.Content = $"Название: {Autopart.AutoPartName}";
            ManufracturerLabel.Content = $"Производитель: {manufracturer.ManufracturerName}";
            CharacterisikLabel.Content = $"Характеристика: {characteristik.Applicability}";
            CostLabel.Content = $"Цена: {Autopart.Cost}";
        }

        private void LoadReviews()
        {
            List<Review> reviews = new List<Review>();
            reviews = db_autopartsstoreContext.DbContext.Review.Where(r =>
            r.IdAutoPart == Autopart.IdAutoPart).ToList();

            ReviewStackPanel.Children.Clear();
            foreach (Review rvw in reviews)
            {
                ReviewStackPanel.Children.Add(new ReviewUserControl(rvw)
                {
                    Width = GetOptimizedWidth()
                });
            }
        }

        private void LoadImage()
        {

        }

        private double GetOptimizedWidth()
        {
            if (WindowState == WindowState.Maximized)
            {
                return RenderSize.Width - 55;
            }
            else
            {
                return Width - 55;
            }
        }

        private void ToBusketButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentUser != null)
            {
                Status status = db_autopartsstoreContext.DbContext.Status.Where(u =>
            u.IdStatus == Autopart.IdStatusAutoPart).FirstOrDefault();
                if (status.StatusName != "Нет в наличии")
                {
                    Busket toBusket = new Busket();
                    toBusket.IdAutoPart = Autopart.IdAutoPart;
                    toBusket.IdUser = currentUser.IdUser;
                    toBusket.OrderStatus = "Формируется";

                    DbContext.Busket.Add(toBusket);
                    DbContext.SaveChanges();

                    MessageBox.Show($"Товар {Autopart.AutoPartName} успешно добавлен в корзину", "Уведомление",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Товара нет в наличии, приносим извинения", "Уведомление",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Чтобы купить данный товар, сначала авторизируйтесь или зарегистрируйтесь",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
}
