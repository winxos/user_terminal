using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using System.Runtime.InteropServices;

namespace user_terminal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Normal;
            this.WindowStyle = System.Windows.WindowStyle.None;
            this.ResizeMode = System.Windows.ResizeMode.NoResize;
            this.Left = 0.0;
            this.Top = 0.0;
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            List<string> i = new List<string> { "水分", "容重", "杂质", "不完善粒" };
            Dictionary<string, string> dict = new Dictionary<string, string> { 
                { "水分", "14031500" }, { "容重", "1010701" }, { "杂质", "14031600" }, { "不完善粒", "1010401" },
                { "脂肪酸值", "4020100" }, { "重金属铅", "02010811" }, { "重金属镉", "02010812" }
            };
            index_type.ItemsSource = dict;
            index_type.SelectedIndex = 0;
        }
        public string http_get(string URL)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Method = "GET";
                request.Timeout = 1000;
                request.ContentType = "application/x-www-form-urlencoded";
                request.KeepAlive = false;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string s = reader.ReadToEnd();
                    return s;
                }
            }
            catch (Exception err)
            {
                lbl_msg.Content = err.Message;
            }
            Console.Read();
            return "failed";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            lbl_msg.Content = "waiting...";
            App.Current.Dispatcher.Invoke(() =>
            {
                lbl_msg.Content = http_get(string.Format("{0}/sinograin/sample/sign?card_code={1}",
                http_addr.Text, card_id.Text));
            });
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            lbl_msg.Content = "waiting...";
            App.Current.Dispatcher.Invoke(() =>
            {
                sample_id.Text = http_get(string.Format("{0}/sinograin/sample/getLastestSample", http_addr.Text));
                lbl_msg.Content = "ok";
            });
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            lbl_msg.Content = "waiting...";
            App.Current.Dispatcher.Invoke(() =>
            {
                KeyValuePair<string, string> kv = (KeyValuePair<string, string>)index_type.SelectedItem;
                lbl_msg.Content = http_get(string.Format("{0}/sinograin/sample/dataSubmit?sample_code={1}&index_code={2}&check_val={3}",
                    http_addr.Text, sample_id.Text, kv.Value, txt_val.Text));
            });
        }
    }
}
