namespace SeaBattle
{
    partial class won_or_lost_form
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
            this.who_won_lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // who_won_lbl
            // 
            this.who_won_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.who_won_lbl.AutoSize = true;
            this.who_won_lbl.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.who_won_lbl.ForeColor = System.Drawing.Color.Red;
            this.who_won_lbl.Location = new System.Drawing.Point(20, 9);
            this.who_won_lbl.Name = "who_won_lbl";
            this.who_won_lbl.Size = new System.Drawing.Size(0, 85);
            this.who_won_lbl.TabIndex = 0;
            this.who_won_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // won_or_lost_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(712, 492);
            this.Controls.Add(this.who_won_lbl);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "won_or_lost_form";
            this.Text = "won_or_lost_form";
            this.Load += new System.EventHandler(this.won_or_lost_form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label who_won_lbl;
    }
}