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
using MemoryGame.DAL;

namespace MemoryGame.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (AuthenticateUser(username, password, out int userStageId))
            {
                // Open the main window if authentication is successful
                var stagewindow = new StageWindow(userStageId);
                stagewindow.Show();
                this.Close(); // Close the login window
            }
            else
            {
                // Show an error message if authentication fails
                MessageBox.Show("Invalid username or password.", "Authentication Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool AuthenticateUser(string username, string password, out int userStageId)
        {
            using (var context = new MemoryGameContext())
            {
                // Hash the password if you are storing hashed passwords
                var hashedPassword = password; // This should be replaced with actual password hashing logic

                var user = context.Logins
                    .FirstOrDefault(u => u.Username == username && u.Password == hashedPassword);

                if(user != null)
                {
                    userStageId = user.StageId;
                    return true;
                }

                else
                {
                    userStageId = 0;
                    return false;
                }
            }
        }
    }
    
}
