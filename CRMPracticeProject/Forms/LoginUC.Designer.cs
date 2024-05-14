namespace CRMPracticeProject.Forms
{
    partial class LoginUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.symbolBox1 = new DevComponents.DotNetBar.Controls.SymbolBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBoxX5 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxX3 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // symbolBox1
            // 
            // 
            // 
            // 
            this.symbolBox1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.symbolBox1.ForeColor = System.Drawing.Color.White;
            this.symbolBox1.Location = new System.Drawing.Point(204, 23);
            this.symbolBox1.Name = "symbolBox1";
            this.symbolBox1.Size = new System.Drawing.Size(192, 198);
            this.symbolBox1.Symbol = "";
            this.symbolBox1.SymbolColor = System.Drawing.Color.White;
            this.symbolBox1.TabIndex = 21;
            this.symbolBox1.Text = "symbolBox1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(73, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(450, 25);
            this.label2.TabIndex = 18;
            this.label2.Text = "Enter Your Infomations to Login User Account";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.textBoxX5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.textBoxX3);
            this.panel3.Location = new System.Drawing.Point(6, 260);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(591, 219);
            this.panel3.TabIndex = 22;
            // 
            // textBoxX5
            // 
            this.textBoxX5.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.textBoxX5.Border.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxX5.Border.Class = "TextBoxBorder";
            this.textBoxX5.Border.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.textBoxX5.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.textBoxX5.Location = new System.Drawing.Point(41, 97);
            this.textBoxX5.Name = "textBoxX5";
            this.textBoxX5.PasswordChar = '*';
            this.textBoxX5.PreventEnterBeep = true;
            this.textBoxX5.Size = new System.Drawing.Size(513, 39);
            this.textBoxX5.TabIndex = 15;
            this.textBoxX5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxX5.WatermarkText = "Password";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pictureBox3);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel4.Location = new System.Drawing.Point(432, 147);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(122, 57);
            this.panel4.TabIndex = 13;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::CRMPracticeProject.Properties.Resources.CheckMark;
            this.pictureBox3.Location = new System.Drawing.Point(73, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(46, 47);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 1;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(6, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 20);
            this.label6.TabIndex = 2;
            this.label6.Text = "Login";
            // 
            // textBoxX3
            // 
            this.textBoxX3.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.textBoxX3.Border.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxX3.Border.Class = "TextBoxBorder";
            this.textBoxX3.Border.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.textBoxX3.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.textBoxX3.Location = new System.Drawing.Point(41, 47);
            this.textBoxX3.Name = "textBoxX3";
            this.textBoxX3.PreventEnterBeep = true;
            this.textBoxX3.Size = new System.Drawing.Size(513, 39);
            this.textBoxX3.TabIndex = 5;
            this.textBoxX3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxX3.WatermarkText = "Username";
            // 
            // LoginUC
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(102)))));
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.symbolBox1);
            this.Controls.Add(this.label2);
            this.Name = "LoginUC";
            this.Size = new System.Drawing.Size(600, 750);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.SymbolBox symbolBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label6;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX3;
    }
}
