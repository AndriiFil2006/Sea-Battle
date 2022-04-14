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
    public partial class won_or_lost_form : Form
    {
        public won_or_lost_form()
        {
            InitializeComponent();
        }

        private void won_or_lost_form_Load(object sender, EventArgs e)
        {
            who_won_lbl.Location = new Point(this.Width * 3 / 4, 36);
            who_won_lbl.Text = "You won!";
        }
    }
}
