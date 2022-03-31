using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using static WpfApp1.Resources.ListFunctions;

using WpfApp1.Resources;
using System.Windows.Controls;
using System.IO;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static int rkkDocCount;
        public static int obrDocCount;
        public static Stopwatch WatchRKK = new Stopwatch();
        public static Stopwatch WatchOBR = new Stopwatch();
        public static string pathRKK;
        public static string pathOBR;
        public static string sortBy;
        private void SortButtonClick(object sender, RoutedEventArgs e)
        {
            Button Sortbtn = (Button)sender;
            string measurement = Sortbtn.Content.ToString();
            sortBy = measurement;
                switch (measurement)
                {
                    case "По фамилии ответственного исполнителя":
                        //Сортировка по фамилии ответственного исполнителя
                        OtchetForAddedElements.Sort((OtchetRecord x, OtchetRecord y) => { return x.GetIspolnitel().CompareTo(y.GetIspolnitel()); });
                        break;
                    case "По количеству РКК":
                        //Сортировка по количеству РКК (в случае равенства – по количеству обращений);
                        OtchetForAddedElements.Sort((OtchetRecord x, OtchetRecord y) => { return y.GetCountRKK().CompareTo(x.GetCountRKK()); });
                        break;
                    case "По количеству обращений":
                        //Сортировка по количеству обращений (в случае равенства – по количеству РКК);
                        OtchetForAddedElements.Sort((OtchetRecord x, OtchetRecord y) => { return y.GetCountOBR().CompareTo(x.GetCountOBR()); });
                        break;
                    case "По общему количеству документов":
                        //Сортировка по общему количеству документов (в случае равенства – по количеству РКК)
                        OtchetForAddedElements.Sort((OtchetRecord x, OtchetRecord y) => { return y.GetCountRKK_OBR().CompareTo(x.GetCountRKK_OBR()); });
                        break;
                }

            // открытие нового окна
            OtchetPage Page = new OtchetPage();
            Page.Owner = this;
            SetOtchet(OtchetForAddedElements);
            Console.WriteLine("SortButtonClick(" + measurement + ") = Clicked");
            Page.ShowDialog();
        }

        private static List<OtchetRecord> OtchetToPage;
        public static List<OtchetRecord> GetOtchet()
        {
            return OtchetToPage;
        }
        public static void SetOtchet(List<OtchetRecord> otchetRecords)
        {
                //OtchetToPage = otchetRecords;
                OtchetToPage = new List<OtchetRecord>();
            foreach (var record in otchetRecords)
                OtchetToPage.Add( record);
        }

        private List<OtchetRecord> OtchetForAddedElements = new List<OtchetRecord>();
        private void LoadFileRKKButtonClick(object sender, RoutedEventArgs e)
        {
            pathRKK = buttonReadRKK.Text;
            if (File.Exists(pathRKK) == true)
            {
                List<Document> DocumentsRKK = new List<Document>();
            DocumentsRKK.Clear();
            WatchRKK.Start();
            DocumentsRKK = ListOfDocumentsFromFile(pathRKK);
            WatchRKK.Stop();
            rkkDocCount = DocumentsRKK.Count;
                if (rkkDocCount == 0)
                    MessageBox.Show("Файл не заполнен!");
            List<OtchetRecord> FromRKK = FillTmpOtchet(DocumentsRKK);
            int exFile = 0; //RKK
            FillOtchet(OtchetForAddedElements, FromRKK, exFile);
            Console.WriteLine("LoadFileRKKButtonClick = Clicked");
            ButLoadFileRKK.IsEnabled = false;
            }
            else
                MessageBox.Show("Введите корректный путь файла!");
        }

        private void LoadFileOBRButtonClick(object sender, RoutedEventArgs e)
        {
            pathOBR = buttonReadOBR.Text;
            if (File.Exists(pathOBR) == true)
            {
                List<Document> DocumentsOBR = new List<Document>();
                DocumentsOBR.Clear();
                WatchOBR.Start();
                DocumentsOBR = ListOfDocumentsFromFile(pathOBR);
                WatchOBR.Stop();
                obrDocCount = DocumentsOBR.Count;
                if (obrDocCount == 0)
                    MessageBox.Show("Файл не заполнен!");
                List<OtchetRecord> FromOBR = FillTmpOtchet(DocumentsOBR);
                int exFile = 1; //RKK
                FillOtchet(OtchetForAddedElements, FromOBR, exFile);
                Console.WriteLine("LoadFileOBRButtonClick = Clicked");
                ButLoadFileOBR.IsEnabled = false;
            }
            else
                MessageBox.Show("Введите корректный путь файла!");
        }
    }
}
