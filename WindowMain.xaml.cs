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

namespace Proiect
{
    /// <summary>
    /// Interaction logic for WindowMain.xaml
    /// </summary>

    public partial class WindowMain : Window
    {
        private int[] sudokuValues;
        private SudokuPlate[] sudokuPositions;

        private enum ColorScheme
        {
            None,
            Unsolved,
            Solved
        }

        public WindowMain()
        {
            InitializeComponent();
            InitField();
        }

        private void SudokuGrid_Click(object sender, RoutedEventArgs e)
        {
            if (((SudokuPlate)sender).Locked == false)
            {
                SudokuNumberSelector.Show((SudokuPlate)sender, CanvasSudoku, Mouse.GetPosition(CanvasSudoku));
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SudokuNumberSelector.Hide();
        }

        private void ButtonGenerate_Click(object sender, RoutedEventArgs e)
        {
            SudokuSolver.Generate(sudokuValues);
            PutOnInterface(ColorScheme.Unsolved);
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 81; ++i)
            {
                sudokuValues[i] = -1;
                sudokuPositions[i].Content = "";
                sudokuPositions[i].Foreground = new SolidColorBrush(Colors.Blue);
                sudokuPositions[i].Locked = false;
            }
        }

        private void ButtonSolve_Click(object sender, RoutedEventArgs e)
        {
            GetFromInterface(true);
            if (SudokuSolver.Solve(sudokuValues))
            {
                PutOnInterface(ColorScheme.Solved);
            }
            else
            {
                PutOnInterface(ColorScheme.Unsolved);
                MessageBox.Show("This puzzle is not solvable");
            }
        }

        private void ButtonCheck_Click(object sender, RoutedEventArgs e)
        {
            GetFromInterface(false);

            foreach (SudokuPlate sp in sudokuPositions)
            {
                if (sp.Locked)
                {
                    sp.Foreground = new SolidColorBrush(Colors.Black);
                }
                else
                {
                    sp.Foreground = new SolidColorBrush(Colors.Blue);
                }
            }

            int[] check = SudokuSolver.Check(sudokuValues);

            if (check == null)
            {
                MessageBox.Show("The solution is correct");
            }
            else
            {
                foreach (int index in check)
                {
                    if (sudokuPositions[index].GetNumber() < 0)
                    {
                        MessageBox.Show("Cell connot be empty");
                        sudokuPositions[index].Focus();
                    }
                    else
                    {
                        sudokuPositions[index].Foreground = new SolidColorBrush(Colors.Red);
                    }
                }
            }
        }

        private void InitField()
        {
            sudokuValues = new int[81];
            sudokuPositions = new SudokuPlate[81];

            for (int i = 0; i < 81; ++i)
            {
                sudokuValues[i] = -1;
            }

            sudokuPositions[0] = SudokuPlate_0_0_0;
            sudokuPositions[1] = SudokuPlate_0_0_1;
            sudokuPositions[2] = SudokuPlate_0_0_2;
            sudokuPositions[3] = SudokuPlate_1_0_0;
            sudokuPositions[4] = SudokuPlate_1_0_1;
            sudokuPositions[5] = SudokuPlate_1_0_2;
            sudokuPositions[6] = SudokuPlate_2_0_0;
            sudokuPositions[7] = SudokuPlate_2_0_1;
            sudokuPositions[8] = SudokuPlate_2_0_2;
            sudokuPositions[9] = SudokuPlate_0_1_0;
            sudokuPositions[10] = SudokuPlate_0_1_1;
            sudokuPositions[11] = SudokuPlate_0_1_2;
            sudokuPositions[12] = SudokuPlate_1_1_0;
            sudokuPositions[13] = SudokuPlate_1_1_1;
            sudokuPositions[14] = SudokuPlate_1_1_2;
            sudokuPositions[15] = SudokuPlate_2_1_0;
            sudokuPositions[16] = SudokuPlate_2_1_1;
            sudokuPositions[17] = SudokuPlate_2_1_2;
            sudokuPositions[18] = SudokuPlate_0_2_0;
            sudokuPositions[19] = SudokuPlate_0_2_1;
            sudokuPositions[20] = SudokuPlate_0_2_2;
            sudokuPositions[21] = SudokuPlate_1_2_0;
            sudokuPositions[22] = SudokuPlate_1_2_1;
            sudokuPositions[23] = SudokuPlate_1_2_2;
            sudokuPositions[24] = SudokuPlate_2_2_0;
            sudokuPositions[25] = SudokuPlate_2_2_1;
            sudokuPositions[26] = SudokuPlate_2_2_2;
            sudokuPositions[27] = SudokuPlate_3_0_0;
            sudokuPositions[28] = SudokuPlate_3_0_1;
            sudokuPositions[29] = SudokuPlate_3_0_2;
            sudokuPositions[30] = SudokuPlate_4_0_0;
            sudokuPositions[31] = SudokuPlate_4_0_1;
            sudokuPositions[32] = SudokuPlate_4_0_2;
            sudokuPositions[33] = SudokuPlate_5_0_0;
            sudokuPositions[34] = SudokuPlate_5_0_1;
            sudokuPositions[35] = SudokuPlate_5_0_2;
            sudokuPositions[36] = SudokuPlate_3_1_0;
            sudokuPositions[37] = SudokuPlate_3_1_1;
            sudokuPositions[38] = SudokuPlate_3_1_2;
            sudokuPositions[39] = SudokuPlate_4_1_0;
            sudokuPositions[40] = SudokuPlate_4_1_1;
            sudokuPositions[41] = SudokuPlate_4_1_2;
            sudokuPositions[42] = SudokuPlate_5_1_0;
            sudokuPositions[43] = SudokuPlate_5_1_1;
            sudokuPositions[44] = SudokuPlate_5_1_2;
            sudokuPositions[45] = SudokuPlate_3_2_0;
            sudokuPositions[46] = SudokuPlate_3_2_1;
            sudokuPositions[47] = SudokuPlate_3_2_2;
            sudokuPositions[48] = SudokuPlate_4_2_0;
            sudokuPositions[49] = SudokuPlate_4_2_1;
            sudokuPositions[50] = SudokuPlate_4_2_2;
            sudokuPositions[51] = SudokuPlate_5_2_0;
            sudokuPositions[52] = SudokuPlate_5_2_1;
            sudokuPositions[53] = SudokuPlate_5_2_2;
            sudokuPositions[54] = SudokuPlate_6_0_0;
            sudokuPositions[55] = SudokuPlate_6_0_1;
            sudokuPositions[56] = SudokuPlate_6_0_2;
            sudokuPositions[57] = SudokuPlate_7_0_0;
            sudokuPositions[58] = SudokuPlate_7_0_1;
            sudokuPositions[59] = SudokuPlate_7_0_2;
            sudokuPositions[60] = SudokuPlate_8_0_0;
            sudokuPositions[61] = SudokuPlate_8_0_1;
            sudokuPositions[62] = SudokuPlate_8_0_2;
            sudokuPositions[63] = SudokuPlate_6_1_0;
            sudokuPositions[64] = SudokuPlate_6_1_1;
            sudokuPositions[65] = SudokuPlate_6_1_2;
            sudokuPositions[66] = SudokuPlate_7_1_0;
            sudokuPositions[67] = SudokuPlate_7_1_1;
            sudokuPositions[68] = SudokuPlate_7_1_2;
            sudokuPositions[69] = SudokuPlate_8_1_0;
            sudokuPositions[70] = SudokuPlate_8_1_1;
            sudokuPositions[71] = SudokuPlate_8_1_2;
            sudokuPositions[72] = SudokuPlate_6_2_0;
            sudokuPositions[73] = SudokuPlate_6_2_1;
            sudokuPositions[74] = SudokuPlate_6_2_2;
            sudokuPositions[75] = SudokuPlate_7_2_0;
            sudokuPositions[76] = SudokuPlate_7_2_1;
            sudokuPositions[77] = SudokuPlate_7_2_2;
            sudokuPositions[78] = SudokuPlate_8_2_0;
            sudokuPositions[79] = SudokuPlate_8_2_1;
            sudokuPositions[80] = SudokuPlate_8_2_2;
        }

        private void GetFromInterface(bool lockFullPlates)
        {
            foreach (SudokuPlate sp in sudokuPositions)
            {
                sudokuValues[sp.VectorIndex] = sp.GetNumber();

                if (lockFullPlates)
                {
                    if (sp.GetNumber() > 0)
                    {
                        sp.Locked = true;
                    }
                    else
                    {
                        sp.Locked = false;
                    }
                }
            }
        }

        private void PutOnInterface(ColorScheme cs = ColorScheme.None)
        {
            foreach (SudokuPlate sp in sudokuPositions)
            {
                if (sudokuValues[sp.VectorIndex] > 0)
                {
                    sp.Content = sudokuValues[sp.VectorIndex].ToString();
                }
                else
                {
                    sp.Content = "";
                }

                switch (cs)
                {
                    case ColorScheme.Unsolved:
                    {
                        if (sudokuValues[sp.VectorIndex] > 0)
                        {
                            sp.Foreground = new SolidColorBrush(Colors.Black);
                            sp.Locked = true;
                        }
                        else
                        {
                            sp.Foreground = new SolidColorBrush(Colors.Blue);
                            sp.Locked = false;
                        }

                        break;
                    }

                    case ColorScheme.Solved:
                    {
                        if (sp.Locked)
                        {
                            sp.Foreground = new SolidColorBrush(Colors.Black);
                        }
                        else
                        {
                            sp.Foreground = new SolidColorBrush(Colors.LimeGreen);
                        }

                        break;
                    }
                }
            }
        }
    }
}
