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

        public ReviewUserControl(Review review)
        {
            InitializeComponent();
            Review = review;

            LoadLabels();
            LoadImage();
        }

        private void LoadLabels()
        {
            User user = db_autopartsstoreContext.DbContext.User.Where(u =>
            u.IdUser == Review.IdUser).FirstOrDefault();

            RaitingLabel.Content = Review.Raiting;
            NameUser.Content = user.Name;
            ReviewDescriptionLabel.Content = Review.Description;
        }

        private void LoadImage()
        {

        }

    }
}
