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
            //who_won_lbl.Location = new Point(this.Width / 2 - 9 * 7 / 2, 36);
            
            double score = 0;
            if (Senter.who_won == "Player")
            {
                score = Math.Round(1000 - Math.Pow(Senter.player_turns, 1.0123) * 10);

                who_won_lbl.Text = "You won! \n\n  It took " + Senter.player_turns + " turns \n\n" + " Your score: " + score + " \n Congratulations";
            }
            else if(Senter.who_won == "Enemy")
            {
                score = Math.Round(1000 - Math.Pow(Senter.enemy_turns, 1.0123) * 10);

                who_won_lbl.Text = "Enemy won! \n\n It took " + Senter.enemy_turns + " turns \n\n" + " Your score: " + score + " \n Better luck next time!";
            }
            else
            {
                who_won_lbl.Text = "ERROR";
            }
        }
    }
}
