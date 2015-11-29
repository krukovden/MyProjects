namespace SAPERmodern
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panelSec2 = new System.Windows.Forms.Panel();
            this.panelSec1 = new System.Windows.Forms.Panel();
            this.panelTwoPoint = new System.Windows.Forms.Panel();
            this.panelMin2 = new System.Windows.Forms.Panel();
            this.panelMin1 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelBomb1 = new System.Windows.Forms.Panel();
            this.panelBomb2 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelSec2
            // 
            this.panelSec2.BackColor = System.Drawing.Color.Red;
            this.panelSec2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelSec2.Location = new System.Drawing.Point(178, 13);
            this.panelSec2.Name = "panelSec2";
            this.panelSec2.Size = new System.Drawing.Size(41, 37);
            this.panelSec2.TabIndex = 5;
            // 
            // panelSec1
            // 
            this.panelSec1.BackColor = System.Drawing.Color.Red;
            this.panelSec1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelSec1.Location = new System.Drawing.Point(131, 13);
            this.panelSec1.Name = "panelSec1";
            this.panelSec1.Size = new System.Drawing.Size(41, 37);
            this.panelSec1.TabIndex = 7;
            // 
            // panelTwoPoint
            // 
            this.panelTwoPoint.BackColor = System.Drawing.Color.Red;
            this.panelTwoPoint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelTwoPoint.BackgroundImage")));
            this.panelTwoPoint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelTwoPoint.Location = new System.Drawing.Point(106, 13);
            this.panelTwoPoint.Name = "panelTwoPoint";
            this.panelTwoPoint.Size = new System.Drawing.Size(19, 37);
            this.panelTwoPoint.TabIndex = 6;
            // 
            // panelMin2
            // 
            this.panelMin2.BackColor = System.Drawing.Color.Red;
            this.panelMin2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelMin2.Location = new System.Drawing.Point(59, 13);
            this.panelMin2.Name = "panelMin2";
            this.panelMin2.Size = new System.Drawing.Size(41, 37);
            this.panelMin2.TabIndex = 4;
            // 
            // panelMin1
            // 
            this.panelMin1.BackColor = System.Drawing.Color.Red;
            this.panelMin1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelMin1.Location = new System.Drawing.Point(12, 12);
            this.panelMin1.Name = "panelMin1";
            this.panelMin1.Size = new System.Drawing.Size(41, 37);
            this.panelMin1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Location = new System.Drawing.Point(225, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(82, 53);
            this.panel1.TabIndex = 8;
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(326, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 22);
            this.label1.TabIndex = 9;
            this.label1.Text = "Бомбы";
            // 
            // panelBomb1
            // 
            this.panelBomb1.BackColor = System.Drawing.Color.Blue;
            this.panelBomb1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panelBomb1.Location = new System.Drawing.Point(392, 13);
            this.panelBomb1.Name = "panelBomb1";
            this.panelBomb1.Size = new System.Drawing.Size(43, 46);
            this.panelBomb1.TabIndex = 10;
            // 
            // panelBomb2
            // 
            this.panelBomb2.BackColor = System.Drawing.Color.Blue;
            this.panelBomb2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panelBomb2.Location = new System.Drawing.Point(439, 13);
            this.panelBomb2.Name = "panelBomb2";
            this.panelBomb2.Size = new System.Drawing.Size(43, 46);
            this.panelBomb2.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(489, 333);
            this.Controls.Add(this.panelBomb2);
            this.Controls.Add(this.panelBomb1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelSec2);
            this.Controls.Add(this.panelSec1);
            this.Controls.Add(this.panelTwoPoint);
            this.Controls.Add(this.panelMin2);
            this.Controls.Add(this.panelMin1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelSec2;
        private System.Windows.Forms.Panel panelSec1;
        private System.Windows.Forms.Panel panelTwoPoint;
        private System.Windows.Forms.Panel panelMin2;
        private System.Windows.Forms.Panel panelMin1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelBomb1;
        private System.Windows.Forms.Panel panelBomb2;
    }
}

