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
using Microsoft.Win32;
using System.IO;

namespace AutoPartsStore.Windows.ManagerWindows
{
    /// <summary>
    /// Логика взаимодействия для EditInsertAutoPartWindow.xaml
    /// </summary>
    public partial class EditInsertAutoPartWindow : Window
    {
        db_autopartsstoreContext DbContext;
        Autopart Autopart;
        OpenFileDialog openFileDialog = new OpenFileDialog();
        string photoPath = null;
        string savingIndex;

        public EditInsertAutoPartWindow(Autopart autopart)
        {
            InitializeComponent();
            DbContext = MainWindow.context;
            Autopart = autopart;

            LoadComboBoxes();
            LoadInfos();
        }

        private void LoadComboBoxes()
        {
            ManufracturerComboBox.Items.Clear();
            CharacteristikComboBox.Items.Clear();
            StatusComboBox.Items.Clear();

            List<Status> displayStatus = new List<Status>();
            displayStatus = DbContext.Status.ToList();

            List<Characteristik> displayCharacteristik = new List<Characteristik>();
            displayCharacteristik = DbContext.Characteristik.ToList();

            List<Manufracturer> displayManufracturer = new List<Manufracturer>();
            displayManufracturer = DbContext.Manufracturer.ToList();

            foreach (Manufracturer mf in displayManufracturer)
            {
                ManufracturerComboBox.Items.Add(mf.ManufracturerName);
            }
            foreach (Characteristik ch in displayCharacteristik)
            {
                CharacteristikComboBox.Items.Add(ch.Description);
            }
            foreach (Status st in displayStatus)
            {
                StatusComboBox.Items.Add(st.StatusName);
            }

            if (savingIndex != null)
            {
                string[] split = savingIndex.Split(';');
                ManufracturerComboBox.SelectedIndex = Convert.ToInt32(split[0]);
                CharacteristikComboBox.SelectedIndex = Convert.ToInt32(split[1]);
                StatusComboBox.SelectedIndex = Convert.ToInt32(split[2]);
            }

            ManufracturerComboBox.SelectedIndex = 0;
            CharacteristikComboBox.SelectedIndex = 0;
            StatusComboBox.SelectedIndex = 0;
        }

        private void LoadInfos()
        {
            if (Autopart != null)
            {
                Manufracturer manufracturer = DbContext.Manufracturer.Where(m =>
                m.IdManufracturer == Autopart.IdManufracturer).FirstOrDefault();

                Characteristik characteristik = DbContext.Characteristik.Where(c =>
                c.IdCharacteristik == Autopart.IdCharacteristik).FirstOrDefault();

                Status status = DbContext.Status.Where(s =>
                s.IdStatus == Autopart.IdStatusAutoPart).FirstOrDefault();

                AutoPartNameTextBox.Text = Autopart.AutoPartName;
                CostTextBox.Text = Autopart.Cost.ToString();

                ManufracturerComboBox.SelectedIndex = manufracturer.IdManufracturer - 1;
                CharacteristikComboBox.SelectedIndex = characteristik.IdCharacteristik - 1;
                StatusComboBox.SelectedIndex = status.IdStatus - 1;
                LoadImage();
            }
            else
            {
                RemoveAutoPartButton.Visibility = Visibility.Collapsed;
                Title = "Добавление новой запчасти";
                EditAutoPartButton.Content = "Добавить запчасть";
                LoadImageNullShip();
            }
        }

        

        private void EditAutoPartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(AutoPartNameTextBox.Text) && !string.IsNullOrWhiteSpace(CostTextBox.Text)
                && ManufracturerComboBox.SelectedIndex != -1 && CharacteristikComboBox.SelectedIndex != -1
                && StatusComboBox.SelectedIndex != -1)
            {
                Manufracturer tempManufracturer = DbContext.Manufracturer.Where(m =>
                ManufracturerComboBox.Text.Contains(m.ManufracturerName)).FirstOrDefault();

                Characteristik tempCharacteristik = DbContext.Characteristik.Where(c =>
                CharacteristikComboBox.Text.Contains(c.Description)).FirstOrDefault();

                int manufracturerID = tempManufracturer.IdManufracturer;
                int characteristikID = tempCharacteristik.IdCharacteristik;
                int statusID = StatusComboBox.SelectedIndex + 1;

                if (EditAutoPartButton.Content.ToString() == "Редактировать запчасть")
                {
                    if (photoPath != null)
                    {
                        string photoName = "\\Images\\" + System.IO.Path.GetRandomFileName() + ".jpg";
                        File.Copy(photoPath, Environment.CurrentDirectory + photoName, true);
                        Autopart.AutoPartImage = photoName;
                    }

                    Autopart.AutoPartName = AutoPartNameTextBox.Text;
                    Autopart.Cost = Convert.ToInt32(CostTextBox.Text);
                    Autopart.IdManufracturer = manufracturerID;
                    Autopart.IdCharacteristik = characteristikID;
                    Autopart.IdStatusAutoPart = statusID;

                    DbContext.SaveChanges();

                    MessageBox.Show("Автозапчасть успешно отредактировано", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                else
                {
                    Autopart newAutopart = new Autopart();

                    if (photoPath != null)
                    {
                        string photoName = "\\Images\\" + System.IO.Path.GetRandomFileName() + ".jpg";
                        File.Copy(photoPath, Environment.CurrentDirectory + photoName, true);
                        newAutopart.AutoPartImage = photoName;
                    }

                    newAutopart.AutoPartName = AutoPartNameTextBox.Text;
                    newAutopart.Cost = Convert.ToInt32(CostTextBox.Text);
                    newAutopart.IdManufracturer = manufracturerID;
                    newAutopart.IdCharacteristik = characteristikID;
                    newAutopart.IdStatusAutoPart = statusID;

                    DbContext.Autopart.Add(newAutopart);
                    DbContext.SaveChanges();

                    MessageBox.Show("Автозапчасть успешно добавлена", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void RemoveAutoPartButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запчасть?", "Предупреждение",
               MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                DbContext.Autopart.Remove(Autopart);
                DbContext.SaveChanges();

                MessageBox.Show("Запчасть успешно удалена", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            else
            {
                return;
            }

        }

        private void LoadImage()
        {
            if (Autopart.AutoPartImage != null && Autopart.AutoPartImage.Length > 0)
            {
                Uri resourceUri = new Uri(Environment.CurrentDirectory + Autopart.AutoPartImage);
                AutoPartImageBox.Source = new BitmapImage(resourceUri);
            }
            else
            {
                AutoPartImageBox.Source = new BitmapImage(new Uri("pack://application:,,,/Images/picture.png"));
                photoPath = null;
            }
        }

        private void LoadImageNullShip()
        {
            AutoPartImageBox.Source = new BitmapImage(new Uri("pack://application:,,,/Images/picture.png"));
            photoPath = null;
        }

        private void AddEditImage_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                photoPath = openFileDialog.FileName;
                AutoPartImageBox.Source = new BitmapImage(new Uri(photoPath));
            }
        }

        private void CostTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void CostTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void AddEditInsertManufracturerButton_Click(object sender, RoutedEventArgs e)
        {
            EditInsertManufracturerWindow editInsertManufracturerWindow = new EditInsertManufracturerWindow();
            
            savingIndex = $"{ManufracturerComboBox.SelectedIndex};{CharacteristikComboBox.SelectedIndex};{StatusComboBox.SelectedIndex}";

            editInsertManufracturerWindow.ShowDialog();
            LoadComboBoxes();
        }

        private void AddEditInsertCharacteristikButton_Click(object sender, RoutedEventArgs e)
        {
#warning Нету характеристики
        }
    }
}
