namespace Office_display
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.DateText = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.TempText = new System.Windows.Forms.TextBox();
            this.InfoTextBox = new System.Windows.Forms.TextBox();
            this.HourAndMinBox = new System.Windows.Forms.TextBox();
            this.secondsBox = new System.Windows.Forms.TextBox();
            this.MRLabel = new System.Windows.Forms.Label();
            this.debugBox = new System.Windows.Forms.TextBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.TestedQtyBox = new System.Windows.Forms.TextBox();
            this.timeTimer = new System.Windows.Forms.Timer(this.components);
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.browserText = new System.Windows.Forms.TextBox();
            this.tempBox = new System.Windows.Forms.TextBox();
            this.browserTimer = new System.Windows.Forms.Timer(this.components);
            this.sunBox = new System.Windows.Forms.TextBox();
            this.NewsTimer = new System.Windows.Forms.Timer(this.components);
            this.CursorBox = new System.Windows.Forms.TextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.DebugTimer = new System.Windows.Forms.Timer(this.components);
            this.IronQtyBox = new System.Windows.Forms.TextBox();
            this.GeneratorQtyBox = new System.Windows.Forms.TextBox();
            this.FinishedQtyBox = new System.Windows.Forms.TextBox();
            this.PlannedQtyBox = new System.Windows.Forms.TextBox();
            this.BalanceText = new System.Windows.Forms.TextBox();
            this.EmojiBox = new System.Windows.Forms.PictureBox();
            this.planPicture = new System.Windows.Forms.PictureBox();
            this.TestPic = new System.Windows.Forms.PictureBox();
            this.PackagingPic = new System.Windows.Forms.PictureBox();
            this.SteamPic = new System.Windows.Forms.PictureBox();
            this.IronPicture = new System.Windows.Forms.PictureBox();
            this.ConnectionPic = new System.Windows.Forms.PictureBox();
            this.LogoPicture = new System.Windows.Forms.PictureBox();
            this.Pictogram2 = new System.Windows.Forms.PictureBox();
            this.Pictogram = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.EmojiBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.planPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TestPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PackagingPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SteamPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IronPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConnectionPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pictogram2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pictogram)).BeginInit();
            this.SuspendLayout();
            // 
            // DateText
            // 
            this.DateText.BackColor = System.Drawing.SystemColors.Window;
            this.DateText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DateText.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.DateText.Font = new System.Drawing.Font("Arial", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateText.Location = new System.Drawing.Point(50, 22);
            this.DateText.Name = "DateText";
            this.DateText.ReadOnly = true;
            this.DateText.Size = new System.Drawing.Size(252, 41);
            this.DateText.TabIndex = 5;
            this.DateText.Text = "2018.05.30, Sze";
            this.DateText.Click += new System.EventHandler(this.DateText_Click);
            this.DateText.TextChanged += new System.EventHandler(this.DateText_TextChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // TempText
            // 
            this.TempText.BackColor = System.Drawing.SystemColors.Window;
            this.TempText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TempText.Font = new System.Drawing.Font("Arial", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TempText.Location = new System.Drawing.Point(337, 22);
            this.TempText.Name = "TempText";
            this.TempText.ReadOnly = true;
            this.TempText.Size = new System.Drawing.Size(88, 41);
            this.TempText.TabIndex = 8;
            // 
            // InfoTextBox
            // 
            this.InfoTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.InfoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InfoTextBox.Font = new System.Drawing.Font("Arial", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InfoTextBox.Location = new System.Drawing.Point(431, 22);
            this.InfoTextBox.Name = "InfoTextBox";
            this.InfoTextBox.ReadOnly = true;
            this.InfoTextBox.Size = new System.Drawing.Size(343, 41);
            this.InfoTextBox.TabIndex = 10;
            this.InfoTextBox.Text = "TESZTÜZEM";
            this.InfoTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // HourAndMinBox
            // 
            this.HourAndMinBox.BackColor = System.Drawing.SystemColors.Window;
            this.HourAndMinBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.HourAndMinBox.Font = new System.Drawing.Font("Arial", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.HourAndMinBox.Location = new System.Drawing.Point(52, 115);
            this.HourAndMinBox.Name = "HourAndMinBox";
            this.HourAndMinBox.ReadOnly = true;
            this.HourAndMinBox.Size = new System.Drawing.Size(93, 138);
            this.HourAndMinBox.TabIndex = 13;
            this.HourAndMinBox.TabStop = false;
            this.HourAndMinBox.Text = "06:00";
            this.HourAndMinBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.HourAndMinBox.Visible = false;
            this.HourAndMinBox.TextChanged += new System.EventHandler(this.HourAndMinBox_TextChanged);
            // 
            // secondsBox
            // 
            this.secondsBox.BackColor = System.Drawing.SystemColors.Window;
            this.secondsBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.secondsBox.Font = new System.Drawing.Font("Arial", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.secondsBox.Location = new System.Drawing.Point(168, 183);
            this.secondsBox.Name = "secondsBox";
            this.secondsBox.ReadOnly = true;
            this.secondsBox.Size = new System.Drawing.Size(61, 55);
            this.secondsBox.TabIndex = 14;
            this.secondsBox.Text = "00";
            this.secondsBox.Visible = false;
            // 
            // MRLabel
            // 
            this.MRLabel.AutoSize = true;
            this.MRLabel.Font = new System.Drawing.Font("Arial", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MRLabel.Location = new System.Drawing.Point(307, 378);
            this.MRLabel.Name = "MRLabel";
            this.MRLabel.Size = new System.Drawing.Size(0, 38);
            this.MRLabel.TabIndex = 17;
            this.MRLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // debugBox
            // 
            this.debugBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.debugBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.debugBox.Location = new System.Drawing.Point(50, 93);
            this.debugBox.Multiline = true;
            this.debugBox.Name = "debugBox";
            this.debugBox.ReadOnly = true;
            this.debugBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.debugBox.Size = new System.Drawing.Size(290, 412);
            this.debugBox.TabIndex = 18;
            this.debugBox.TextChanged += new System.EventHandler(this.debugBox_TextChanged);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 5000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // TestedQtyBox
            // 
            this.TestedQtyBox.BackColor = System.Drawing.SystemColors.Window;
            this.TestedQtyBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TestedQtyBox.Font = new System.Drawing.Font("Arial", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestedQtyBox.Location = new System.Drawing.Point(422, 455);
            this.TestedQtyBox.Name = "TestedQtyBox";
            this.TestedQtyBox.ReadOnly = true;
            this.TestedQtyBox.Size = new System.Drawing.Size(130, 81);
            this.TestedQtyBox.TabIndex = 20;
            this.TestedQtyBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // timeTimer
            // 
            this.timeTimer.Interval = 250;
            this.timeTimer.Tick += new System.EventHandler(this.timeTimer_Tick);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(90, 354);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(250, 62);
            this.webBrowser1.TabIndex = 23;
            this.webBrowser1.Url = new System.Uri("http://api.openweathermap.org/data/2.5/weather?id=3050594&units=metric&mode=xml&A" +
        "PPID=c111852ee383ccebbd6a7dd872613afc", System.UriKind.Absolute);
            this.webBrowser1.Visible = false;
            // 
            // browserText
            // 
            this.browserText.AcceptsReturn = true;
            this.browserText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.browserText.Location = new System.Drawing.Point(265, 427);
            this.browserText.Multiline = true;
            this.browserText.Name = "browserText";
            this.browserText.ReadOnly = true;
            this.browserText.Size = new System.Drawing.Size(204, 109);
            this.browserText.TabIndex = 24;
            this.browserText.Visible = false;
            this.browserText.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            this.browserText.DoubleClick += new System.EventHandler(this.textBox1_DoubleClick);
            // 
            // tempBox
            // 
            this.tempBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tempBox.Location = new System.Drawing.Point(504, 252);
            this.tempBox.Name = "tempBox";
            this.tempBox.ReadOnly = true;
            this.tempBox.Size = new System.Drawing.Size(204, 15);
            this.tempBox.TabIndex = 25;
            this.tempBox.Visible = false;
            // 
            // browserTimer
            // 
            this.browserTimer.Enabled = true;
            this.browserTimer.Interval = 600000;
            this.browserTimer.Tick += new System.EventHandler(this.browserTimer_Tick);
            // 
            // sunBox
            // 
            this.sunBox.Location = new System.Drawing.Point(504, 301);
            this.sunBox.Name = "sunBox";
            this.sunBox.ReadOnly = true;
            this.sunBox.Size = new System.Drawing.Size(204, 22);
            this.sunBox.TabIndex = 26;
            this.sunBox.Visible = false;
            // 
            // NewsTimer
            // 
            this.NewsTimer.Enabled = true;
            this.NewsTimer.Interval = 5000;
            this.NewsTimer.Tick += new System.EventHandler(this.NewsTimer_Tick);
            // 
            // CursorBox
            // 
            this.CursorBox.Location = new System.Drawing.Point(799, 599);
            this.CursorBox.Name = "CursorBox";
            this.CursorBox.Size = new System.Drawing.Size(1, 22);
            this.CursorBox.TabIndex = 29;
            // 
            // DebugTimer
            // 
            this.DebugTimer.Enabled = true;
            this.DebugTimer.Interval = 180000;
            this.DebugTimer.Tick += new System.EventHandler(this.DebugTimer_Tick);
            // 
            // IronQtyBox
            // 
            this.IronQtyBox.BackColor = System.Drawing.SystemColors.Window;
            this.IronQtyBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.IronQtyBox.Font = new System.Drawing.Font("Arial", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IronQtyBox.Location = new System.Drawing.Point(75, 455);
            this.IronQtyBox.Name = "IronQtyBox";
            this.IronQtyBox.ReadOnly = true;
            this.IronQtyBox.Size = new System.Drawing.Size(130, 81);
            this.IronQtyBox.TabIndex = 44;
            this.IronQtyBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GeneratorQtyBox
            // 
            this.GeneratorQtyBox.BackColor = System.Drawing.SystemColors.Window;
            this.GeneratorQtyBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GeneratorQtyBox.Font = new System.Drawing.Font("Arial", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GeneratorQtyBox.Location = new System.Drawing.Point(252, 455);
            this.GeneratorQtyBox.Name = "GeneratorQtyBox";
            this.GeneratorQtyBox.ReadOnly = true;
            this.GeneratorQtyBox.Size = new System.Drawing.Size(130, 81);
            this.GeneratorQtyBox.TabIndex = 45;
            this.GeneratorQtyBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FinishedQtyBox
            // 
            this.FinishedQtyBox.BackColor = System.Drawing.SystemColors.Window;
            this.FinishedQtyBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FinishedQtyBox.Font = new System.Drawing.Font("Arial", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FinishedQtyBox.Location = new System.Drawing.Point(609, 455);
            this.FinishedQtyBox.Name = "FinishedQtyBox";
            this.FinishedQtyBox.ReadOnly = true;
            this.FinishedQtyBox.Size = new System.Drawing.Size(130, 81);
            this.FinishedQtyBox.TabIndex = 46;
            this.FinishedQtyBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PlannedQtyBox
            // 
            this.PlannedQtyBox.BackColor = System.Drawing.SystemColors.Window;
            this.PlannedQtyBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PlannedQtyBox.Font = new System.Drawing.Font("Arial", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlannedQtyBox.Location = new System.Drawing.Point(609, 217);
            this.PlannedQtyBox.Name = "PlannedQtyBox";
            this.PlannedQtyBox.ReadOnly = true;
            this.PlannedQtyBox.Size = new System.Drawing.Size(130, 81);
            this.PlannedQtyBox.TabIndex = 48;
            this.PlannedQtyBox.Text = "0";
            this.PlannedQtyBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // BalanceText
            // 
            this.BalanceText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BalanceText.Font = new System.Drawing.Font("Arial", 120F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BalanceText.Location = new System.Drawing.Point(189, 93);
            this.BalanceText.Name = "BalanceText";
            this.BalanceText.Size = new System.Drawing.Size(414, 230);
            this.BalanceText.TabIndex = 49;
            this.BalanceText.Text = "+0";
            this.BalanceText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // EmojiBox
            // 
            this.EmojiBox.Image = global::Office_display.Properties.Resources._5;
            this.EmojiBox.Location = new System.Drawing.Point(66, 139);
            this.EmojiBox.Name = "EmojiBox";
            this.EmojiBox.Size = new System.Drawing.Size(139, 159);
            this.EmojiBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.EmojiBox.TabIndex = 50;
            this.EmojiBox.TabStop = false;
            // 
            // planPicture
            // 
            this.planPicture.Image = global::Office_display.Properties.Resources.plan_logo;
            this.planPicture.InitialImage = null;
            this.planPicture.Location = new System.Drawing.Point(625, 115);
            this.planPicture.Name = "planPicture";
            this.planPicture.Size = new System.Drawing.Size(100, 96);
            this.planPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.planPicture.TabIndex = 47;
            this.planPicture.TabStop = false;
            // 
            // TestPic
            // 
            this.TestPic.Image = global::Office_display.Properties.Resources.testpic;
            this.TestPic.InitialImage = null;
            this.TestPic.Location = new System.Drawing.Point(440, 354);
            this.TestPic.Name = "TestPic";
            this.TestPic.Size = new System.Drawing.Size(100, 70);
            this.TestPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.TestPic.TabIndex = 43;
            this.TestPic.TabStop = false;
            // 
            // PackagingPic
            // 
            this.PackagingPic.Image = global::Office_display.Properties.Resources.packaging;
            this.PackagingPic.InitialImage = null;
            this.PackagingPic.Location = new System.Drawing.Point(625, 354);
            this.PackagingPic.Name = "PackagingPic";
            this.PackagingPic.Size = new System.Drawing.Size(100, 70);
            this.PackagingPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PackagingPic.TabIndex = 42;
            this.PackagingPic.TabStop = false;
            this.PackagingPic.DoubleClick += new System.EventHandler(this.browserTimer_Tick);
            // 
            // SteamPic
            // 
            this.SteamPic.Image = global::Office_display.Properties.Resources.steam;
            this.SteamPic.InitialImage = null;
            this.SteamPic.Location = new System.Drawing.Point(265, 354);
            this.SteamPic.Name = "SteamPic";
            this.SteamPic.Size = new System.Drawing.Size(100, 70);
            this.SteamPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.SteamPic.TabIndex = 41;
            this.SteamPic.TabStop = false;
            // 
            // IronPicture
            // 
            this.IronPicture.Image = global::Office_display.Properties.Resources.iron;
            this.IronPicture.InitialImage = null;
            this.IronPicture.Location = new System.Drawing.Point(90, 354);
            this.IronPicture.Name = "IronPicture";
            this.IronPicture.Size = new System.Drawing.Size(100, 70);
            this.IronPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.IronPicture.TabIndex = 40;
            this.IronPicture.TabStop = false;
            // 
            // ConnectionPic
            // 
            this.ConnectionPic.Image = global::Office_display.Properties.Resources.no_signal;
            this.ConnectionPic.Location = new System.Drawing.Point(736, 115);
            this.ConnectionPic.Name = "ConnectionPic";
            this.ConnectionPic.Size = new System.Drawing.Size(38, 36);
            this.ConnectionPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ConnectionPic.TabIndex = 31;
            this.ConnectionPic.TabStop = false;
            this.ConnectionPic.Visible = false;
            // 
            // LogoPicture
            // 
            this.LogoPicture.Image = global::Office_display.Properties.Resources.laurastar_logo1;
            this.LogoPicture.Location = new System.Drawing.Point(0, 0);
            this.LogoPicture.Name = "LogoPicture";
            this.LogoPicture.Size = new System.Drawing.Size(40, 560);
            this.LogoPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.LogoPicture.TabIndex = 30;
            this.LogoPicture.TabStop = false;
            // 
            // Pictogram2
            // 
            this.Pictogram2.Location = new System.Drawing.Point(427, 27);
            this.Pictogram2.Name = "Pictogram2";
            this.Pictogram2.Size = new System.Drawing.Size(38, 36);
            this.Pictogram2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pictogram2.TabIndex = 28;
            this.Pictogram2.TabStop = false;
            this.Pictogram2.Visible = false;
            // 
            // Pictogram
            // 
            this.Pictogram.Image = global::Office_display.Properties.Resources.clock_logo;
            this.Pictogram.Location = new System.Drawing.Point(300, 25);
            this.Pictogram.Name = "Pictogram";
            this.Pictogram.Size = new System.Drawing.Size(35, 43);
            this.Pictogram.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pictogram.TabIndex = 27;
            this.Pictogram.TabStop = false;
            this.Pictogram.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.EmojiBox);
            this.Controls.Add(this.BalanceText);
            this.Controls.Add(this.PlannedQtyBox);
            this.Controls.Add(this.planPicture);
            this.Controls.Add(this.FinishedQtyBox);
            this.Controls.Add(this.GeneratorQtyBox);
            this.Controls.Add(this.IronQtyBox);
            this.Controls.Add(this.TestPic);
            this.Controls.Add(this.PackagingPic);
            this.Controls.Add(this.SteamPic);
            this.Controls.Add(this.IronPicture);
            this.Controls.Add(this.ConnectionPic);
            this.Controls.Add(this.LogoPicture);
            this.Controls.Add(this.CursorBox);
            this.Controls.Add(this.Pictogram2);
            this.Controls.Add(this.Pictogram);
            this.Controls.Add(this.sunBox);
            this.Controls.Add(this.tempBox);
            this.Controls.Add(this.browserText);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.TestedQtyBox);
            this.Controls.Add(this.debugBox);
            this.Controls.Add(this.MRLabel);
            this.Controls.Add(this.secondsBox);
            this.Controls.Add(this.HourAndMinBox);
            this.Controls.Add(this.InfoTextBox);
            this.Controls.Add(this.TempText);
            this.Controls.Add(this.DateText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "ProdClock v2.0 - 2020.11.18";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.browserTimer_Tick);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.EmojiBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.planPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TestPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PackagingPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SteamPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IronPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConnectionPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pictogram2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pictogram)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox DateText;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox TempText;
        private System.Windows.Forms.TextBox InfoTextBox;
        private System.Windows.Forms.TextBox HourAndMinBox;
        private System.Windows.Forms.TextBox secondsBox;
        private System.Windows.Forms.Label MRLabel;
        private System.Windows.Forms.TextBox debugBox;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.TextBox TestedQtyBox;
        private System.Windows.Forms.Timer timeTimer;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TextBox browserText;
        private System.Windows.Forms.TextBox tempBox;
        private System.Windows.Forms.Timer browserTimer;
        private System.Windows.Forms.TextBox sunBox;
        private System.Windows.Forms.Timer NewsTimer;
        private System.Windows.Forms.PictureBox Pictogram;
        private System.Windows.Forms.PictureBox Pictogram2;
        private System.Windows.Forms.TextBox CursorBox;
        private System.Windows.Forms.PictureBox LogoPicture;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer DebugTimer;
        private System.Windows.Forms.PictureBox ConnectionPic;
        private System.Windows.Forms.PictureBox IronPicture;
        private System.Windows.Forms.PictureBox SteamPic;
        private System.Windows.Forms.PictureBox PackagingPic;
        private System.Windows.Forms.PictureBox TestPic;
        private System.Windows.Forms.TextBox IronQtyBox;
        private System.Windows.Forms.TextBox GeneratorQtyBox;
        private System.Windows.Forms.TextBox FinishedQtyBox;
        private System.Windows.Forms.PictureBox planPicture;
        private System.Windows.Forms.TextBox PlannedQtyBox;
        private System.Windows.Forms.TextBox BalanceText;
        private System.Windows.Forms.PictureBox EmojiBox;
    }
}

