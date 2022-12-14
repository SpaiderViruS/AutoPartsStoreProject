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
    /// Логика взаимодействия для ManagersBusketAutopartsUserControl.xaml
    /// </summary>
    public partial class ManagersBusketAutopartsUserControl : UserControl
    {
        Busketautopart Busketautopart { get; set; }
        Autopart Autopart { get; set; }
        public ManagersBusketAutopartsUserControl(Busketautopart busketautopart)
        {
            InitializeComponent();
            Busketautopart = busketautopart;

            Autopart = db_autopartsstoreContext.DbContext.Autopart.Where(a =>
            a.IdAutoPart == Busketautopart.IdAutopart).FirstOrDefault();

            LoadLabels();
            LoadImage();
        }

        private void LoadLabels()
        {
            Characteristik characteristik = db_autopartsstoreContext.DbContext.Characteristik.Where(a =>
            a.Idmanufracturer == Autopart.IdManufracturer).FirstOrDefault();

            AutopartNameLabel.Content = $"Наименование: {Autopart.AutoPartName}";
            DescriptionAutopartLabel.Content = $"Описание: {Autopart.Description}";
            //CharacteristikLabel.Content = $"Применяемость: {characteristik.Description}";
            CostAutopartLabel.Content = $"Стоимость: {Autopart.Cost} ₽";
        }

        private void LoadImage()
        {
            if (Autopart.AutoPartImage != null && Autopart.AutoPartImage.Length > 0)
            {
                Uri resUri = new Uri(Environment.CurrentDirectory + Autopart.AutoPartImage);
                AutopartImage.Source = new BitmapImage(resUri);
            }
            else
            {
                AutopartImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/picture.png"));
            }
        }
    }
}
