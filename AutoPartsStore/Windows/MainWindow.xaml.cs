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
using AutoPartsStore.UserControls;
using AutoPartsStore.Windows;
using AutoPartsStore.Windows.UserWindows;

namespace AutoPartsStore
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static db_autopartsstoreContext context;
        public static User enteredUser;
        private int currentPage = 1;
        private int countPages;

        public MainWindow(User user)
        {
            InitializeComponent();
            context = new db_autopartsstoreContext();
            enteredUser = user;

            if (user != null)
            {
                GoToUserProfileButton.Visibility = Visibility.Visible;
            }

            // Тут сделать проверку на уровень доступа пользователя (Возможно не надо, роль можно смотреть при переходах в формы)

            RefreshWindow();
        }

        private void RefreshWindow()
        {
            List<Autopart> autopart = new List<Autopart>();
            autopart = context.Autopart.ToList();

            AutoPathListView.Items.Clear();
            foreach (Autopart ap in autopart)
            {
                AutoPathListView.Items.Add(new AutoPartUserControl(ap)
                {
                    Width = GetOptimizedWidth()
                });
            }
        }

        private double GetOptimizedWidth()
        {
            if (WindowState == WindowState.Maximized)
            {
                return (RenderSize.Width - 55) / 1.5;
            }
            else
            {
                return (Width - 55) / 1.5;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RefreshWindow();
        }

        private void AutoPathListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (AutoPathListView.SelectedItem != null)
            {
                Autopart autopart = ((AutoPartUserControl)AutoPathListView.SelectedItem).Autopart;

                // Возможно вот здесь надо проверку на роль (пока нету)

                AutoPartInfoWindow autoPartInfoWindow = new AutoPartInfoWindow(autopart, enteredUser);
                autoPartInfoWindow.ShowDialog();

                //if (Passenger != null)
                //{
                //    TourInfo tourInfo = new TourInfo(tour, Passenger);
                //    tourInfo.ShowDialog();
                //}
                //else if (Captain != null)
                //{
                //    UpdateInsertTour updateInsertTour = new UpdateInsertTour(tour);
                //    updateInsertTour.ShowDialog();
                //}


                //else
                //{
                //    TourInfo tourInfo = new TourInfo(tour, null);
                //    tourInfo.ShowDialog();
                //}
            }
            RefreshWindow();
        }

        private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GoToUserProfileButton_Click(object sender, RoutedEventArgs e)
        {
            UserProfileWindow userProfileWindow = new UserProfileWindow(enteredUser);
            userProfileWindow.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
        }
    }
}
