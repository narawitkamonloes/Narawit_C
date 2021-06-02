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
    public partial class Adminform : Form
    {
        public string txt;
        private void show()
        {
            string connectionSting = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
            MySqlConnection con = new MySqlConnection(connectionSting);
            DataSet ds = new DataSet();
            con.Open();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT id,brand,Productname,Price,amount,Image,Locatin FROM admindata ";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            con.Close();
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }
        private void showotw()
        {
            string connectionSting = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
            MySqlConnection con = new MySqlConnection(connectionSting);
            DataSet ds = new DataSet();
            con.Open();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            cmd.CommandText = $"SELECT id,Name,Brand,Productname,Size,amount,price,address,Stastus FROM userdata WHERE Stastus = \"{"Paid"}\" ";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            con.Close();
            dataGridView2.DataSource = ds.Tables[0].DefaultView;
        }
        public Adminform()
        {
            InitializeComponent();
        }

        private void Adminform_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Adminform_Load(object sender, EventArgs e)
        {
            label1.Text = txt;
            showotw();
            show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void AddproductstBtn_Click(object sender, EventArgs e)
        {
            groupBox1.BringToFront();
            showotw();
            show();
        }
        private void ShowListBtn_Click(object sender, EventArgs e)
        {
            groupBox2.BringToFront();
            showotw();
            show();
        }

        private void BrowseBtn_Click(object sender, EventArgs e)
        {
            Stream myStreeem = null;
            OpenFileDialog openFiledialog = new OpenFileDialog();
            openFiledialog.Filter = "Image files(*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            if (openFiledialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    if ((myStreeem = openFiledialog.OpenFile()) != null)
                    {
                        string FileName = openFiledialog.FileName;
                        textLocation.Text = FileName;
                        if (myStreeem.Length > 512000)
                        {
                            MessageBox.Show("File Size limit exceeded");
                        }
                        else
                        {
                            pictureBox3.Load(FileName);
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("ex.Message");
                }
            }
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            
            groupBox3.BringToFront();
        }

        private void BackBtn1_Click(object sender, EventArgs e)
        {
            groupBox3.BringToFront();
        }
        private void reset()
        {
            textProductname.Text = String.Empty;
            textLocation.Text = String.Empty;
            textprice.Text = String.Empty;
            textbrand.Text = String.Empty;
            pictureBox3.Image = pictureBox5.Image;
            AmountBtn.Text = String.Empty;
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
            MySqlConnection conn = new MySqlConnection(connection);
            byte[] image = null;
            //pictureBox3.ImageLocation = textLocation.Text;
            string filepath = textLocation.Text;
            FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            image = br.ReadBytes((int)fs.Length);
            string sql = $" INSERT INTO admindata (brand,Productname,Price,amount,Locatin,Image) VALUES(\"{textbrand.Text}\",\"{textProductname.Text}\",\"{textprice.Text}\",\"{AmountBtn.Text}\",\"{textLocation.Text}\",@Imgg)";
            if (conn.State != ConnectionState.Open)
            {
                pictureBox3.Image = pictureBox5.Image;
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@Imgg", image));
                int x = cmd.ExecuteNonQuery();
                conn.Close();
                //pictureBox3.Image = null;
                reset();
                show();
                
            }
        }

        private void textprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                MessageBox.Show("Please enter numbers only");
                e.Handled = true;
            }
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            int selectedRow = dataGridView1.CurrentCell.RowIndex;
            int deleteId = Convert.ToInt32(dataGridView1.Rows[selectedRow].Cells["id"].Value);
            string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
            MySqlConnection conn = new MySqlConnection(connection);
            String sql = "DELETE FROM admindata WHERE id = '" + deleteId + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            conn.Close();
            if (rows > 0)
            {
                MessageBox.Show("Data deleted");
                reset();
                show();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            int selectedb = dataGridView1.CurrentCell.RowIndex;
            var ShowImage = dataGridView1.Rows[selectedb].Cells["brand"].Value;
            int selectedp = dataGridView1.CurrentCell.RowIndex;
            var ShowI = dataGridView1.Rows[selectedp].Cells["Productname"].Value;
            int selectedRow = dataGridView1.CurrentCell.RowIndex;
            var ShowImagpr = dataGridView1.Rows[selectedRow].Cells["price"].Value;
            int selectedRowss = dataGridView1.CurrentCell.RowIndex;
            var ShowImagprss = dataGridView1.Rows[selectedRowss].Cells["Locatin"].Value;
            int selectamount = dataGridView1.CurrentCell.RowIndex;
            var Showamount = dataGridView1.Rows[selectamount].Cells["amount"].Value;
            textbrand.Text = ShowImage.ToString();
            textProductname.Text = ShowI.ToString();
            textprice.Text = ShowImagpr.ToString();
            textLocation.Text = ShowImagprss.ToString();
            AmountBtn.Text = Showamount.ToString();
            string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
            MySqlConnection conn = new MySqlConnection(connection);
            dataGridView1.CurrentRow.Selected = true;
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
                pictureBox3.Image = new Bitmap(ms);
            }
        }
        private void ResetBtn_Click_1(object sender, EventArgs e)
        {
            reset();

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.CurrentRow.Selected = true;
            int selectedRow = dataGridView2.CurrentCell.RowIndex;
            var ShowImaget = dataGridView2.Rows[selectedRow].Cells["Name"].Value;
            label11.Text = ShowImaget.ToString();
            int selectedRowman = dataGridView2.CurrentCell.RowIndex;
            var ShowImageta = dataGridView2.Rows[selectedRowman].Cells["Brand"].Value;
            label12.Text = ShowImageta.ToString();
            int selectedRowa = dataGridView2.CurrentCell.RowIndex;
            var ShowImagets = dataGridView2.Rows[selectedRowa].Cells["Productname"].Value;
            label13.Text = ShowImagets.ToString();
            int selectedRowf = dataGridView2.CurrentCell.RowIndex;
            var ShowImagert = dataGridView2.Rows[selectedRowf].Cells["price"].Value;
            label14.Text = ShowImagert.ToString();
            int selectedRowg = dataGridView2.CurrentCell.RowIndex;
            var ShowImageht = dataGridView2.Rows[selectedRowg].Cells["Size"].Value;
            label15.Text = ShowImageht.ToString();
            int selectedRojw = dataGridView2.CurrentCell.RowIndex;
            var ShowImagetl = dataGridView2.Rows[selectedRojw].Cells["Stastus"].Value;
            label18.Text = ShowImagetl.ToString();
            int selectamount = dataGridView2.CurrentCell.RowIndex;
            var Showamounts = dataGridView2.Rows[selectamount].Cells["amount"].Value;
            label21.Text = Showamounts.ToString();
            int selectAdd = dataGridView2.CurrentCell.RowIndex;
            var ShowAdd= dataGridView2.Rows[selectAdd].Cells["address"].Value;
            label23.Text = ShowAdd.ToString();
            string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT Image FROM admindata WHERE Productname =\"{label13.Text}\"", conn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {

                MemoryStream ms = new MemoryStream((byte[])ds.Tables[0].Rows[0]["Image"]);
                pictureBox4.Image = new Bitmap(ms);
            }   
        }
        private void Editbtn_Click(object sender, EventArgs e)
        {
            int selectedRow = dataGridView2.CurrentCell.RowIndex;
            int deleteId = Convert.ToInt32(dataGridView2.Rows[selectedRow].Cells["id"].Value);
            string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
            MySqlConnection conn = new MySqlConnection(connection);
            String sql = "DELETE FROM userdata WHERE id = '" + deleteId + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            conn.Close();
            if (rows > 0)
            {
                MessageBox.Show("Data deleted");
                showotw();
                label11.Text = "--";
                label12.Text = "--";
                label13.Text = "--";
                label14.Text = "--";
                label15.Text = "--";
                pictureBox4.Image = pictureBox8.Image;
                label18.Text = "";
                label21.Text = "0";
                label23.Text = "--";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            label11.Text = "--";
            label12.Text = "--";
            label13.Text = "--";
            label14.Text = "--";
            label15.Text = "--";
            pictureBox4.Image = pictureBox8.Image;
            label18.Text = "";
            label21.Text = "0";
            label23.Text = "--";
        }

        private void EditBtns_Click(object sender, EventArgs e)
        {
            int selectedRowman = dataGridView1.CurrentCell.RowIndex;
            int editId = Convert.ToInt32(dataGridView1.Rows[selectedRowman].Cells["id"].Value);
            string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
            MySqlConnection conn = new MySqlConnection(connection);
            String sqls = "UPDATE admindata SET brand='"+textbrand.Text+"',Productname='"+textProductname.Text+"',Price='"+textprice.Text+"',amount='"+ AmountBtn .Text+ "'WHERE id ='" + editId + "'";
            MySqlCommand cmdss = new MySqlCommand(sqls, conn);
            conn.Open();
            int rosws = cmdss.ExecuteNonQuery();
            if (rosws > 0)
            {
                show();
            }

        }

        private void textprice_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int selectedRowman = dataGridView1.CurrentCell.RowIndex;
            int editId = Convert.ToInt32(dataGridView1.Rows[selectedRowman].Cells["id"].Value);
            string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
            MySqlConnection conn = new MySqlConnection(connection);
            byte[] image = null;
            string manss = textLocation.Text;
            FileStream fs = new FileStream(manss, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            image = br.ReadBytes((int)fs.Length);
            String sql = "UPDATE admindata SET  Locatin='"+textLocation.Text+"',image = @Imgg  WHERE id = '" + editId + "'";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add(new MySqlParameter("@Imgg", image));
            int rows = cmd.ExecuteNonQuery();
            conn.Close();
            if (rows > 0)
            {
                MessageBox.Show("Information edited");
                show();
            }
        }
    }
}
