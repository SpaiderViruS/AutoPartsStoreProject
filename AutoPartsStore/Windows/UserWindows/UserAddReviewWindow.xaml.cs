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
        Review Review { get; set; }
        int raiting = 5;
        public UserAddReviewWindow(User user, Autopart autopart, Review review)
        {
            InitializeComponent();
            DbContext = MainWindow.context;
            User = user;
            Autopart = autopart;
            Review = review;

            if (Review != null)
            {
                ReviewRichBox.Document.Blocks.Add(new Paragraph(new Run(Review.Description)));
                Title = "Редактировать отзыв";

                switch (Review.Raiting)
                {
                    case 1:
                        OneStarImage_MouseLeftButtonDown(null, null);
                        break;
                    case 2:
                        TwoStarImage_MouseLeftButtonDown(null, null);
                        break;
                    case 3:
                        ThreeStarImage_MouseLeftButtonDown(null, null);
                        break;
                    case 4:
                        FourStarImage_MouseLeftButtonDown(null, null);
                        break;
                    case 5:
                        FiveStarImage_MouseLeftButtonDown(null, null);
                        break;
                }
            }
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

                if (Review != null)
                {
                    Review.Raiting = raiting;
                    Review.Description = text.Text;
                    DbContext.SaveChanges();

                    MessageBox.Show("Отзыв изменён", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
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
                }
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
            OneStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Star.png"));
            TwoStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/NullStar.png"));
            ThreeStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/NullStar.png"));
            FourStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/NullStar.png"));
            FiveStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/NullStar.png"));
            raiting = 1;
        }

        private void TwoStarImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OneStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Star.png"));
            TwoStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Star.png"));
            ThreeStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/NullStar.png"));
            FourStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/NullStar.png"));
            FiveStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/NullStar.png"));
            raiting = 2;
        }

        private void ThreeStarImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OneStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Star.png"));
            TwoStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Star.png"));
            ThreeStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Star.png"));
            FourStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/NullStar.png"));
            FiveStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/NullStar.png"));
            raiting = 3;
        }

        private void FourStarImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OneStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Star.png"));
            TwoStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Star.png"));
            ThreeStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Star.png"));
            FourStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Star.png"));
            FiveStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/NullStar.png"));
            raiting = 4;
        }

        private void FiveStarImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OneStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Star.png"));
            TwoStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Star.png"));
            ThreeStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Star.png"));
            FourStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Star.png"));
            FiveStarImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Star.png"));
            raiting = 5;
        }

    }
}
