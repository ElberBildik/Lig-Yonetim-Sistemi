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
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost;port=5432; user ID = postgres; password = 123; Database = Proje");

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ogrencisayfa ac = new ogrencisayfa();
            ac.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ogretmen ac = new ogretmen();
            ac.Show();
        }
    }
}
