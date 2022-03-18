using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection connection;
        OleDbDataAdapter adapter;
        OleDbCommand command;
        DataTable table;

        private void Form1_Load(object sender, EventArgs e)
        {
            doldur();
        }
            private void doldur()
            {
               connection = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=vt1.accdb");
               adapter = new OleDbDataAdapter("select * from bilgiler",connection);
               connection.Open();
               table = new DataTable();
               adapter.Fill(table);
               dataGridView1.DataSource = table;
               connection.Close();
            }
        private void btnkaydet_Click(object sender, EventArgs e)
        {
            command = new OleDbCommand();
            connection.Open();
            command.Connection = connection;
            command.CommandText = " insert into bilgiler (ad,soyad,doğumtarihi,kardeşsayısı,cinsiyet,notortalaması,aylıkgeliri,eğitimdurumu,dahaöncebaşvuruyaptınmı) values (@ad,@soyad,@doğumtarihi,@kardeşsayısı,@cinsiyet,@notortalaması,@aylıkgeliri,@eğitimdurumu,@dahaöncebaşvuruyaptınmı)";

            command.Parameters.AddWithValue("@ad",txtad.Text);
            command.Parameters.AddWithValue("@soyad", txtsoyad.Text);
            command.Parameters.AddWithValue("@doğumtarihi", dateTimePicker1.Value.ToShortDateString());
            command.Parameters.AddWithValue("@kardeşsayısı", int.Parse(txtkardessayisi.Text));

            if (rdberkek.Checked)
                command.Parameters.AddWithValue("@cinsiyet", rdberkek.Text);
            if (rdbkadın.Checked)
                command.Parameters.AddWithValue("@cinsiyet", rdbkadın.Text);

            command.Parameters.AddWithValue("@notortalaması", double.Parse(txtortalama.Text));
            command.Parameters.AddWithValue("@aylıkgeliri", decimal.Parse(txtaylikgeilir.Text));
            command.Parameters.AddWithValue("@eğitimdurumu", cmbegitim.SelectedItem.ToString());

            if (chdyaptınmı.Checked)
                command.Parameters.AddWithValue("@dahaöncebaşvuruyaptınmı", true);
            else
                command.Parameters.AddWithValue("@dahaöncebaşvuruyaptınmı", false);

            command.ExecuteNonQuery();
            connection.Close();
            doldur();
        }
    }
}
