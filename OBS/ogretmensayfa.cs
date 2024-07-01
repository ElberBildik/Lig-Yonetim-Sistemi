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
    public partial class ogretmensayfa : Form
    {
         
public ogretmensayfa()
        {
            InitializeComponent();
        }
        //öğrenci listeleme butonu
        private void button1_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection("server=localhost;port=5432; user ID = postgres; password = 123; Database = Proje");
            conn.Open();
            string query = "SELECT * FROM futbolcu"; // öğrenci tablosundan veri seç
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "futbolcu");

            // DataGridView'e verileri yükleme
            dataGridView1.DataSource = dataSet.Tables["futbolcu"];

        }



        //öğrenci arama butonu
        private void button5_Click(object sender, EventArgs e)
        {
            string ad = textBox3.Text; // TextBox'tan adı al
            string soyad = textBox2.Text; // TextBox'tan soyadı al
            string sinif = comboBox1.SelectedItem.ToString(); // ComboBox'tan seçilen sınıfı al

            string connString = "Host=localhost;Port=5432;Username=postgres;Password=123;Database=Proje";

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                string sorgu = $"SELECT * FROM futbolcu WHERE ad = @ad AND soyad = @soyad AND Takim = @sinif";

                using (NpgsqlCommand command = new NpgsqlCommand(sorgu, conn))
                {
                    command.Parameters.AddWithValue("@ad", ad);
                    command.Parameters.AddWithValue("@soyad", soyad);
                    command.Parameters.AddWithValue("@sinif", sinif);

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset);

                    DataTable table = dataset.Tables[0];

                    dataGridView1.DataSource = table; // DataGridView'e verileri ata
                }
            }


        }




        //sayfa açıldıldığında öğretmenin branşına göre hizmet verecektir.
        private void ogretmensayfa_Load(object sender, EventArgs e)
        {
            
        }



        //öğrenci silme butonu
        private void button4_Click(object sender, EventArgs e)
        {
            //öğrenci silmek için sorguyu yazdık 
            string deleteQuery = "DELETE FROM futbolcu WHERE futbolcu_no = @id";
            using (NpgsqlConnection conn = new NpgsqlConnection("server=localhost;port=5432; user ID=postgres; password=123; Database=Proje"))
            {
                try
                {
                    conn.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand(deleteQuery, conn))
                    {
                        command.Parameters.AddWithValue("@id", Convert.ToInt32(txtOgrNo.Text)); // Eğer ID bir sayıysa bu şekilde çevirebilirsiniz

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Silme işlemi başarılıysa gerekli işlemleri yapabilirsiniz
                            MessageBox.Show("Kayıt başarıyla silindi.");
                        }
                        else
                        {
                            MessageBox.Show("Kayıt silinemedi.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }


        }
        //datagiredden veri alma
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //datagrid deki verileri textbox lara çektik
            txtOgrNo.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtSınıf.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            
            txtCinsiyet.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE futbolcu SET takim=@takim WHERE futbolcu_no=@futbolcu_no";
            using (NpgsqlConnection conn = new NpgsqlConnection("server=localhost;port=5432; user ID=postgres; password=123; Database=Proje"))
            {
                try
                {
                    conn.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand(sorgu, conn))
                    {
                        
                        command.Parameters.AddWithValue("@takim", txtSınıf.Text);
                        
                        command.Parameters.AddWithValue("@futbolcu_no", Convert.ToInt32(txtOgrNo.Text)); 
                        

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla güncellendi.");
                        }
                        else
                        {
                            MessageBox.Show("Kayıt güncellenemedi.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

        }
        //öğrenci ekleme buntonu
        private void button6_Click(object sender, EventArgs e)
        {
           
            string sorgu = "INSERT INTO futbolcu (futbolcu_no, ad, soyad, takim, cinsiyet) VALUES (@ogrenci_no, @ad, @soyad, @sinifi,@cinsiyet)";

            using (NpgsqlConnection conn = new NpgsqlConnection("server=localhost;port=5432; user ID=postgres; password=123; Database=Proje"))
            {
                try
                {
                    conn.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand(sorgu, conn))
                    {
                        command.Parameters.AddWithValue("@ogrenci_no", Convert.ToInt32(txtOgrNo.Text));
                        command.Parameters.AddWithValue("@ad", txtAd.Text);
                        command.Parameters.AddWithValue("@soyad", txtSoyad.Text);
                        command.Parameters.AddWithValue("@sinifi", txtSınıf.Text);
                        command.Parameters.AddWithValue("@cinsiyet", txtCinsiyet.Text);



                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Futbolcu başarıyla eklendi.");
                        }
                        else
                        {
                            MessageBox.Show("Futbolcu eklenemedi.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }



        }
        //öğrenci güncelleme butonu
        private void button7_Click(object sender, EventArgs e)
        {
            string sorgu2 = "UPDATE ogrenci SET matematiknot=@matematiknot, fiziknot=@fiziknot,edebiyatnot=@edebiyatnot,kimyanot=@kimyanot,tarihnot=@tarihnot,biyolojinot=@biyolojinot,coğrafyanot=@coğrafyanot WHERE ogrenci_no=@ogrenci_no";
            using (NpgsqlConnection conn = new NpgsqlConnection("server=localhost;port=5432; user ID=postgres; password=123; Database=Proje"))
            {
                try
                {
                    conn.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand(sorgu2, conn))
                    {
                        //veriler giriş objelerinden çekildi.
                        

                        

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Notlar başarıyla güncellendi.");
                        }
                        else
                        {
                            MessageBox.Show("Notlar güncellenemedi.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

        }
        //öğrenci girişi yapıldığında textboxları aktif etme
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                txtAd.Enabled = true;
                txtSoyad.Enabled = true;
                txtCinsiyet.Enabled = true;
                txtOgrNo.Enabled = true;
                checkBox1.Checked = true; // Bu satır, checkbox'u işaretli olarak tutar
            }
            else
            {
                txtAd.Enabled = false;
                txtSoyad.Enabled = false;
                txtCinsiyet.Enabled = false;
                txtOgrNo.Enabled = false;
                checkBox1.Checked = false; // Bu satır, checkbox'u işaretsiz olarak tutar
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        
            

        private void label19_Click(object sender, EventArgs e)
        {

        }
        //duyuru oluşturma
        private void button9_Click(object sender, EventArgs e)
        {
            // Kullanıcı tarafından girilen değerleri al
            string baslik = txtOlusturan.Text;
            string icerik = rchTxtAciklama.Text;

            // PostgreSQL bağlantı dizesi
            string connString = "Host=localhost;Port=5432;Username=postgres;Password=123;Database=Proje";

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // INSERT INTO sorgusuyla değerleri ekleyelim
                string sorgu = "INSERT INTO duyuru (baslik, icerik) VALUES (@baslik, @icerik)";

                using (NpgsqlCommand command = new NpgsqlCommand(sorgu, conn))
                {
                    // Parametreleri ekleyelim
                    command.Parameters.AddWithValue("@baslik", baslik);
                    command.Parameters.AddWithValue("@icerik", icerik);

                    // Sorguyu çalıştıralım
                    int affectedRows = command.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        // Ekleme başarılı oldu
                        MessageBox.Show("Duyuru başarıyla eklendi.");
                    }
                    else
                    {
                        // Ekleme başarısız oldu
                        MessageBox.Show("Duyuru eklenirken bir hata oluştu.");
                    }
                }
            }

        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            string connString = "Host=localhost;Port=5432;Username=postgres;Password=123;Database=Proje";

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Duyuru tablosundaki tüm verileri almak için SELECT sorgusu
                string sorgu = "SELECT * FROM duyuru";

                using (NpgsqlCommand command = new NpgsqlCommand(sorgu, conn))
                {
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset);

                    DataTable table = dataset.Tables[0];

                    // DataGridView'e verileri aktar
                    dataGridView1.DataSource = table;
                }
            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("KAPATILIYOR...!");
            System.Threading.Thread.Sleep(100);
            Application.Exit();
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        
    }
    }

