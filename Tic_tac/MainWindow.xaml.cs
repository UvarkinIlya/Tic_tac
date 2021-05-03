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

namespace Tic_tac
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int size = 20;
        public MainWindow()
        {
            InitializeComponent();
            Board board = new Board();
            board.Create(MyBoard, size, player);
        }

        private void MyBoard_MouseWheel(object sender, MouseWheelEventArgs e){
            int del = -e.Delta / 120;
            size += 5 * del;
            if (size > 40){
                size = 40;
            }else if(size < 5){
                size = 5;
            }
            Board board = new Board();
            //board.Create(MyBoard, size);
        }
    }
}
