using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cur
{
        public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const double k = 0.05;
        const double sigma = 0.1;
        const double mu = 0.05;
        const double dt = 0.1;
        public double USD = 1000;
        public double BTC = 0;
        private double normalRV()
        {
            const double var = 1;
            const double mean = 0;
            Random rnd = new Random();
            double a1 = rnd.NextDouble();
            double a2 = rnd.NextDouble();
            double kor = Math.Sqrt(-2 * Math.Log(a1));
            double cos = Math.Cos(2 * Math.PI * a2);
            double x = (double)(kor * cos * Math.Sqrt(var) + mean);
            
            return x;
        }
        
        private void btStart_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            double rate = (double)edRate.Value;
            int days = (int)edDays.Value;
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(0, rate);

            for (int i = 1; i <= days; i++)
            {
                rate = rate * Math.Exp((mu - sigma * sigma / 2)*dt + sigma * normalRV()*Math.Sqrt(dt));
                edRate.Value = (decimal)rate;
                chart1.Series[0].Points.AddXY(i, rate);
            }
        }

        private void buttonBuy_Click(object sender, EventArgs e)
        {
            double amount = (double)edAmount.Value;
            double rate = (double)edRate.Value;
            double jojo = rate * amount;
            if (jojo<=USD)
            {
                USD -= jojo;
                BTC += amount;
            }
            labBTC.Text = "BTC: " + BTC;
            labDollars.Text = "USD: " + USD;
        }

        private void buttonSell_Click(object sender, EventArgs e)
        {
            double amount = (double)edAmount.Value;
            double rate = (double)edRate.Value;
            double jojo = amount;
            if (jojo <= BTC)
            {
                BTC -= amount;
                USD += amount*rate;
            }
            labBTC.Text = "BTC: " + BTC;
            labDollars.Text = "USD: " + USD;
        }
    }
}
