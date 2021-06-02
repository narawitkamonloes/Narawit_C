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
    public partial class Form1 : Form
    {
        string man;
        string manUser;
        public void selecttextAdmin()
        {
            String name1 = textuUser.Text;
            String name2 = textPassword.Text;
            string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();
            string sql = $"SELECT 	Name FROM admin WHERE Users =\"{name1}\" AND Password=\"{name2}\" ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                man  = dr.GetValue(0).ToString();
            }
        }
        public void selecttextUser()
        {
            String name1 = textuUser.Text;
            String name2 = textPassword.Text;
            string connection = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Open();
            string sql = $"SELECT 	Name FROM user WHERE Username =\"{name1}\" AND 	Password=\"{name2}\" ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                manUser = label1.Text = dr.GetValue(0).ToString();
            }
        }

        public Form1()
        {
            InitializeComponent();
        }
        private void LOGIN()
        {
            try
            {
                string connectionSting = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
                MySqlConnection conn = new MySqlConnection(connectionSting);
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT Username,Password FROM user WHERE Username ='" + textuUser.Text + "'AND Password = '" + textPassword.Text + "'" + "", conn))
                {
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        selecttextUser();
                        this.Hide();
                        Userform f = new Userform();
                        f.txtUser = manUser;
                        f.Show();
                    }
                    else
                    {
                        MessageBox.Show("Password is incorrect");
                        textPassword.Text = String.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to close the program?", "", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            try 
            {
                string connectionSting = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
                MySqlConnection conn = new MySqlConnection(connectionSting);
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT Users,Password FROM admin WHERE Users ='" + textuUser.Text + "'AND Password = '" + textPassword.Text + "'", conn))
                {
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        selecttextAdmin();
                        this.Hide();
                        Adminform f = new Adminform();
                        f.txt = man;
                        f.Show();
                    }
                    else
                    {
                        LOGIN();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Apply_for_password f = new Apply_for_password();
            f.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Forgot_your_password f = new Forgot_your_password();
            f.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (textPassword.PasswordChar == '\0')
            {
                pictureBox5.BringToFront();
                
                textPassword.PasswordChar = '*';
            }

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (textPassword.PasswordChar == '*')
            {
                pictureBox4.BringToFront();
                textPassword.PasswordChar = '\0';
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
