using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeaBattle
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            /*
            Graphics gr = OutputBox.CreateGraphics();
            Rectangle rect = new Rectangle(this.Width / 2, this.Height / 2, 30, 30);
            Pen shipPen = new Pen(Color.Black, 55);
            gr.DrawRectangle(shipPen, rect);
            */
        }

        private void OutputBox_Click(object sender, EventArgs e)
        {
            /*
            Graphics gr = OutputBox.CreateGraphics();
            Rectangle rect = new Rectangle(this.Width / 2, this.Height / 2, 30, 30);
            Pen shipPen = new Pen(Color.Blue, 5);
            gr.DrawRectangle(shipPen, rect);*/
        }

        public void draw_table(bool isEnemy)
        {
            int x_indent = 10;
            int y_indent;
            Pen tablePen = new Pen(Color.Black, 5);
            Graphics gr = OutputBox.CreateGraphics();
            if(isEnemy == false)
            {
                y_indent = 10;
            }
            else
            {
                y_indent = 10 * 2 + OutputBox.Height;
            }
            for(int i = 0; i < 10; i++)
            {
                gr.DrawLine(tablePen, x_indent, y_indent + (OutputBox.Height / 2 / 10) * i, OutputBox.Width - x_indent, y_indent + (OutputBox.Height / 2 / 10) * i);
            }
            for(int i = 0; i < 12; i++)
            {
                gr.DrawLine(tablePen, x_indent + OutputBox.Width / 12 * i, y_indent, x_indent + OutputBox.Width / 12 * i, OutputBox.Height / 2 - y_indent);
            }
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            Graphics gr = OutputBox.CreateGraphics();
            Rectangle rect = new Rectangle(this.Width / 2, this.Height / 2, 30, 30);
            Pen shipPen = new Pen(Color.Blue, 5);
            gr.DrawRectangle(shipPen, rect);
        }

        private void Main_MouseEnter(object sender, EventArgs e)
        {
            /*
            Graphics gr = OutputBox.CreateGraphics();
            Rectangle rect = new Rectangle(this.Width / 2, this.Height / 2, 30, 30);
            Pen shipPen = new Pen(Color.Blue, 5);
            gr.DrawRectangle(shipPen, rect);*/
        }

        private void lbl_X_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 start_form = new Form1();
            start_form.Show();
        }

        private void lbl_X_MouseEnter(object sender, EventArgs e)
        {
            lbl_X.ForeColor = Color.Red;
        }

        private void lbl_X_MouseLeave(object sender, EventArgs e)
        {
            lbl_X.ForeColor = Color.White;
        }
        Point lastpoint;
        private void Main_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }



        private void OutputBox_MouseEnter(object sender, EventArgs e)
        {
            draw_table(false);
        }
    }
}
