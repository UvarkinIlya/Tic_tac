using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows;

namespace Tic_tac
{
    class Board{
        private const int MaxBoard = 40;
        int[,] Map = new int[MaxBoard, MaxBoard];//Массив с ходами(-1 - пустая,0 - нолик, 1 - крестик)
        bool label = false;//Крестик или нолик(false - нолик, true - крестик)
        string src_zero = "C:/Users/79914/source/repos/Tic_tac/Tic_tac/Resoures/3.png";
        string src_dagger = "C:/Users/79914/source/repos/Tic_tac/Tic_tac/Resoures/1.png";
        Button player;
        Grid myBoard;
        int size;
        public void Create(Grid _myBoard, int _size, Button _player){
            myBoard = _myBoard;
            size = _size;

            int n = size < MaxBoard ? size : MaxBoard;
            n = n > 5 ? n : 5;

            myBoard.Width = 600;
            myBoard.Height = 600;
            //myBoard.Background = Brushes.Red;
            myBoard.ColumnDefinitions.Clear();
            myBoard.RowDefinitions.Clear();
            //myBoard.ShowGridLines = true;
            //myBoard.lineColor = Color.
            for (int i = 0; i < n; i++){
                ColumnDefinition col = new ColumnDefinition();
                myBoard.ColumnDefinitions.Add(col);

                RowDefinition row = new RowDefinition();
                myBoard.RowDefinitions.Add(row);
            }

            int tab = (MaxBoard - n) / 2;
            for (int i = 0; i < n; i++){
                for(int j = 0; j < n; j++){
                    Button button = new Button();
                    button.Background = Brushes.Black;
                    button.Tag = 100 * (tab + i) + (tab + j);

                    /*Image image = new Image();
                    Uri resourceUri = new Uri("C:/Users/79914/source/repos/Tic_tac/Tic_tac/Resoures/3.png");
                    image.Source = new BitmapImage(resourceUri);*/

                    button.Click += SquareClick;
                    //button.Content = image;

                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    myBoard.Children.Add(button);
                }
            }
            myBoard.Width = 600;
            myBoard.Height = 600;
            FillMap();
            /*Grid.SetColumn(myBoard, n);
            Grid.SetRow(myBoard, n);*/
            player = _player;
            player.Background = Brushes.Black;
            PaintPlayer();
        }

        private void PaintPlayer(){
            /*Отрисовывает button чей ход*/
            Image image = new Image();
            Uri resourceUri = label ? new Uri(src_dagger) : new Uri(src_zero);
            image.Source = new BitmapImage(resourceUri);

            player.Content = image;
        }

        private void FillMap(){
            /*Заполнение карты -1*/
            for(int i = 0; i < MaxBoard; i++){
                for(int j = 0; j < MaxBoard; j++){
                    Map[i, j] = -1;
                }
            }
        }

        private void SquareClick(object sender, EventArgs e){
            /*Определяет в какую клетку был клик*/
            Button button = (Button)sender;
            int id_i = (int)button.Tag / 100;
            int id_j = (int)button.Tag % 100;

            if (Map[id_i, id_j] != -1) return;

            PaintSquare(button, id_i, id_j);
            //MarkingMap(id_i, id_j);
            Map[id_i, id_j] =  label ? 1 : 0;
            PaintPlayer();
            if( CheckWin() ){
                string message = "Выиграли: ";
                message += label ? "нолики" : "крестики";
                message += "\n Сыграть еще раз? ";
               MessageBoxResult result = MessageBox.Show(message,
                                          "Confirmation",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);
                if(result == MessageBoxResult.Yes){
                    Create(myBoard, size, player);
                }

                if (result == MessageBoxResult.No){
                    Application.Current.Shutdown();
                }
            }
        }

        private bool CheckWin(){
            /*Проверка не выиграл ли один из игроков*/
            for(int i = 0; i < MaxBoard; i++){
                for(int j = 0; j < MaxBoard; j++){
                    if(Map[i, j] == 0 || Map[i, j] == 1){
                        if( CheckSquare(i, j)){
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool CheckSquare(int Square_i, int Square_j){
            int label = Map[Square_i, Square_j];
            int i, j;

            //Вправо
            for(i = Square_i; i < Square_i + 5; i++){
                if (Map[i, Square_j] != label) break;
            }

            if (i == Square_i + 5){
                return true;
            }

            //Влево
            for (i = Square_i - 5; i < Square_i; i++){
                if (Map[i, Square_j] != label) break;
            }

            if (i == Square_i){
                return true;
            }

            //Вниз
            for (j = Square_j; j < Square_j + 5; j++){
                if (Map[Square_i, j] != label) break;
            }

            if (j == Square_j + 5){
                return true;
            }

            //Вверх
            for (j = Square_j - 5; j < Square_j; j++){
                if (Map[Square_i, j] != label) break;
            }

            if (j == Square_j){
                return true;
            }

            //Вправо-вверх
            for (i = Square_i, j = Square_j; i < Square_i + 5 && j < Square_j + 5;i++, j++){
                if (Map[i, j] != label) break;
            }

            if (i == Square_i + 5){
                return true;
            }

            //Влево-вверх
            for(i = Square_i, j = Square_j; i > Square_i - 5 && j < Square_j + 5; i--, j++){
                if (Map[i, j] != label) break;
            }

            if (i == Square_i - 5){
                return true;
            }

            //Вниз-влево
            for(i = Square_i, j = Square_j; i > Square_i - 5 && j > Square_j - 5; i--, j--){
                if (Map[i, j] != label) break;
            }

            if (i == Square_i - 5){
                return true;
            }

            //Вниз-вправо
            for (i = Square_i, j = Square_j; i < Square_i + 5 && j > Square_j - 5; i++, j--){
                if (Map[i, j] != label) break;
            }

            if (i == Square_i + 5){
                return true;
            }

            return false;
        }

        private void PaintSquare(Button button, int id_i, int id_j){
            /*Отрисовывает крестики и нолики*/
            Image image = new Image();
            Uri resourceUri = label ? new Uri(src_dagger) : new Uri(src_zero);
            label = !label;
            image.Source = new BitmapImage(resourceUri);

            button.Content = image;
        }
    }

}
