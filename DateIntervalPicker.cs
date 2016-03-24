using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fieldtool
{
    public partial class DateIntervalPicker : UserControl
    {
        private DateTime MinIntervalDate { get; set; }
        private DateTime MaxIntervalDate { get; set; }

        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public DateIntervalPicker()
        {
            InitializeComponent();
        }
        public void SetDateInterval(DateTime startDate, DateTime endDate)
        {
            MinIntervalDate = startDate;
            MaxIntervalDate = endDate;

            dateTimePicker1.MinDate = MinIntervalDate;
            dateTimePicker1.MaxDate = MaxIntervalDate;
            dateTimePicker1.Value = MinIntervalDate;

            dateTimePicker2.MinDate = MinIntervalDate;
            dateTimePicker2.MaxDate = MaxIntervalDate;
            dateTimePicker2.Value = MaxIntervalDate;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            StartDate = dateTimePicker1.Value;
            InvokeIntervalChanged();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            EndDate = dateTimePicker2.Value;
            InvokeIntervalChanged();
        }

        public EventHandler IntervalChanged;
        private void InvokeIntervalChanged()
        {
            IntervalChanged?.Invoke(this, new EventArgs());
        }

        private void btn7d_Click(object sender, EventArgs e)
        {
            dateTimePicker2.Value = MaxIntervalDate;
            var startDate = MaxIntervalDate.Subtract(new TimeSpan(7, 0, 0, 0));

            if (startDate > MinIntervalDate)
                dateTimePicker1.Value = startDate;
            else
                dateTimePicker1.Value = MinIntervalDate;
        }

        private void btn30d_Click(object sender, EventArgs e)
        {
            dateTimePicker2.Value = MaxIntervalDate;
            var startDate = MaxIntervalDate.Subtract(new TimeSpan(30, 0, 0, 0));

            if (startDate > MinIntervalDate)
                dateTimePicker1.Value = startDate;
            else
                dateTimePicker1.Value = MinIntervalDate;
        }

        private void btn90d_Click(object sender, EventArgs e)
        {
            dateTimePicker2.Value = MaxIntervalDate;
            var startDate = MaxIntervalDate.Subtract(new TimeSpan(90, 0, 0, 0));

            if (startDate > MinIntervalDate)
                dateTimePicker1.Value = startDate;
            else
                dateTimePicker1.Value = MinIntervalDate;
        }
    }
}
