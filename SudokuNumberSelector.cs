using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Proiect
{
    class SudokuNumberSelector
    {
        private static SudokuNumberSelector sm_instance;

        private static SudokuPlate sm_caller;
        private static Canvas sm_parent;

        private static bool sm_shown = false;

        private Panel m_panel;
        private Grid m_grid;
        private Button m_buttonClear;
        private Button m_button1;
        private Button m_button2;
        private Button m_button3;
        private Button m_button4;
        private Button m_button5;
        private Button m_button6;
        private Button m_button7;
        private Button m_button8;
        private Button m_button9;

        private SudokuNumberSelector()
        {
            m_buttonClear = new Button();
            m_buttonClear.Margin = new Thickness(2);
            m_buttonClear.Height = 20;
            m_buttonClear.FontSize = 10;
            m_buttonClear.Content = "Clear";
            m_buttonClear.Click += new RoutedEventHandler(ButtonClear_Click);

            m_button1 = new Button();
            m_button1.Margin = new Thickness(2);
            m_button1.FontSize = 13;
            m_button1.Content = "1";
            m_button1.Click += new RoutedEventHandler(ButtonNumber_Click);

            m_button2 = new Button();
            m_button2.Margin = new Thickness(2);
            m_button2.FontSize = 13;
            m_button2.Content = "2";
            m_button2.Click += new RoutedEventHandler(ButtonNumber_Click);

            m_button3 = new Button();
            m_button3.Margin = new Thickness(2);
            m_button3.FontSize = 13;
            m_button3.Content = "3";
            m_button3.Click += new RoutedEventHandler(ButtonNumber_Click);

            m_button4 = new Button();
            m_button4.Margin = new Thickness(2);
            m_button4.FontSize = 13;
            m_button4.Content = "4";
            m_button4.Click += new RoutedEventHandler(ButtonNumber_Click);

            m_button5 = new Button();
            m_button5.Margin = new Thickness(2);
            m_button5.FontSize = 13;
            m_button5.Content = "5";
            m_button5.Click += new RoutedEventHandler(ButtonNumber_Click);

            m_button6 = new Button();
            m_button6.Margin = new Thickness(2);
            m_button6.FontSize = 13;
            m_button6.Content = "6";
            m_button6.Click += new RoutedEventHandler(ButtonNumber_Click);

            m_button7 = new Button();
            m_button7.Margin = new Thickness(2);
            m_button7.FontSize = 13;
            m_button7.Content = "7";
            m_button7.Click += new RoutedEventHandler(ButtonNumber_Click);

            m_button8 = new Button();
            m_button8.Margin = new Thickness(2);
            m_button8.FontSize = 13;
            m_button8.Content = "8";
            m_button8.Click += new RoutedEventHandler(ButtonNumber_Click);

            m_button9 = new Button();
            m_button9.Margin = new Thickness(2);
            m_button9.FontSize = 13;
            m_button9.Content = "9";
            m_button9.Click += new RoutedEventHandler(ButtonNumber_Click);

            m_grid = new Grid();
            m_grid.Margin = new Thickness(2);
            RowDefinition gridRow0 = new RowDefinition();
            RowDefinition gridRow1 = new RowDefinition();
            RowDefinition gridRow2 = new RowDefinition();
            RowDefinition gridRow3 = new RowDefinition();
            gridRow0.Height = new GridLength(1, GridUnitType.Auto);
            gridRow1.Height = new GridLength(1, GridUnitType.Star);
            gridRow2.Height = new GridLength(1, GridUnitType.Star);
            gridRow3.Height = new GridLength(1, GridUnitType.Star);
            m_grid.RowDefinitions.Add(gridRow0);
            m_grid.RowDefinitions.Add(gridRow1);
            m_grid.RowDefinitions.Add(gridRow2);
            m_grid.RowDefinitions.Add(gridRow3);
            ColumnDefinition gridColumn0 = new ColumnDefinition();
            ColumnDefinition gridColumn1 = new ColumnDefinition();
            ColumnDefinition gridColumn2 = new ColumnDefinition();
            gridColumn0.Width = new GridLength(1, GridUnitType.Star);
            gridColumn1.Width = new GridLength(1, GridUnitType.Star);
            gridColumn2.Width = new GridLength(1, GridUnitType.Star);
            m_grid.ColumnDefinitions.Add(gridColumn0);
            m_grid.ColumnDefinitions.Add(gridColumn1);
            m_grid.ColumnDefinitions.Add(gridColumn2);
            Grid.SetRow(m_buttonClear, 0);
            Grid.SetColumn(m_buttonClear, 0);
            Grid.SetColumnSpan(m_buttonClear, 3);
            Grid.SetRow(m_button1, 1);
            Grid.SetColumn(m_button1, 0);
            Grid.SetRow(m_button2, 1);
            Grid.SetColumn(m_button2, 1);
            Grid.SetRow(m_button3, 1);
            Grid.SetColumn(m_button3, 2);
            Grid.SetRow(m_button4, 2);
            Grid.SetColumn(m_button4, 0);
            Grid.SetRow(m_button5, 2);
            Grid.SetColumn(m_button5, 1);
            Grid.SetRow(m_button6, 2);
            Grid.SetColumn(m_button6, 2);
            Grid.SetRow(m_button7, 3);
            Grid.SetColumn(m_button7, 0);
            Grid.SetRow(m_button8, 3);
            Grid.SetColumn(m_button8, 1);
            Grid.SetRow(m_button9, 3);
            Grid.SetColumn(m_button9, 2);
            m_grid.Children.Add(m_buttonClear);
            m_grid.Children.Add(m_button1);
            m_grid.Children.Add(m_button2);
            m_grid.Children.Add(m_button3);
            m_grid.Children.Add(m_button4);
            m_grid.Children.Add(m_button5);
            m_grid.Children.Add(m_button6);
            m_grid.Children.Add(m_button7);
            m_grid.Children.Add(m_button8);
            m_grid.Children.Add(m_button9);

            m_panel = new DockPanel();
            m_panel.Height = 160;
            m_panel.Width = 130;
            m_panel.Background = new SolidColorBrush(Colors.LightSteelBlue);
            m_panel.Children.Add(m_grid);
        }

        public static void Show(SudokuPlate caller, Canvas parent, Point coordinates)
        {
            if (sm_instance == null)
            {
                sm_instance = new SudokuNumberSelector();
            }

            sm_caller = caller;
            sm_parent = parent;

            if (coordinates.X > 450)
            {
                coordinates.X = 450;
            }
            if (coordinates.Y > 450)
            {
                coordinates.Y = 450;
            }

            Canvas.SetLeft(sm_instance.m_panel, coordinates.X);
            Canvas.SetTop(sm_instance.m_panel, coordinates.Y);

            if (!sm_shown)
            {
                sm_parent.Children.Add(sm_instance.m_panel);
                sm_shown = true;
            }
        }

        public static void Hide()
        {
            if (sm_instance == null)
            {
                sm_instance = new SudokuNumberSelector();
            }

            if (sm_shown && sm_parent != null)
            {
                sm_parent.Children.Remove(sm_instance.m_panel);
                sm_shown = false;
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            sm_caller.Content = "";
            Hide();
        }

        private void ButtonNumber_Click(object sender, RoutedEventArgs e)
        {
            sm_caller.Content = ((Button)sender).Content;
            Hide();
        }
    }
}
