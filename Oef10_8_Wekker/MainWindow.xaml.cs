using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Windows.Threading;

namespace AlarmEx
{
    /// <summary>
    /// Hoofdstuk 10, Oefening 8: Wekker
    /// </summary>
    /// <remarks>
    /// In deze oplossing is een aantal <c>Console.WriteLine</c> statements
    /// opgenomen. Dit helpt bij het debuggen van de applicatie. Merk op dat het
    /// gebruik van de debugger bij dit soort applicaties (Timers) haast
    /// onmogelijk is.</b>
    /// </remarks>
    public partial class MainWindow : Window
    {
        private Alarm wekker;
        private Color normalColor;
        private Color alarmColor;
        private SolidColorBrush brush;

        private DispatcherTimer clockTimer = new DispatcherTimer();
        private DispatcherTimer alarmTimer = new DispatcherTimer();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            cmbColors.ItemsSource = typeof(Colors).GetProperties();

            wekker = new Alarm();
            clockTimer.Start();
            normalColor = Colors.White;
            brush = new SolidColorBrush(normalColor);
            clockLabel.Background = brush;
            alarmColor = Colors.Red;

            clockTimer.Interval = TimeSpan.FromSeconds(1);
            alarmTimer.Interval = TimeSpan.FromMilliseconds(500);

            clockTimer.Tick += clockTimer_Tick;
            alarmTimer.Tick += alarmTimer_Tick;
        }

        void alarmTimer_Tick(object sender, EventArgs e)
        {
            if (brush.Color == normalColor)
            {
                brush.Color = alarmColor;
                System.Media.SystemSounds.Beep.Play();
            }
            else
            {
                brush.Color = normalColor;
            }
        }

        /// <summary>
        /// Handles the Tick event of the clockTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void clockTimer_Tick(object sender, EventArgs e)
        {
            clockLabel.Content = wekker.CurrentTime;
            if (wekker.IsAlarmPassed())
            {
                Console.WriteLine("Alarm passed");
                alarmTimer.Start();
            }
            else
            {
                Console.WriteLine("Alarm NOT passed");
            }
            if (wekker.ShouldStopBeeping())
            {
                Console.WriteLine("ShouldStopBeeping");
                alarmTimer.Stop();
                wekker.Reset();
                alarmTextBox.Text = "";
                brush.Color = normalColor;
            }
        }

        /// <summary>
        /// Handles the Click event of the setButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void setButton_Click(object sender, RoutedEventArgs e)
        {
            // If no alarmtime is given then set an alarm after 10 seconds
            if (alarmTextBox.Text.Trim() == "")
            {
                DateTime now = DateTime.Now;
                int h, m, s;
                h = now.Hour;
                m = now.Minute;
                s = now.Second + 10;
                if(s > 60)
                {
                    m++;
                    s -= 60;
                }
                string alarmtext = h < 10 ? "0" + h.ToString() : h.ToString();
                alarmtext += ":";
                alarmtext += m < 10 ? "0" + m.ToString() : m.ToString();
                alarmtext += ":";
                alarmtext += s < 10 ? "0" + s.ToString() : s.ToString();
                alarmTextBox.Text = alarmtext; 
            }

            // Set the beep time
            int beeptime = 10;
            if(!int.TryParse(alarmDuurTextBox.Text , out beeptime))
            {
                alarmDuurTextBox.Text = "10";
            }

            wekker.AlarmTime = alarmTextBox.Text;
            wekker.BeepTime = beeptime;
        }

        /// <summary>
        /// Handles the SelectionChanged event of the cmbColors control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void cmbColors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            alarmColor = (Color)(cmbColors.SelectedItem as PropertyInfo).GetValue(null, null); 
        }

        /// <summary>
        /// Handles the Click event of the setRandomButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void setRandomButton_Click(object sender, RoutedEventArgs e)
        {
            // Choose a random time for the alarm
            // Not really helpfull, just for fun
            Random r = new Random();
            string alarmtext = "";
            int hour = r.Next(24);
            int minute = r.Next(60);
            int second = r.Next(60);
            alarmtext += hour < 10 ? "0" + hour.ToString() : hour.ToString();
            alarmtext += ":";
            alarmtext += minute < 10 ? "0" + minute.ToString() : minute.ToString();
            alarmtext += ":";
            alarmtext += second < 10 ? "0" + second.ToString() : second.ToString();
            alarmTextBox.Text = alarmtext;
        }
    }
}
