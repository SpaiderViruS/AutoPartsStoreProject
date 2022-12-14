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

namespace AutoPartsStore.Windows.ManagerWindows
{
    /// <summary>
    /// Логика взаимодействия для AccountingReportWindow.xaml
    /// </summary>
    public partial class AccountingReportWindow : Window
    {
        db_autopartsstoreContext DbContext;
        bool StartDate = false;
        bool EndDate = false;

        public AccountingReportWindow()
        {
            InitializeComponent();
            DbContext = new db_autopartsstoreContext();

            UpdateListView();
        }

        private void UpdateListView()
        {
            OrdersListView.Items.Clear();
            List<Order> displayOrder = new List<Order>();
            displayOrder = DbContext.Order.ToList();

            if (StartDate && EndDate)
            {
                if (StartDatePicker.SelectedDate != null &&
                    EndDatePicker.SelectedDate != null)
                {
                    displayOrder = displayOrder.Where(o =>
                    o.DateOrder >= StartDatePicker.SelectedDate &&
                    o.DateOrder <= EndDatePicker.SelectedDate).ToList();
                }
            }

            foreach(Order order in displayOrder)
            {
                OrdersListView.Items.Add(new AccountingOrderUserControl(order)
                {
                    Width = GetOptimizedWidth()
                });
            }

            if (OrdersListView.Items.Count <= 0)
            {
                StartAccounting.IsEnabled = false;
            }
            else
            {
                StartAccounting.IsEnabled = true;
            }
        }

        private double GetOptimizedWidth()
        {
            if (WindowState == WindowState.Maximized)
            {
                return (RenderSize.Width - 55) / 1.25;
            }
            else
            {
                return (Width - 55) / 1.25;
            }
        }

        private void StartDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            StartDate = true;
            UpdateListView();
        }

        private void EndDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            EndDate = true;
            UpdateListView();
        }

        private void StartAccounting_Click(object sender, RoutedEventArgs e)
        {
            if (StartDate && EndDate)
            {
                List<Order> selectedOrders = new List<Order>();

                foreach (AccountingOrderUserControl accountingUserCotrol in
                    OrdersListView.Items)
                {
                    selectedOrders.Add(accountingUserCotrol.Order);
                }

                if (selectedOrders.Count != 0)
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
                    range.Text = $"Отчёт 1\n".ToUpper();

                    range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    range.ParagraphFormat.SpaceAfter = 0;
                    range.Font.Name = "Times New Roman";

                    range.Font.Size = 14;

                    start = wordDoc.Range().End - 1; end = wordDoc.Range().End - 1;
                    range = wordDoc.Range(ref start, ref end);

                    range.Text = $"Период от: {StartDatePicker.SelectedDate} по {EndDatePicker.SelectedDate}\n";

                    range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    range.ParagraphFormat.SpaceAfter = 0;
                    range.Font.Name = "Times New Roman";
                    range.Font.Size = 14;



                    start = wordDoc.Range().End - 1; end = wordDoc.Range().End - 1;
                    range = wordDoc.Range(ref start, ref end);

                    Word.Table table = wordDoc.Tables.Add(range, selectedOrders.Count + 1, 3, missing, missing);

                    table.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    table.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;

                    table.Range.Font.Name = "Times New Roman";

                    table.Cell(1, 1).Range.Text = "Автозапчасти";
                    table.Cell(1, 2).Range.Text = "Кол - во проданных";
                    table.Cell(1, 3).Range.Text = "Общая сумма";

                    int profit = 0;

                    for (int i = 0; i < selectedOrders.Count; i++)
                    {
                        table.Cell(i + 2, 1).Range.Text = selectedOrders[i].IdBusket.ToString();

                        table.Cell(i + 2, 1).Range.Font.Size = 14;

                        List<Busketautopart> temp = new List<Busketautopart>();
                        temp = DbContext.Busketautopart.Where(t => t.IdBusket == selectedOrders[i].IdBusket).ToList();

                        table.Cell(i + 2, 2).Range.Text = temp.Count().ToString();

                        table.Cell(i + 2, 2).Range.Font.Size = 14;

                        table.Cell(i + 2, 3).Range.Text = selectedOrders[i].TotalCost.ToString();
                        profit += selectedOrders[i].TotalCost;

                        table.Cell(i + 2, 3).Range.Font.Size = 14;
                    }

                    start = wordDoc.Range().End - 1; end = wordDoc.Range().End - 1;
                    range = wordDoc.Range(ref start, ref end);
                    range.Text = $"\nВыручка: {profit} ₽";
                    range.Font.Name = "Times New Roman";
                    range.Font.Size = 14;

                    start = wordDoc.Range().End - 1; end = wordDoc.Range().End - 1;
                    range = wordDoc.Range(ref start, ref end);
                    range.Text = "\nПодпись: ____________";
                    range.Font.Name = "Times New Roman";
                    range.Font.Size = 14;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста выберите период", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }
    }
}
