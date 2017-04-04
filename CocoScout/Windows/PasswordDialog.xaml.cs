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

namespace CocoScout.Windows
{
    /// <summary>
    /// Interaction logic for PasswordDialog.xaml
    /// </summary>
    public partial class PasswordDialog : Window
    {
        public static bool IsAllowed = false;

        public PasswordDialog()
        {
            InitializeComponent();
        }

        private void OKEvent(object sender, RoutedEventArgs e)
        {
            if(PasswordEntry.Password == "@CocoNuts")
            {
                IsAllowed = true;
                this.Close();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Password is incorrect.", "Please ask for the password.", MessageBoxButton.OK);
            }
        }

        private void CancelEvent(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
