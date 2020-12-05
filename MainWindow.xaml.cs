﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace Shutdown
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int time = 60;
        bool up = true;
        DispatcherTimer Timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            grid.DataContext = time;
            Timer.Tick += new EventHandler(TimerTick);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();
            /*
            while (up)
            {
                Thread.Sleep(1000);
                timeSpan = DateTime.Now - beginTime;
                if (timeSpan.TotalSeconds > time)
                    up = false;
            }
            Process.Start("c:/windows/system32/shutdown.exe", "-s");
            */

        }

        private void TimerTick(object sender, EventArgs e)
        {
            time--;
            label.Content = time.ToString();
            //MessageBox.Show(time.ToString());
            if (time <= 0)
            {
                Process.Start("c:/windows/system32/shutdown.exe", "-c 长时间不使用自动关机 -s -t 0");
                Close();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Windows_Loaded(object sender, EventArgs e)
        {
            
        }
    }
}
