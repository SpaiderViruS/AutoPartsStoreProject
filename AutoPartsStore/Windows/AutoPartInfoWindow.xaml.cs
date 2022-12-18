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
using AutoPartsStore.Windows.ManagerWindows;
using AutoPartsStore.Windows.UserWindows;

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
        Review review;
        double totalRaiting = 0;
        public AutoPartInfoWindow(Autopart autopart, User user)
        {
            InitializeComponent();
            currentUser = user;
            Autopart = autopart;
            DataContext = db_autopartsstoreContext.DbContext;
            DbContext = MainWindow.context;

            if (user != null)
            {
                if (user.IdRole == 3)
                {
                    EditButton.Visibility = Visibility.Visible;
                    ToBusketButton.Visibility = Visibility.Collapsed;
                }
            }


            LoadLabels();
            LoadReviews();
            LoadImage();
            if (currentUser != null)
            {
                CheckReviewAndBusket();
            }
            else
            {
                ToBusketButton.Visibility = Visibility.Collapsed;
            }
        }

        private void CheckReviewAndBusket()
        {
            List<Busketautopart> busketautoparts = new List<Busketautopart>();
            busketautoparts = DbContext.Busketautopart.ToList();
            List<Review> checkReview = new List<Review>();
            checkReview = DbContext.Review.ToList();

            Busket tempBusket = DbContext.Busket.Where(b =>
            b.IdUser == currentUser.IdUser).FirstOrDefault();

            if (tempBusket != null)
            {
                foreach (Busketautopart b in busketautoparts)
                {
                    if (tempBusket.IdBusket == b.IdBusket)
                    {
                        if (b.IdAutopart == Autopart.IdAutoPart)
                        {
                            WriteReviewButton.Visibility = Visibility.Visible;
                            break;
                        }
                    }
                }

                review = DbContext.Review.Where(r =>
                r.IdUser == currentUser.IdUser && r.IdAutoPart == Autopart.IdAutoPart).FirstOrDefault();

                if (review != null)
                {
                    WriteReviewButton.Content = "Редактировать отзыв";

                }
            }
            else
            {
                return;
            }
            
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
            CharacterisikLabel.Content = $"Характеристика: {characteristik.Description}";
            CostLabel.Content = $"Цена: {Autopart.Cost}";
        }

        private void LoadReviews()
        {
            List<Review> reviews = new List<Review>();
            reviews = DbContext.Review.Where(r =>
            r.IdAutoPart == Autopart.IdAutoPart).ToList();

            ReviewStackPanel.Children.Clear();
            foreach (Review rvw in reviews)
            {
                totalRaiting += rvw.Raiting;
                ReviewStackPanel.Children.Add(new ReviewUserControl(rvw)
                {
                    Width = GetOptimizedWidth()
                });
            }
            totalRaiting = totalRaiting / reviews.Count;
            TotalRaitingLabel.Content = $"Общий рейтинг: {totalRaiting}";
            totalRaiting = 0;
        }

        private void LoadImage()
        {
            if (Autopart.AutoPartImage != null && Autopart.AutoPartImage.Length > 0)
            {
                Uri resUri = new Uri(Environment.CurrentDirectory + Autopart.AutoPartImage);
                ImageAutoPartImageBox.Source = new BitmapImage(resUri);
            }
            else
            {
                ImageAutoPartImageBox.Source = new BitmapImage(new Uri("pack://application:,,,/Images/picture.png"));
            }
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
                    Busket toBusket = DbContext.Busket.Where(b =>
                    b.IdUser == currentUser.IdUser).FirstOrDefault();

                    if (toBusket == null)
                    {
                        toBusket = new Busket();
                        toBusket.IdUser = currentUser.IdUser;
                        toBusket.OrderStatus = "";
                        DbContext.Busket.Add(toBusket);
                        DbContext.SaveChanges();
                    }

                    Busketautopart busketautopart = new Busketautopart();
                    busketautopart.IdAutopart = Autopart.IdAutoPart;
                    busketautopart.IdBusket = toBusket.IdBusket;

                    toBusket.OrderStatus = "Формируется";

                    DbContext.Busketautopart.Add(busketautopart);
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

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditInsertAutoPartWindow editInsertAutoPart = new EditInsertAutoPartWindow(Autopart);
            editInsertAutoPart.ShowDialog();
        }

        private void WriteReviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (WriteReviewButton.Content.ToString() == "Редактировать отзыв")
            {
                UserAddReviewWindow userAddReviewWindow = new UserAddReviewWindow(currentUser, Autopart, review);
                userAddReviewWindow.ShowDialog();
            }
            else
            {
                UserAddReviewWindow userAddReviewWindow = new UserAddReviewWindow(currentUser, Autopart, null);
                userAddReviewWindow.ShowDialog();
            }
            LoadReviews();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
