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
using AutoPartsStore.Windows;
using AutoPartsStore.Models;

namespace AutoPartsStore.Windows.ManagerWindows
{
    /// <summary>
    /// Логика взаимодействия для EditInsertManufracturerWindow.xaml
    /// </summary>
    public partial class EditInsertManufracturerWindow : Window
    {
        db_autopartsstoreContext DbContext;
        public EditInsertManufracturerWindow()
        {
            InitializeComponent();
            DbContext = MainWindow.context;

            DeleteManufracturer.IsEnabled = false;
            EditManufracturer.IsEnabled = false;

            LoadComboBox();
            LoadListView();
        }

        private void LoadComboBox()
        {
            List<Country> displayCountry = new List<Country>();
            displayCountry = DbContext.Country.ToList();

            foreach (Country country in displayCountry)
            {
                CountryManufracturerComboBox.Items.Add(country.CountryName);
            }
            CountryManufracturerComboBox.SelectedIndex = 0;
        }

        private void LoadListView()
        {
            List<Manufracturer> displayManufracturer = new List<Manufracturer>();
            displayManufracturer = DbContext.Manufracturer.ToList();

            ManufracturerListView.Items.Clear();

            foreach (Manufracturer mfr in displayManufracturer)
            {
                Country country = DbContext.Country.Where(c =>
                c.IdCountry == mfr.IdCountry).FirstOrDefault();
                ManufracturerListView.Items.Add($"{mfr.IdManufracturer}: {mfr.ManufracturerName}" +
                    $"\nСтрана производства {country.CountryName}");
            }
        }

        private void ManufracturerListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ManufracturerListView.SelectedItem != null)
            {
                string[] temp = ManufracturerListView.SelectedItem.ToString().Split(':');

                Manufracturer manufracturer = DbContext.Manufracturer.Where(m =>
                m.IdManufracturer == Convert.ToInt32(temp[0])).FirstOrDefault();

                ManufracturerNameTextBox.Text = manufracturer.ManufracturerName;
                CountryManufracturerComboBox.SelectedIndex = manufracturer.IdCountry - 1;

                EditManufracturer.IsEnabled = true;
                DeleteManufracturer.IsEnabled = true;

            }
        }

        private void AddManufracturer_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ManufracturerNameTextBox.Text) &&
                CountryManufracturerComboBox.SelectedIndex != -1)
            {
                Manufracturer newManufracturer = new Manufracturer();
                newManufracturer.ManufracturerName = ManufracturerNameTextBox.Text;
                newManufracturer.IdCountry = CountryManufracturerComboBox.SelectedIndex + 1;

                DbContext.Manufracturer.Add(newManufracturer);
                DbContext.SaveChanges();

                MessageBox.Show("Производитель успешно добавлен", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                LoadListView();
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните поля", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void EditManufracturer_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ManufracturerNameTextBox.Text) &&
                CountryManufracturerComboBox.SelectedIndex != -1)
            {
                string[] temp = ManufracturerListView.SelectedItem.ToString().Split(':');

                Manufracturer selectedManufracturer = DbContext.Manufracturer.Where(m =>
                m.IdManufracturer == Convert.ToInt32(temp[0])).FirstOrDefault();

                selectedManufracturer.ManufracturerName = ManufracturerNameTextBox.Text;
                selectedManufracturer.IdCountry = CountryManufracturerComboBox.SelectedIndex + 1;

                MessageBox.Show("Производитель успешно изменён", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                DbContext.SaveChanges();
                LoadListView();
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните поля", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteManufracturer_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ManufracturerNameTextBox.Text) &&
                CountryManufracturerComboBox.SelectedIndex != -1)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить выбранного произодителя?" +
                    "\nВнимание, если вы удалите производителя, все товары связанные с ним так же будут удалены", 
                    "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) 
                    == MessageBoxResult.Yes)
                {
                    string[] temp = ManufracturerListView.SelectedItem.ToString().Split(':');

                    Manufracturer selectedManufracturer = DbContext.Manufracturer.Where(m =>
                    m.IdManufracturer == Convert.ToInt32(temp[0])).FirstOrDefault();

                    DbContext.Manufracturer.Remove(selectedManufracturer);
                    DbContext.SaveChanges();

                    MessageBox.Show("Производитель успешно удалён", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadListView();
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните поля", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
