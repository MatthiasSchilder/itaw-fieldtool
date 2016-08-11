using System;
using System.Windows.Forms;

namespace fieldtool.Controls
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

            dateTimePicker1.MinDate = new DateTime(1900, 1, 1);
            dateTimePicker1.MaxDate = new DateTime(2100, 12, 31);

            dateTimePicker1.MinDate = MinIntervalDate;
            dateTimePicker1.MaxDate = MaxIntervalDate;
            dateTimePicker1.Value = MinIntervalDate;

            dateTimePicker2.MinDate = new DateTime(1900, 1, 1);
            dateTimePicker2.MaxDate = new DateTime(2100, 12, 31);

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

        public event EventHandler IntervalChanged;
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

        private void btn14d_Click(object sender, EventArgs e)
        {
            dateTimePicker2.Value = MaxIntervalDate;
            var startDate = MaxIntervalDate.Subtract(new TimeSpan(14, 0, 0, 0));

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

        private void button1_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = MinIntervalDate;
            dateTimePicker2.Value = MaxIntervalDate;
        }
    }
}
