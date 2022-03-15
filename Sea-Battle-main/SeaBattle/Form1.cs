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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string[] Alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        double[] rows = new double[64], enemy_rows = new double[64], columns = new double[64], enemy_columns = new double[64];
        int y_indent = 25;
        int row_len = 5;
        int column_len = 5;

        //array ships will have 4 values: 0 - not in the table, 1 - on the table and haven't hited, 2 - on the table, hitted, 3 - dead
        int[] ships = new int[8];
        int[] row_ship = new int[8];
        int[] column_ship = new int[8];
        bool[] hor_ship = new bool[8];
        int[] deck_ship = new int[8];

        int[] enemy_ships = new int[8];
        int[] enemy_row_ship = new int[8];
        int[] enemy_column_ship = new int[8];
        bool[] enemy_hor_ship = new bool[8];
        int[] enemy_deck_ship = new int[8];
        //int[] ships_left = new int[8];
        //int[] enemy_ships_left = new int[8];
        int sel_ship = -1;
        int[] ships_left = { 2, 2, 1, 1 };
        int[] enemy_ships_left = { 2, 2, 1, 1 };
        int sel_row, sel_column;
        bool is_horisontal = true;
        // array that will have 4 values about cell: 0 - no ship on cell; 1 - alive untoacheble ship on the cell, 2 -alive shoted ship on the cell, 3 - rip
        int[,] cells = new int[10, 10];    
        bool is_button_clicked = false;

        public void fill_cells_arr()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    cells[i, j] = 0;
                }
            }
        }

        public void draw_table(bool isEnemy)
        {

            int x_indent = 30; 
            Color table_col;
            Graphics gr = pctrBxOut.CreateGraphics();
            if (isEnemy == false)
            {
                //y_indent = 25;
                table_col = Color.Blue;
            }
            else
            {
                x_indent = pctrBxOut.Width / 2 + 30;
                //  y_indent = pctrBxOut.Height / 2 + 25;
                table_col = Color.Red;
            }
            table_col = Color.Black;
            Pen tablePen = new Pen(table_col, 3);
            Brush txtBrush = new SolidBrush(table_col);
            Font txtFont = new Font("Arial", 16);
            /*
            for (int i = 0; i < 11; i++)
            {
                gr.DrawLine(tablePen, x_indent, y_indent + (pctrBxOut.Height / 10) * i, pctrBxOut.Width - x_indent, y_indent + (pctrBxOut.Height / 10) * i);
                gr.DrawLine(tablePen, x_indent + pctrBxOut.Width / 10 * i, y_indent, x_indent + pctrBxOut.Width / 10 * i, pctrBxOut.Height / 2 + y_indent - (pctrBxOut.Width / 20));
            }*/
            /*
            for(int i = 0; i < 11; i++)
            {
                gr.DrawLine(tablePen, x_indent + (pctrBxOut.Width - x_indent * 2) / 10 * i, y_indent, x_indent + (pctrBxOut.Width - x_indent * 2) / 10 * i, y_indent + (pctrBxOut.Height - 50) / (11 * 2) * 10);

                gr.DrawLine(tablePen, x_indent, y_indent + (pctrBxOut.Height - 50) / (11 * 2) * i, pctrBxOut.Width - x_indent - 2, y_indent + (pctrBxOut.Height - 50) / (11 * 2) * i);
            }
            */
            //   gr.DrawLine(tablePen, pctrBxOut.Width - x_indent * 2, y_indent, pctrBxOut.Width - x_indent * 2, y_indent + pctrBxOut.Height / 2 - 50);
            for (int i = 0; i < 11; i++)
            {
                gr.DrawLine(tablePen, x_indent + (pctrBxOut.Width - 60) / (2 * 11) * i, y_indent, x_indent + (pctrBxOut.Width - 60) / (2 * 11) * i, y_indent + (pctrBxOut.Height - 50) / (10) * 10);
                if (i < 10)
                {
                    gr.DrawString(Alphabet[i], txtFont, txtBrush, x_indent - 25, Convert.ToInt32(y_indent + (pctrBxOut.Height - 50) / 10 * i + (pctrBxOut.Height - 50) / 10 * 0.5));
                    columns[i] = y_indent + (pctrBxOut.Height - 60) / 10 * (i + 1);
                    enemy_rows[i] = rows[i] + pctrBxOut.Width / 2;

                    gr.DrawString("" + (i + 1), txtFont, txtBrush, Convert.ToInt32(x_indent + (pctrBxOut.Width - 60) / (2 * 11) * i + (pctrBxOut.Width - 60) / (2 * 11) * 0.5 - 8), y_indent - 25);
                    rows[i] = 30 + (pctrBxOut.Width - 60) / (2 * 11) * i;
                    //enemy_columns[i] = pctrBxOut.Width / 2 + 30 + (pctrBxOut.Width - 60) / (2 * 11) * i;
                    enemy_columns[i] = columns[i];
                }
                gr.DrawLine(tablePen, x_indent - 8, y_indent + (pctrBxOut.Height - 50) / (10) * i, x_indent + pctrBxOut.Width / 2 - 60 - 15 - 8, y_indent + (pctrBxOut.Height - 50) / (10) * i);
            }
            row_len = Convert.ToInt16(rows[2] - rows[1]);
            column_len = Convert.ToInt16(columns[2] - columns[1]);
        }

        public void add_ship(int x, int y, int num_ship, Color ship_color, bool isHorisontal, string label)
        {
            Graphics gr = pctrBxOut.CreateGraphics();
            Pen shipPen = new Pen(ship_color, 5);
            Brush txtBrush = new SolidBrush(ship_color);
            Font txtFont = new Font("Arial", 16);
            Brush ship_brush = new SolidBrush(ship_color);
            Pen gun_pen = new Pen(ship_color, 8);
            Brush gun_brush = new SolidBrush(Color.Red);
            Brush fire_brush = new SolidBrush(Color.Yellow);
            Brush txtBrush1 = new SolidBrush(Color.Black);

            if (isHorisontal == true)
            {
                if (num_ship != 2)
                {
                    gr.DrawLine(shipPen, x + row_len, y - column_len, x, y - column_len / 2);
                    gr.DrawLine(shipPen, x + row_len, y, x, y - column_len / 2);

                    for (int i = 0; i < num_ship - 2; i++)
                    {
                        gr.DrawLine(shipPen, x + row_len * (i + 1), y - column_len, x + row_len * (i + 2), y - column_len);
                        gr.DrawLine(shipPen, x + row_len * (i + 1), y, x + row_len * (i + 2), y);
                    }

                    int rad;
                    if (column_len > row_len)
                    {
                        rad = row_len;
                    }
                    else
                    {
                        rad = column_len;
                    }

                    int new_num_ship = num_ship;
                    if (new_num_ship % 2 == 0)
                    {
                        new_num_ship--;
                    }

                    gr.FillEllipse(txtBrush, x + row_len * (new_num_ship / 2), y - column_len / 2 - rad / 2, rad, rad);
                    gr.DrawLine(gun_pen, x + (row_len) * (new_num_ship / 2 + 1), y - column_len / 2, Convert.ToInt16(x + (row_len) * (new_num_ship / 2 + 1.5)), y - column_len / 2);

                    gr.DrawLine(shipPen, x + row_len * (num_ship - 1), y - column_len, x + row_len * num_ship, y - column_len / 2);
                    gr.DrawLine(shipPen, x + row_len * (num_ship - 1), y, x + row_len * num_ship, y - column_len / 2);

                    gr.DrawString(label, txtFont, txtBrush1, Convert.ToInt32(x + row_len * (num_ship - 0.5)), y - 8);
                }
                else
                {
                    gr.DrawLine(shipPen, x + row_len * 2 / 3, y - column_len, x, y - column_len / 2);
                    gr.DrawLine(shipPen, x + row_len * 2 / 3, y, x, y - column_len / 2);

                    gr.DrawLine(shipPen, x + row_len * 2 / 3, y - column_len, x + row_len * 4 / 3, y - column_len);
                    gr.DrawLine(shipPen, x + row_len * 2 / 3, y, x + row_len * 4 / 3, y);

                    gr.DrawLine(shipPen, x + row_len * 4 / 3, y - column_len, x + row_len * num_ship, y - column_len / 2);
                    gr.DrawLine(shipPen, x + row_len * 4 / 3, y, x + row_len * num_ship, y - column_len / 2);

                    gr.DrawString(label, txtFont, txtBrush1, x + row_len * 5 / 3, y - 8);

                    int rad;
                    if (column_len > row_len)
                    {
                        rad = row_len;
                    }
                    else
                    {
                        rad = column_len;
                    }

                    gr.FillRectangle(ship_brush, x + row_len - rad / 4, y - column_len / 2 - rad / 4, rad / 2,rad / 2);

                    gr.FillEllipse(gun_brush, x + row_len - rad / 4, y - column_len / 2 - rad / 4, rad / 5, rad / 5);
                    gr.FillEllipse(fire_brush, x + row_len - rad / 4 + rad / 32, y - column_len / 2 - rad / 4 + rad / 32, rad / 8, rad / 8);

                    gr.FillEllipse(gun_brush, x + row_len - rad / 4, y - column_len / 2 + rad / 4 - rad / 5, rad / 5, rad / 5);
                    gr.FillEllipse(fire_brush, x + row_len - rad / 4 + rad / 32, y - column_len / 2 + rad / 4 - rad / 5 + rad / 32, rad / 8, rad / 8);

                    gr.FillEllipse(gun_brush, x + row_len + rad / 4 - rad / 5, y - column_len / 2 - rad / 4, rad / 5, rad / 5);
                    gr.FillEllipse(fire_brush, x + row_len + rad / 4 - rad / 5 + rad / 32, y - column_len / 2 - rad / 4 + rad / 32, rad / 8, rad / 8);

                    gr.FillEllipse(gun_brush, x + row_len + rad / 4 - rad / 5, y - column_len / 2 + rad / 4 - rad / 5, rad / 5, rad / 5);
                    gr.FillEllipse(fire_brush, x + row_len + rad / 4 - rad / 5 + rad / 32, y - column_len / 2 + rad / 4 - rad / 5 + rad / 32, rad / 8, rad / 8);
                }
            }
            else
            {
                if(num_ship != 2)
                {
                    gr.DrawLine(shipPen, x + row_len, y + column_len - column_len + 8, x + row_len / 2, y - column_len);
                    gr.DrawLine(shipPen, x, y + column_len - column_len + 8, x + row_len / 2, y - column_len);
                    for(int i = 0; i < num_ship - 2; i++)
                    {
                        gr.DrawLine(shipPen, x + row_len, y + column_len * (i + 1) + 8 - column_len, x + row_len, y + column_len * (i + 2) + 8 - column_len);
                        gr.DrawLine(shipPen, x, y + column_len * (i + 1) - column_len + 8, x, y + column_len * (i + 2) + 8 - column_len);
                    }
                    gr.DrawLine(shipPen, x + row_len, y + column_len * (num_ship - 1) + 8 - column_len, x + row_len / 2, y + column_len * num_ship + 8 - column_len);
                    gr.DrawLine(shipPen, x, y + column_len * (num_ship - 1) + 8 - column_len, x + row_len / 2, y + column_len * num_ship + 8 - column_len);

                    gr.DrawString(label, txtFont, txtBrush1, Convert.ToInt32(x + row_len * 0.66), Convert.ToInt32(y + column_len * (num_ship) - 8 - column_len));

                    int rad;
                    if (column_len > row_len)
                    {
                        rad = row_len;
                    }
                    else
                    {
                        rad = column_len;
                    }
                    gr.FillEllipse(txtBrush, x, y + column_len * (num_ship / 2) + 8 - column_len, rad, rad);
                    gr.DrawLine(gun_pen, x + row_len / 2, y + column_len * (num_ship / 2) + 16 - column_len, x + row_len / 2, Convert.ToInt32(y + column_len * (num_ship / 2 - 0.5) - column_len + 8));
                }
                else
                {
                    int rad;
                    if (column_len > row_len)
                    {
                        rad = row_len;
                    }
                    else
                    {
                        rad = column_len;
                    }

                    gr.DrawLine(shipPen, x + row_len, y + column_len * 2 / 3 + 8 - column_len, x + row_len / 2, y - column_len);
                    gr.DrawLine(shipPen, x, y + column_len * 2 / 3 + 8 - column_len, x + row_len / 2, y - column_len);

                    gr.DrawLine(shipPen, x + row_len, y + column_len * 2 / 3 - column_len + 8, x + row_len, y + column_len * 4 / 3 - column_len);
                    gr.DrawLine(shipPen, x, y + column_len * 2 / 3 - column_len + 8, x, y + column_len * 4 / 3 - column_len);

                    gr.DrawLine(shipPen, x + row_len, y + column_len * 4 / 3 - column_len, x + row_len / 2, y + column_len * num_ship - column_len);
                    gr.DrawLine(shipPen, x, y + column_len * 4 / 3 - column_len, x + row_len / 2, y + column_len * num_ship - column_len);

                    gr.DrawString(label, txtFont, txtBrush1, Convert.ToInt32(x + row_len * 0.66), y + column_len * 2 - 8 - column_len);


                    gr.FillRectangle(ship_brush, x + rad / 4, y + column_len - rad / 4 + 8 - column_len, rad / 2, rad / 2);

                    gr.FillEllipse(gun_brush, x + rad / 4, y + column_len - rad / 4 + 8 - column_len, rad / 5, rad / 5);
                    gr.FillEllipse(fire_brush, x + rad / 4 + rad / 32, y + column_len - rad / 4 + 8 + rad / 32 - column_len, rad / 8, rad / 8);

                    gr.FillEllipse(gun_brush, Convert.ToInt32(x + 1.5 * rad / 4 + rad / 5), y + column_len - rad / 4 + 8 - column_len, rad / 5, rad / 5);
                    gr.FillEllipse(fire_brush, Convert.ToInt32(x + 1.5 * rad / 4 + rad / 5 + 1.5 * rad / 32), y + column_len - rad / 4 + 8 + rad / 32 - column_len, rad / 8, rad / 8);

                    gr.FillEllipse(gun_brush, x + rad / 4, y + column_len + rad / 4 - rad / 5 + 8 - column_len, rad / 5, rad / 5);
                    gr.FillEllipse(fire_brush, Convert.ToInt32(x + rad / 4 + 1.5 * rad / 32), y + column_len + rad / 4 - rad / 5 + 8 + rad / 32 - column_len, rad / 8, rad / 8);

                    gr.FillEllipse(gun_brush,Convert.ToInt32(x + rad / 4 + 1.5 * rad / 5), y + column_len + rad / 4 - rad / 5 + 8 - column_len, rad / 5, rad / 5);
                    gr.FillEllipse(fire_brush, Convert.ToInt32(x + rad / 4 + 1.5 * rad / 5 + rad / 32), y + column_len + rad / 4 - rad / 5 + 8 + rad / 32 - column_len, rad / 8, rad / 8);
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {          /*
            Main main_form = new Main();
            main_form.Show();
            this.Hide();*/
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbl_exit_MouseLeave(object sender, EventArgs e)
        {
            lbl_exit.ForeColor = Color.White;
        }

        private void lbl_exit_MouseEnter(object sender, EventArgs e)
        {
            lbl_exit.ForeColor = Color.Red;
        }
        Point lastpoint;
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
            
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }

        private void pctrBxOut_MouseClick(object sender, MouseEventArgs e)
        {
            if (is_button_clicked == true)
            {
                label1.Text = "";
                sel_row = -1;
                sel_column = -1;
                Color table_col = Color.Black;
                Graphics gr = pctrBxOut.CreateGraphics();
                Pen tablePen = new Pen(table_col, 3);
                Brush txtBrush = new SolidBrush(table_col);
                Font txtFont = new Font("Arial", 16);
                draw_table(false);
                bool isBreak = false;
                //draw_table(true);
                // gr.DrawString(".", txtFont, txtBrush, Convert.ToInt32(columns[2]), Convert.ToInt16(rows[2]));
                //gr.DrawString(".", txtFont, txtBrush, Convert.ToInt32(enemy_columns[2]), Convert.ToInt16(enemy_rows[2]));
                /*
                ships_left[0] = 2;
                ships_left[1] = 2;
                ships_left[2] = 1;
                ships_left[3] = 1;

                enemy_ships_left[0] = 2;
                enemy_ships_left[1] = 2;
                enemy_ships_left[2] = 1;
                enemy_ships_left[3] = 1;*/

                if (e.X >= enemy_rows[0] && e.X <= enemy_rows[5] && e.Y >= enemy_columns[5] && e.Y <= enemy_columns[6])
                {
                    if (ships_left[3] != 0)
                    {
                        is_horisontal = true;
                        sel_ship = 3;
                    }
                    else
                    {
                        label1.ForeColor = Color.Red;
                        label1.Text = "Error has been occured! You can't pick this ship, because you don't have any of them left";
                    }
                    //add_ship(Convert.ToInt16(enemy_rows[0]), Convert.ToInt16(enemy_columns[6]), 5, Color.Yellow, true, "" + ships_left[3]);
                }
                else if (e.X >= enemy_rows[0] && e.X <= enemy_rows[2] && e.Y >= enemy_columns[0] - column_len && e.Y <= enemy_columns[0])
                {
                    if (ships_left[0] != 0)
                    {
                        is_horisontal = true;
                        sel_ship = 0;
                    }
                    else
                    {
                        label1.ForeColor = Color.Red;
                        label1.Text = "Error has been occured! You can't pick this ship, because you don't have any of them left";
                    }
                    // add_ship(Convert.ToInt16(enemy_rows[0]), Convert.ToInt16(enemy_columns[6]), 2, Color.Yellow, true, "" + ships_left[0]);

                }
                else if (e.X >= enemy_rows[0] && e.X <= enemy_rows[3] && e.Y >= enemy_columns[1] && e.Y <= enemy_columns[2])
                {
                    if (ships_left[1] != 0)
                    {
                        is_horisontal = true;
                        sel_ship = 1;
                    }
                    else
                    {
                        label1.ForeColor = Color.Red;
                        label1.Text = "Error has been occured! You can't pick this ship, because you don't have any of them left";
                    }
                }
                else if(e.X >= enemy_rows[0] && e.X <= enemy_rows[4] && e.Y >= enemy_columns[3] && e.Y <= enemy_columns[4])
                {
                    if (ships_left[2] != 0)
                    {
                        sel_ship = 2;
                        is_horisontal = true;
                    }
                    else
                    {
                        label1.ForeColor = Color.Red;
                        label1.Text = "Error has been occured! You can't pick this ship, because you don't have any of them left";
                    }
                }
                else if(e.X >= enemy_rows[7] && e.X <= enemy_rows[8] && e.Y >= enemy_columns[0] && e.Y <= enemy_columns[2])
                {
                    if (ships_left[0] != 0)
                    {
                        is_horisontal = false;
                        sel_ship = 0;
                    }
                    else
                    {
                        label1.ForeColor = Color.Red;
                        label1.Text = "Error has been occured! You can't pick this ship, because you don't have any of them left";
                    }
                }
                else if (e.X >= enemy_rows[7] && e.X <= enemy_rows[8] && e.Y >= enemy_columns[3] && e.Y <= enemy_columns[6])
                {
                    if (ships_left[1] != 0)
                    {
                        is_horisontal = false;
                        sel_ship = 1;
                    }
                    else
                    {
                        label1.ForeColor = Color.Red;
                        label1.Text = "Error has been occured! You can't pick this ship, because you don't have any of them left";
                    }
                }
                else if (e.X >= enemy_rows[9] && e.X <= enemy_rows[9] + row_len && e.Y >= enemy_columns[0] && e.Y <= enemy_columns[4])
                {
                    if (ships_left[2] != 0)
                    {
                        is_horisontal = false;
                        sel_ship = 2;
                    }
                    else
                    {
                        label1.ForeColor = Color.Red;
                        label1.Text = "Error has been occured! You can't pick this ship, because you don't have any of them left";
                    }
                }
                else if (e.X >= enemy_rows[9] && e.X <= enemy_rows[9] + row_len && e.Y >= enemy_columns[5] && e.Y <= enemy_columns[9])
                {
                    if (ships_left[3] != 0)
                    {
                        is_horisontal = false;
                        sel_ship = 3;
                    }
                    else
                    {
                        label1.ForeColor = Color.Red;
                        label1.Text = "Error has been occured! You can't pick this ship, because you don't have any of them left";
                    }
                }

                for (int i = 0; i < 10; i++)
                {
                    if (e.Y > columns[i] - column_len && e.Y < columns[i])
                    {
                        sel_column = i;
                    }
                    if (e.X > rows[i] && e.X < rows[i] + row_len)
                    {
                        sel_row = i;
                    }
                }

                pctrBxOut.Refresh();
                draw_table(false);

                if (sel_ship != -1 && sel_row != -1 && sel_column != -1)
                {
                    if (ships_left[sel_ship] != 0)
                    {
                        for (int i = 0; i < sel_ship + 2; i++)
                        {
                            if (is_horisontal == false)
                            {
                                if (i + sel_column < 10)
                                {
                                    if (cells[sel_row, i + sel_column] == 0)
                                    {
                                        cells[sel_row, i + sel_column] = 1;
                                    }
                                    else
                                    {
                                        label1.ForeColor = Color.Red;
                                        isBreak = true;
                                        label1.Text = "Error has been occured! You can't put one ship on another one!";
                                        break;
                                    }
                                }
                                else
                                {
                                    label1.ForeColor = Color.Red;
                                    label1.Text = "Error has been occured! Index was out of the border!";
                                    isBreak = true;
                                    break;
                                }
                            }
                            else
                            {
                                if (i + sel_row < 10)
                                {
                                    if (cells[sel_row + i, sel_column] == 0)
                                    {
                                        cells[sel_row + i, sel_column] = 1;
                                    }
                                    else
                                    {
                                        label1.ForeColor = Color.Red;
                                        label1.Text = "Error has been occured! You can't put one ship on another one!";
                                        isBreak = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    label1.ForeColor = Color.Red;
                                    label1.Text = "Error has been occured! Index was out of the border!";
                                    isBreak = true;
                                    break;
                                }
                            }
                        }
                        //add_ship(Convert.ToInt16(rows[sel_row]), Convert.ToInt16(columns[sel_column]), sel_ship + 2, Color.Blue, is_horisontal, "");
                        if (isBreak == false)
                        {
                            if (sel_ship == 0)
                            {
                                if (ships[0] == 1)
                                {
                                    ships[1] = 1;
                                    row_ship[1] = sel_row;
                                    column_ship[1] = sel_column;
                                    deck_ship[1] = sel_ship + 2;
                                    hor_ship[1] = is_horisontal;
                                }
                                else
                                {
                                    ships[0] = 1;
                                    row_ship[0] = sel_row;
                                    column_ship[0] = sel_column;
                                    deck_ship[0] = sel_ship + 2;
                                    hor_ship[0] = is_horisontal;
                                }
                            }
                            else if (sel_ship == 1)
                            {
                                if (ships[2] == 1)
                                {
                                    ships[3] = 1;
                                    row_ship[3] = sel_row;
                                    column_ship[3] = sel_column;
                                    deck_ship[3] = sel_ship + 2;
                                    hor_ship[3] = is_horisontal;
                                }
                                else
                                {
                                    ships[2] = 1;
                                    row_ship[2] = sel_row;
                                    column_ship[2] = sel_column;
                                    deck_ship[2] = sel_ship + 2;
                                    hor_ship[2] = is_horisontal;
                                }
                            }
                            else if (sel_ship == 2)
                            {
                                ships[4] = 1;
                                row_ship[4] = sel_row;
                                column_ship[4] = sel_column;
                                deck_ship[4] = sel_ship + 2;
                                hor_ship[4] = is_horisontal;
                            }
                            else if (sel_ship == 3)
                            {
                                ships[5] = 1;
                                row_ship[5] = sel_row;
                                column_ship[5] = sel_column;
                                deck_ship[5] = sel_ship + 2;
                                hor_ship[5] = is_horisontal;
                            }
                            ships_left[sel_ship]--;
                        }
                    }
                    else
                    {
                        label1.ForeColor = Color.Red;
                        label1.Text = "Error has been occured! You can't put that ship, because you don't have any of them!";
                    }
                }

                for(int i = 0; i < 6; i++)
                {
                    if(ships[i] == 1)
                    {
                        add_ship(Convert.ToInt16(rows[row_ship[i]]), Convert.ToInt16(columns[column_ship[i]]), deck_ship[i] , Color.Blue, hor_ship[i], "");
                    }
                }

                add_ship(Convert.ToInt16(enemy_rows[0]), Convert.ToInt16(enemy_columns[0]), 2, Color.Blue, true, "" + ships_left[0]);
                add_ship(Convert.ToInt16(enemy_rows[0]), Convert.ToInt16(enemy_columns[2]), 3, Color.Blue, true, "" + ships_left[1]);
                add_ship(Convert.ToInt16(enemy_rows[0]), Convert.ToInt16(enemy_columns[4]), 4, Color.Blue, true, "" + ships_left[2]);
                add_ship(Convert.ToInt16(enemy_rows[0]), Convert.ToInt16(enemy_columns[6]), 5, Color.Blue, true, "" + ships_left[3]);

                add_ship(Convert.ToInt16(enemy_rows[7]), Convert.ToInt16(enemy_columns[0]), 2, Color.Blue, false, "" + ships_left[0]);
                add_ship(Convert.ToInt16(enemy_rows[7]), Convert.ToInt16(enemy_columns[3]), 3, Color.Blue, false, "" + ships_left[1]);
                add_ship(Convert.ToInt16(enemy_rows[9]), Convert.ToInt16(enemy_columns[0]), 4, Color.Blue, false, "" + ships_left[2]);
                add_ship(Convert.ToInt16(enemy_rows[9]), Convert.ToInt16(enemy_columns[5]), 5, Color.Blue, false, "" + ships_left[3]);
                /*
                listBox1.Items.Clear();
                for(int i = 0; i < 10; i++)
                {
                    for(int j = 0; j < 10; j++)
                    {
                        if (cells[i, j] != 0)
                        {
                            listBox1.Items.Add("Row: " + i + " Column: " + j + " Value: " + cells[i, j]);
                        }
                    }
                }*/
            }
        }

        private void btnStart_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {/*
            Frm_start start_form = new Frm_start { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.Panel_Form.Controls.Clear();
            start_form.FormBorderStyle = FormBorderStyle.None;
            this.Panel_Form.Controls.Add(start_form);
            start_form.Show();*/
            //draw_table(false);
            //draw_table(true);
            pctrBxOut.Hide();
            fill_cells_arr();
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            btnStart.Hide();
            pctrBxOut.Show();
            pctrBxOut.BackColor = Color.White;
            pctrBxOut.Refresh();
            is_button_clicked = true;
            /*while(true)
            {
                if(pctrBxOut.BackColor != Color.White)
                {

                }
                else
                {
                    break;
                }
            }*/
            //draw_table(true);
            draw_table(false);
            /*
            add_ship(Convert.ToInt16(rows[2]), Convert.ToInt16(columns[1]), 5, Color.Blue, true, "");
            add_ship(Convert.ToInt16(rows[2]), Convert.ToInt16(columns[3]), 4, Color.Blue, true, "");
            add_ship(Convert.ToInt16(rows[2]), Convert.ToInt16(columns[5]), 2, Color.Blue, true, "");
            add_ship(Convert.ToInt16(rows[6]), Convert.ToInt16(columns[5]), 3, Color.Blue, true, "");
            add_ship(Convert.ToInt16(rows[3]), Convert.ToInt16(columns[6]), 4, Color.Blue, false, "");
            add_ship(Convert.ToInt16(rows[5]), Convert.ToInt16(columns[5]), 2, Color.Blue, false, "");
            add_ship(Convert.ToInt16(rows[9]), Convert.ToInt16(columns[0]), 2, Color.Blue, false, "");*/
            
            add_ship(Convert.ToInt16(rows[7]) + pctrBxOut.Width / 2, Convert.ToInt16(enemy_columns[0]), 2, Color.Blue, false, "" + enemy_ships_left[0]);
            add_ship(Convert.ToInt16(rows[7]) + pctrBxOut.Width / 2, Convert.ToInt16(enemy_columns[3]), 3, Color.Blue, false, "" + enemy_ships_left[1]);
            add_ship(Convert.ToInt16(rows[9]) + pctrBxOut.Width / 2, Convert.ToInt16(enemy_columns[0]), 4, Color.Blue, false, "" + enemy_ships_left[2]);
            add_ship(Convert.ToInt16(rows[9]) + pctrBxOut.Width / 2, Convert.ToInt16(enemy_columns[5]), 5, Color.Blue, false, "" + enemy_ships_left[3]);

            add_ship(Convert.ToInt16(enemy_rows[0]), Convert.ToInt16(enemy_columns[0]), 2, Color.Blue, true, "" + ships_left[0]);
            add_ship(Convert.ToInt16(enemy_rows[0]), Convert.ToInt16(enemy_columns[2]), 3, Color.Blue, true, "" + ships_left[1]);
            add_ship(Convert.ToInt16(enemy_rows[0]), Convert.ToInt16(enemy_columns[4]), 4, Color.Blue, true, "" + ships_left[2]);
            add_ship(Convert.ToInt16(enemy_rows[0]), Convert.ToInt16(enemy_columns[6]), 5, Color.Blue, true, "" + ships_left[3]);
        }

        private void pctrBxOut_Click(object sender, EventArgs e)
        {
            /*
            Color table_col = Color.Black;
            Graphics gr = pctrBxOut.CreateGraphics();
            Pen tablePen = new Pen(table_col, 3);
            Brush txtBrush = new SolidBrush(table_col);
            Font txtFont = new Font("Arial", 16);
            draw_table(false);
            //draw_table(true);
            // gr.DrawString(".", txtFont, txtBrush, Convert.ToInt32(columns[2]), Convert.ToInt16(rows[2]));
            //gr.DrawString(".", txtFont, txtBrush, Convert.ToInt32(enemy_columns[2]), Convert.ToInt16(enemy_rows[2]));
            /*
            ships_left[0] = 2;
            ships_left[1] = 2;
            ships_left[2] = 1;
            ships_left[3] = 1;

            enemy_ships_left[0] = 2;
            enemy_ships_left[1] = 2;
            enemy_ships_left[2] = 1;
            enemy_ships_left[3] = 1;

            add_ship(Convert.ToInt16(rows[2]), Convert.ToInt16(columns[1]), 5, Color.Blue, true, "");
            add_ship(Convert.ToInt16(rows[2]), Convert.ToInt16(columns[3]), 4, Color.Blue, true, "");
            add_ship(Convert.ToInt16(rows[2]), Convert.ToInt16(columns[5]), 2, Color.Blue, true, "");
            add_ship(Convert.ToInt16(rows[6]), Convert.ToInt16(columns[5]), 3, Color.Blue, true, "");
            add_ship(Convert.ToInt16(rows[3]), Convert.ToInt16(columns[6]), 4, Color.Blue, false, "");
            add_ship(Convert.ToInt16(rows[5]), Convert.ToInt16(columns[5]), 2, Color.Blue, false, "");
            add_ship(Convert.ToInt16(rows[9]), Convert.ToInt16(columns[0]), 2, Color.Blue, false, "");

            add_ship(Convert.ToInt16(enemy_rows[0]), Convert.ToInt16(enemy_columns[0]), 2, Color.Blue, true, "" + ships_left[0]);
            add_ship(Convert.ToInt16(enemy_rows[0]), Convert.ToInt16(enemy_columns[2]), 3, Color.Blue, true, "" + ships_left[1]);
            add_ship(Convert.ToInt16(enemy_rows[0]), Convert.ToInt16(enemy_columns[4]), 4, Color.Blue, true, "" + ships_left[2]);
            add_ship(Convert.ToInt16(enemy_rows[0]), Convert.ToInt16(enemy_columns[6]), 5, Color.Blue, true, "" + ships_left[3]);

            add_ship(Convert.ToInt16(enemy_rows[7]), Convert.ToInt16(enemy_columns[0]), 2, Color.Blue, false, "" + enemy_ships_left[0]);
            add_ship(Convert.ToInt16(enemy_rows[7]), Convert.ToInt16(enemy_columns[3]), 3, Color.Blue, false, "" + enemy_ships_left[1]);
            add_ship(Convert.ToInt16(enemy_rows[9]), Convert.ToInt16(enemy_columns[0]), 4, Color.Blue, false, "" + enemy_ships_left[2]);
            add_ship(Convert.ToInt16(enemy_rows[9]), Convert.ToInt16(enemy_columns[5]), 5, Color.Blue, false, "" + enemy_ships_left[3]);
         */   
        }
    }
}
