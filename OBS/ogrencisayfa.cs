using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace OBS
{
    public partial class ogrencisayfa : Form
    {
        public ogrencisayfa()
        {
            InitializeComponent();
        }

        private void ogrencisayfa_Load(object sender, EventArgs e)
        {
           
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Öğrenci numarasını al
            int ogrenciNo = Convert.ToInt32(txtOgrNo.Text);

            // PostgreSQL bağlantı dizesi
            string connString = "server=localhost;port=5432; user ID = postgres; password = 123; Database = Proje";

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Öğrenci numarasına bağlı olarak verileri getiren sorgu
                string sorgu = @"SELECT Futbolcu_no,ad,soyad,takim
        FROM futbolcu
        WHERE Futbolcu_no = @ogrenciNo";

                using (NpgsqlCommand command = new NpgsqlCommand(sorgu, conn))
                {
                    command.Parameters.AddWithValue("@ogrenciNo", ogrenciNo);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Columns.Add("Futbolcu No", typeof(int));
                            dataTable.Columns.Add("Ad", typeof(string));
                            dataTable.Columns.Add("Soyad", typeof(string));
                            dataTable.Columns.Add("Takım", typeof(string));

                            dataTable.Rows.Add(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));

                            // DataGridView'e DataTable'ı atayarak verileri göster
                            dataGridView1.DataSource = dataTable;

                            txtAd.Text = reader.GetString(1);
                            txtSoyad.Text = reader.GetString(2);
                            txtTakim.Text = reader.GetString(3);

                        }
                        else
                        {
                            // Öğrenci bulunamadı, kullanıcıya bildir
                            MessageBox.Show("Futbolcu bulunamadı.");
                        }
                    }
                }
            }



        }
        //duyuru butonu
        private void btnDuyuru_Click(object sender, EventArgs e)
        {
            // PostgreSQL bağlantı dizesi
            string connString = "server=localhost;port=5432; user ID = postgres; password = 123; Database = Proje";

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Duyuru sütununu çeken sorgu
                string sorgu = "SELECT baslik, icerik FROM duyuru";

                using (NpgsqlCommand command = new NpgsqlCommand(sorgu, conn))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        // Verileri saklamak için bir DataTable oluştur
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        // DataGridView'e verileri ata
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }



        }
        
        private void btnAra_Click(object sender, EventArgs e)
        {
            string ad = txtAramaAd.Text;
            string soyad = txtAramaSoyad.Text;
            string sinif = cmbSinifi.SelectedItem?.ToString(); // ComboBox'tan seçilen sınıfı al

            string connString = "Host=localhost;Port=5432;Username=postgres;Password=123;Database=Proje";

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                string sorgu = "SELECT futbolcu_no, ad, soyad, sinifi FROM futbolcu WHERE ad = @ad AND soyad = @soyad";

                // Sınıf değeri girildi ise sorguya sınıfı da ekleyelim
                if (!string.IsNullOrEmpty(sinif))
                {
                    sorgu += " AND takim = @sinif";
                }

                using (NpgsqlCommand command = new NpgsqlCommand(sorgu, conn))
                {
                    command.Parameters.AddWithValue("@ad", ad);
                    command.Parameters.AddWithValue("@soyad", soyad);

                    // Sınıf değeri girildi ise parametreyi ekle
                    if (!string.IsNullOrEmpty(sinif))
                    {
                        command.Parameters.AddWithValue("@sinif", sinif);
                    }

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset);

                    if (dataset.Tables[0].Rows.Count > 0)
                    {
                        DataTable table = dataset.Tables[0];
                        dataGridView1.DataSource = table; // DataGridView'e verileri ata
                    }
                    else
                    {
                        MessageBox.Show("Futbolcu bulunamadı.");
                    }
                }
            }

        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            MessageBox.Show("KAPATILIYOR...!");
            System.Threading.Thread.Sleep(100);
            Application.Exit();
        }

        private void btnAra_Click_1(object sender, EventArgs e)
        {
            string ad = txtAramaAd.Text;
            string soyad = txtAramaSoyad.Text;
            string takim = cmbSinifi.SelectedItem?.ToString();

            string connString = "server=localhost;port=5432; user ID = postgres; password = 123; Database = Proje";

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                string sorgu = "SELECT futbolcu_no, ad, soyad, takim FROM futbolcu WHERE ad = @ad AND soyad = @soyad";

                // Takım değeri seçildi ise sorguya takımı da ekleyelim
                if (!string.IsNullOrEmpty(takim))
                {
                    sorgu += " AND takim = @takim";
                }

                using (NpgsqlCommand command = new NpgsqlCommand(sorgu, conn))
                {
                    command.Parameters.AddWithValue("@ad", ad);
                    command.Parameters.AddWithValue("@soyad", soyad);

                    // Takım değeri seçildi ise parametreyi ekle
                    if (!string.IsNullOrEmpty(takim))
                    {
                        command.Parameters.AddWithValue("@takim", takim);
                    }

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);
                            dataGridView1.DataSource = dataTable; // DataGridView'e verileri ata
                        }
                        else
                        {
                            MessageBox.Show("Futbolcu bulunamadı.");
                        }
                    }
                }
            }
        }
    }

    }
  

