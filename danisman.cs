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
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();            
        }
        public static int ogr_bolumid;
        public static string ogr_adi;
        public static string ogr_soyadi;
        public static int ogr_donem;
        public static int ogr_ogrid;
        anatablo ogrgetir = new anatablo();//ogr nesnesi oluşturuluyor.
        private void Form1_Load(object sender, EventArgs e)
        {
            ogrgetir.danisman_ders_program(giris.dnsid);
            dataGridView4.DataSource = ogrgetir.dataTable6;

            label1.Text = giris.adi + " " + giris.soyadi;
            ogrgetir.dersler(giris.dnsid);
            dataGridView3.DataSource = ogrgetir.dataTable3;
            //---------------------------------------------
            ogrgetir.data(giris.d_bolum);
            dataGridView1.DataSource = ogrgetir.dataTable1;
        }
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            ogrgetir.secilmismi(Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value));
            if (ogrgetir.dataSet8.Tables[0].Rows.Count!=0)
            {
                if (dataGridView1.CurrentRow.Cells[6].Selected)
                {
                    Form1.ogr_bolumid = Convert.ToInt16(dataGridView1.CurrentRow.Cells[1].Value);
                    Form1.ogr_adi = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    Form1.ogr_soyadi = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    Form1.ogr_donem = Convert.ToInt16(dataGridView1.CurrentRow.Cells[5].Value);
                    Form1.ogr_ogrid = Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value);
                    danisman_ogrenci_detay ogrdetay = new danisman_ogrenci_detay();
                    ogrdetay.Show();
                }
            }
            else
            {
                label3.Text = "Öğrenci ders seçimi yapmamıştır.";
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView3.CurrentRow.Cells[4].Selected)
            {
                ogrgetir.dataTable4.Clear();
                ogrgetir.ogrenciler(Convert.ToInt16(dataGridView3.CurrentRow.Cells[0].Value), Convert.ToInt16(dataGridView3.CurrentRow.Cells[1].Value), giris.dnsid);
                if (ogrgetir.dataTable4.Rows.Count != 0)
                {
                    dataGridView2.DataSource = ogrgetir.dataTable4;
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

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView2.Visible = false;
            button1.Visible = false;
        }
    }
}
