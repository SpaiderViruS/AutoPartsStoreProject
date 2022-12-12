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
    /// Логика взаимодействия для ReviewUserControl.xaml
    /// </summary>
    public partial class ReviewUserControl : UserControl
    {
        Review Review { get; set; }
        User User;

        public ReviewUserControl(Review review)
        {
            InitializeComponent();
            Review = review;

            User = db_autopartsstoreContext.DbContext.User.Where(u =>
            u.IdUser == Review.IdUser).FirstOrDefault();

            LoadLabels();
            LoadImage();
        }

        private void LoadLabels()
        {          
            RaitingLabel.Content = Review.Raiting;
            NameUser.Content = User.Name;
            ReviewDescriptionLabel.Content = Review.Description;
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
