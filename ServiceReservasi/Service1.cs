using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceReservasi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        string constring = "Data Source=LAPTOP-8MKEQ456;Initial Catalog=WCFReservasi;Persist Security Info=True;User ID=sa;Password=mentepermaib20";
        SqlConnection koneksi;
        SqlCommand com;

        public string pemesanan(string IDPemesanan, string NamaCustomer, string NoTelpon, int JumlahPemesanan, string IDLokasi)
        {
            string a = "gagal";
            try
            {
                string sql = "insert into dbo.Pemesanan values ('" + IDPemesanan + "', '" + NamaCustomer + "', '" + NoTelpon + "', " + JumlahPemesanan + " , '" + IDLokasi + "')";
                koneksi = new SqlConnection(constring); 
                com = new SqlCommand(sql, koneksi);
                koneksi.Open();
                com.ExecuteNonQuery();
                koneksi.Close();

                string sql2 = "update dbo.Lokasi set Kuota = Kuota - " + JumlahPemesanan + "where ID_Lokasi = '" + IDLokasi + "' ";
                koneksi = new SqlConnection(constring);
                com = new SqlCommand(sql2, koneksi);
                koneksi.Open();
                com.ExecuteNonQuery();
                koneksi.Close();
                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }

        public List<DetailLokasi> DetailLokasi()
        {
            List<DetailLokasi> LokasiFull = new List<DetailLokasi>(); //proses untuk mendeclare nama list yg telah dibuat dengan nama baru 
            try
            {
                string sql = "select ID_lokasi, Nama_lokasi, Dekskripsi_full, Kuota from dbo.Lokasi"; //declare query 
                koneksi = new SqlConnection(constring); // fungsi konek ke database 
                com = new SqlCommand(sql, koneksi); //proses execute query 
                koneksi.Open(); //membuka koneksi 
                SqlDataReader reader = com.ExecuteReader(); //menampilkan data query 
                while (reader.Read())
                {
                    /* nama class*/
                    DetailLokasi data = new DetailLokasi();
                    data.IDLokasi = reader.GetString(0); 
                    data.NamaLokasi = reader.GetString(1);
                    data.DeskripsiFull = reader.GetString(2);
                    data.Kuota = reader.GetInt32(3);
                    LokasiFull.Add(data); //mengumpulkan data yang awalnya dari array
                }
                koneksi.Close(); //untuk menutup akses ke database
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return LokasiFull;
        }
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public string editPemesanan(string IDPemesanan, string NamaCustomer, string No_telpon)
        {
            string a = "gagal";
            try
            {

                string sql = "update dbo.Pemesanan set Nama_Customer = '" +NamaCustomer+ "', No_telpon = '"+No_telpon+ "'" +"where ID_Reservasi = '"+IDPemesanan+"'";
                koneksi = new SqlConnection(constring);
                com = new SqlCommand(sql, koneksi);
                koneksi.Open();
                com.ExecuteNonQuery();
                koneksi.Close();
                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es);
            }
            return a;
        }

        public string deletePemesanan(string IDPemesanan)
        {
            string a = "gagal";
            try
            {
                string sql = "delete from dbo.Pemesanan where ID_reservasi = '" + IDPemesanan + "'";
                koneksi = new SqlConnection(constring); // fungsi konek ke database
                com = new SqlCommand(sql, koneksi);
                koneksi.Open();
                com.ExecuteNonQuery();
                koneksi.Close();
                a = "sukses";
            }
            catch (Exception es)
            {
                Console.WriteLine(es); 
            }
            return a;
        }
        public List<CekLokasi> ReviewLokasi()
        {
            throw new NotImplementedException();
        }

        public List<Pemesanan> Pemesanan()
        {
            List<Pemesanan> pemesanans = new List<Pemesanan>();//declare nama list
            try
            {
                string sql = " select ID reservasi, Nama_customer, No_telpon," + "Jumlah pemesanan, Nama_Lokasi from dbo.Pemesanan p join dbo.Lokasi 1 on p.ID_lokasi = 1.ID_lokasi";
                koneksi = new SqlConnection(constring); // fungsi konek ke database 
                com = new SqlCommand(sql, koneksi); //proses execute query 
                koneksi.Open(); //membuka koneksi 
                SqlDataReader reader = com.ExecuteReader(); // menampilkan data query 
                while (reader.Read())
                {
                    /*nama class*/
                    Pemesanan data = new Pemesanan(); //deklarasi data, mengambil 1persatu dari database 
                    //bentuk array 
                    data.IDPemesanan = reader.GetString(0); // itu index, ada dikolom keberapa di string sql diatas 
                    data.NamaCustomer = reader.GetString(1);
                    data.NoTelpon = reader.GetString(2);
                    data.JumlahPemesanan = reader.GetInt32(3);
                    data.Lokasi = reader.GetString(4);
                    pemesanans.Add(data); //mengumpulkan data yang awalnya dari array
                    
                }
                koneksi.Close(); //untuk menutup akses ke database
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return pemesanans;
        }
    }
}
