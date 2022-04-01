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
                    dg_countRKK_OBR = list[i].GetCountRKK_OBR() 
                });
            }

            DG.ItemsSource = coll;
            
            dgname_nn.Header = "№ п.п.";
            dgname_ispolnitel.Header = "Ответственный\nисполнитель";
            dgname_countRKK.Header = "Количество\nнеисполненных\nвходящих документов";
            dgname_countOBR.Header = "Количество\nнеисполненных\nписьменных\nобращений граждан";
            dgname_countRKK_OBR.Header = "Общее количество\nдокументов и\nобращений";

            CountDocs.Text =
            "Не исполнено в срок " + (rkkDocCount + obrDocCount) + " документов, из них:\n\n" + 
            "- количество неисполненных входящих документов: " + rkkDocCount + ";\n\n" + 
            "- количество неисполненных письменных обращений граждан: " + obrDocCount + ";";

            SortBy.Text = "Сортировка по: " + sortBy;
            CreationDate.Text = "Дата составления справки: " + DateTime.Now.ToShortDateString();

        }

        private void Information_Click(object sender, RoutedEventArgs e)
        {

            char[] pathSplitter = {'\\', '/'};

            if (pathRKK == null)
                pathRKK = "%файл отсутствует%";
            if (pathOBR == null)
                pathOBR = "%файл отсутствует%";
            string namePathRKK = pathRKK.Split(pathSplitter)[pathRKK.Split(pathSplitter).Length - 1];
            string namePathOBR = pathOBR.Split(pathSplitter)[pathOBR.Split(pathSplitter).Length - 1];
            MessageBox.Show(
                "Неисполненные входящие документы: " + namePathRKK + "\n" +
                "Время считывания файла \"" + namePathRKK + "\": " + WatchRKK.Elapsed + "\n" + 
                "Неисполненные письменные обращения граждан: " + namePathOBR + "\n" +
                "Время считывания файла \"" + namePathOBR + "\": " + WatchOBR.Elapsed
                );

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
                File.WriteAllLines(saveFileDialog.FileName, DGstring);
        }
    }
}
