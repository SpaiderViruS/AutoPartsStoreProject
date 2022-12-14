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
using AutoPartsStore.UserControls;
using Word = Microsoft.Office.Interop.Word;

namespace AutoPartsStore.Windows.UserWindows
{
    /// <summary>
    /// Логика взаимодействия для UserOrdersWindow.xaml
    /// </summary>
    public partial class UserOrdersWindow : Window
    {
        db_autopartsstoreContext DbContext;
        User User { get; set; }
        public UserOrdersWindow(User user)
        {
            InitializeComponent();
            User = user;
            DbContext = UserProfileWindow.DbContext;

            LoadListView();
        }

        private void LoadListView()
        {
            OrdersListView.Items.Clear();

            List<Order> displayOrder = new List<Order>();
            displayOrder = DbContext.Order.ToList();

            Busket busket = DbContext.Busket.Where(b =>
            b.IdUser == User.IdUser).FirstOrDefault();

            foreach (Order order in displayOrder)
            {
                if (busket.IdBusket == order.IdBusket)
                {
                    if (busket.OrderStatus != "Формируется")
                    {
                        OrdersListView.Items.Add(new OrderUserControl(order)
                        {
                            Width = GetOptimizedWidth()
                        });
                    }
                }
            }
            if (OrdersListView.Items.Count <= 0)
            {
                List<Busketautopart> displayBusket = new List<Busketautopart>();
                displayBusket = DbContext.Busketautopart.ToList();

                foreach (Busketautopart bsk in displayBusket)
                {
                    if (bsk.IdBusket == busket.IdBusket)
                    {
                        if (busket.OrderStatus != "Формируется")
                        {
                            OrdersListView.Items.Add(new UserBusketUserControl(bsk)
                            {
                                Width = GetOptimizedWidth()
                            });
                        }
                    }
                }
                AccountingCheckBtn.IsEnabled = false;
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

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AccountingCheckBtn_Click(object sender, RoutedEventArgs e)
        {
            List<Busketautopart> displayBusket = new List<Busketautopart>();
            displayBusket = DbContext.Busketautopart.ToList();

            foreach (OrderUserControl ubc in
                    OrdersListView.Items)
            {
                displayBusket.Add(ubc.Busketautopart);
            }

            if (OrdersListView.Items.Count > 0)
            {
                Word.Application wordApp = new Word.Application();
                wordApp.Visible = true;
                Object template = Type.Missing;
                Object newTemplate = false;
                Object documentType = Word.WdNewDocumentType.wdNewBlankDocument;
                Object visible = true;
                object missing = Type.Missing;
                Word._Document wordDoc = wordApp.Documents.Add(
                    ref missing, ref missing, ref missing, ref missing);
                object start = 0, end = 0;

                Word.Range range = wordDoc.Range(ref start, ref end);
                range.Text = $"Чек 1\n".ToUpper();

                range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                range.ParagraphFormat.SpaceAfter = 0;
                range.Font.Name = "Times New Roman";
                range.Font.Size = 14;

                start = wordDoc.Range().End - 1; end = wordDoc.Range().End - 1;
                range = wordDoc.Range(ref start, ref end);

                range.Text = $"Компания Би-би\n";
#warning НЕ СКЛАДЫВАЕТСЯ КОЛИЧЕСТВО
                range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                range.ParagraphFormat.SpaceAfter = 0;
                range.Font.Name = "Times New Roman";
                range.Font.Size = 14;

                start = wordDoc.Range().End - 1; end = wordDoc.Range().End - 1;
                range = wordDoc.Range(ref start, ref end);

                range.Text = $"Дата: {DateTime.Now.ToShortDateString()}\n";

                range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                range.ParagraphFormat.SpaceAfter = 0;
                range.Font.Name = "Times New Roman";
                range.Font.Size = 14;

                start = wordDoc.Range().End - 1; end = wordDoc.Range().End - 1;
                range = wordDoc.Range(ref start, ref end);

                range.Text = $"Клиент: {User.Name} {User.Surname} {User.Patronomyc}\n";

                range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                range.ParagraphFormat.SpaceAfter = 0;
                range.Font.Name = "Times New Roman";
                range.Font.Size = 14;

                start = wordDoc.Range().End - 1; end = wordDoc.Range().End - 1;
                range = wordDoc.Range(ref start, ref end);

                Word.Table table = wordDoc.Tables.Add(range, displayBusket.Count + 1, 5, missing, missing);

                table.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                table.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;

                table.Range.Font.Name = "Times New Roman";

                table.Cell(1, 1).Range.Text = "Артикул";
                table.Cell(1, 2).Range.Text = "Запчасти";
                table.Cell(1, 3).Range.Text = "Цена за единицу";
                table.Cell(1, 4).Range.Text = "Кол - во";
                table.Cell(1, 5).Range.Text = "Общая цена";

                int profit = 0;

                for (int i = 0; i < displayBusket.Count; i++)
                {
                    table.Cell(i + 2, 1).Range.Text = displayBusket[i].IdAutopartNavigation.IdAutoPart.ToString();

                    table.Cell(i + 2, 1).Range.Font.Size = 14;                                       

                    table.Cell(i + 2, 2).Range.Text = displayBusket[i].IdAutopartNavigation.AutoPartName.ToString();

                    table.Cell(i + 2, 2).Range.Font.Size = 14;


                    table.Cell(i + 2, 3).Range.Text = displayBusket[i].IdAutopartNavigation.Cost.ToString();
                    profit += displayBusket[i].IdAutopartNavigation.Cost;

                    table.Cell(i + 2, 3).Range.Font.Size = 14;

                    List<Busketautopart> temp = new List<Busketautopart>();
                    temp = DbContext.Busketautopart.Where(b =>
                    b.IdBusketAutopart == displayBusket[i].IdBusketAutopart).ToList();
                    temp = temp.Where(b =>
                    b.IdAutopart == displayBusket[i].IdAutopart).ToList();

                    table.Cell(i + 2, 4).Range.Text = temp.Count.ToString();

                    table.Cell(i + 2, 4).Range.Font.Size = 14;

                    int tempProfit = 0;
                    foreach (Busketautopart busketautopart in temp)
                    {
                        tempProfit += busketautopart.IdAutopartNavigation.Cost;
                    }

                    table.Cell(i + 2, 5).Range.Text = tempProfit.ToString();

                    table.Cell(i + 2, 5).Range.Font.Size = 14;
                }

                start = wordDoc.Range().End - 1; end = wordDoc.Range().End - 1;
                range = wordDoc.Range(ref start, ref end);
                range.Text = $"\nОбщая цена: {profit} ₽";
                range.Font.Name = "Times New Roman";
                range.Font.Size = 14;
            }
        }
    }
}
