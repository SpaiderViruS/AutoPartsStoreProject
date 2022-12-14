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

namespace AutoPartsStore.Windows.UserWindows
{
    /// <summary>
    /// Логика взаимодействия для UserAddReviewWindow.xaml
    /// </summary>
    public partial class UserAddReviewWindow : Window
    {
        db_autopartsstoreContext DbContext;
        User User { get; set; }
        Autopart Autopart { get; set; }
        int raiting = 0;
        public UserAddReviewWindow(User user, Autopart autopart)
        {
            InitializeComponent();
            DbContext = AutoPartInfoWindow.DbContext;
            User = user;
            Autopart = autopart;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddReviewButton_Click(object sender, RoutedEventArgs e)
        {
            TextRange text = new TextRange(ReviewRichBox.Document.ContentStart,
                ReviewRichBox.Document.ContentEnd);

            if (/*!string.IsNullOrEmpty(text.Text) && */raiting != 0)
            {
                Review review = new Review();
                review.Raiting = raiting;
                review.Description = text.Text;
                review.IdAutoPart = Autopart.IdAutoPart;
                review.IdUser = User.IdUser;

                DbContext.Review.Add(review);
                DbContext.SaveChanges();

                MessageBox.Show("Отзыв оставлен", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста поставьте рейтинг", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void OneStarImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OneStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/UserPicture.png"));
            TwoStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/picture.png"));
            ThreeStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/picture.png"));
            FourStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/picture.png"));
            FiveStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/picture.png"));
            raiting = 1;
        }

        private void TwoStarImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OneStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/UserPicture.png"));
            TwoStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/UserPicture.png"));
            ThreeStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/picture.png"));
            FourStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/picture.png"));
            FiveStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/picture.png"));
            raiting = 2;
        }

        private void ThreeStarImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OneStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/UserPicture.png"));
            TwoStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/UserPicture.png"));
            ThreeStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/UserPicture.png"));
            FourStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/picture.png"));
            FiveStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/picture.png"));
            raiting = 3;
        }

        private void FourStarImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OneStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/UserPicture.png"));
            TwoStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/UserPicture.png"));
            ThreeStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/UserPicture.png"));
            FourStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/UserPicture.png"));
            FiveStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/picture.png"));
            raiting = 4;
        }

        private void FiveStarImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OneStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/UserPicture.png"));
            TwoStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/UserPicture.png"));
            ThreeStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/UserPicture.png"));
            FourStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/UserPicture.png"));
            FiveStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/UserPicture.png"));
            raiting = 5;
        }

    }
}
