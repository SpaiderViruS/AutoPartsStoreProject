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

namespace AutoPartsStore.Windows.ManagerWindows
{
    /// <summary>
    /// Логика взаимодействия для EditInsertCharacteristik.xaml
    /// </summary>
    public partial class EditInsertCharacteristik : Window
    {
        db_autopartsstoreContext DbContext;
        public EditInsertCharacteristik()
        {
            InitializeComponent();
            DbContext = MainWindow.context;

            DeleteCharacteristik.IsEnabled = false;
            EditCharacteristik.IsEnabled = false;

            LoadComboBox();
            LoadListView();
        }

        private void LoadComboBox()
        {
            List<Manufracturer> displayManufracturer = new List<Manufracturer>();
            displayManufracturer = DbContext.Manufracturer.ToList();

            foreach (Manufracturer manufracturer in displayManufracturer)
            {
                ManufracturerComboBox.Items.Add(manufracturer.ManufracturerName);
            }
            ManufracturerComboBox.SelectedIndex = 0;
        }

        private void LoadListView()
        {
            List<Characteristik> displayCharacteristik = new List<Characteristik>();
            displayCharacteristik = DbContext.Characteristik.ToList();

            CharacteristikListView.Items.Clear();

            foreach (Characteristik characteristik in displayCharacteristik)
            {
                Manufracturer manufracturer = DbContext.Manufracturer.Where(c =>
                c.IdManufracturer == characteristik.Idmanufracturer).FirstOrDefault();

                CharacteristikListView.Items.Add($"{characteristik.IdCharacteristik}: {characteristik.Description}" +
                    $"\nПроизводитель {manufracturer.ManufracturerName}");
            }
        }

        private void CharacteristikListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CharacteristikListView.SelectedItem != null)
            {
                string[] temp = CharacteristikListView.SelectedItem.ToString().Split(':');

                Characteristik characteristik = DbContext.Characteristik.Where(m =>
                m.IdCharacteristik == Convert.ToInt32(temp[0])).FirstOrDefault();

                CharacteristikNameTextBox.Text = characteristik.Description;
                ManufracturerComboBox.SelectedIndex = characteristik.Idmanufracturer - 1;

                DeleteCharacteristik.IsEnabled = true;
                EditCharacteristik.IsEnabled = true;

            }
        }

        private void AddCharacteristik_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(CharacteristikNameTextBox.Text) &&
                ManufracturerComboBox.SelectedIndex != -1)
            {
                Manufracturer selectedManufracturer = DbContext.Manufracturer.Where(m =>
                m.ManufracturerName.ToLower().Contains(ManufracturerComboBox.SelectedItem.ToString().ToLower())).FirstOrDefault();

                Characteristik checkCharacteristik = DbContext.Characteristik.Where(c =>
                c.Description.ToLower().Contains(CharacteristikNameTextBox.Text.ToLower())
                && c.Idmanufracturer == selectedManufracturer.IdManufracturer).FirstOrDefault();

                if (checkCharacteristik == null)
                {

                    Characteristik newcharacteristik = new Characteristik();
                    newcharacteristik.Description = CharacteristikNameTextBox.Text;
                    newcharacteristik.Idmanufracturer = selectedManufracturer.IdManufracturer;

                    DbContext.Characteristik.Add(newcharacteristik);
                    DbContext.SaveChanges();

                    MessageBox.Show("Характеристика успешно добавлена", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);                    
                }
                else
                {
                    MessageBox.Show("Такая характеристика уже существует", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
                LoadListView();
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните поля", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void EditCharacteristik_Click(object sender, RoutedEventArgs e)
        {
            if (CharacteristikListView.SelectedItem != null)
            {
                if (!string.IsNullOrEmpty(CharacteristikNameTextBox.Text) &&
                    ManufracturerComboBox.SelectedIndex != -1)
                {
                    string[] temp = CharacteristikListView.SelectedItem.ToString().Split(':');

                    Characteristik selectedCharacteristik = DbContext.Characteristik.Where(m =>
                    m.IdCharacteristik == Convert.ToInt32(temp[0])).FirstOrDefault();

                    Manufracturer selectedManufracturer = DbContext.Manufracturer.Where(m =>
                    m.ManufracturerName.ToLower().Contains(ManufracturerComboBox.SelectedItem.ToString().ToLower())).FirstOrDefault();

                    selectedCharacteristik.Description = CharacteristikNameTextBox.Text;
                    selectedCharacteristik.Idmanufracturer = selectedManufracturer.IdManufracturer;

                    MessageBox.Show("Характеристика успешно изменена", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadListView();
                }
                else
                {
                    MessageBox.Show("Пожалуйста, заполните поля", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void DeleteCharacteristik_Click(object sender, RoutedEventArgs e)
        {
            if (CharacteristikListView.SelectedItem != null)
            {
                if (!string.IsNullOrEmpty(CharacteristikNameTextBox.Text) &&
                    ManufracturerComboBox.SelectedIndex != -1)
                {
                    if (MessageBox.Show("Вы уверены, что хотите удалить выбранную характеристику?" +
                        "\nВнимание, если вы удалите характеристику, все товары связанные с ним так же будут удалены",
                        "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question)
                        == MessageBoxResult.Yes)
                    {
                        string[] temp = CharacteristikListView.SelectedItem.ToString().Split(':');

                        Characteristik selectedCharacteristik = DbContext.Characteristik.Where(m =>
                        m.IdCharacteristik == Convert.ToInt32(temp[0])).FirstOrDefault();

                        DbContext.Characteristik.Remove(selectedCharacteristik);
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
}
