//using System.Linq;
//using System.Windows;
//using MemoryGame.DAL;
//using MemoryGame.Models;

//namespace MemoryGame
//{
//    public partial class MainWindow : Window
//    {
//        public MainWindow()
//        {
//            InitializeComponent();
//        }

//        private void LoadData_Click(object sender, RoutedEventArgs e)
//        {
//            LoadData();
//        }

//        private void LoadData()
//        {
//            using (var context = new MemoryGameContext())
//            {
//                var logins = context.Logins.ToList();

//                // Log the number of logins retrieved
//                MessageBox.Show($"Number of logins loaded: {logins.Count}");

//                // Update the ListView with the loaded data
//                LoginsListView.ItemsSource = logins;
//            }
//        }

//        private void AddUser_Click(object sender, RoutedEventArgs e)
//        {
//            AddUser();
//            LoadData(); // Refresh the data to show the new user
//        }

//        private void AddUser()
//        {
//            using (var context = new MemoryGameContext())
//            {
//                var newUser = new Login
//                {
//                    Username = "newuser",
//                    Password = "newpassword",  // Added Password
//                    StageId = 1 // Assuming stage with ID 1 exists
//                };
//                context.Logins.Add(newUser);
//                context.SaveChanges();
//            }
//        }
//    }
//}
