namespace FileMeneger
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.listView1 = new System.Windows.Forms.ListView();
            this.btFill = new System.Windows.Forms.Button();
            this.comboDriers = new System.Windows.Forms.ComboBox();
            this.comboView = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(27, 165);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(377, 253);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // btFill
            // 
            this.btFill.Location = new System.Drawing.Point(331, 125);
            this.btFill.Name = "btFill";
            this.btFill.Size = new System.Drawing.Size(73, 34);
            this.btFill.TabIndex = 1;
            this.btFill.Text = "НАЗАД";
            this.btFill.UseVisualStyleBackColor = true;
            this.btFill.Click += new System.EventHandler(this.btFill_Click);
            // 
            // comboDriers
            // 
            this.comboDriers.FormattingEnabled = true;
            this.comboDriers.Location = new System.Drawing.Point(77, 81);
            this.comboDriers.Name = "comboDriers";
            this.comboDriers.Size = new System.Drawing.Size(121, 21);
            this.comboDriers.TabIndex = 2;
            this.comboDriers.SelectedIndexChanged += new System.EventHandler(this.comboDriers_SelectedIndexChanged);
            this.comboDriers.Click += new System.EventHandler(this.comboDriers_Click);
            // 
            // comboView
            // 
            this.comboView.FormattingEnabled = true;
            this.comboView.Location = new System.Drawing.Point(283, 81);
            this.comboView.Name = "comboView";
            this.comboView.Size = new System.Drawing.Size(121, 21);
            this.comboView.TabIndex = 3;
            this.comboView.SelectedIndexChanged += new System.EventHandler(this.comboView_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Drievers";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(253, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "View";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 430);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboView);
            this.Controls.Add(this.comboDriers);
            this.Controls.Add(this.btFill);
            this.Controls.Add(this.listView1);
            this.Name = "Form1";
            this.Text = "Менеджер";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btFill;
        private System.Windows.Forms.ComboBox comboDriers;
        private System.Windows.Forms.ComboBox comboView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

