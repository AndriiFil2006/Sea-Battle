namespace SeaBattle
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lbl_exit = new System.Windows.Forms.Label();
            this.pctrBxOut = new System.Windows.Forms.PictureBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.lbl_diff = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.show_panel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pctrBxOut)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_exit
            // 
            this.lbl_exit.AutoSize = true;
            this.lbl_exit.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl_exit.Location = new System.Drawing.Point(1162, 9);
            this.lbl_exit.Name = "lbl_exit";
            this.lbl_exit.Size = new System.Drawing.Size(26, 25);
            this.lbl_exit.TabIndex = 1;
            this.lbl_exit.Text = "X";
            this.lbl_exit.Click += new System.EventHandler(this.label1_Click);
            this.lbl_exit.MouseEnter += new System.EventHandler(this.lbl_exit_MouseEnter);
            this.lbl_exit.MouseLeave += new System.EventHandler(this.lbl_exit_MouseLeave);
            // 
            // pctrBxOut
            // 
            this.pctrBxOut.BackColor = System.Drawing.Color.LightSlateGray;
            this.pctrBxOut.Location = new System.Drawing.Point(-2, 37);
            this.pctrBxOut.Name = "pctrBxOut";
            this.pctrBxOut.Size = new System.Drawing.Size(1204, 653);
            this.pctrBxOut.TabIndex = 3;
            this.pctrBxOut.TabStop = false;
            this.pctrBxOut.Click += new System.EventHandler(this.pctrBxOut_Click);
            this.pctrBxOut.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pctrBxOut_MouseClick);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(536, 572);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(210, 95);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.button1_Click_1);
            this.btnStart.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnStart_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 25);
            this.label1.TabIndex = 5;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 25;
            this.listBox1.Location = new System.Drawing.Point(1214, 9);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(374, 679);
            this.listBox1.TabIndex = 6;
            // 
            // lbl_diff
            // 
            this.lbl_diff.AutoSize = true;
            this.lbl_diff.ForeColor = System.Drawing.Color.White;
            this.lbl_diff.Location = new System.Drawing.Point(545, 237);
            this.lbl_diff.Name = "lbl_diff";
            this.lbl_diff.Size = new System.Drawing.Size(155, 25);
            this.lbl_diff.TabIndex = 7;
            this.lbl_diff.Text = "Pick the difficulty";
            this.lbl_diff.Click += new System.EventHandler(this.label2_Click_1);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(550, 276);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(150, 33);
            this.comboBox1.TabIndex = 8;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // show_panel
            // 
            this.show_panel.Location = new System.Drawing.Point(12, 13);
            this.show_panel.Name = "show_panel";
            this.show_panel.Size = new System.Drawing.Size(42, 21);
            this.show_panel.TabIndex = 9;
            this.show_panel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(1554, 688);
            this.Controls.Add(this.show_panel);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lbl_diff);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.pctrBxOut);
            this.Controls.Add(this.lbl_exit);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form Start";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pctrBxOut)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbl_exit;
        private System.Windows.Forms.PictureBox pctrBxOut;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label lbl_diff;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel show_panel;
    }
}

