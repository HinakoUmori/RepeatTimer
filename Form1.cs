using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CountUpToTheLimit
{
    public partial class RepeatTimer : Form
    {
        DateTime pv_start; //DateTime when btnStart is pressed
        DateTime pv_stop; //DateTIme when btnStop is pressed
        TimeSpan span; //Validable to hold timespan temporary
        TimeSpan nowspan; //TimeSpan from latest press of btnStart
        TimeSpan pastspan; //TimeSpan by pressing btnStop
        TimeSpan show; //TimeSpan to show
        TimeSpan limit; //Valiable to hold the value of txtLimit

        public RepeatTimer()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            nowspan = DateTime.Now - pv_start;
            show = nowspan + pastspan;
            if (show > limit)
            {
                lblTime.Text = "00:00:00";
                span = DateTime.Now - DateTime.Now;
                nowspan = span;
                pastspan = span;
                show = span;
                pv_start = DateTime.Now;
            }
            lblTime.Text = show.ToString(@"mm\:ss\:ff");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int tmpLimit = 0;
            try
            {
                pv_start = DateTime.Now;
                tmpLimit = int.Parse(txtLimit.Text.Trim());
                limit = new TimeSpan(0, 0, tmpLimit);
                txtLimit.Enabled = false;
                timer1.Enabled = true;
                btnStart.Enabled = false;
                btnStop.Enabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            pv_stop = DateTime.Now;
            span = pv_stop - pv_start;
            pastspan += span;
            txtLimit.Enabled = true;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            lblTime.Text = "00:00:00";
            span = DateTime.Now - DateTime.Now;
            nowspan = span;
            pastspan = span;
            txtLimit.Enabled = true;
            show = span;
            pv_start = DateTime.Now;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
            lblTime.Text = "00:00:00";
            pastspan = DateTime.Now - DateTime.Now;
            btnStop.Enabled = false;
        }


    }
}
