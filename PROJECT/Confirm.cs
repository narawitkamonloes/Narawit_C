using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace PROJECT
{
    
    public partial class Confirm : Form
    {
        public string txts;
        public string nameuser;
        public string priceall;
        
        public Confirm()
        {
            InitializeComponent();
        }
        private void insert()       
        {
            string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
            MySqlConnection conn = new MySqlConnection(connection);
            String sql = "UPDATE userdata SET Stastus ='" + label7.Text + "' WHERE Name = '" + label5.Text + "' AND  Stastus ='"+ label9.Text + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            conn.Close();
            if (rows > 0)
            {
                clear();
                MessageBox.Show("Thank you for using the service");
                this.Hide();
            }
        }
        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void showstock()
        {
            string connectionSting = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
            MySqlConnection con = new MySqlConnection(connectionSting);
            DataSet ds = new DataSet();
            con.Open();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            cmd.CommandText = $"SELECT Name,brand,Productname,Size,Price,amount FROM userdata WHERE Stastus = \"{"Not yet paid"}\" ";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            con.Close();
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }
        private void Confirm_Load(object sender, EventArgs e)
        {
            showstock();
            label5.Text = nameuser;
            textBox1.Text = priceall;
        }
        private void clear()
        {
            string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
            MySqlConnection conn = new MySqlConnection(connection);
            String sql = "DELETE FROM userdata  WHERE Stastus = '" + label12.Text + "' ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            showstock();
            textBox1.Text = "0";
        }
        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("Please pay first.");
            }
            else
            {
                DialogResult result = MessageBox.Show("Will not be able to edit the information \n if there is a problem please contact Admin \nYes or No", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    insert();
                }
                else
                {
                    this.Hide();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double P = int.Parse(textBox1.Text);
            double R = int.Parse(textBox2.Text);
            if (R < P)
            {
                MessageBox.Show("Please enter the correct amount =_=");
            }
            if (R >= P)
            {
                double sum = R - P;
                textBox3.Text = sum.ToString();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
            MySqlConnection conn = new MySqlConnection(connection);
            int selectedRows = dataGridView1.CurrentCell.RowIndex;
            var ShowImages = dataGridView1.Rows[selectedRows].Cells["Productname"].Value;
            conn.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT Image FROM admindata WHERE Productname =\"{ShowImages}\"", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {

                MemoryStream ms = new MemoryStream((byte[])ds.Tables[0].Rows[0]["Image"]);
                pictureBox1.Image = new Bitmap(ms);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                MessageBox.Show("Please enter numbers only");
                e.Handled = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
