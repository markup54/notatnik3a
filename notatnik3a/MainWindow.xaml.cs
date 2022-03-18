﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace notatnik3a
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string Path;
        private string SaveText;
        private bool xChanged=false;
        public MainWindow()
        {
            Path = null;
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            //if(SaveText != tekst.Text)
            if(xChanged)
            if(MessageBox.Show("Czy chcesz zapisać zmiany?",
                "Ostrzeżenie", MessageBoxButton.YesNo,
                MessageBoxImage.Warning)== MessageBoxResult.Yes)
            {
                Save_Click(sender, e);
            }
            Close();
        }

        private void Save_As_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "PlainText | *.txt";
            dialog.Title = "Zapisywanie";
            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, tekst.Text);
                Path= dialog.FileName;
                //SaveText = tekst.Text;
                xChanged = false;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Path != null)
            {
                File.WriteAllText(Path, tekst.Text);
                //SaveText = tekst.Text;
                xChanged = false;
            }
            else { Save_As_Click(sender,e); }
        }

        private void Changed(object sender, TextChangedEventArgs e)
        {
            xChanged = true;
        }

        private void Open_click(object sender, RoutedEventArgs e)
        {
            if (xChanged)
                if (MessageBox.Show("Czy chcesz zapisać zmiany?",
                    "Ostrzeżenie", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Save_Click(sender, e);
                }
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "PlainText | *.txt";
            dialog.Title = "Otwieranie";
            if(dialog.ShowDialog() == true)
            {
                Path = dialog.FileName;
                tekst.Text = File.ReadAllText(Path);
                xChanged= false;
            }
        }

        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            if (tekst.Text !=null)
            {
                MessageBoxResult result 
                    = MessageBox.Show("Czy chcesz utworzyć nowy plik?",
                    "Nowy plik",
                    MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        xChanged = false;
                        tekst.Text = null;
                        Path = null;
                        break;
                }
            }
        }
    }
}
