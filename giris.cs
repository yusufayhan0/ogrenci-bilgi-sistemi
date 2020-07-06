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
    public partial class giris : Form
    {
        public giris()
        {
            InitializeComponent();
        }
        public static int bolumid;
        public static string adi;
        public static string soyadi;
        public static int donem;
        public static int ogrid;
        public static int d_bolum;
        public static int dnsid;
        anatablo kodlar = new anatablo();
        private void button1_Click(object sender, EventArgs e)
        {
            if (kodlar.girisyap(textBox1.Text, textBox2.Text) != 0)//dogrumu yanlismi
            {
                if (kodlar.girisyap(textBox1.Text, textBox2.Text) == 1)//ogrenci
                {
                    bolumid = Convert.ToInt16(kodlar.dataSet2.Tables[0].Rows[0][1]);
                    adi = kodlar.dataSet2.Tables[0].Rows[0][2].ToString();
                    soyadi = kodlar.dataSet2.Tables[0].Rows[0][3].ToString();
                    donem = Convert.ToInt16(kodlar.dataSet2.Tables[0].Rows[0][8]);
                    ogrid = Convert.ToInt16(kodlar.dataSet2.Tables[0].Rows[0][0]);
                    ogrencidetay ogrn = new ogrencidetay();
                    ogrn.Show();
                }
                else if (kodlar.girisyap(textBox1.Text, textBox2.Text) == 2)//ogretmen
                {
                    bolumid = Convert.ToInt16(kodlar.dataSet4.Tables[0].Rows[0][1]);
                    adi = kodlar.dataSet4.Tables[0].Rows[0][2].ToString();
                    soyadi = kodlar.dataSet4.Tables[0].Rows[0][3].ToString();
                    ogrid = Convert.ToInt16(kodlar.dataSet4.Tables[0].Rows[0][0]);
                    ogretmen ogrtmn = new ogretmen();
                    ogrtmn.Show();
                }
                else if (kodlar.girisyap(textBox1.Text, textBox2.Text) == 3)//danisman
                {
                    d_bolum = Convert.ToInt16(kodlar.dataSet4.Tables[0].Rows[0][1]);
                    dnsid = Convert.ToInt16(kodlar.dataSet4.Tables[0].Rows[0][0]);
                    adi = kodlar.dataSet4.Tables[0].Rows[0][2].ToString();
                    soyadi = kodlar.dataSet4.Tables[0].Rows[0][3].ToString();
                    Form1 danisman_giris = new Form1();
                    danisman_giris.Show();
                }
                kodlar.dataSet2.Clear();
                textBox1.Text = "";
                textBox2.Text = "";
                label3.Text = "";
            }
            else
            {
                label3.Text = "Kullanıcı Adı veya Şifre Hatalı.";
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }
        private void giris_Load(object sender, EventArgs e)
        {

        }
    }
}
