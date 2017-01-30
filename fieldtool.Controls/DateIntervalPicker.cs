using System;
using System.Windows.Forms;

namespace fieldtool.Controls
{
    public partial class DateIntervalPicker : UserControl
    {
        private DateTime MinIntervalTimestamp { get; set; }
        private DateTime MaxIntervalTimestamp { get; set; }

        public DateTime StartTimpestamp { get; private set; }
        public DateTime EndTimestamp { get; private set; }

        public DateIntervalPicker()
        {
            InitializeComponent();
        }
        public void SetDateInterval(DateTime startDate, DateTime endDate)
        {
            MinIntervalTimestamp = startDate;
            MaxIntervalTimestamp = endDate;

            dateTimePicker1.MinDate = new DateTime(1900, 1, 1);
            dateTimePicker1.MaxDate = new DateTime(2100, 12, 31);

            dateTimePicker1.MinDate = MinIntervalTimestamp;
            dateTimePicker1.MaxDate = MaxIntervalTimestamp;
            dateTimePicker1.Value = MinIntervalTimestamp;

            dateTimePicker2.MinDate = new DateTime(1900, 1, 1);
            dateTimePicker2.MaxDate = new DateTime(2100, 12, 31);

            dateTimePicker2.MinDate = MinIntervalTimestamp;
            dateTimePicker2.MaxDate = MaxIntervalTimestamp;
            dateTimePicker2.Value = MaxIntervalTimestamp;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            StartTimpestamp = dateTimePicker1.Value;
            InvokeIntervalChanged();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            EndTimestamp = dateTimePicker2.Value;
            InvokeIntervalChanged();
        }

        public event EventHandler IntervalChanged;
        private void InvokeIntervalChanged()
        {
            IntervalChanged?.Invoke(this, new EventArgs());
        }

        private void btn7d_Click(object sender, EventArgs e)
        {
            dateTimePicker2.Value = MaxIntervalTimestamp;
            var startDate = MaxIntervalTimestamp.Subtract(new TimeSpan(7, 0, 0, 0));

            if (startDate > MinIntervalTimestamp)
                dateTimePicker1.Value = startDate;
            else
                dateTimePicker1.Value = MinIntervalTimestamp;
        }

        private void btn14d_Click(object sender, EventArgs e)
        {
            dateTimePicker2.Value = MaxIntervalTimestamp;
            var startDate = MaxIntervalTimestamp.Subtract(new TimeSpan(14, 0, 0, 0));

            if (startDate > MinIntervalTimestamp)
                dateTimePicker1.Value = startDate;
            else
                dateTimePicker1.Value = MinIntervalTimestamp;
        }

        private void btn30d_Click(object sender, EventArgs e)
        {
            dateTimePicker2.Value = MaxIntervalTimestamp;
            var startDate = MaxIntervalTimestamp.Subtract(new TimeSpan(30, 0, 0, 0));

            if (startDate > MinIntervalTimestamp)
                dateTimePicker1.Value = startDate;
            else
                dateTimePicker1.Value = MinIntervalTimestamp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = MinIntervalTimestamp;
            dateTimePicker2.Value = MaxIntervalTimestamp;
        }
    }
}
