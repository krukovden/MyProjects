namespace PictureOnControl
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
            this.panelGame = new System.Windows.Forms.Panel();
            this.panelObj = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.GrShape = new System.Windows.Forms.GroupBox();
            this.radioCircul = new System.Windows.Forms.RadioButton();
            this.radioRect = new System.Windows.Forms.RadioButton();
            this.GrOption = new System.Windows.Forms.GroupBox();
            this.checkMove = new System.Windows.Forms.CheckBox();
            this.checkTeleport = new System.Windows.Forms.CheckBox();
            this.checkWarning = new System.Windows.Forms.CheckBox();
            this.GrControl = new System.Windows.Forms.GroupBox();
            this.btLeft = new System.Windows.Forms.Button();
            this.btRight = new System.Windows.Forms.Button();
            this.btDown = new System.Windows.Forms.Button();
            this.bTCenter = new System.Windows.Forms.Button();
            this.btUp = new System.Windows.Forms.Button();
            this.panelGame.SuspendLayout();
            this.panel1.SuspendLayout();
            this.GrShape.SuspendLayout();
            this.GrOption.SuspendLayout();
            this.GrControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelGame
            // 
            this.panelGame.BackColor = System.Drawing.Color.White;
            this.panelGame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelGame.Controls.Add(this.panelObj);
            this.panelGame.Location = new System.Drawing.Point(3, 4);
            this.panelGame.Name = "panelGame";
            this.panelGame.Size = new System.Drawing.Size(866, 575);
            this.panelGame.TabIndex = 0;
            // 
            // panelObj
            // 
            this.panelObj.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelObj.BackgroundImage")));
            this.panelObj.Location = new System.Drawing.Point(359, 221);
            this.panelObj.Name = "panelObj";
            this.panelObj.Size = new System.Drawing.Size(128, 128);
            this.panelObj.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.GrShape);
            this.panel1.Controls.Add(this.GrOption);
            this.panel1.Controls.Add(this.GrControl);
            this.panel1.Location = new System.Drawing.Point(875, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(278, 575);
            this.panel1.TabIndex = 1;
            // 
            // GrShape
            // 
            this.GrShape.Controls.Add(this.radioCircul);
            this.GrShape.Controls.Add(this.radioRect);
            this.GrShape.Location = new System.Drawing.Point(15, 412);
            this.GrShape.Name = "GrShape";
            this.GrShape.Size = new System.Drawing.Size(235, 150);
            this.GrShape.TabIndex = 2;
            this.GrShape.TabStop = false;
            this.GrShape.Text = "Shape";
            // 
            // radioCircul
            // 
            this.radioCircul.AutoSize = true;
            this.radioCircul.Location = new System.Drawing.Point(78, 106);
            this.radioCircul.Name = "radioCircul";
            this.radioCircul.Size = new System.Drawing.Size(51, 17);
            this.radioCircul.TabIndex = 1;
            this.radioCircul.TabStop = true;
            this.radioCircul.Text = "Circul";
            this.radioCircul.UseVisualStyleBackColor = true;
            // 
            // radioRect
            // 
            this.radioRect.AutoSize = true;
            this.radioRect.Location = new System.Drawing.Point(78, 58);
            this.radioRect.Name = "radioRect";
            this.radioRect.Size = new System.Drawing.Size(74, 17);
            this.radioRect.TabIndex = 0;
            this.radioRect.TabStop = true;
            this.radioRect.Text = "Rectangle";
            this.radioRect.UseVisualStyleBackColor = true;
            // 
            // GrOption
            // 
            this.GrOption.Controls.Add(this.checkMove);
            this.GrOption.Controls.Add(this.checkTeleport);
            this.GrOption.Controls.Add(this.checkWarning);
            this.GrOption.Location = new System.Drawing.Point(14, 249);
            this.GrOption.Name = "GrOption";
            this.GrOption.Size = new System.Drawing.Size(236, 141);
            this.GrOption.TabIndex = 1;
            this.GrOption.TabStop = false;
            this.GrOption.Text = "Option";
            // 
            // checkMove
            // 
            this.checkMove.AutoSize = true;
            this.checkMove.Location = new System.Drawing.Point(79, 109);
            this.checkMove.Name = "checkMove";
            this.checkMove.Size = new System.Drawing.Size(77, 17);
            this.checkMove.TabIndex = 2;
            this.checkMove.Text = "Auto move";
            this.checkMove.UseVisualStyleBackColor = true;
          
            // 
            // checkTeleport
            // 
            this.checkTeleport.AutoSize = true;
            this.checkTeleport.Location = new System.Drawing.Point(79, 76);
            this.checkTeleport.Name = "checkTeleport";
            this.checkTeleport.Size = new System.Drawing.Size(65, 17);
            this.checkTeleport.TabIndex = 1;
            this.checkTeleport.Text = "Teleport";
            this.checkTeleport.UseVisualStyleBackColor = true;
            // 
            // checkWarning
            // 
            this.checkWarning.AutoSize = true;
            this.checkWarning.Location = new System.Drawing.Point(79, 43);
            this.checkWarning.Name = "checkWarning";
            this.checkWarning.Size = new System.Drawing.Size(66, 17);
            this.checkWarning.TabIndex = 0;
            this.checkWarning.Text = "Warning";
            this.checkWarning.UseVisualStyleBackColor = true;
            // 
            // GrControl
            // 
            this.GrControl.Controls.Add(this.btLeft);
            this.GrControl.Controls.Add(this.btRight);
            this.GrControl.Controls.Add(this.btDown);
            this.GrControl.Controls.Add(this.bTCenter);
            this.GrControl.Controls.Add(this.btUp);
            this.GrControl.Location = new System.Drawing.Point(3, 13);
            this.GrControl.Name = "GrControl";
            this.GrControl.Size = new System.Drawing.Size(262, 208);
            this.GrControl.TabIndex = 0;
            this.GrControl.TabStop = false;
            this.GrControl.Text = "Controls";
            // 
            // btLeft
            // 
            this.btLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btLeft.BackgroundImage")));
            this.btLeft.Location = new System.Drawing.Point(6, 81);
            this.btLeft.Name = "btLeft";
            this.btLeft.Size = new System.Drawing.Size(78, 50);
            this.btLeft.TabIndex = 4;
            this.btLeft.UseVisualStyleBackColor = true;
            // 
            // btRight
            // 
            this.btRight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btRight.BackgroundImage")));
            this.btRight.Location = new System.Drawing.Point(174, 81);
            this.btRight.Name = "btRight";
            this.btRight.Size = new System.Drawing.Size(78, 50);
            this.btRight.TabIndex = 3;
            this.btRight.UseVisualStyleBackColor = true;
            // 
            // btDown
            // 
            this.btDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btDown.BackgroundImage")));
            this.btDown.Location = new System.Drawing.Point(90, 137);
            this.btDown.Name = "btDown";
            this.btDown.Size = new System.Drawing.Size(78, 50);
            this.btDown.TabIndex = 2;
            this.btDown.UseVisualStyleBackColor = true;
            // 
            // bTCenter
            // 
            this.bTCenter.Location = new System.Drawing.Point(90, 81);
            this.bTCenter.Name = "bTCenter";
            this.bTCenter.Size = new System.Drawing.Size(78, 50);
            this.bTCenter.TabIndex = 1;
            this.bTCenter.Text = "button2";
            this.bTCenter.UseVisualStyleBackColor = true;
            this.bTCenter.Click += new System.EventHandler(this.bTCenter_Click);
            // 
            // btUp
            // 
            this.btUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btUp.BackgroundImage")));
            this.btUp.Location = new System.Drawing.Point(90, 25);
            this.btUp.Name = "btUp";
            this.btUp.Size = new System.Drawing.Size(78, 50);
            this.btUp.TabIndex = 0;
            this.btUp.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1153, 583);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelGame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelGame.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.GrShape.ResumeLayout(false);
            this.GrShape.PerformLayout();
            this.GrOption.ResumeLayout(false);
            this.GrOption.PerformLayout();
            this.GrControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelGame;
        private System.Windows.Forms.Panel panelObj;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox GrShape;
        private System.Windows.Forms.RadioButton radioCircul;
        private System.Windows.Forms.RadioButton radioRect;
        private System.Windows.Forms.GroupBox GrOption;
        private System.Windows.Forms.CheckBox checkMove;
        private System.Windows.Forms.CheckBox checkTeleport;
        private System.Windows.Forms.CheckBox checkWarning;
        private System.Windows.Forms.GroupBox GrControl;
        private System.Windows.Forms.Button btLeft;
        private System.Windows.Forms.Button btRight;
        private System.Windows.Forms.Button btDown;
        private System.Windows.Forms.Button bTCenter;
        private System.Windows.Forms.Button btUp;
    }
}

