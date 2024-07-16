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
using MemoryGame.Models;
using MemoryGame.Service;

namespace MemoryGame.View
{
    /// <summary>
    /// Interaction logic for LevelWindow.xaml
    /// </summary>
    public partial class LevelWindow : Window
    {
        private int _stageId;
        private readonly LevelSevice _levelService;

        public LevelWindow(int stageId)
        {
            InitializeComponent();
            _stageId = stageId;
            
            LoadStages();
        }
        private void LoadStages()
        {
                var levels = _levelService.GetAllLevel().ToList();

                // Clear existing buttons if needed
                LevelPanel.Children.Clear();

                foreach (var level in levels)
                {
                    var button = new Button
                    {
                        Content = level.LevelTitle, // Use StageTitle instead of StageName
                        Tag = level.LevelId, // Store StageId in Tag for later use
                        Margin = new Thickness(5),
                        Padding = new Thickness(10),
                        HorizontalAlignment = HorizontalAlignment.Stretch
                    };
                    button.Click += LevelButton_Click;

                    LevelPanel.Children.Add(button);
                
            }
        }

        private void LevelButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var levelId = (int)button.Tag;
                switch(levelId)
                {
                    case 1:
                        var easy = new EasyLevelWindow();
                        easy.Show();
                        break;
                    case 2:
                        var capoo = new Level2Window();
                        capoo.Show();
                        break;
                }
               
                this.Close();
                // Handle the stage button click event, e.g., open the stage or display stage details
                //MessageBox.Show($"Level {levelId} clicked!");
            }
        }
    }
}
