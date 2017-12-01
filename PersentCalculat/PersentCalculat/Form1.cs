using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersentCalculat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Text = String.Empty;
            label2.Text = String.Empty;
            label3.Text = String.Empty;
            label4.Text = String.Empty;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[0-9\,\.]") && e.KeyChar != 8)
                e.Handled = true;
        }

        private int countSigns;
        private void CalculatePesent()
        {
            if (textBox1.Text[textBox1.TextLength - 1] == ',' || textBox1.Text[textBox1.TextLength - 1] == '.')
                return;

            double temp = Double.Parse(textBox1.Text.Replace(".", ","));

            if (textBox1.Text.Replace(".", ",").Contains(","))
            {
                countSigns = CalculateSignsCount(textBox1.Text);
            }

            label1.Text = String.Format("TP 0.6 - {0}", Math.Round((temp / 100 * 0.6 + temp), countSigns).ToString());
            label2.Text = String.Format("ST 0.2 - {0}", Math.Round((temp / 100 * 0.2 + temp), countSigns).ToString());
            label3.Text = String.Format("ST -0.2 - {0}", Math.Round((temp - temp / 100 * 0.2), countSigns).ToString());
            label4.Text = String.Format("TP -0.6 - {0}", Math.Round((temp - temp / 100 * 0.6), countSigns).ToString());
            countSigns = 0;
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBox1.Text != String.Empty)
                CalculatePesent();
            else
            {
                label1.Text = String.Empty;
                label2.Text = String.Empty;
                label3.Text = String.Empty;
                label4.Text = String.Empty;
            }
        }
        private static int CalculateSignsCount(string number)
        {
            if (!number.Replace(".", ",").Contains(","))
                return 1;
            return (number.Length - 1) - number.Replace(".", ",").IndexOf(",");
        }
    }
}
