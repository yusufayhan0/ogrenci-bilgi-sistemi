using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace danisman
{
    public partial class ogretmen : Form
    {
        public ogretmen()
        {
            InitializeComponent();
        }
        anatablo ogrtmn = new anatablo();
        private void ogretmen_Load(object sender, EventArgs e)
        {
            label1.Text = giris.adi+" "+giris.soyadi;
            ogrtmn.dersler(giris.ogrid);
            dataGridView1.DataSource = ogrtmn.dataTable3;

            ogrtmn.danisman_ders_program(giris.ogrid);
            dataGridView4.DataSource = ogrtmn.dataTable6;
        }
        int bolum;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[4].Selected)
            {
                ogrtmn.dataTable4.Clear();
                ogrtmn.ogrenciler(Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value), Convert.ToInt16(dataGridView1.CurrentRow.Cells[1].Value), giris.ogrid);
                if (ogrtmn.dataTable4.Rows.Count != 0)
                {                    
                    dataGridView2.DataSource = ogrtmn.dataTable4;
                    dataGridView2.Visible = true;
                    button1.Visible = true;
                    label2.Text = "";
                }
                else
                {
                    label2.Text = "Bu dersi hiçbir öğrenci seçmemiştir.";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView2.Visible = false;
            button1.Visible = false;
        }
    }
}
