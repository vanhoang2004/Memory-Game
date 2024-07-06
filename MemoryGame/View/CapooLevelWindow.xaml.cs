using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MemoryGame.View
{
    public partial class CapooLevelWindow : Window
    {
        private List<string> imagePaths = new List<string>
        {
            "pack://application:,,,/MemoryGame;component/Asset/Capoo/capoo2.gif",
            "pack://application:,,,/MemoryGame;component/Asset/Capoo/capoo3.png",
            "pack://application:,,,/MemoryGame;component/Asset/Capoo/capoo4.png",
            "pack://application:,,,/MemoryGame;component/Asset/Capoo/capoo5.png",
            "pack://application:,,,/MemoryGame;component/Asset/Capoo/capoo6.png",
            "pack://application:,,,/MemoryGame;component/Asset/Capoo/capoo7.gif",
            "pack://application:,,,/MemoryGame;component/Asset/Capoo/capoo8.png",
            "pack://application:,,,/MemoryGame;component/Asset/Capoo/capoo9.jpg"
        };

        private List<Button> buttons;
        private Button firstClick, secondClick;
        private DispatcherTimer timer;

        public CapooLevelWindow()
        {
            InitializeComponent();
            InitializeGameBoard();
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Timer_Tick;
        }

        private void InitializeGameBoard()
        {
            var pairedImages = imagePaths.Concat(imagePaths).ToList();
            var random = new Random();
            var shuffledImages = pairedImages.OrderBy(x => random.Next()).ToList();

            buttons = new List<Button>();
            int rows = MemoryGrid.RowDefinitions.Count;
            int cols = MemoryGrid.ColumnDefinitions.Count;
            int index = 0;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (index >= shuffledImages.Count)
                        break;

                    var button = new Button
                    {
                        Width = 100, // Adjust the size as needed
                        Height = 100, // Adjust the size as needed
                        Margin = new Thickness(5),
                        Tag = shuffledImages[index]
                    };
                    button.Click += Button_Click;

                    var image = new Image
                    {
                        Source = new BitmapImage(new Uri(shuffledImages[index], UriKind.Absolute)),
                        Visibility = Visibility.Collapsed // Hide images initially
                    };

                    button.Content = image;
                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, col);
                    MemoryGrid.Children.Add(button);
                    buttons.Add(button);

                    index++;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null || button.Content is not Image image)
                return;

            if (firstClick != null && secondClick != null)
                return;

            if (image.Visibility == Visibility.Visible)
                return;

            image.Visibility = Visibility.Visible;

            if (firstClick == null)
            {
                firstClick = button;
            }
            else
            {
                secondClick = button;
                if ((string)firstClick.Tag == (string)secondClick.Tag)
                {
                    firstClick = null;
                    secondClick = null;
                    CheckForWinner();
                }
                else
                {
                    timer.Start();
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            if (firstClick != null && secondClick != null)
            {
                ((Image)firstClick.Content).Visibility = Visibility.Collapsed;
                ((Image)secondClick.Content).Visibility = Visibility.Collapsed;

                firstClick = null;
                secondClick = null;
            }
        }

        private void CheckForWinner()
        {
            if (buttons.All(button => ((Image)button.Content).Visibility == Visibility.Visible))
            {
                MessageBox.Show("You've matched all the pairs!");
                Close();
            }
        }
    }
}
