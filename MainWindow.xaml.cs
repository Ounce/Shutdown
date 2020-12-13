using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
        long staytime = 600;
        long stay;
        bool up = true;
        DispatcherTimer Timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            grid.DataContext = time;
            Timer.Tick += new EventHandler(TimerTick);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            stay = GetLastInputTime() / 1000;
            if (stay < staytime)
            {
                textBlock.Text = "无操作时间（秒）";
                label.Content = stay.ToString();
                up = true;
                return;
            }
            time--;
            textBlock.Text = "关机倒计时（秒）：";
            label.Content = time.ToString();
            this.WindowState = WindowState.Normal;
            this.Show();
            if (time <= 0)
                Process.Start("c:/windows/system32/shutdown.exe", "-c 长时间不使用自动关机 -s -t 0");
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Windows_Loaded(object sender, EventArgs e)
        {
            
        }

        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwTime;
        }

        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        static long GetLastInputTime()
        {
            LASTINPUTINFO vLastInputInfo = new LASTINPUTINFO();
            vLastInputInfo.cbSize = Marshal.SizeOf(vLastInputInfo);
            if (!GetLastInputInfo(ref vLastInputInfo))
            {
                return 0;
            }
            return Environment.TickCount - (long)vLastInputInfo.dwTime;
        }
    }
}
