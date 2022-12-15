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
using AutoPartsStore.Windows.UserWindows;

namespace AutoPartsStore.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        db_autopartsstoreContext DbContext;
        User User { get; set; }
        public RegistrationWindow(User user)
        {
            InitializeComponent();
            User = user;

            if (User != null)
            {
                RegistrationLabel.Content = "Редактирование";
                TryRegistration.Content = "Редактировать";
                Title = "Редактирование";
                LoginTextBox.Visibility = Visibility.Hidden;

                NameTextBox.Text = User.Name;
                SurnameTextBox.Text = User.Surname;
                PatronomycTextBox.Text = User.Patronomyc;
                PasswordPasswodBox.Password = User.Password;

                DbContext = UserProfileWindow.DbContext;
            }
            else
            {
                DbContext = AuthorizationWindow.DbContext;
            }
        }

        private void TryRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (User != null)
            {
                if (!string.IsNullOrEmpty(NameTextBox.Text) && !string.IsNullOrEmpty(PasswordPasswodBox.Password))
                {
                    User selectedUser = DbContext.User.Where(u =>
                    u.IdUser == User.IdUser).FirstOrDefault();

                    selectedUser.Name = NameTextBox.Text;
                    selectedUser.Surname = SurnameTextBox.Text;
                    selectedUser.Patronomyc = PatronomycTextBox.Text;
                    selectedUser.Password = PasswordPasswodBox.Password;

                    DbContext.SaveChanges();

                    MessageBox.Show("Информация отредактирована", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    Close();
                }
            }
            else 
            {
                if (!string.IsNullOrEmpty(NameTextBox.Text) && !string.IsNullOrEmpty(LoginTextBox.Text)
                    && !string.IsNullOrEmpty(PasswordPasswodBox.Password))
                {
                    User checkLogin = DbContext.User.Where(u =>
                    u.Login == LoginTextBox.Text).FirstOrDefault();

                    if (checkLogin == null)
                    {
                        User newUser = new User();
                        newUser.Surname = SurnameTextBox.Text;
                        newUser.Patronomyc = PatronomycTextBox.Text;
                        newUser.Name = NameTextBox.Text;
                        newUser.Login = LoginTextBox.Text;
                        newUser.Password = PasswordPasswodBox.Password;
                        newUser.Image = null;
                        newUser.IdRole = 1;

                        DbContext.User.Add(newUser);
                        DbContext.SaveChanges();

                        MessageBox.Show("Вы успешно зарегистрировались", "Информация",
                            MessageBoxButton.OK, MessageBoxImage.Information);

                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Данный логин уже занят, попробуйте другой", "Информация",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста заполните обязательные поля", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            //authorizationWindow.Show();
        }

        private void NameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Char.IsDigit(e.Text, 0);
        }
    }
}
