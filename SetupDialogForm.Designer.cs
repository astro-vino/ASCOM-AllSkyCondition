namespace ASCOM.AllSkyCondition
{
    partial class SetupDialogForm
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
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.picASCOM = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkConnected = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtImagePath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.numCycle = new System.Windows.Forms.NumericUpDown();
            this.numCloudy = new System.Windows.Forms.NumericUpDown();
            this.numCovered = new System.Windows.Forms.NumericUpDown();
            this.chkDebugLog = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCycle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCloudy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCovered)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.Location = new System.Drawing.Point(379, 184);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(59, 24);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(379, 214);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(59, 25);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "ASCOM SafetyMonitor Driver for AllSkyCondition";
            // 
            // picASCOM
            // 
            this.picASCOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picASCOM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picASCOM.Image = global::ASCOM.AllSkyCondition.Properties.Resources.ASCOM;
            this.picASCOM.Location = new System.Drawing.Point(390, 9);
            this.picASCOM.Name = "picASCOM";
            this.picASCOM.Size = new System.Drawing.Size(48, 56);
            this.picASCOM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picASCOM.TabIndex = 3;
            this.picASCOM.TabStop = false;
            this.picASCOM.Click += new System.EventHandler(this.BrowseToAscom);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "AllSky Image Path:";
            // 
            // chkConnected
            // 
            this.chkConnected.AutoSize = true;
            this.chkConnected.Location = new System.Drawing.Point(16, 220);
            this.chkConnected.Name = "chkConnected";
            this.chkConnected.Size = new System.Drawing.Size(82, 17);
            this.chkConnected.TabIndex = 7;
            this.chkConnected.Text = "Connected";
            this.chkConnected.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Cycle Time (minutes):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Cloudy Tolerance (minutes):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Covered Tolerance (minutes):";
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Location = new System.Drawing.Point(16, 193);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(340, 1);
            this.label6.TabIndex = 11;
            // 
            // txtImagePath
            // 
            this.txtImagePath.Location = new System.Drawing.Point(158, 65);
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.Size = new System.Drawing.Size(215, 20);
            this.txtImagePath.TabIndex = 12;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(379, 63);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(59, 23);
            this.btnBrowse.TabIndex = 13;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // numCycle
            // 
            this.numCycle.Location = new System.Drawing.Point(158, 98);
            this.numCycle.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCycle.Name = "numCycle";
            this.numCycle.Size = new System.Drawing.Size(60, 20);
            this.numCycle.TabIndex = 14;
            this.numCycle.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numCloudy
            // 
            this.numCloudy.Location = new System.Drawing.Point(158, 130);
            this.numCloudy.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCloudy.Name = "numCloudy";
            this.numCloudy.Size = new System.Drawing.Size(60, 20);
            this.numCloudy.TabIndex = 15;
            this.numCloudy.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // numCovered
            // 
            this.numCovered.Location = new System.Drawing.Point(158, 162);
            this.numCovered.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCovered.Name = "numCovered";
            this.numCovered.Size = new System.Drawing.Size(60, 20);
            this.numCovered.TabIndex = 16;
            this.numCovered.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // chkDebugLog
            // 
            this.chkDebugLog.AutoSize = true;
            this.chkDebugLog.Location = new System.Drawing.Point(16, 197);
            this.chkDebugLog.Name = "chkDebugLog";
            this.chkDebugLog.Size = new System.Drawing.Size(113, 17);
            this.chkDebugLog.TabIndex = 17;
            this.chkDebugLog.Text = "Enable Debug Log";
            this.chkDebugLog.UseVisualStyleBackColor = true;
            // 
            // SetupDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 251);
            this.Controls.Add(this.chkDebugLog);
            this.Controls.Add(this.numCovered);
            this.Controls.Add(this.numCloudy);
            this.Controls.Add(this.numCycle);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtImagePath);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkConnected);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picASCOM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupDialogForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AllSkyCondition Setup";
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCycle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCloudy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCovered)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picASCOM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkConnected;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtImagePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.NumericUpDown numCycle;
        private System.Windows.Forms.NumericUpDown numCloudy;
        private System.Windows.Forms.NumericUpDown numCovered;
        private System.Windows.Forms.CheckBox chkDebugLog;
    }
}
