using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Windows;
using WpfApp1.Resources;
using static WpfApp1.MainWindow;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для OtchetPage.xaml
    /// </summary>
    public partial class OtchetPage : Window
    {
        public OtchetPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        private ObservableCollection<OtchetView> coll;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<OtchetRecord> list = GetOtchet();

            if (coll == null)
            {
                coll = new ObservableCollection<OtchetView>();
                DG.ItemsSource = coll;
            }

            for (int i = 0; i < list.Count; i++)
            {
                coll.Add(new OtchetView() {
                    dg_nn = i + 1,
                    dg_ispolnitel = list[i].GetIspolnitel(),
                    dg_countRKK = list[i].GetCountRKK(),
                    dg_countOBR = list[i].GetCountOBR(),
                    dg_countRKK_OBR = list[i].GetCountRKK_OBR() });
            }
            


            DG.ItemsSource = coll;

            


            CountDocs.Text =
    "Не исполнено в срок " + (rkkDocCount + obrDocCount) + " документов, из них:\n\n" + 
    "- количество неисполненных входящих документов: " + rkkDocCount + ";\n\n" + 
    "- количество неисполненных письменных обращений граждан: " + obrDocCount + ";";

            SortBy.Text = "Сортировка по: " + sortBy;
            CreationDate.Text = "Дата составления справки: " + DateTime.Now.ToShortDateString();

        }

        private void Information_Click(object sender, RoutedEventArgs e)
        {

            char pathSplitter = '\\';


            string namePathRKK = pathRKK.Split(pathSplitter)[pathRKK.Split(pathSplitter).Length - 1];
            string namePathOBR = pathOBR.Split(pathSplitter)[pathOBR.Split(pathSplitter).Length - 1];
            //Не работает namePathRKK namePathOBR
            MessageBox.Show(
                "Время считывания файла \"" + namePathRKK + "\": " + WatchRKK.Elapsed + "\n" + 
                "Время считывания файла \"" + namePathOBR + "\": " + WatchOBR.Elapsed + "\n" +
                "Неисполненные входящие документы: " + namePathRKK + "\n" +
                "Неисполненные письменные обращения граждан: " + namePathOBR
                );

        }

        private void printDG()
        {

            //for (int i = 0; i < OtchetForAddedElements.Count; i++)
            //    writer.WriteLine($"{i,-10}{OtchetForAddedElements[i].GetIspolnitel(),-25}{OtchetForAddedElements[i].GetCountRKK(),-25}" +
            //        $"{OtchetForAddedElements[i].GetCountOBR(),-25}{OtchetForAddedElements[i].GetCountRKK_OBR(),-25}");

            string[] DGstring = new string[coll.Count];
            for (int i = 0; i < coll.Count; i++)
                DGstring[i] = $"{i,-10}{coll[i].dg_ispolnitel,-25}{coll[i].dg_countRKK,-25}" + $"{coll[i].dg_countOBR,-25}{coll[i].dg_countRKK_OBR,-25}";
        
            
        }
        private void PrintToFile_Click(object sender, RoutedEventArgs e)
        {
            string[] arr = { Head.Text, CountDocs.Text, SortBy.Text, CreationDate.Text };

            int len = coll.Count + arr.Length;
            string[] DGstring = new string[len];
            DGstring[0] = arr[0] + "\n"; DGstring[1] = arr[1] + "\n"; DGstring[2] = arr[2] + "\n";

            for (int i = 0; i < coll.Count; i++)
                DGstring[i+3] = $"{i + 1,-10}{coll[i].dg_ispolnitel,-25}{coll[i].dg_countRKK,-25}" + $"{coll[i].dg_countOBR,-25}{coll[i].dg_countRKK_OBR,-25}";
            DGstring[len - 1] = "\n" + arr[3];

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllLines(saveFileDialog.FileName, DGstring);
            }
        }
    }
}
