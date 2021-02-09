using database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login_window
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
           
            //user.AGE = Convert.ToInt32(textBox5.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Column user = new Column();

            user.ID = textBox1.Text;
            user.PW = textBox2.Text;
            user.NAME = textBox3.Text;
            user.PHONEN = textBox4.Text;
            user.AGE = 30;

            Insert insert = new Insert();
            insert.any(user);

            this.DialogResult = DialogResult.OK;
            MessageBox.Show("회원가입 성공!!");
        }
    }
}
