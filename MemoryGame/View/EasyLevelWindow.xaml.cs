using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MemoryGame.View
{
    public partial class EasyLevelWindow : Window
    {
        //private List<string> imagePaths = new List<string>
        //{
        //    "Asset/Capoo/capoo2.gif",
        //    "Asset/Capoo/capoo3.png",
        //    "Asset/Capoo/capoo4.png",
        //    "Asset/Capoo/capoo5.png",
        //    "Asset/Capoo/capoo6.png",
        //    "Asset/Capoo/capoo7.gif",
        //    "Asset/Capoo/capoo8.png",
        //    "Asset/Capoo/capoo9.jpg"
        //};

        private List<Button> buttons;
        private Button firstClick, secondClick;
        private DispatcherTimer timer;

        Random random = new Random();
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b","b","v","v","w","w","z","z"
        };

        
        public EasyLevelWindow()
        {
            InitializeComponent();
            InitializeGameBoard();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private void InitializeGameBoard()
        {
            //var pairedImages = imagePaths.Concat(imagePaths).ToList();
            //var random = new Random();
            var shuffledIcons = icons.OrderBy(x => random.Next()).ToList();

            buttons = new List<Button>();
            int rows = MemoryGrid.RowDefinitions.Count;
            int cols = MemoryGrid.ColumnDefinitions.Count;
            int index = 0;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (index >= shuffledIcons.Count)
                        break;

                    var button = new Button();
                    button.Tag = shuffledIcons[index];
                    button.Click += Button_Click;

                    var textBlock = new TextBlock()
                    {
                        Text = shuffledIcons[index],
                        Visibility = Visibility.Collapsed,
                        FontFamily = new System.Windows.Media.FontFamily("Wingdings"),
                        FontSize = 32,
                            HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    button.Content = textBlock;
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
            if (button == null || button.Content is not TextBlock textBlock)
                return;

            if (firstClick != null && secondClick != null)
                return;

            if (textBlock.Visibility == Visibility.Visible)
                return;

            textBlock.Visibility = Visibility.Visible;

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
                ((TextBlock)firstClick.Content).Visibility = Visibility.Collapsed;
                ((TextBlock)secondClick.Content).Visibility = Visibility.Collapsed;

                firstClick = null;
                secondClick = null;
            }
        }

        private void CheckForWinner()
        {
            foreach (var button in buttons)
            {
                if (((TextBlock)button.Content).Visibility == Visibility.Collapsed)
                    return;
            }

            MessageBox.Show("You've matched all the pairs!");
            Close();
        }
    }
}
