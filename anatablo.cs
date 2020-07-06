using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;

/// <summary>
/// Summary description for anatablo
/// </summary>
public class anatablo
{
    public SqlConnection baglanti = new SqlConnection();
    //-----------------------------------------------------
    public SqlDataAdapter dataAdapter1 = new SqlDataAdapter();
    public SqlCommand SelectCommand1 = new SqlCommand();
    public DataSet dataSet1 = new DataSet();


    public SqlDataAdapter dataAdapter2 = new SqlDataAdapter();
    public SqlCommand SelectCommand2 = new SqlCommand();
    public DataSet dataSet2 = new DataSet();

    public SqlDataAdapter dataAdapter3 = new SqlDataAdapter();
    public SqlCommand SelectCommand3 = new SqlCommand();
    public DataSet dataSet3 = new DataSet();

    public SqlDataAdapter dataAdapter4 = new SqlDataAdapter();
    public SqlCommand SelectCommand4 = new SqlCommand();
    public DataSet dataSet4 = new DataSet();

    public SqlDataAdapter dataAdapter5 = new SqlDataAdapter();
    public SqlCommand SelectCommand5 = new SqlCommand();
    public DataTable dataTable1 = new DataTable();

    public SqlDataAdapter dataAdapter6 = new SqlDataAdapter();
    public SqlCommand SelectCommand6 = new SqlCommand();
    public DataSet dataSet6 = new DataSet();

    public SqlDataAdapter dataAdapter7 = new SqlDataAdapter();
    public SqlCommand SelectCommand7 = new SqlCommand();
    public DataSet dataSet7 = new DataSet();

    public SqlDataAdapter dataAdapter8 = new SqlDataAdapter();
    public SqlCommand SelectCommand8 = new SqlCommand();
    public DataTable dataTable2 = new DataTable();

    public SqlDataAdapter dataAdapter9 = new SqlDataAdapter();
    public SqlCommand SelectCommand9 = new SqlCommand();
    public DataTable dataTable3 = new DataTable();

    public SqlDataAdapter dataAdapter10 = new SqlDataAdapter();
    public SqlCommand SelectCommand10 = new SqlCommand();
    public DataTable dataTable4 = new DataTable();

    public SqlDataAdapter dataAdapter11 = new SqlDataAdapter();
    public SqlCommand SelectCommand11 = new SqlCommand();
    public DataSet dataSet8 = new DataSet();

    public SqlDataAdapter dataAdapter12 = new SqlDataAdapter();
    public SqlCommand SelectCommand12 = new SqlCommand();
    public DataSet dataSet9 = new DataSet();

    public SqlDataAdapter dataAdapter13 = new SqlDataAdapter();
    public SqlCommand SelectCommand13 = new SqlCommand();
    public DataSet dataSet10 = new DataSet();

    public SqlDataAdapter dataAdapter14 = new SqlDataAdapter();
    public SqlCommand SelectCommand14 = new SqlCommand();
    public DataTable dataTable5 = new DataTable();

    public SqlDataAdapter dataAdapter15 = new SqlDataAdapter();
    public SqlCommand SelectCommand15 = new SqlCommand();
    public DataTable dataTable6 = new DataTable();

    public bool varmi(int ID)//ogrenci ders seçmiþmi
    {
        dataAdapter6.SelectCommand.CommandText = "select * from dersSecim where ogrID='"+ID+"'";
        dataAdapter6.Fill(dataSet6);
        if (dataSet6.Tables[0].Rows.Count>0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void derssil(int id)
    {
        dataSet9.Clear();
        dataAdapter12.SelectCommand.CommandText = "delete from dersSecim where ogrID='"+id+"'";
        dataAdapter12.Fill(dataSet9);
    }

    public void secilen(int ogrid)//seçilen dersler gelir
    {
        dataSet6.Clear();
        dataAdapter6.SelectCommand.CommandText = "select dersAdi,kredi from dersler where dersID in(select dersID from dersSecim where ogrID='" + ogrid + "')";
        dataAdapter6.Fill(dataSet6);
    }

    public void transcript(int ogrid)//transcript 
    {
        dataAdapter8.SelectCommand.CommandText = "select dersID,bolumID,dersAdi,kredi from dersler where dersID in(select dersID from dersSecim where ogrID='" + ogrid + "')";
        dataAdapter8.Fill(dataTable2);
    }

    public void secilmeyen(int bolum, int donem,int ID)//seçilmeyen dersler gelir
    {
        dataAdapter7.SelectCommand.CommandText = "select * from dersler where (bolumID='"+bolum+"' or bolumID=0) and donem='"+donem+"' and dersID not in (select dersID from dersSecim where ogrID='" + ID + "')";
        dataAdapter7.Fill(dataSet7);
    }

    public void dersler(int ogrid)//oðretmenin gireceði dersler gelir
    {
        dataAdapter9.SelectCommand.CommandText = "select * from dersler where ogr_dns='"+ogrid+"'";
        dataAdapter9.Fill(dataTable3);
    }

    public void ogrenciler(int dersid,int donem,int dnsid)//öðretmen gireceði derslerdeki öðrenciler gelir
    {
        dataAdapter10.SelectCommand.CommandText = "select ogrID,ad,soyad,donem,bolumID from ogrenci where ogrID in(select ogrID from dersSecim where dersID='"+dersid+"')";
        dataAdapter10.Fill(dataTable4);
    }

    public void dersKaydet(string dersid12, int ogrid)//seçilien dersleri kayýt eder
    {
        dataSet1.Clear();
        dataAdapter1.SelectCommand.CommandText = "insert into dersSecim (dersID,ogrID)values('" + dersid12 + "','" + ogrid + "')";
        dataAdapter1.Fill(dataSet1);
    }

    public void secilmismi(int ogrenciid) 
    {
        dataSet8.Clear();
        dataAdapter11.SelectCommand.CommandText = "select * from dersSecim where ogrID='"+ogrenciid+"'";
        dataAdapter11.Fill(dataSet8);
    }

    public void data(int bolum)//danýþmanýn kendi bölümüne ait öðrenciler gelir
    {
        dataAdapter5.SelectCommand.CommandText = "select * from ogrenci where bolumID='" + bolum + "'";
        dataAdapter5.Fill(dataTable1);
    }

    public void onayla(int ogrid)
    {
        dataAdapter13.SelectCommand.CommandText = "update ogrenci set onay='1' where ogrID='" + ogrid + "'";
        dataAdapter13.Fill(dataSet10);   
    }

    public void dersGetir(int bolumid,int donem)//öðrencinin bölüm ve dönemine göre dersler gelir
    {
        dataSet3.Clear();
        dataAdapter3.SelectCommand.CommandText = "select * from dersler where (bolumID=" + bolumid + " or bolumID=0) and donem=" + donem + " order by dersAdi";
        dataAdapter3.Fill(dataSet3);
    }

    public void ogrenci_ders_program(int ogrid)//ogrenci ders programý getiriyor
    {
        dataAdapter14.SelectCommand.CommandText = "select distinct(drs.dersAdi) as[Ders Adi],prg.dersID as[Ders No],prg.gun as[Gün],prg.saat as[Saat] from dersler drs,ders_program prg, ogrenci talebe where drs.dersID=prg.dersID and talebe.onay='1' and talebe.ogrID='"+ogrid+"' and prg.dersID in(select secim.dersID from dersSecim secim where secim.ogrID='" + ogrid + "') order by prg.gun,prg.saat";
        dataAdapter14.Fill(dataTable5);
    }

    public void danisman_ders_program(int dnsid)//ogretmen ders programý getiriyor
    {
        dataAdapter15.SelectCommand.CommandText = "select prg.programID as [Program No],drs.dersAdi as[Ders Adý],prg.gun as[Gün],prg.saat as[Saat] from dersler drs,ders_program prg where drs.dersID=prg.dersID and prg.dersID in(select ogr.dersID from dersler ogr where ogr.ogr_dns='" + dnsid + "') order by prg.gun,prg.saat";
        dataAdapter15.Fill(dataTable6);
    }

    public int girisyap(string kullaniciadi,string sifre)//giriþ
    {
        //öðrenci kontrol edilir
        dataAdapter2.SelectCommand.CommandText = "select * from ogrenci where kullaniciAdi='"+kullaniciadi+"' and sifre='"+sifre+"'";
        dataAdapter2.Fill(dataSet2);

        if (dataSet2.Tables[0].Rows.Count!=0)
        {
            return 1;//ogrenci
        }
        else
        {
            //öðrenci yoksa öðretmen-danýþman kontrol edilir
            dataSet4.Clear();
            dataAdapter4.SelectCommand.CommandText = "select * from danisman where kullaniciAdi='" + kullaniciadi + "' and sifre='" + sifre + "'";
            dataAdapter4.Fill(dataSet4);
            if (dataSet4.Tables[0].Rows.Count != 0)
            {
                if (dataSet4.Tables[0].Rows[0][7].ToString()=="True")
                {
                    return 3;//danisman
                }
                else
                {
                    return 2;//ogrentmen
                }
            }
            else
            {
                return 0;//kayýt yok
            }
        }
    }

    public anatablo()//nesne oluþturulduðunda ilk çalýþcak kodlar
    {
        dataAdapter1.SelectCommand = SelectCommand1;
        dataAdapter2.SelectCommand = SelectCommand2;
        dataAdapter3.SelectCommand = SelectCommand3;
        dataAdapter4.SelectCommand = SelectCommand4;
        dataAdapter5.SelectCommand = SelectCommand5;
        dataAdapter6.SelectCommand = SelectCommand6;
        dataAdapter7.SelectCommand = SelectCommand7;
        dataAdapter8.SelectCommand = SelectCommand8;
        dataAdapter9.SelectCommand = SelectCommand9;
        dataAdapter10.SelectCommand = SelectCommand10;
        dataAdapter11.SelectCommand = SelectCommand11;
        dataAdapter12.SelectCommand = SelectCommand12;
        dataAdapter13.SelectCommand = SelectCommand13;
        dataAdapter14.SelectCommand = SelectCommand14;
        dataAdapter15.SelectCommand = SelectCommand15;

        SelectCommand1.Connection = baglanti;
        SelectCommand2.Connection = baglanti;
        SelectCommand3.Connection = baglanti;
        SelectCommand4.Connection = baglanti;
        SelectCommand5.Connection = baglanti;
        SelectCommand6.Connection = baglanti;
        SelectCommand7.Connection = baglanti;
        SelectCommand8.Connection = baglanti;
        SelectCommand9.Connection = baglanti;
        SelectCommand10.Connection = baglanti;
        SelectCommand11.Connection = baglanti;
        SelectCommand12.Connection = baglanti;
        SelectCommand13.Connection = baglanti;
        SelectCommand14.Connection = baglanti;
        SelectCommand15.Connection = baglanti;

        //veri tabaný baðlantý
        string connection_string = "Data Source=DELL\\SQLEXPRESS;Initial Catalog=ogrenciIsleri;Integrated Security=True";
       
        baglanti.ConnectionString = connection_string;

    }

    public void kapat()  // baðlantý kapat
    {
        baglanti.Close();
    }
    public void ac() //baðlantý aç
    {
        baglanti.Open();
    }
}

    



   
 



