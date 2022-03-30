using System;
using System.Windows;


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
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CountDocs.Text = @"Не использовано в срок XXX документов, из них:

 - количество неисполненных входящих документов - XXX

 - количество неисп ОБР - XXX";

            CreationDate.Text = "Дата составления справки: " + DateTime.Now.ToShortDateString();
        }
    }
}
