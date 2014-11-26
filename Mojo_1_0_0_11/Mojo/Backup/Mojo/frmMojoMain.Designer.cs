namespace Mojo
{
    partial class frmMojoMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMojoMain));
            this.lblExtension = new System.Windows.Forms.Label();
            this.tbExtension = new System.Windows.Forms.TextBox();
            this.lblCaller = new System.Windows.Forms.Label();
            this.tbCallerId = new System.Windows.Forms.TextBox();
            this.tmBufferPoll = new System.Windows.Forms.Timer(this.components);
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblCalledAt = new System.Windows.Forms.Label();
            this.tbCalledAt = new System.Windows.Forms.TextBox();
            this.btn1 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btn4 = new System.Windows.Forms.Button();
            this.btn5 = new System.Windows.Forms.Button();
            this.btn6 = new System.Windows.Forms.Button();
            this.btn7 = new System.Windows.Forms.Button();
            this.btn8 = new System.Windows.Forms.Button();
            this.btn9 = new System.Windows.Forms.Button();
            this.btn0 = new System.Windows.Forms.Button();
            this.lblDialed = new System.Windows.Forms.Label();
            this.tbDialed = new System.Windows.Forms.TextBox();
            this.btnDial = new System.Windows.Forms.Button();
            this.btnToggle = new System.Windows.Forms.Button();
            this.btnMute = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnLine1 = new FButton.FsButton();
            this.btnLine2 = new FButton.FsButton();
            this.btnHold = new System.Windows.Forms.Button();
            this.lblMwi = new System.Windows.Forms.Label();
            this.btnMwi = new System.Windows.Forms.Button();
            this.tmMwiPoll = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblExtension
            // 
            this.lblExtension.AutoSize = true;
            this.lblExtension.Location = new System.Drawing.Point(12, 20);
            this.lblExtension.Name = "lblExtension";
            this.lblExtension.Size = new System.Drawing.Size(56, 13);
            this.lblExtension.TabIndex = 13;
            this.lblExtension.Text = "Extension:";
            // 
            // tbExtension
            // 
            this.tbExtension.Location = new System.Drawing.Point(76, 13);
            this.tbExtension.Name = "tbExtension";
            this.tbExtension.Size = new System.Drawing.Size(100, 20);
            this.tbExtension.TabIndex = 0;
            this.tbExtension.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbExtension_KeyPress);
            // 
            // lblCaller
            // 
            this.lblCaller.AutoSize = true;
            this.lblCaller.Location = new System.Drawing.Point(12, 55);
            this.lblCaller.Name = "lblCaller";
            this.lblCaller.Size = new System.Drawing.Size(50, 13);
            this.lblCaller.TabIndex = 14;
            this.lblCaller.Text = "Caller ID:";
            // 
            // tbCallerId
            // 
            this.tbCallerId.Location = new System.Drawing.Point(76, 48);
            this.tbCallerId.Name = "tbCallerId";
            this.tbCallerId.ReadOnly = true;
            this.tbCallerId.Size = new System.Drawing.Size(100, 20);
            this.tbCallerId.TabIndex = 16;
            // 
            // tmBufferPoll
            // 
            this.tmBufferPoll.Enabled = true;
            this.tmBufferPoll.Interval = 1000;
            this.tmBufferPoll.Tick += new System.EventHandler(this.tmBufferPoll_Tick);
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Location = new System.Drawing.Point(186, 13);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 20);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Text = "Log In";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblCalledAt
            // 
            this.lblCalledAt.AutoSize = true;
            this.lblCalledAt.Location = new System.Drawing.Point(12, 90);
            this.lblCalledAt.Name = "lblCalledAt";
            this.lblCalledAt.Size = new System.Drawing.Size(52, 13);
            this.lblCalledAt.TabIndex = 15;
            this.lblCalledAt.Text = "Called At:";
            // 
            // tbCalledAt
            // 
            this.tbCalledAt.Location = new System.Drawing.Point(76, 83);
            this.tbCalledAt.Name = "tbCalledAt";
            this.tbCalledAt.ReadOnly = true;
            this.tbCalledAt.Size = new System.Drawing.Size(136, 20);
            this.tbCalledAt.TabIndex = 17;
            // 
            // btn1
            // 
            this.btn1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn1.Location = new System.Drawing.Point(15, 120);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(38, 23);
            this.btn1.TabIndex = 2;
            this.btn1.Text = "1";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // btn2
            // 
            this.btn2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn2.Location = new System.Drawing.Point(69, 120);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(38, 23);
            this.btn2.TabIndex = 3;
            this.btn2.Text = "2";
            this.btn2.UseVisualStyleBackColor = true;
            this.btn2.Click += new System.EventHandler(this.btn2_Click);
            // 
            // btn3
            // 
            this.btn3.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn3.Location = new System.Drawing.Point(122, 120);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(38, 23);
            this.btn3.TabIndex = 4;
            this.btn3.Text = "3";
            this.btn3.UseVisualStyleBackColor = true;
            this.btn3.Click += new System.EventHandler(this.btn3_Click);
            // 
            // btn4
            // 
            this.btn4.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn4.Location = new System.Drawing.Point(15, 158);
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(38, 23);
            this.btn4.TabIndex = 5;
            this.btn4.Text = "4";
            this.btn4.UseVisualStyleBackColor = true;
            this.btn4.Click += new System.EventHandler(this.btn4_Click);
            // 
            // btn5
            // 
            this.btn5.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn5.Location = new System.Drawing.Point(69, 158);
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(38, 23);
            this.btn5.TabIndex = 6;
            this.btn5.Text = "5";
            this.btn5.UseVisualStyleBackColor = true;
            this.btn5.Click += new System.EventHandler(this.btn5_Click);
            // 
            // btn6
            // 
            this.btn6.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn6.Location = new System.Drawing.Point(122, 158);
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(38, 23);
            this.btn6.TabIndex = 7;
            this.btn6.Text = "6";
            this.btn6.UseVisualStyleBackColor = true;
            this.btn6.Click += new System.EventHandler(this.btn6_Click);
            // 
            // btn7
            // 
            this.btn7.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn7.Location = new System.Drawing.Point(15, 196);
            this.btn7.Name = "btn7";
            this.btn7.Size = new System.Drawing.Size(38, 23);
            this.btn7.TabIndex = 8;
            this.btn7.Text = "7";
            this.btn7.UseVisualStyleBackColor = true;
            this.btn7.Click += new System.EventHandler(this.btn7_Click);
            // 
            // btn8
            // 
            this.btn8.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn8.Location = new System.Drawing.Point(69, 196);
            this.btn8.Name = "btn8";
            this.btn8.Size = new System.Drawing.Size(38, 23);
            this.btn8.TabIndex = 9;
            this.btn8.Text = "8";
            this.btn8.UseVisualStyleBackColor = true;
            this.btn8.Click += new System.EventHandler(this.btn8_Click);
            // 
            // btn9
            // 
            this.btn9.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn9.Location = new System.Drawing.Point(122, 196);
            this.btn9.Name = "btn9";
            this.btn9.Size = new System.Drawing.Size(38, 23);
            this.btn9.TabIndex = 10;
            this.btn9.Text = "9";
            this.btn9.UseVisualStyleBackColor = true;
            this.btn9.Click += new System.EventHandler(this.btn9_Click);
            // 
            // btn0
            // 
            this.btn0.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn0.Location = new System.Drawing.Point(69, 234);
            this.btn0.Name = "btn0";
            this.btn0.Size = new System.Drawing.Size(38, 23);
            this.btn0.TabIndex = 11;
            this.btn0.Text = "0";
            this.btn0.UseVisualStyleBackColor = true;
            this.btn0.Click += new System.EventHandler(this.btn0_Click);
            // 
            // lblDialed
            // 
            this.lblDialed.AutoSize = true;
            this.lblDialed.Location = new System.Drawing.Point(183, 125);
            this.lblDialed.Name = "lblDialed";
            this.lblDialed.Size = new System.Drawing.Size(40, 13);
            this.lblDialed.TabIndex = 18;
            this.lblDialed.Text = "Dialed:";
            // 
            // tbDialed
            // 
            this.tbDialed.Location = new System.Drawing.Point(186, 146);
            this.tbDialed.Name = "tbDialed";
            this.tbDialed.Size = new System.Drawing.Size(90, 20);
            this.tbDialed.TabIndex = 19;
            this.tbDialed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDialed_KeyPress);
            // 
            // btnDial
            // 
            this.btnDial.Location = new System.Drawing.Point(186, 172);
            this.btnDial.Name = "btnDial";
            this.btnDial.Size = new System.Drawing.Size(90, 23);
            this.btnDial.TabIndex = 12;
            this.btnDial.Text = "Dial";
            this.btnDial.UseVisualStyleBackColor = true;
            this.btnDial.Click += new System.EventHandler(this.btnCall_Click);
            // 
            // btnToggle
            // 
            this.btnToggle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToggle.Location = new System.Drawing.Point(186, 48);
            this.btnToggle.Name = "btnToggle";
            this.btnToggle.Size = new System.Drawing.Size(75, 20);
            this.btnToggle.TabIndex = 20;
            this.btnToggle.Text = "Call Control";
            this.btnToggle.UseVisualStyleBackColor = true;
            this.btnToggle.Click += new System.EventHandler(this.btnToggle_Click);
            // 
            // btnMute
            // 
            this.btnMute.BackColor = System.Drawing.Color.ForestGreen;
            this.btnMute.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMute.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnMute.Location = new System.Drawing.Point(253, 80);
            this.btnMute.Name = "btnMute";
            this.btnMute.Size = new System.Drawing.Size(33, 23);
            this.btnMute.TabIndex = 21;
            this.btnMute.Text = "On";
            this.btnMute.UseVisualStyleBackColor = false;
            this.btnMute.Click += new System.EventHandler(this.btnMute_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(229, 83);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(18, 20);
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // btnLine1
            // 
            this.btnLine1.BackColor = System.Drawing.Color.Gray;
            this.btnLine1.FlasherButtonColorOff = System.Drawing.SystemColors.Control;
            this.btnLine1.FlasherButtonColorOn = System.Drawing.Color.LightGreen;
            this.btnLine1.Location = new System.Drawing.Point(186, 206);
            this.btnLine1.Name = "btnLine1";
            this.btnLine1.Size = new System.Drawing.Size(90, 23);
            this.btnLine1.TabIndex = 23;
            this.btnLine1.Text = "Idle";
            this.btnLine1.UseVisualStyleBackColor = false;
            this.btnLine1.Click += new System.EventHandler(this.btnLine1_Click);
            // 
            // btnLine2
            // 
            this.btnLine2.BackColor = System.Drawing.Color.Gray;
            this.btnLine2.FlasherButtonColorOff = System.Drawing.SystemColors.Control;
            this.btnLine2.FlasherButtonColorOn = System.Drawing.Color.LightGreen;
            this.btnLine2.Location = new System.Drawing.Point(186, 235);
            this.btnLine2.Name = "btnLine2";
            this.btnLine2.Size = new System.Drawing.Size(90, 23);
            this.btnLine2.TabIndex = 24;
            this.btnLine2.Text = "Idle";
            this.btnLine2.UseVisualStyleBackColor = false;
            this.btnLine2.Click += new System.EventHandler(this.btnLine2_Click);
            // 
            // btnHold
            // 
            this.btnHold.BackColor = System.Drawing.Color.Red;
            this.btnHold.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnHold.Location = new System.Drawing.Point(186, 265);
            this.btnHold.Name = "btnHold";
            this.btnHold.Size = new System.Drawing.Size(90, 23);
            this.btnHold.TabIndex = 25;
            this.btnHold.Text = "Hold";
            this.btnHold.UseVisualStyleBackColor = false;
            this.btnHold.Click += new System.EventHandler(this.btnHold_Click);
            // 
            // lblMwi
            // 
            this.lblMwi.AutoSize = true;
            this.lblMwi.Location = new System.Drawing.Point(29, 275);
            this.lblMwi.Name = "lblMwi";
            this.lblMwi.Size = new System.Drawing.Size(50, 13);
            this.lblMwi.TabIndex = 26;
            this.lblMwi.Text = "Message";
            // 
            // btnMwi
            // 
            this.btnMwi.BackColor = System.Drawing.Color.Gray;
            this.btnMwi.Location = new System.Drawing.Point(85, 270);
            this.btnMwi.Name = "btnMwi";
            this.btnMwi.Size = new System.Drawing.Size(22, 23);
            this.btnMwi.TabIndex = 27;
            this.btnMwi.UseVisualStyleBackColor = false;
            // 
            // tmMwiPoll
            // 
            this.tmMwiPoll.Interval = 5000;
            this.tmMwiPoll.Tick += new System.EventHandler(this.tmMwiPoll_Tick);
            // 
            // frmMojoMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 300);
            this.Controls.Add(this.btnMwi);
            this.Controls.Add(this.lblMwi);
            this.Controls.Add(this.btnHold);
            this.Controls.Add(this.btnLine2);
            this.Controls.Add(this.btnLine1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnMute);
            this.Controls.Add(this.btnToggle);
            this.Controls.Add(this.btnDial);
            this.Controls.Add(this.tbDialed);
            this.Controls.Add(this.lblDialed);
            this.Controls.Add(this.btn0);
            this.Controls.Add(this.btn9);
            this.Controls.Add(this.btn8);
            this.Controls.Add(this.btn7);
            this.Controls.Add(this.btn6);
            this.Controls.Add(this.btn5);
            this.Controls.Add(this.btn4);
            this.Controls.Add(this.btn3);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.tbCalledAt);
            this.Controls.Add(this.lblCalledAt);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.tbCallerId);
            this.Controls.Add(this.lblCaller);
            this.Controls.Add(this.tbExtension);
            this.Controls.Add(this.lblExtension);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMojoMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Mojo";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblExtension;
        private System.Windows.Forms.TextBox tbExtension;
        private System.Windows.Forms.Label lblCaller;
        private System.Windows.Forms.TextBox tbCallerId;
        private System.Windows.Forms.Timer tmBufferPoll;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblCalledAt;
        private System.Windows.Forms.TextBox tbCalledAt;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Button btn4;
        private System.Windows.Forms.Button btn5;
        private System.Windows.Forms.Button btn6;
        private System.Windows.Forms.Button btn7;
        private System.Windows.Forms.Button btn8;
        private System.Windows.Forms.Button btn9;
        private System.Windows.Forms.Button btn0;
        private System.Windows.Forms.Label lblDialed;
        private System.Windows.Forms.TextBox tbDialed;
        private System.Windows.Forms.Button btnDial;
        private System.Windows.Forms.Button btnToggle;
        private System.Windows.Forms.Button btnMute;
        private System.Windows.Forms.PictureBox pictureBox1;
        private FButton.FsButton btnLine1;
        private FButton.FsButton btnLine2;
        private System.Windows.Forms.Button btnHold;
        private System.Windows.Forms.Label lblMwi;
        private System.Windows.Forms.Button btnMwi;
        private System.Windows.Forms.Timer tmMwiPoll;
    }
}

