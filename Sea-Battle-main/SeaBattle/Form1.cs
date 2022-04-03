﻿using System;
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
        int ship_cross = -1;
        int cross_on_ship = -1;

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
      //  int[] enemy_deck_ship = new int[8];
        //int[] ships_left = new int[8];
        //int[] enemy_ships_left = new int[8];
        int sel_ship = -1;
        int[] ships_left = { 2, 2, 1, 1 };
        int[] enemy_ships_left = { 2, 2, 1, 1 };
        int sel_row, sel_column;
        bool is_horisontal = true;
        // array that will have 4 values about cell: 0 - no ship on cell; 1 - alive untoacheble ship on the cell, 2 -alive shoted ship on the cell, 3 - rip, 4 - shoted cell, with no ship
        int[,] cells = new int[10, 10];
        int[,] enemy_cells = new int[10, 10];
        bool is_button_clicked = false;
        bool cont = false;
        int[,] shots = new int[10, 10];
        int[,] enemy_shots = new int[10, 10];
        bool players_turn = true;

        public void fill_cells_arr()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    cells[i, j] = 0;
                    enemy_cells[i, j] = 0;
                    shots[i, j] = 0;
                    enemy_shots[i, j] = 0;
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

                    gr.DrawString("" + (i + 1), txtFont, txtBrush, Convert.ToInt32(x_indent + (pctrBxOut.Width - 60) / (2 * 11) * i + (pctrBxOut.Width - 60) / (2 * 11) * 0.5 - 8), y_indent - 25);
                    rows[i] = 30 + (pctrBxOut.Width - 60) / (2 * 11) * i;
                    //enemy_columns[i] = pctrBxOut.Width / 2 + 30 + (pctrBxOut.Width - 60) / (2 * 11) * i;
                    enemy_columns[i] = y_indent + (pctrBxOut.Height - 60) / 10 * (i + 1);
                    enemy_rows[i] = 30 + (pctrBxOut.Width - 60) / (2 * 11) * i + pctrBxOut.Width / 2;
                   //listBox1.Items.Add("I: " + i + " enemy_row " + enemy_rows[i] + " enemy_columns " + enemy_columns[i]);
                   // listBox1.Items.Add("I: " + i + " row " + rows[i] + " columns " + columns[i]);
                }
                gr.DrawLine(tablePen, x_indent , y_indent + (pctrBxOut.Height - 50) / (10) * i, x_indent + pctrBxOut.Width / 2 - 60 - 15 - 4, y_indent + (pctrBxOut.Height - 50) / (10) * i);
            }
            row_len = Convert.ToInt16(rows[2] - rows[1]);
            column_len = Convert.ToInt16(columns[2] - columns[1]);
        }

        public void put_enemies_ships()
        {
            Random rand = new Random();
            //Random rand_col = new Random();
            //Random rand_hor = new Random();
            int num_deck = 0;
            enemy_ships_left[0] = 2;
            enemy_ships_left[1] = 2;
            enemy_ships_left[2] = 1;
            enemy_ships_left[3] = 1;

            for (int i = 0; i < 6; i++)
            {
                if (i == 0 || i == 1)
                {
                    num_deck = 2;
                }
                else if (i == 2 || i == 3)
                {
                    num_deck = 3;
                }
                else if (i == 4)
                {
                    num_deck = 4;
                }
                else
                {
                    num_deck = 5;
                }
                while (true)
                {
                    int left_ships = 0;
                    for (int j = 0; j < enemy_ships_left.Length; j++)
                    {
                        left_ships += enemy_ships_left[num_deck - 2];
                    }

                    int row = rand.Next(0, 10);
                    int column = rand.Next(0, 10);
                    bool is_hor = false;
                    if (rand.Next(0, 2) == 0)
                    {
                        is_hor = false;
                    }
                    else if(rand.Next(0, 2) == 1)
                    {
                        is_hor = true;
                    }
                    bool is_break = false;                    
                    for(int j = 0; j < num_deck; j++)
                    {
                        if(is_hor == false)
                        {
                            if (column + num_deck < 10)
                            {
                                if (enemy_cells[row, column + j] == 0)
                                {
                                    is_break = true;
                                }
                                else
                                {
                                    is_break = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (row + num_deck < 10)
                            {
                                if (enemy_cells[row + j, column] == 0)
                                {
                                    is_break = true;
                                }
                                else
                                {
                                    is_break = false;
                                    break;
                                }
                            }
                        }
                    }
                    if(is_break == true)
                    {
                        if (is_hor == false)
                        {
                            for (int j = 0; j < num_deck; j++)
                            {
                                enemy_cells[row, column + j] = 1;
                            }
                        }
                        else
                        {
                            for (int j = 0; j < num_deck; j++)
                            {
                                enemy_cells[row + j, column] = 1;
                            }
                        }
                        enemy_row_ship[i] = row;
                        enemy_column_ship[i] = column;
                        enemy_hor_ship[i] = is_hor;
                        enemy_ships[i] = 1;
                        enemy_ships_left[num_deck - 2]--;
                        break;
                    }
                }
            }
            enemy_ships_left[0] = 2;
            enemy_ships_left[1] = 2;
            enemy_ships_left[2] = 1;
            enemy_ships_left[3] = 1;
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
                    y = y + y / column_len;
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
                    y = y + y / column_len;
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

        public void draw_hit(int x, int y)
        {
            y = y + y / column_len;
            Graphics gr = pctrBxOut.CreateGraphics();
            Pen redPen = new Pen(Color.Red, 3);
            Pen yellowPen = new Pen(Color.Yellow, 2);
            
            gr.DrawLine(redPen, x + row_len / 2, y - column_len, x + row_len / 3, y - column_len / 2);           
            gr.DrawLine(redPen, x + row_len / 2, y - column_len, x + 2 * row_len / 3, y - column_len / 2);
            gr.DrawLine(yellowPen, x + row_len / 2, y - column_len, x + row_len / 3 + 2, y - column_len / 2);
            gr.DrawLine(yellowPen, x + row_len / 2, y - column_len, x + 2 * row_len / 3 - 2, y - column_len / 2);

            gr.DrawLine(redPen, x + row_len, y - column_len * 3 / 4, x + 2 * row_len / 3, y - column_len / 4);
            gr.DrawLine(redPen, x + row_len, y - column_len * 3 / 4, x + 2 * row_len / 3, y - column_len / 2);
            gr.DrawLine(yellowPen, x + row_len, y - column_len * 3 / 4, x + 2 * row_len / 3 + 2, y - column_len / 2);
            gr.DrawLine(yellowPen, x + row_len, y - column_len * 3 / 4, x + 2 * row_len / 3 - 2, y - column_len / 4);
            
            gr.DrawLine(redPen, x, y - column_len * 3 / 4, x + row_len / 3, y - column_len / 4);
            gr.DrawLine(redPen, x, y - column_len * 3 / 4, x + row_len / 3, y - column_len / 2);
            gr.DrawLine(yellowPen, x, y - column_len * 3 / 4, x + row_len / 3 + 2, y - column_len / 2);
            gr.DrawLine(yellowPen, x, y - column_len * 3 / 4, x + row_len / 3 - 2, y - column_len / 4);

            gr.DrawLine(redPen, x + row_len / 6, y, x + row_len / 3, y - column_len / 4);
            gr.DrawLine(redPen, x + row_len / 2, y - column_len / 4, x + row_len / 6, y);
            gr.DrawLine(yellowPen, x + row_len / 2, y - column_len / 4 - 2, x + row_len / 6, y);
            gr.DrawLine(yellowPen, x + row_len / 6, y, x + row_len / 3 - 2, y - column_len / 4);

            gr.DrawLine(redPen, x + row_len * 5 / 6, y, x + 2 * row_len / 3, y - column_len / 4);
            gr.DrawLine(redPen, x + row_len / 2, y - column_len / 4, x + row_len * 5 / 6, y);
            gr.DrawLine(yellowPen, x + row_len / 2, y - column_len / 4 - 2, x + row_len * 5 / 6, y);
            gr.DrawLine(yellowPen, x + row_len * 5 / 6, y, x + 2 * row_len / 3 + 2, y - column_len / 4);
        }
        public void draw_cross(int x, int y)
        {
            y = y + y / column_len;
            Graphics gr = pctrBxOut.CreateGraphics();
            Pen tablePen = new Pen(Color.Red, 3);
            gr.DrawLine(tablePen, x, y, x + row_len, y - column_len);
            gr.DrawLine(tablePen, x + row_len, y, x, y - column_len);
        }

        public void draw_small_cross(int x, int y)
        {
            Graphics gr = pctrBxOut.CreateGraphics();
            Pen tablePen = new Pen(Color.Red, 3);
            gr.DrawLine(tablePen, x, y, x + 15, y - 15);
            gr.DrawLine(tablePen, x + 15, y, x, y - 15);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }

        private void pctrBxOut_MouseClick(object sender, MouseEventArgs e)
        {
            listBox1.Items.Clear();
            if (is_button_clicked == true)
            {
                btnStart.Hide();
                //cross_on_ship = -1;
                //listBox1.Items.Clear();
                int left_ships = 0;
                for (int i = 0; i < ships_left.Length; i++)
                {
                    if (ships_left[i] != 0)
                    {
                        left_ships += ships_left[i];
                    }
                }
                if (cont == false)
                {
                    //listBox1.Items.Clear();
                    for(int i = 0; i < 10; i++)
                    {
                        for(int j = 0; j < 10; j++)
                        {
                            if (cells[i, j] != 0)
                            {
                               // listBox1.Items.Add("Rows " + i + " Columns " + j + " Value " + cells[i, j]);
                            }
                        }
                    }
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
                    else if (e.X >= enemy_rows[0] && e.X <= enemy_rows[4] && e.Y >= enemy_columns[3] && e.Y <= enemy_columns[4])
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
                    else if (e.X >= enemy_rows[7] && e.X <= enemy_rows[8] && e.Y >= enemy_columns[0] && e.Y <= enemy_columns[2])
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
                    //draw_cross(Convert.ToInt32(rows[2]), Convert.ToInt32(columns[2]));
                    //draw_small_cross(Convert.ToInt32(rows[0]), Convert.ToInt32(columns[0]));
                    int sel_cell_with_ship = 0;

                    if (sel_ship != -1 && sel_row != -1 && sel_column != -1)
                    {
                        //isBreak == true;
                        if (ships_left[sel_ship] == 0)
                        {
                            isBreak = true;
                        }
                            for (int i = 0; i < sel_ship + 2; i++)
                            {
                                if (is_horisontal == false)
                                {
                                    if (i + sel_column < 10)
                                    {
                                        if (cells[sel_row, i + sel_column] == 0)
                                        {
                                            //cells[sel_row, i + sel_column] = 1;
                                        }
                                        else
                                        {
                                            label1.ForeColor = Color.Red;
                                            isBreak = true;
                                            if (i == 0)
                                            {
                                                sel_cell_with_ship++;
                                            }
                                            else
                                            {
                                                isBreak = true;
                                                label1.Text = "Error has been occured! You can't put one ship on another one!";
                                            }
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
                                            //cells[sel_row + i, sel_column] = 1;
                                        }
                                        else
                                        {
                                            label1.ForeColor = Color.Red;
                                            isBreak = true;
                                            if (i == 0)
                                            {
                                                sel_cell_with_ship++;
                                            }
                                            else
                                            {
                                                isBreak = true;
                                                label1.Text = "Error has been occured! You can't put one ship on another one!";
                                            }
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
                            for(int i = 0; i < sel_ship + 2; i++)
                            {
                                if(is_horisontal == false)
                                {
                                    cells[sel_row, sel_column + i] = 1;
                                }
                                else
                                {
                                    cells[sel_row + i, sel_column] = 1;
                                }
                            }
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
                                if (left_ships == 1)
                                {
                                    pctrBxOut_MouseClick(pctrBxOut, e);
                                }        
                            }
                            else
                            {
                                if (sel_cell_with_ship != 0)
                                {
                                    bool broken = false;
                                    int needed_ship = -1;
                                    for (int i = 0; i < ships.Length; i++)
                                    {
                                       // listBox1.Items.Add("ships.Length: " + ships.Length);
                                        if (ships[i] == 1)
                                        {
                                            int curr_ship;
                                            if (i == 0 || i == 1)
                                            {
                                                curr_ship = 2;
                                            }
                                            else if (i == 2 || i == 3)
                                            {
                                                curr_ship = 3;
                                            }
                                            else if (i == 4)
                                            {
                                                curr_ship = 4;
                                            }
                                            else
                                            {
                                                curr_ship = 5;
                                            }
                                            for (int j = 0; j <= curr_ship; j++)
                                            {
                                                if (hor_ship[i] == true)
                                                {
                                                    if (column_ship[i] == sel_column && (row_ship[i] + j) == sel_row + 1)
                                                    {
                                                        needed_ship = i;
                                                        //listBox1.Items.Add("exp " + (row_ship[i] + j));
                                                        //listBox1.Items.Add("reality " + (sel_row + 1));
                                                        broken = true;
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    if (column_ship[i] + j == sel_column + 1 && row_ship[i] == sel_row)
                                                    {
                                                        needed_ship = i;
                                                        //listBox1.Items.Add("i: " + i);
                                                        broken = true;
                                                        break;
                                                    }
                                                }
                                            }
                                        }  
                                        if(broken == true)
                                        {
                                            break;
                                        }
                                    }
                                    ship_cross = needed_ship;
                                    //listBox1.Items.Clear();
                                    //listBox1.Items.Add("ships_cross: " + ship_cross);
                                    //listBox1.Items.Add("needed_ship " + needed_ship);
                                    if (is_horisontal == true)
                                    {
                                        //column_ship[2] == sel_column && row_ship[2] + 3 + 1 == sel_row
                                        //listBox1.Items.Add("Colomn_ship " + (column_ship[2]) + " Sel_column: " + sel_column);
                                        //listBox1.Items.Add("row_ship " + (row_ship[2] + 2) + " Sel_row " + sel_row);

                                    }
                                    else
                                    {
                                       // listBox1.Items.Add("Colomn_ship " + (column_ship[2] + 3) + " Sel_column: " + sel_column);
                                        //listBox1.Items.Add("row_ship " + row_ship[2] + " Sel_row " + sel_row);
                                    }
                                }
                            }
                        }
                    /* else
                     {
                         label1.ForeColor = Color.Red;
                         label1.Text = "Error has been occured! You can't put that ship, because you don't have any of them!";
                     }*/
                    //}

                    if (cross_on_ship != -1)
                    {
                        int curr_ship = -1;
                        if (ships[ship_cross] == 1)
                        {
                            if (ship_cross == 0 || ship_cross == 1)
                            {
                                curr_ship = 0;
                            }
                            else if (ship_cross == 2 || ship_cross == 3)
                            {
                                curr_ship = 1;
                            }
                            else if (ship_cross == 4)
                            {
                                curr_ship = 2;
                            }
                            else
                            {
                                curr_ship = 3;
                            }
                        }
                        if (hor_ship[ship_cross] == true)
                        {
                            if (e.X > Convert.ToInt16(rows[row_ship[ship_cross]]) && e.Y < Convert.ToInt16(columns[column_ship[ship_cross]] + column_ship[ship_cross]) && e.X < Convert.ToInt16(rows[row_ship[ship_cross]] + 15) && e.Y > Convert.ToInt16(columns[column_ship[ship_cross]] - 15 + column_ship[ship_cross]))
                            {
                                for (int i = row_ship[ship_cross]; i < row_ship[ship_cross] + curr_ship + 2; i++)
                                {
                                    cells[i, column_ship[ship_cross]] = 0;
                                    //listBox1.Items.Add("" + (cells[i, column_ship[ship_cross]]));
                                }
                                ships_left[curr_ship]++;
                                ships[ship_cross] = 0;
                                row_ship[ship_cross] = -1;
                                column_ship[ship_cross] = -1;
                                cross_on_ship = -1;
                                ship_cross = -1;
                            }
                            else
                            {
                                cross_on_ship = -1;
                                ship_cross = -1;
                            }
                        }
                        else
                        {
                            if (e.X > Convert.ToInt16(rows[row_ship[ship_cross]]) && e.Y < Convert.ToInt16(columns[column_ship[ship_cross]]- column_len + 15) && e.X < Convert.ToInt16(rows[row_ship[ship_cross]] + 15) && e.Y > Convert.ToInt16(columns[column_ship[ship_cross]] - column_len))
                            {
                                for (int i = column_ship[ship_cross]; i < column_ship[ship_cross] + curr_ship + 2; i++)
                                {
                                    cells[row_ship[ship_cross], i] = 0;
                                    //listBox1.Items.Add("" + (cells[i, column_ship[ship_cross]]));
                                }
                                ships_left[curr_ship]++;
                                ships[ship_cross] = 0;
                                row_ship[ship_cross] = -1;
                                column_ship[ship_cross] = -1;
                                cross_on_ship = -1;
                                ship_cross = -1;
                            }
                            else
                            {
                                cross_on_ship = -1;
                                ship_cross = -1;
                            }
                        }
                    }

                    for (int i = 0; i < 6; i++)
                    {
                        if (ships[i] == 1)
                        {
                            add_ship(Convert.ToInt16(rows[row_ship[i]]), Convert.ToInt16(columns[column_ship[i]]), deck_ship[i], Color.Blue, hor_ship[i], "");
                        }
                    }

                    if (ship_cross != -1)
                    {
                        if (hor_ship[ship_cross] == true)
                        {
                            draw_small_cross(Convert.ToInt32(rows[row_ship[ship_cross]]), Convert.ToInt32(columns[column_ship[ship_cross]] + column_ship[ship_cross]));
                        }
                        else
                        {
                            draw_small_cross(Convert.ToInt32(rows[row_ship[ship_cross]]), Convert.ToInt32(columns[column_ship[ship_cross]] - column_len + 15));
                        }
                        cross_on_ship = 1;
                    }
                    else
                    {
                        //listBox1.Items.Add("ship_cross ==  " + ship_cross + " cross_on ship ==" + cross_on_ship);
                    }
                    //label1.Text = "Crossed ship " + ship_cross;

                    if (left_ships != 0)
                    {
                        add_ship(Convert.ToInt16(enemy_rows[0]), Convert.ToInt16(enemy_columns[0]), 2, Color.Blue, true, "" + ships_left[0]);
                        add_ship(Convert.ToInt16(enemy_rows[0]), Convert.ToInt16(enemy_columns[2]), 3, Color.Blue, true, "" + ships_left[1]);
                        add_ship(Convert.ToInt16(enemy_rows[0]), Convert.ToInt16(enemy_columns[4]), 4, Color.Blue, true, "" + ships_left[2]);
                        add_ship(Convert.ToInt16(enemy_rows[0]), Convert.ToInt16(enemy_columns[6]), 5, Color.Blue, true, "" + ships_left[3]);

                        add_ship(Convert.ToInt16(enemy_rows[7]), Convert.ToInt16(enemy_columns[0]), 2, Color.Blue, false, "" + ships_left[0]);
                        add_ship(Convert.ToInt16(enemy_rows[7]), Convert.ToInt16(enemy_columns[3]), 3, Color.Blue, false, "" + ships_left[1]);
                        add_ship(Convert.ToInt16(enemy_rows[9]), Convert.ToInt16(enemy_columns[0]), 4, Color.Blue, false, "" + ships_left[2]);
                        add_ship(Convert.ToInt16(enemy_rows[9]), Convert.ToInt16(enemy_columns[5]), 5, Color.Blue, false, "" + ships_left[3]);
                    }
                    else
                    {
                        label1.Text = "No ships left";
                        btnStart.Text = "Ready!";
                        btnStart.Size = new Size(200, 100);
                        btnStart.Location = new Point(this.Width - (Math.Abs(this.Width - pctrBxOut.Width)) - 210, this.Height - (Math.Abs(this.Height - pctrBxOut.Height)) - 55);
                        btnStart.Show();
                    }
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
                if(cont == true)
                {
                    /*
                    deck_ship[0] = 2;
                    deck_ship[1] = 2;
                    deck_ship[2] = 3;
                    deck_ship[3] = 3;
                    deck_ship[4] = 4;
                    deck_ship[5] = 5;*/
                    for(int i = 0; i < 6; i++)
                    {
                        listBox1.Items.Add("deck_ship[" + i + "] = " + deck_ship[i]);
                    }

                    btnStart.Hide();
                    pctrBxOut.Refresh();
                    draw_table(true);
                    draw_table(false);
                    for (int i = 0; i < 6; i++)
                    {
                        if (ships[i] == 1)
                        {
                            add_ship(Convert.ToInt16(rows[row_ship[i]]), Convert.ToInt16(columns[column_ship[i]]), deck_ship[i], Color.Blue, hor_ship[i], "");
                            add_ship(Convert.ToInt16(enemy_rows[enemy_row_ship[i]]), Convert.ToInt16(enemy_columns[enemy_column_ship[i]]), deck_ship[i], Color.Red, enemy_hor_ship[i], "");
                            //listBox1.Items.Add("i: " + i + " enemy_rows[enemy_row_ship[i]] " + enemy_rows[enemy_row_ship[i]]);
                            //listBox1.Items.Add("enemy_columns[enemy_column_ship[i]] " + enemy_columns[enemy_column_ship[i]]);
                            //listBox1.Items.Add("Enemy_row_ship[i] " + enemy_row_ship[i] + " enemy_column_ship " + enemy_column_ship[i]);
                            //listBox1.Items.Add("");
                        }
                    }  
                    if(players_turn == true)
                    {
                        label1.Text = "It's your turn";
                        label1.ForeColor = Color.Red;
                        label1.Location = new Point(pctrBxOut.Width / 2 - 14 * 6, 9);
                        int sel_hit_row = -1;
                        int sel_hit_column = -1;
                        /*
                        listBox1.Items.Clear();
                        listBox1.Items.Add("Enemy_rows[1]" + enemy_rows[1]);
                        listBox1.Items.Add("Enemy_columns[1]" + enemy_columns[1]);
                        listBox1.Items.Add("X of click: " + e.X);
                        listBox1.Items.Add("Y of click: " + e.Y);
                        */
                        int enemy_left_ships = 0;
                        for (int i = 0; i < 10; i++)
                        {
                            for(int j = 0; j < 10; j++)
                            {
                                if (shots[i, j] == 0)
                                {
                                    if (enemy_rows[i] + row_len > e.X && enemy_rows[i] < e.X && enemy_columns[j] > e.Y && enemy_columns[j] - column_len < e.Y)
                                    {
                                        sel_hit_row = i;
                                        sel_hit_column = j;
                                    }
                                }
                                else
                                {
                                    label1.Text = "You've already shoted this cell!";
                                }
                            }
                        }
                        if (sel_hit_row != -1 && sel_hit_column != -1)
                        {
                            //listBox1.Items.Clear();
                            //listBox1.Items.Add("Hitted");
                            if (enemy_cells[sel_hit_row, sel_hit_column] == 0)
                            {
                                shots[sel_hit_row, sel_hit_column] = 1;
                                listBox1.Items.Add("Missed");
                                draw_cross(Convert.ToInt16(enemy_rows[sel_hit_row]), Convert.ToInt16(enemy_columns[sel_hit_column]));
                                enemy_cells[sel_hit_row, sel_hit_column] = 4;
                                shots[sel_hit_row, sel_hit_column] = 1;
                            }
                            else if(enemy_cells[sel_hit_row, sel_hit_column] == 1)
                            {
                                shots[sel_hit_row, sel_hit_column] = 2;
                                listBox1.Items.Add("Hitted");
                                draw_hit(Convert.ToInt16(enemy_rows[sel_hit_row]), Convert.ToInt16(enemy_columns[sel_hit_column]));
                                enemy_cells[sel_hit_row, sel_hit_column] = 2;
                                shots[sel_hit_row, sel_hit_column] = 2;
                                for (int i = 0; i < enemy_ships.Length; i++)
                                {
                                    bool matched = true;
                                    for(int j = 0; j < deck_ship[i]; j++)
                                    {
                                        listBox1.Items.Add("deck_ship[" + i + "] = " + deck_ship[i]);

                                        if (enemy_hor_ship[i] == false)
                                        {
                                            if(shots[enemy_row_ship[i], enemy_column_ship[i] + j] != 2)
                                            {
                                                matched = false;
                                            }
                                        }
                                        else
                                        {
                                            if (shots[enemy_row_ship[i] + j, enemy_column_ship[i]] != 2)
                                            {
                                                matched = false;
                                            }
                                        }
                                    }
                                    if(matched)
                                    {
                                        enemy_ships[i] = 3;
                                        if(i == 0 || i == 1)
                                        {
                                            if(enemy_ships_left[0] != 0)
                                            {
                                                enemy_ships_left[0] = 1;
                                            }
                                            if(enemy_ships[0] == 3 && enemy_ships[1] == 3)
                                            {
                                                enemy_ships_left[0] = 0;
                                            }
                                        }
                                        else if (i == 2 || i == 3)
                                        {
                                            if (enemy_ships_left[1] != 0)
                                            {
                                                enemy_ships_left[1] = 1;
                                            }
                                            if(enemy_ships[2] == 3 && enemy_ships[3] == 3)
                                            {
                                                enemy_ships_left[1] = 0;
                                            }
                                        }
                                        else if (enemy_ships[4] == 3)
                                        {
                                            if (enemy_ships_left[2] != 0)
                                            {
                                                enemy_ships_left[2] = 0;
                                            }
                                        }
                                        else if (enemy_ships[5] == 3)
                                        {
                                            if (enemy_ships_left[3] != 0)
                                            {
                                                enemy_ships_left[3] = 0;
                                            }
                                        }
                                    }
                                }
                            }
                            bool is_all_ships_dead = false;

                            for (int i = 0; i < enemy_ships_left.Length; i++)
                            {
                                enemy_left_ships += enemy_ships_left[i];
                            }
                            /*
                            for (int j = 0; j < enemy_ships.Length; j++)
                            {
                                if (enemy_ships[j] == 3)
                                {
                                    listBox1.Items.Add("Ships[" + j + "] has been destroyed");                                
                                }
                                else
                                {
                                    is_all_ships_dead = false;
                                }
                            }*/

                            if (enemy_left_ships == 0)
                            {
                                is_all_ships_dead = true;
                                MessageBox.Show("Congratulations! You won!");
                            }

                            //listBox1.Items.Add("Enemy_ships_left.Length" + enemy_ships_left.Length);

                            listBox1.Items.Add("Enemy_left_ships = " + enemy_left_ships);

                            listBox1.Items.Add("Is_all_ships_dead " + is_all_ships_dead);

                        }

                        for (int i = 0; i < 10; i++)
                        {
                            for (int j = 0; j < 10; j++)
                            {
                                if (shots[i, j] == 1)
                                {
                                    draw_cross(Convert.ToInt32(enemy_rows[i]), Convert.ToInt32(enemy_columns[j]));
                                }
                                if (shots[i, j] == 2)
                                {
                                    draw_hit(Convert.ToInt32(enemy_rows[i]), Convert.ToInt32(enemy_columns[j]));
                                }
                            }
                        }
                    }
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            //label2.Text = "loh";
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            put_enemies_ships();
        }

        private void btnStart_MouseClick(object sender, MouseEventArgs e)
        {
            if(is_button_clicked == false)
            {
                btnStart.Hide();
                pctrBxOut.Show();
                pctrBxOut.BackColor = Color.White;
                pctrBxOut.Refresh();
                is_button_clicked = true;
                pctrBxOut_MouseClick(pctrBxOut, e);
                put_enemies_ships();
                //draw_hit(Convert.ToInt16(rows[0]), Convert.ToInt16(columns[0]));
                /*
                for(int i = 0; i < 6; i++)
                {
                    listBox1.Items.Add("Ship: " + i + " Row: " + enemy_rows[i] + " Column: " + enemy_columns[i]);
                    listBox1.Items.Add(" Is hor: " + enemy_hor_ship[i]);
                }
                for(int i = 0; i < 10; i++)
                {
                    for(int j = 0; j < 10; j++)
                    {
                        if(enemy_cells[i,j] != 0)
                        {
                            listBox1.Items.Add("Cell_row " + i + " Cell_col " + j + " Value: " + enemy_cells[i, j]);
                        }
                    }
                }
                */
            }
            else if(cont == false) //&& e.X > this.Width * 3 / 4)
            {
                cont = true;
                //InvokeOnClick(pctrBxOut, e);
                pctrBxOut_MouseClick(pctrBxOut, e);
            }
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
            /*
            if (is_button_clicked == false)
            {
                btnStart.Hide();
                pctrBxOut.Show();
                pctrBxOut.BackColor = Color.White;
                pctrBxOut.Refresh();
                is_button_clicked = true;
                while(true)
                {
                    if(pctrBxOut.BackColor != Color.White)
                    {

                    }
                    else
                    {
                        break;
                    }
                }
                //draw_table(true);
                //draw_table(false);
                
                add_ship(Convert.ToInt16(rows[2]), Convert.ToInt16(columns[1]), 5, Color.Blue, true, "");
                add_ship(Convert.ToInt16(rows[2]), Convert.ToInt16(columns[3]), 4, Color.Blue, true, "");
                add_ship(Convert.ToInt16(rows[2]), Convert.ToInt16(columns[5]), 2, Color.Blue, true, "");
                add_ship(Convert.ToInt16(rows[6]), Convert.ToInt16(columns[5]), 3, Color.Blue, true, "");
                add_ship(Convert.ToInt16(rows[3]), Convert.ToInt16(columns[6]), 4, Color.Blue, false, "");
                add_ship(Convert.ToInt16(rows[5]), Convert.ToInt16(columns[5]), 2, Color.Blue, false, "");
                add_ship(Convert.ToInt16(rows[9]), Convert.ToInt16(columns[0]), 2, Color.Blue, false, "");

                add_ship(Convert.ToInt16(rows[7]) + pctrBxOut.Width / 2, Convert.ToInt16(enemy_columns[0]), 2, Color.Blue, false, "" + enemy_ships_left[0]);
                add_ship(Convert.ToInt16(rows[7]) + pctrBxOut.Width / 2, Convert.ToInt16(enemy_columns[3]), 3, Color.Blue, false, "" + enemy_ships_left[1]);
                add_ship(Convert.ToInt16(rows[9]) + pctrBxOut.Width / 2, Convert.ToInt16(enemy_columns[0]), 4, Color.Blue, false, "" + enemy_ships_left[2]);
                add_ship(Convert.ToInt16(rows[9]) + pctrBxOut.Width / 2, Convert.ToInt16(enemy_columns[5]), 5, Color.Blue, false, "" + enemy_ships_left[3]);

                add_ship(Convert.ToInt16(enemy_rows[0]), Convert.ToInt16(enemy_columns[0]), 2, Color.Blue, true, "" + ships_left[0]);
                add_ship(Convert.ToInt16(enemy_rows[0]), Convert.ToInt16(enemy_columns[2]), 3, Color.Blue, true, "" + ships_left[1]);
                add_ship(Convert.ToInt16(enemy_rows[0]), Convert.ToInt16(enemy_columns[4]), 4, Color.Blue, true, "" + ships_left[2]);
                add_ship(Convert.ToInt16(enemy_rows[0]), Convert.ToInt16(enemy_columns[6]), 5, Color.Blue, true, "" + ships_left[3]);
            }
            else if(cont == false)
            {
                cont = true;
                //InvokeOnClick(pctrBxOut, e);
                pctrBxOut_MouseClick(pctrBxOut, e);
            }*/
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
