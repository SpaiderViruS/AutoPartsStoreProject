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

namespace AutoPartsStore.Windows
{
    /// <summary>
    /// Логика взаимодействия для AutoPartInfoWindow.xaml
    /// </summary>
    public partial class AutoPartInfoWindow : Window
    {
        public static db_autopartsstoreContext DbContext;
        User currentUser { get; set; }
        Autopart Autopart { get; set; }
        public AutoPartInfoWindow(Autopart autopart, User user)
        {
            InitializeComponent();
            currentUser = user;

            LoadLabels();
            LoadImage();
        }

        private void LoadLabels()
        {
            //Status statusTest = db_autopartsstoreContext.DbContext.Status.Where(u =>
            //u.IdStatus == Autopart.IdStatusAutoPart).FirstOrDefault();
            Manufracturer manufracturer = db_autopartsstoreContext.DbContext.Manufracturer.Where(m =>
            m.IdManufracturer == Autopart.IdManufracturer).FirstOrDefault();

            Characteristik characteristik = db_autopartsstoreContext.DbContext.Characteristik.Where(m =>
            m.IdCharacteristik == Autopart.IdCharacteristik).FirstOrDefault();

            AutoPartNameLabel.Content = $"Название: {Autopart.AutoPartName}";
            ManufracturerLabel.Content = $"Производитель: {manufracturer.ManufracturerName}";
            CharacterisikLabel.Content = $"Характеристика: {characteristik.Applicability}";
            CostLabel.Content = $"Цена: {Autopart.Cost}";
        }

        private void LoadImage()
        {

        }
    }
}
