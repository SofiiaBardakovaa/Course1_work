using System.Text.Json;
using Course_work.AdminUC;
using Course_work.Models;

namespace Course_work
{
    public partial class MainForm : Form
    {
        public object UC_D_MedicineInstance { get; internal set; }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
         
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnSingIn_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "sofiiaB" && txtPassword.Text == "12345")
            {
                Admin am = new Admin();
                am.Show();
                this.Hide();
            }
            else if (txtUsername.Text == "drSofiia" && txtPassword.Text == "12345")
            {
                Doctor am = new Doctor();
                am.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Clear();
                txtPassword.Clear();
                txtUsername.Focus();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
