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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe_WPF
{
    public partial class MainWindow : Window
    {
        private string currentPlayer = "X";
        private string[] board = new string[9];

        public MainWindow()
        {
            InitializeComponent();
            ResetGame();
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            int index = int.Parse(clickedButton.Tag.ToString());

            if (string.IsNullOrEmpty(board[index]))
            {
                board[index] = currentPlayer;
                clickedButton.Content = currentPlayer;

                if (CheckWin())
                {
                    MessageOutput.Text = $"{currentPlayer} Wins!\nPress any key to restart.";
                    DisableButtons();
                }
                else if (Array.IndexOf(board, null) == -1) // Check for draw
                {
                    MessageOutput.Text = "It's a draw!\nPress any key to restart.";
                    DisableButtons();
                }
                else
                {
                    currentPlayer = (currentPlayer == "X") ? "O" : "X"; // Switch player
                }
            }
        }

        private bool CheckWin()
        {
            int[,] winPatterns = new int[,]
            {
                {0, 1, 2}, {3, 4, 5}, {6, 7, 8}, // Rows
                {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, // Columns
                {0, 4, 8}, {2, 4, 6}            // Diagonals
            };

            for (int i = 0; i < 8; i++)
            {
                int a = winPatterns[i, 0], b = winPatterns[i, 1], c = winPatterns[i, 2];
                if (!string.IsNullOrEmpty(board[a]) && board[a] == board[b] && board[a] == board[c])
                {
                    return true;
                }
            }
            return false;
        }

        private void DisableButtons()
        {
            foreach (UIElement element in GameGrid.Children) 
            {
                if (element is Button btn)
                {
                    btn.IsEnabled = false;
                }
            }
        }

        private void ResetGame()
        {
            currentPlayer = "X";
            board = new string[9];
            MessageOutput.Text = "";

            foreach (UIElement element in GameGrid.Children) 
            {
                if (element is Button btn)
                {
                    btn.Content = "";
                    btn.IsEnabled = true;
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            ResetGame(); // Reset game when any key is pressed
        }
    }
}
