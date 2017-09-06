using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;


namespace Countdown
{
    public partial class Form1 : Form
    {
        DateTime dateTimeNow = DateTime.Now;
        DateTime dateTimePick;
        TimeSpan dateTimeLeft;
        SaveLoadDate saveLoad = new SaveLoadDate();
        bool ticking;
        bool playing = true;

        public Form1()
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm:ss";

            dateTimePick = saveLoad.Load();
            dateTimePicker1.Value = dateTimePick;

            if(dateTimePick>DateTime.Now)
            {
                ticking = true;
            }

            if(ticking)
            {
                dateTimePicker1.Enabled = false;
                timerTime.Enabled = true;
                button1.Text = "Done!";
                RefreshTime();
            }
            else
            {
                dateTimePicker1.Enabled = true;
                button1.Text = "Start!";
                labelDay.Text = "";
                labelTime.Text = "";
            }

        }

        private void RefreshTime()
        {
            dateTimeNow = DateTime.Now;
            dateTimeLeft = dateTimePick - dateTimeNow;

            int days = dateTimeLeft.Days;
            int hours = dateTimeLeft.Hours;
            int minutes = dateTimeLeft.Minutes;
            int seconds = dateTimeLeft.Seconds;

            string sDays = days.ToString();
            string sHours = hours.ToString();
            string sMinutes = minutes.ToString();
            string sSeconds = seconds.ToString();

            bool endTime;

            endTime = TimeEnd();

            if(endTime)
            {
                if(playing)
                {
                    playing = false;
                    SoundPlayer snd = new SoundPlayer(Properties.Resources.end);
                    snd.Play();
                }
               
                timerTime.Enabled = false;
                labelDay.Text = "";
                labelTime.Text = "You failed...";
                button1.Text = "Start!";
                ticking = false;
                dateTimePicker1.Enabled = true;
                return;
            }

            if(days >= 0 && hours >= 0 && minutes == 0 && seconds == 0)
            {
                SoundPlayer snd = new SoundPlayer(Properties.Resources.Hour);
                snd.Play();
            }

            if (days <= 0 && hours <= 0 && minutes <= 0 && seconds <= 10)
            {
                SoundPlayer snd = new SoundPlayer(Properties.Resources.seconds);
                snd.Play();
            }



            if (days < 10)
            sDays = "  " + sDays;

            if (hours < 10)
                sHours = 0 + sHours;

            if (minutes < 10)
                sMinutes = 0 + sMinutes;

            if (seconds < 10)
                sSeconds = 0 + sSeconds;



            labelDay.Text = "  " + sDays + " days";
            labelTime.Text = sHours + " - " + sMinutes + " - " + sSeconds;

            button1.Text = "Done!";
        }

        private bool TimeEnd()
        {
            if (dateTimeLeft.Days <= 0 && dateTimeLeft.Hours <= 0 && dateTimeLeft.Minutes <= 0 && dateTimeLeft.Seconds <= 0)
            {
                return true;
            }
            else return false;
        }

        private void timerTime_Tick(object sender, EventArgs e)
        {
            RefreshTime();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePick = dateTimePicker1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(ticking)
            {
                dateTimePicker1.Enabled = true;
                button1.Text = "Start!";
                labelDay.Text = "";
                labelTime.Text = "Congratulations!";
                labelTime.Left = 43;
                ticking = false;
                timerTime.Enabled = false;
                saveLoad.Save(DateTime.Now);
            }
            else
            {
                playing = true;
                ticking = true;
                labelTime.Left = 71;
                saveLoad.Save(dateTimePick);
                RefreshTime();
                timerTime.Enabled = true;
                dateTimePicker1.Enabled = false;
            }
           
        }
    }
}
