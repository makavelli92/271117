using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelStrategy
{
    public partial class FormAllSignal : Form
    {
        public FormAllSignal()
        {
            InitializeComponent();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.Empty)
                this.dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
            else
                this.dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Empty;
        }
    }
}
