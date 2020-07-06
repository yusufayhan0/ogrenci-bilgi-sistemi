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
    public partial class danisman_ogrenci_detay : Form
    {
        public danisman_ogrenci_detay()
        {
            InitializeComponent();
        }
        anatablo d_ogr_detay = new anatablo();
        private void danisman_ogrenci_detay_Load(object sender, EventArgs e)
        {
            d_ogr_detay.transcript(Form1.ogr_ogrid);
            dataGridView2.DataSource = d_ogr_detay.dataTable2;
            label12.Text = "Toplam Kredi " + label9.Text;

            label11.Text = Form1.ogr_adi + " " +Form1.ogr_soyadi;
            d_ogr_detay.secilmeyen(Form1.ogr_bolumid,Form1.ogr_donem, Form1.ogr_ogrid);

            d_ogr_detay.dersGetir(Form1.ogr_bolumid, Form1.ogr_donem);
            for (int i = 0; i < d_ogr_detay.dataSet3.Tables[0].Rows.Count; i++)
            {
                label10.Text = Convert.ToString(Convert.ToInt16(label10.Text) + Convert.ToInt16(d_ogr_detay.dataSet3.Tables[0].Rows[i][4]));
            }
            label10.Text = Convert.ToString(Convert.ToInt16(label10.Text) - 3);

            //seçilen ve seçilmeyen dersler gelir
            for (int i = 0; i < d_ogr_detay.dataSet7.Tables[0].Rows.Count; i++)
            {
                listBox4.Items.Add(d_ogr_detay.dataSet7.Tables[0].Rows[i][3].ToString());
            }
            d_ogr_detay.secilen(Form1.ogr_ogrid);
            for (int i = 0; i < d_ogr_detay.dataSet6.Tables[0].Rows.Count; i++)
            {
                label9.Text = Convert.ToString(Convert.ToInt16(label9.Text) + Convert.ToInt16(d_ogr_detay.dataSet6.Tables[0].Rows[i][1].ToString()));
                listBox3.Items.Add(d_ogr_detay.dataSet6.Tables[0].Rows[i][0].ToString());
            }
            //------------------------------------------------------
            d_ogr_detay.transcript(giris.ogrid);//trascript çalışır
            dataGridView1.DataSource = d_ogr_detay.dataTable2;//datagridview komutu değişir
            label6.Text = "Toplam kredi " + label4.Text;//transcript toplam kredi labeli

            DateTime tarih = new DateTime();//tarih adında tipi datetime
            tarih = DateTime.Now;//sistem tarihini alıyor
            DateTime tarih2 = new DateTime(2016,12,20);//elle tarih girme

            //tarih geçtiğinde onaylayamıyacak
            if (tarih < tarih2)//karşılaştır
            {
                button5.Enabled = true;
            }
            else
            {
                button5.Enabled = false;
                label7.Text = "Onay tarihi geçmiştir";
                button4.Enabled = false;
                button6.Enabled = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            d_ogr_detay.derssil(Form1.ogr_ogrid);
            d_ogr_detay.dersGetir(Form1.ogr_bolumid, Form1.ogr_donem);
            for (int i = 0; i < listBox3.Items.Count; i++)//seçilen dersler kayıt
            {
                for (int j = 0; j < d_ogr_detay.dataSet3.Tables[0].Rows.Count; j++)
                {
                    if (listBox3.Items[i].ToString() == d_ogr_detay.dataSet3.Tables[0].Rows[j][3].ToString())
                    {
                        d_ogr_detay.dersKaydet(d_ogr_detay.dataSet3.Tables[0].Rows[j][0].ToString(), Form1.ogr_ogrid);
                    }
                }
            }
            //onay yapıldığında
            d_ogr_detay.onayla(Form1.ogr_ogrid);
            label7.Text = "Ders onay işleminiz başarıyla gerçekleşmiştir.";
            d_ogr_detay.dataTable2.Clear();
            d_ogr_detay.transcript(Form1.ogr_ogrid);
            dataGridView2.DataSource = d_ogr_detay.dataTable2;//transcripti yeniler
            label12.Text = "Toplam Kredi " + label9.Text;//transcript toplam kredi labeli
            d_ogr_detay.dataSet3.Clear();
        }

        int k_top = 0;//kredi toplama değişkeni
        int k_cik = 23;

        private void button6_Click(object sender, EventArgs e)
        {
            k_top = Convert.ToInt16(label9.Text);
            d_ogr_detay.dersGetir(Form1.ogr_bolumid, Form1.ogr_donem);
            if (listBox4.SelectedItems.Count != 0)
            {
                listBox3.Items.Add(listBox4.SelectedItem);
                for (int i = 0; i < d_ogr_detay.dataSet3.Tables[0].Rows.Count; i++)//kredi toplama
                {
                    if (listBox4.SelectedItem.ToString() == d_ogr_detay.dataSet3.Tables[0].Rows[i][3].ToString())
                    {
                        k_top += Convert.ToInt16(d_ogr_detay.dataSet3.Tables[0].Rows[i][4]);
                        label9.Text = k_top.ToString();
                        break;
                    }
                }
                listBox4.Items.Remove(listBox4.SelectedItem);
                if (Convert.ToInt16(label9.Text) > 20)
                {
                    label7.Text = "Kredi yeterli.";
                    button5.Enabled = true;
                }
                else
                {
                    label7.Text = "Yeterli kredi yok !";
                    button5.Enabled = false;
                }
            }
            else
            {
                label7.Text = "Ders eklemek için ders seçiniz !";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            k_top = Convert.ToInt16(label9.Text);
            d_ogr_detay.dersGetir(Form1.ogr_bolumid, Form1.ogr_donem);
            if (listBox3.SelectedItems.Count != 0)
            {
                listBox4.Items.Add(listBox3.SelectedItem);
                for (int i = 0; i < d_ogr_detay.dataSet3.Tables[0].Rows.Count; i++)//kredi toplama
                {
                    if (listBox3.SelectedItem.ToString() == d_ogr_detay.dataSet3.Tables[0].Rows[i][3].ToString())
                    {
                        k_top -= Convert.ToInt16(d_ogr_detay.dataSet3.Tables[0].Rows[i][4]);
                        label9.Text = k_top.ToString();
                        break;
                    }
                }
                listBox3.Items.Remove(listBox3.SelectedItem);
                if (Convert.ToInt16(label9.Text) > 20)
                {
                    button5.Enabled = true;
                    label7.Text = "Kredi yeterli.";
                }
                else
                {
                    button5.Enabled = false;
                    label7.Text = "Yeterli kredi yok !";
                }
            }
            else
            {
                label7.Text = "Ders kaldırmak için ders seçiniz !";
            }
        }
    }
}
