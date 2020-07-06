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
    public partial class ogrencidetay : Form
    {
        public ogrencidetay()
        {
            InitializeComponent();
            
        }
        anatablo detay = new anatablo();
        int k_top = 0;//kredi toplama değişkeni
        private void ogrencidetay_Load(object sender, EventArgs e)
        {
            label3.Text = "0";//başlangıç toplam kredi labeli
            label1.Text = giris.adi + " " + giris.soyadi;
            if (detay.varmi(giris.ogrid))//daha önceden ders girişi yapılmışmı
            {
                label2.Text = "Ders seçimi yapılmıştır.";
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                detay.secilmeyen(giris.bolumid,giris.donem,giris.ogrid);
                detay.ogrenci_ders_program(giris.ogrid);//ders programı
                if (detay.dataTable5.Rows.Count==0)
                {
                    label7.Text = "Danışman onaylamadığından ders programı gözükmüyor";
                }
                else
                {                    
                    dataGridView2.DataSource = detay.dataTable5;
                    dataGridView2.Visible = true;
                }

                //seçilen ve seçilmeyen dersler gelir
                for (int i = 0; i < detay.dataSet7.Tables[0].Rows.Count; i++)
                {
                    listBox1.Items.Add(detay.dataSet7.Tables[0].Rows[i][3].ToString());                    
                }
                detay.secilen(giris.ogrid);
                for (int i = 0; i < detay.dataSet6.Tables[0].Rows.Count; i++)
                {
                    label4.Text = Convert.ToString(Convert.ToInt16(label4.Text) + Convert.ToInt16(detay.dataSet6.Tables[0].Rows[i][4]));
                    listBox2.Items.Add(detay.dataSet6.Tables[0].Rows[i][3].ToString());
                }
                //------------------------------------------------------
                detay.transcript(giris.ogrid);//trascript çalışır
                dataGridView1.DataSource = detay.dataTable2;//datagridview komutu değişir
                label6.Text = "Toplam kredi " + label4.Text;//transcript toplam kredi labeli

                detay.dersGetir(giris.bolumid, giris.donem);
                for (int i = 0; i < detay.dataSet3.Tables[0].Rows.Count; i++)
                {
                    label3.Text = Convert.ToString(Convert.ToInt16(label3.Text) + Convert.ToInt16(detay.dataSet3.Tables[0].Rows[i][4]));                    
                }
            }
            else
            {
                label7.Text = "Ders seçimi yapılmadığından program hazır değildir.";
                label6.Text = "Ders seçimi yapılmamıştır.";
                detay.dersGetir(giris.bolumid, giris.donem);
                for (int i = 0; i < detay.dataSet3.Tables[0].Rows.Count; i++)
                {
                    listBox1.Items.Add(detay.dataSet3.Tables[0].Rows[i][3]);
                    label3.Text = Convert.ToString(Convert.ToInt16(label3.Text) + Convert.ToInt16(detay.dataSet3.Tables[0].Rows[i][4]));
                    //toplam += Convert.ToInt16(detay.dataSet3.Tables[0].Rows[i][4]); //gelen kradileri toplama
                }
                dataGridView1.Visible = false;
            }
            label3.Text = Convert.ToString(Convert.ToInt16(label3.Text) - 3);
        }
        

        int k_cik = 23;
        private void button2_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox2.Items.Count; i++)//seçilen dersler kayıt
            {
                for (int j = 0; j < detay.dataSet3.Tables[0].Rows.Count; j++)
                {
                    if (listBox2.Items[i].ToString() == detay.dataSet3.Tables[0].Rows[j][3].ToString())
                    {
                        detay.dersKaydet(detay.dataSet3.Tables[0].Rows[j][0].ToString(),giris.ogrid);
                        break;
                    }
                }
            }
            //onay yapıldığında
            label2.Text = "Ders seçim işleminiz başarıyla gerçekleşmiştir.";
            label7.Text = "Danışman onaylamadığından ders programı gözükmüyor";
            dataGridView1.Visible = true;
            detay.transcript(giris.ogrid);
            dataGridView1.DataSource = detay.dataTable2;
            label6.Text = "Toplam Kredi "+label4.Text;//transcript toplam kredi labeli
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void button1_Click_1(object sender, EventArgs e)//ders ekle
        {
            if (listBox1.SelectedItems.Count != 0)
            {
                listBox2.Items.Add(listBox1.SelectedItem);
                for (int i = 0; i < detay.dataSet3.Tables[0].Rows.Count; i++)//kredi toplama
                {
                    if (listBox1.SelectedItem == detay.dataSet3.Tables[0].Rows[i][3])
                    {
                        k_top += Convert.ToInt16(detay.dataSet3.Tables[0].Rows[i][4]);
                        label4.Text = k_top.ToString();
                        break;
                    }
                }
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
            else
            {
                label2.Text = "Ders eklemek için ders seçiniz !";
            }
            if (Convert.ToInt16(label4.Text) > Convert.ToInt16(label3.Text))
            {
                button2.Visible = true;
                label2.Text = "Krediniz yeterli.";
            }
            else
            {
                label2.Text = "Yeterli krediye ulaşamadınız !";
            }
        }

        private void button3_Click_1(object sender, EventArgs e)//ders kaldır
        {
            if (listBox2.SelectedItems.Count != 0)
            {
                listBox1.Items.Add(listBox2.SelectedItem);
                for (int i = 0; i < detay.dataSet3.Tables[0].Rows.Count; i++)//kredi çıkarma
                {
                    if (listBox2.SelectedItem == detay.dataSet3.Tables[0].Rows[i][3])
                    {
                        k_top -= Convert.ToInt16(detay.dataSet3.Tables[0].Rows[i][4]);
                        label4.Text = k_top.ToString();
                        break;
                    }
                }
                listBox2.Items.Remove(listBox2.SelectedItem);
            }
            else
            {
                label2.Text = "Ders kaldırmak için seçim yapmalısınız !";
            }
            if (Convert.ToInt16(label4.Text) < Convert.ToInt16(label3.Text))
            {
                button2.Visible = false;
                label2.Text = "Yeterli krediye ulaşamadınız !";
            }
        }
    }
}
