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

namespace AutoPartsStore.Windows.AdministratorWindow
{
    /// <summary>
    /// Логика взаимодействия для AdministrationWindow.xaml
    /// </summary>
    public partial class AdministrationWindow : Window
    {
        db_autopartsstoreContext DbContext;
        User User;
        public AdministrationWindow(User user)
        {
            InitializeComponent();
            User = user;

            SNPLabel.Content = $"Администратор: {User.Name} {User.Surname} {User.Patronomyc}";

            DbContext = new db_autopartsstoreContext();

            EditManagerButton.IsEnabled = false;
            DeleteMangerButton.IsEnabled = false;

            UpdateListView();
        }

        private void UpdateListView()
        {
            ManagersListView.Items.Clear();
            List<User> displayManagers = new List<User>();
            displayManagers = DbContext.User.ToList();

            foreach (User user in displayManagers)
            {
                if (user.IdRole == 4)
                {
                    ManagersListView.Items.Add($"{user.IdUser}: {user.Name} {user.Surname} {user.Patronomyc}\n" +
                        $"Логин:{user.Login}\nПароль:{user.Password}");
                }
            }
        }

        private void AddNewManagerButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NameManagerTextBox.Text) && !string.IsNullOrEmpty(SurNameManagerTextBox.Text)
                && !string.IsNullOrEmpty(PatronomycManagerTextBox.Text) && !string.IsNullOrEmpty(LoginManagerTextBox.Text)
                && !string.IsNullOrEmpty(PasswordManagerTextBox.Text))
            {
                User checkManager = DbContext.User.Where(u =>
                u.Login == LoginManagerTextBox.Text.Trim()).FirstOrDefault();

                if (checkManager == null)
                {
                    User newUser = new User();
                    newUser.Name = NameManagerTextBox.Text.Trim();
                    newUser.Surname = SurNameManagerTextBox.Text.Trim();
                    newUser.Patronomyc = PatronomycManagerTextBox.Text.Trim();
                    newUser.Login = LoginManagerTextBox.Text.Trim();
                    newUser.Password = PasswordManagerTextBox.Text.Trim();
                    newUser.IdRole = 4;

                    DbContext.User.Add(newUser);
                    DbContext.SaveChanges();

                    MessageBox.Show("Менеджер успешно зарегистрирован", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    UpdateListView();
                }
                else
                {
                    MessageBox.Show("Логин уже занят", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста заполните поля", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void EditManagerButton_Click(object sender, RoutedEventArgs e)
        {
            if (ManagersListView.SelectedItem != null)
            {
                string[] temp = ManagersListView.SelectedItem.ToString().Split('\n');
                string[] split = temp[1].Split(':');

                User user = DbContext.User.Where(u =>
                u.Login == split[1]).FirstOrDefault();

                if (!string.IsNullOrEmpty(NameManagerTextBox.Text) && !string.IsNullOrEmpty(SurNameManagerTextBox.Text)
                && !string.IsNullOrEmpty(PatronomycManagerTextBox.Text) && !string.IsNullOrEmpty(LoginManagerTextBox.Text)
                && !string.IsNullOrEmpty(PasswordManagerTextBox.Text))
                {
                    user.Name = NameManagerTextBox.Text.Trim();
                    user.Surname = SurNameManagerTextBox.Text.Trim();
                    user.Patronomyc = PatronomycManagerTextBox.Text.Trim();
                    user.Login = LoginManagerTextBox.Text.Trim();
                    user.Password = PasswordManagerTextBox.Text.Trim();

                    MessageBox.Show("Менеджер отредактирован", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    DbContext.SaveChanges();
                    UpdateListView();
                }
                else
                {
                    MessageBox.Show("Пожалуйста заполните поля", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void DeleteMangerButton_Click(object sender, RoutedEventArgs e)
        {
            if (ManagersListView.SelectedItem != null)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить выбранного менеджера?", "Вопрос", 
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    string[] temp = ManagersListView.SelectedItem.ToString().Split('\n');
                    string[] split = temp[1].Split(':');

                    User user = DbContext.User.Where(u =>
                    u.Login == split[1]).FirstOrDefault();

                    DbContext.User.Remove(user);
                    DbContext.SaveChanges();

                    MessageBox.Show("Менеджер успешно удалён", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    UpdateListView();
                }
                else
                {
                    return;
                }
            }
        }

        private void ManagersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ManagersListView.SelectedItem != null)
            {
                //string[] temp = ManufracturerListView.SelectedItem.ToString().Split(':');
                string[] temp = ManagersListView.SelectedItem.ToString().Split('\n');
                string[] split = temp[1].Split(':');

                User user = DbContext.User.Where(u =>
                u.Login == split[1]).FirstOrDefault();

                NameManagerTextBox.Text = user.Name;
                SurNameManagerTextBox.Text = user.Surname;
                PatronomycManagerTextBox.Text = user.Patronomyc;
                LoginManagerTextBox.Text = user.Login;
                PasswordManagerTextBox.Text = user.Password;

                EditManagerButton.IsEnabled = true;
                DeleteMangerButton.IsEnabled = true;

            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
        }
    }
}
