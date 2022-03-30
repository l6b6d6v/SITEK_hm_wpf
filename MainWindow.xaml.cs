using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using static WpfApp1.Resources.ListFunctions;

using WpfApp1.Resources;
using System.Windows.Controls;

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


        private void SortButtonClick(object sender, RoutedEventArgs e)
        {
            Button Sortbtn = (Button)sender;
            string measurement = Sortbtn.Content.ToString();
                switch (measurement)
                {
                    case "По фамилии":
                        //Сортировка по фамилии ответственного исполнителя
                        OtchetForAddedElements.Sort((OtchetRecord x, OtchetRecord y) => { return x.GetIspolnitel().CompareTo(y.GetIspolnitel()); });
                        break;
                    case "По ркк":
                        //Сортировка по количеству РКК (в случае равенства – по количеству обращений);
                        OtchetForAddedElements.Sort((OtchetRecord x, OtchetRecord y) => { return y.GetCountRKK().CompareTo(x.GetCountRKK()); });
                        break;
                    case "По обращениям":
                        //Сортировка по количеству обращений (в случае равенства – по количеству РКК);
                        OtchetForAddedElements.Sort((OtchetRecord x, OtchetRecord y) => { return y.GetCountOBR().CompareTo(x.GetCountOBR()); });
                        break;
                    case "По общему количеству":
                        //Сортировка по общему количеству документов (в случае равенства – по количеству РКК)
                        OtchetForAddedElements.Sort((OtchetRecord x, OtchetRecord y) => { return y.GetCountRKK_OBR().CompareTo(x.GetCountRKK_OBR()); });
                        break;
                }

            // открытие нового окна
            OtchetPage Page = new OtchetPage();
            Page.Owner = this;
            Page.SortBy.Text = "Сортировка: " + measurement;
            Page.CountDocs.Text =
                "Не исполнено в срок ХХХ документов, из них:\n\n- количество неисполненных входящих документов: XXX;\n\n- количество неисполненных письменных обращений граждан: XXX;" ;
            //Page.qe.Text = "Неисполненные входящие документы: ";
            //Page.qe.Text = "Неисполненные письменные обращения граждан: ";
            
            Page.ShowDialog();
        }

        private List<OtchetRecord> OtchetForAddedElements = new List<OtchetRecord>();
        private void LoadFileRKKButtonClick(object sender, RoutedEventArgs e)
        {
            string pathRKK = buttonReadRKK.Text;

            Stopwatch WatchRKK = new Stopwatch();
            WatchRKK.Start();
            List<Document> DocumentsRKK = ListOfDocumentsFromFile(pathRKK);
            WatchRKK.Stop();


            List<OtchetRecord> FromRKK = FillTmpOtchet(DocumentsRKK);
            FillOtchet(OtchetForAddedElements, FromRKK);

        }

        private void LoadFileOBRButtonClick(object sender, RoutedEventArgs e)
        {
            string pathOBR = buttonReadOBR.Text;

            Stopwatch WatchOBR = new Stopwatch();
            WatchOBR.Start();
            List<Document> DocumentsOBR = ListOfDocumentsFromFile(pathOBR);
            WatchOBR.Stop();

            List<OtchetRecord> FromOBR = FillTmpOtchet(DocumentsOBR);
            FillOtchet(OtchetForAddedElements, FromOBR);
        }

        public static class CallBackMy
        {
            public delegate void callbackEvent(string what);
            public static callbackEvent callbackEventHandler;
        }
    }
}
