using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LevelStrategy.Model;

namespace LevelStrategy
{
    public partial class MainForm : Form
    {
        public DataReception data;

        public static DataGridView grid;

        private bool cycleRead = true;
        private bool cycleWrite = true;

        private Task first;
        private Task two;

        public MainForm()
        {
            InitializeComponent();

            grid = this.dataGridView1;

            AddItemToCb();

            data = new DataReception();
            first = Task.Run(() => {
                                       data.Start(ref cycleRead);
            });
            two = Task.Run(() => {
                data.CycleSetCommand(data.SW_Command, data.SR_FlagCommand, data.SW_FlagCommand, ref cycleWrite);
            });
        }

        
        private void AddItemToCb()
        {
            foreach (var item in Enum.GetValues(typeof(ClassCod)))
            {
                cbClass.Items.Add(item);
            }
            foreach (var item in Enum.GetValues(typeof(TimeFrame)))
            {
                cbTimeFrame.Items.Add(item);
            }
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            if (cbClass.Text != String.Empty && cbSecurity.Text != String.Empty && cbTimeFrame.Text != String.Empty && !this.data.listBars.Any(x => x.Name == cbSecurity.Text && x.TimeFrame == (int)Enum.GetValues(typeof(TimeFrame)).Cast<TimeFrame>().First(y => y.ToString() == cbTimeFrame.Text)))
            {
                TimeFrame frame = Enum.GetValues(typeof(TimeFrame)).Cast<TimeFrame>().First(x => x.ToString() == cbTimeFrame.Text);
                string classCod = cbClass.Text;
                string security = cbSecurity.Text;
                Task.Run(() =>
                {
                    data.SetQUIKCommandDataObject(data.SW_Command, data.SR_FlagCommand, data.SW_FlagCommand, DataReception.GetCommandStringCb(classCod, security, frame));
                });
                listSecurity.Text += cbSecurity.Text + "\n";
                listSecurity.AppendText(Environment.NewLine);
                cbClass.Text = String.Empty;
                cbSecurity.Text = String.Empty;
                cbTimeFrame.Text = String.Empty;
            }
        }
        private void cbClass_Leave(object sender, EventArgs e)
        {
            if (cbClass.Text != String.Empty)
            {
                if (cbClass.Text == "SPBFUT")
                {
                    cbSecurity.Items.Clear();
                    foreach (var item in Enum.GetValues(typeof(Futures)))
                    {
                        cbSecurity.Items.Add(item);
                    }
                }
                if (cbClass.Text == "TQBR")
                {
                    cbSecurity.Items.Clear();
                    foreach (var item in Enum.GetValues(typeof(Security)))
                    {
                        cbSecurity.Items.Add(item);
                    }
                }
             //   cbSecurity.Enabled = true;
            }
            else
            {
                cbSecurity.Items.Clear();
            }
        }
        public enum ClassCod
        {
            SPBFUT = 1,
            TQBR = 2
        }
        public enum Futures
        {
            GZZ7,
            SRZ7,
            EuZ7,
            GDZ7,
            RIZ7,
            SiZ7,
            BRZ7
        }
        public enum Security
        {
            SBER,
            SBERP,
            GAZP,
            LKOH,
            MTSS,
            MGNT,
            MOEX,
            NVTK,
            NLMK,
            RASP,
            VTBR,
            RTKM,
            ROSN,
            AFLT,
            AKRN,
            AFKS,
            PHOR,
            GMKN,
            CHMF,
            SNGS,
            URKA,
            FEES,
            ALRS,
            APTK,
            YNDX,
            MTLRP,
            MAGN,
            BSPB,
            MTLR
        }
        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (data.listBars.Count > 0)
            {
                Bars temp = this.data.listBars.OfType<Bars>().FirstOrDefault(x => x.NumberGrid == e.RowIndex);
                FormAllSignal form = new FormAllSignal();
                form.Text = temp.Name;
                form.Show();
                foreach (SignalData i in temp.listSignal)
                {
                    form.dataGridView1.Rows.Add(temp.Name, i.SignalType, i.DateBsy, i.DateBpy1, i.DateBpy2, i.Level, i.Lyft, i.CancelSignal, i.TimeNow);
                    form.dataGridView1.Rows[form.dataGridView1.RowCount - 1].MinimumHeight = 35;
                }
            }
        }
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Empty;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            cycleRead = false;
            cycleWrite = false;
            while(!first.IsCompleted && !two.IsCompleted)
                Thread.Sleep(100);
        }
    }
}

