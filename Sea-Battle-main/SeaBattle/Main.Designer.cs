namespace SeaBattle
{
    partial class Main
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
            this.OutputBox = new System.Windows.Forms.PictureBox();
            this.lbl_X = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.OutputBox)).BeginInit();
            this.SuspendLayout();
            // 
            // OutputBox
            // 
            this.OutputBox.BackColor = System.Drawing.Color.White;
            this.OutputBox.Location = new System.Drawing.Point(13, 48);
            this.OutputBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.Size = new System.Drawing.Size(550, 550);
            this.OutputBox.TabIndex = 0;
            this.OutputBox.TabStop = false;
            this.OutputBox.Click += new System.EventHandler(this.OutputBox_Click);
            this.OutputBox.MouseEnter += new System.EventHandler(this.OutputBox_MouseEnter);
            // 
            // lbl_X
            // 
            this.lbl_X.AutoSize = true;
            this.lbl_X.ForeColor = System.Drawing.Color.White;
            this.lbl_X.Location = new System.Drawing.Point(670, 9);
            this.lbl_X.Name = "lbl_X";
            this.lbl_X.Size = new System.Drawing.Size(26, 25);
            this.lbl_X.TabIndex = 1;
            this.lbl_X.Text = "X";
            this.lbl_X.Click += new System.EventHandler(this.lbl_X_Click);
            this.lbl_X.MouseEnter += new System.EventHandler(this.lbl_X_MouseEnter);
            this.lbl_X.MouseLeave += new System.EventHandler(this.lbl_X_MouseLeave);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(673, 666);
            this.Controls.Add(this.lbl_X);
            this.Controls.Add(this.OutputBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Main_MouseDown);
            this.MouseEnter += new System.EventHandler(this.Main_MouseEnter);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Main_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.OutputBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox OutputBox;
        private System.Windows.Forms.Label lbl_X;
    }
}