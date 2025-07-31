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
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for LoginWind.xaml
    /// </summary>
    public partial class LoginWind : Window
    {
        private string _username;
        private string _password;
        public bool MustClose { get; set; }
        public bool Fill { get; set; }

        public LoginWind()
        {
            InitializeComponent();
            MustClose = false;
            Fill = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MustClose = (Fill) ? false : true;
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DataIsValid())
            {
                _username = this.usernameTb.Text;
                _password = this.passwordPb.Password;
                Fill = true;
                this.Close();
            }
        }

        public List<string> GetLoginData()
        {
            List<string> data = new List<string>();
            data.Add(_username);
            data.Add(_password);
            return data;
        }
        public void ErrorData()
        {
            this.usernameTb.BorderBrush = Brushes.Red;
            this.passwordPb.BorderBrush = Brushes.Red;
            this.usernameTb.Text = "";
            this.passwordPb.Password = "";
        }

        private void ClearBorders()
        {
            this.usernameTb.BorderBrush = Brushes.LightCyan;
            this.passwordPb.BorderBrush = Brushes.LightCyan;
        }

        private bool DataIsValid()
        {
            ClearBorders();
            if (String.IsNullOrEmpty(this.usernameTb.Text) || String.IsNullOrWhiteSpace(this.usernameTb.Text))
            {
                this.usernameTb.BorderBrush = Brushes.Red;
                return false;
            }
            if (String.IsNullOrEmpty(this.passwordPb.Password) || String.IsNullOrWhiteSpace(this.passwordPb.Password))
            {
                this.passwordPb.BorderBrush = Brushes.Red;
                return false;
            }
            return true;
        }
    }
}