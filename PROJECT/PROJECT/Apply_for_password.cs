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

namespace PROJECT
{
    
    public partial class Apply_for_password : Form
    {
        string man;
        string mans;
        private void ClearText()
        {
            //textName.Text = String.Empty;
            //textEmail.Text = String.Empty;
            //textPhone.Text = String.Empty;
            textUsername.Text = String.Empty;
            textPassword.Text = String.Empty;
            textConfirm.Text = String.Empty;
        }
        private void Check()
        {
            String name1 = textUsername.Text;
            
            string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();
            string sql = $"SELECT 	Username FROM user WHERE 	Username =\"{name1}\" ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                man  = dr.GetValue(0).ToString();
            }
        }
        private void Check1()
        {
            String name2 = textConfirm.Text;
            string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();
            string sql = $"SELECT 	Password FROM user WHERE 	Password =\"{name2}\" ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                mans  = dr.GetValue(0).ToString();
            }
        }
        private void SUBMIT()
        {
            string connectionSting = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
            MySqlConnection conn = new MySqlConnection(connectionSting);
            conn.Open();
            String sql = "INSERT INTO user(Name,Phone,Email,Username,Password,address)VALUES('" + textName.Text + "','" + textPhone.Text + "','" + textEmail.Text + "','" + textUsername.Text + "','" + textPassword.Text + "','"+ textBox1.Text+ "')";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            try
            {
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Password registration is complete.");
                    this.Hide();
                    Form1 f = new Form1();
                    f.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Apply_for_password()
        {
            InitializeComponent();
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (textPassword.PasswordChar == '*')
            {
                pictureBox4.BringToFront();

                textPassword.PasswordChar = '\0';
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (textPassword.PasswordChar == '\0')
            {
                pictureBox5.BringToFront();

                textPassword.PasswordChar = '*';
            }
        }

        private void Apply_for_password_Load(object sender, EventArgs e)
        {
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {

            Check1();
            Check();
            //string Name = textName.Text;
           // string Phone = textPhone.Text;
            //string Email = textEmail.Text;
            string User = textUsername.Text;
            string password = textPassword.Text;
            string confirm = textConfirm.Text;
            if (man == User && mans == confirm)
            {
                ClearText();
                MessageBox.Show("You already have this account", "",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    if (password == confirm)
                    {
                        SUBMIT();
                    }
                    else
                    {
                        MessageBox.Show("Passwords do not match");
                        textConfirm.Text = String.Empty;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Apply_for_password_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
