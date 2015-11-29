namespace Anketa
{
    partial class Language
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radBatEnglish = new System.Windows.Forms.RadioButton();
            this.radBatRus = new System.Windows.Forms.RadioButton();
            this.btOk = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btOk);
            this.groupBox1.Controls.Add(this.radBatEnglish);
            this.groupBox1.Controls.Add(this.radBatRus);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Выбор языка";
            // 
            // radBatEnglish
            // 
            this.radBatEnglish.AutoSize = true;
            this.radBatEnglish.Location = new System.Drawing.Point(23, 65);
            this.radBatEnglish.Name = "radBatEnglish";
            this.radBatEnglish.Size = new System.Drawing.Size(85, 17);
            this.radBatEnglish.TabIndex = 1;
            this.radBatEnglish.TabStop = true;
            this.radBatEnglish.Text = "Английский";
            this.radBatEnglish.UseVisualStyleBackColor = true;
            // 
            // radBatRus
            // 
            this.radBatRus.AutoSize = true;
            this.radBatRus.Location = new System.Drawing.Point(23, 30);
            this.radBatRus.Name = "radBatRus";
            this.radBatRus.Size = new System.Drawing.Size(67, 17);
            this.radBatRus.TabIndex = 0;
            this.radBatRus.TabStop = true;
            this.radBatRus.Text = "Русский";
            this.radBatRus.UseVisualStyleBackColor = true;
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(113, 44);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(81, 23);
            this.btOk.TabIndex = 2;
            this.btOk.Text = "Хорошо";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // Language
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 117);
            this.Controls.Add(this.groupBox1);
            this.Name = "Language";
            this.Text = "Language";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radBatEnglish;
        private System.Windows.Forms.RadioButton radBatRus;
        private System.Windows.Forms.Button btOk;
    }
}