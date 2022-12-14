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
using AutoPartsStore.Windows.AdministratorWindow;

namespace AutoPartsStore.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public static db_autopartsstoreContext DbContext;
        public AuthorizationWindow()
        {
            InitializeComponent();
            DbContext = new db_autopartsstoreContext();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(LoginTextBox.Text)
                && !string.IsNullOrEmpty(PasswordPasswordBox.Password))
            {
                User user = DbContext.User.Where(u => u.Login == LoginTextBox.Text.Trim()
                && u.Password == PasswordPasswordBox.Password).FirstOrDefault();

                if (user != null)
                {
                    if (user.IdRole != 2)
                    {
                        MainWindow mainWindow = new MainWindow(user);
                        mainWindow.Show();
                        Close();
                    }
                    else
                    {
                        AdministrationWindow administratorWindow = new AdministrationWindow(user);
                        administratorWindow.Show();
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("Неверный логин/пароль", "Ошибка", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Заполните поля", "Уведомление", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void EnterAsGuest_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(null);
            mainWindow.Show();
            Close();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow(null);
            registrationWindow.ShowDialog();
        }
    }
}
