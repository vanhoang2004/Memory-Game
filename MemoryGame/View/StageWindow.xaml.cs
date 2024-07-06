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
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace MemoryGame.View
{
    /// <summary>
    /// Interaction logic for StageWindow.xaml
    /// </summary>
    public partial class StageWindow : Window
    {
        private int _userStageId;
        public StageWindow(int userStageId)
        {
            InitializeComponent();
            _userStageId = userStageId;
            LoadStages();
        }
        private void LoadStages()
        {
            using (var context = new MemoryGameContext())
            {
                var stages = context.Stages.ToList();

                // Clear existing buttons if needed
                StagesPanel.Children.Clear();

                foreach (var stage in stages)
                {
                    var button = new Button
                    {
                        Content = stage.StageTitle, // Use StageTitle instead of StageName
                        Tag = stage.StageId, // Store StageId in Tag for later use
                        Margin = new Thickness(5),
                        Padding = new Thickness(10),
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        Background = new SolidColorBrush(GetButtonColor(stage.StageId))
                    };
                    button.Click += StageButton_Click;

                    StagesPanel.Children.Add(button);
                }
            }
        }

        private Color GetButtonColor(int stageId)
        {
            if(stageId < _userStageId)
            {
                return Colors.LightGreen;
            }
            else if(stageId == _userStageId)
            {
                return Colors.Yellow;
            }
            else
            {
                return Colors.LightGray;
            }
        }
        private void StageButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var stageId = (int)button.Tag;

                //pass the stageid here
                var levelWindow = new LevelWindow(stageId);
                levelWindow.Show();
                this.Close();
                // Handle the stage button click event, e.g., open the stage or display stage details
                //MessageBox.Show($"Stage {stageId} clicked!");
               
            }
        }
    }
}
