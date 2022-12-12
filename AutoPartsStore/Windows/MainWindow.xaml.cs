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

            LoadListViews();
            RefreshWindow();
        }

        private void LoadListViews()
        {
            FilterListView.Items.Add("Все");
            FilterListView.Items.Add("Все");
            FilterListView.Items.Add("Все");
            FilterListView.Items.Add("Все");

            SortingListView.Items.Add("Все");
            SortingListView.Items.Add("Наименование ↑");
            SortingListView.Items.Add("Наименование ↓");
            SortingListView.Items.Add("Стоимость ↑");
            SortingListView.Items.Add("Стоимость ↓");

            FilterListView.SelectedIndex = 0;
            SortingListView.SelectedIndex = 0;
        }

        private void RefreshWindow()
        {
            List<Autopart> autopart = new List<Autopart>();
            autopart = context.Autopart.ToList();

            if (!string.IsNullOrEmpty(SearchTextBox.Text.ToLower()))
            {
                autopart = autopart.Where(a =>
                a.AutoPartName.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            }

            if (SortingListView.SelectedIndex != 1)
            {
                if (SortingListView.SelectedIndex == 1)
                {
                    autopart = autopart.OrderBy(a =>
                    a.AutoPartName).ToList();
                }
                else if (SortingListView.SelectedIndex == 2)
                {
                    autopart = autopart.OrderByDescending(a =>
                    a.AutoPartName).ToList();
                }
                else if (SortingListView.SelectedIndex == 3)
                {
                    autopart = autopart.OrderBy(a =>
                    a.Cost).ToList();
                }
                else if (SortingListView.SelectedIndex == 4)
                {
                    autopart = autopart.OrderByDescending(a =>
                    a.Cost).ToList();
                }
            }

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

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshWindow();
        }

        private void SortingListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshWindow();
        }

        private void FilterListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshWindow();
        }
    }
}
