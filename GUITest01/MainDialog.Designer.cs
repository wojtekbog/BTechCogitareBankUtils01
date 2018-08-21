namespace GUITest01
{
    partial class MainDialog
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
            this.button1 = new System.Windows.Forms.Button();
            this.certFName = new System.Windows.Forms.TextBox();
            this.openFileCert = new System.Windows.Forms.OpenFileDialog();
            this.F4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(424, 253);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 28);
            this.button1.TabIndex = 2;
            this.button1.Text = "GetStatements";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // certFName
            // 
            this.certFName.Location = new System.Drawing.Point(13, 13);
            this.certFName.Name = "certFName";
            this.certFName.Size = new System.Drawing.Size(467, 20);
            this.certFName.TabIndex = 3;
            // 
            // openFileCert
            // 
            this.openFileCert.FileName = "c:\\Temp\\pekao.p12";
            // 
            // F4
            // 
            this.F4.Location = new System.Drawing.Point(487, 9);
            this.F4.Name = "F4";
            this.F4.Size = new System.Drawing.Size(34, 23);
            this.F4.TabIndex = 4;
            this.F4.Text = "...";
            this.F4.UseVisualStyleBackColor = true;
            this.F4.Click += new System.EventHandler(this.F4_Click);
            // 
            // MainDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 292);
            this.Controls.Add(this.F4);
            this.Controls.Add(this.certFName);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainDialog";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox certFName;
        private System.Windows.Forms.OpenFileDialog openFileCert;
        private System.Windows.Forms.Button F4;
    }
}

