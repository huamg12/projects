using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

// 一维滤波器信息结构体
class kalmanType
{
    public double X;   // k-1时刻的滤波值，即是k-1时刻的值
    public double P;   // 估计误差协方差
    public double K;   // Kalamn增益
    public double A;   // x(n)=A*x(n-1)+u(n), u(n)~N(0,Q)
    public double H;   // z(n)=H*x(n)+w(n),   w(n)~N(0,R)
    public double Q;   // 过程噪声偏差的方差
    public double R;   // 测量噪声偏差，(系统搭建好以后，通过测量统计实验获得)
};

namespace SerialPort2005
{

    partial class SerialPortComm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxDataBits = new System.Windows.Forms.ComboBox();
            this.cbxParity = new System.Windows.Forms.ComboBox();
            this.cbxStopBits = new System.Windows.Forms.ComboBox();
            this.cbxBaudRate = new System.Windows.Forms.ComboBox();
            this.cbxCOMPort = new System.Windows.Forms.ComboBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnOpenCOM = new System.Windows.Forms.Button();
            this.btnCleanData = new System.Windows.Forms.Button();
            this.btnCheckCOM = new System.Windows.Forms.Button();
            this.rbnHex = new System.Windows.Forms.RadioButton();
            this.rbnChar = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabCtlDraw = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbxSendData = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbxRecvData = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabCtlDraw.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.pictureBox1.Location = new System.Drawing.Point(7, 7);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(735, 465);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Picture file|*.jpg;*.jpeg;*.png;*.bmp";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 480);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 32);
            this.button1.TabIndex = 2;
            this.button1.Text = "SerialPortComm";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxDataBits);
            this.groupBox1.Controls.Add(this.cbxParity);
            this.groupBox1.Controls.Add(this.cbxStopBits);
            this.groupBox1.Controls.Add(this.cbxBaudRate);
            this.groupBox1.Controls.Add(this.cbxCOMPort);
            this.groupBox1.Controls.Add(this.btnSend);
            this.groupBox1.Controls.Add(this.btnOpenCOM);
            this.groupBox1.Controls.Add(this.btnCleanData);
            this.groupBox1.Controls.Add(this.btnCheckCOM);
            this.groupBox1.Controls.Add(this.rbnHex);
            this.groupBox1.Controls.Add(this.rbnChar);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(4, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(170, 411);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PortSetting:";
            // 
            // cbxDataBits
            // 
            this.cbxDataBits.FormattingEnabled = true;
            this.cbxDataBits.Location = new System.Drawing.Point(64, 153);
            this.cbxDataBits.Name = "cbxDataBits";
            this.cbxDataBits.Size = new System.Drawing.Size(99, 24);
            this.cbxDataBits.TabIndex = 9;
            // 
            // cbxParity
            // 
            this.cbxParity.FormattingEnabled = true;
            this.cbxParity.Location = new System.Drawing.Point(64, 122);
            this.cbxParity.Name = "cbxParity";
            this.cbxParity.Size = new System.Drawing.Size(99, 24);
            this.cbxParity.TabIndex = 9;
            // 
            // cbxStopBits
            // 
            this.cbxStopBits.FormattingEnabled = true;
            this.cbxStopBits.Location = new System.Drawing.Point(64, 91);
            this.cbxStopBits.Name = "cbxStopBits";
            this.cbxStopBits.Size = new System.Drawing.Size(99, 24);
            this.cbxStopBits.TabIndex = 9;
            // 
            // cbxBaudRate
            // 
            this.cbxBaudRate.FormattingEnabled = true;
            this.cbxBaudRate.Location = new System.Drawing.Point(65, 60);
            this.cbxBaudRate.Name = "cbxBaudRate";
            this.cbxBaudRate.Size = new System.Drawing.Size(99, 24);
            this.cbxBaudRate.TabIndex = 9;
            // 
            // cbxCOMPort
            // 
            this.cbxCOMPort.FormattingEnabled = true;
            this.cbxCOMPort.Location = new System.Drawing.Point(65, 29);
            this.cbxCOMPort.Name = "cbxCOMPort";
            this.cbxCOMPort.Size = new System.Drawing.Size(99, 24);
            this.cbxCOMPort.TabIndex = 9;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(100, 373);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(63, 26);
            this.btnSend.TabIndex = 8;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnOpenCOM
            // 
            this.btnOpenCOM.Location = new System.Drawing.Point(100, 228);
            this.btnOpenCOM.Name = "btnOpenCOM";
            this.btnOpenCOM.Size = new System.Drawing.Size(63, 26);
            this.btnOpenCOM.TabIndex = 8;
            this.btnOpenCOM.Text = "Open";
            this.btnOpenCOM.UseVisualStyleBackColor = true;
            this.btnOpenCOM.Click += new System.EventHandler(this.btnOpenCOM_Click);
            // 
            // btnCleanData
            // 
            this.btnCleanData.Location = new System.Drawing.Point(12, 373);
            this.btnCleanData.Name = "btnCleanData";
            this.btnCleanData.Size = new System.Drawing.Size(59, 26);
            this.btnCleanData.TabIndex = 7;
            this.btnCleanData.Text = "Clear";
            this.btnCleanData.UseVisualStyleBackColor = true;
            this.btnCleanData.Click += new System.EventHandler(this.btnCleanData_Click);
            // 
            // btnCheckCOM
            // 
            this.btnCheckCOM.Location = new System.Drawing.Point(12, 228);
            this.btnCheckCOM.Name = "btnCheckCOM";
            this.btnCheckCOM.Size = new System.Drawing.Size(59, 26);
            this.btnCheckCOM.TabIndex = 7;
            this.btnCheckCOM.Text = "Find";
            this.btnCheckCOM.UseVisualStyleBackColor = true;
            this.btnCheckCOM.Click += new System.EventHandler(this.btnCheckCOM_Click);
            // 
            // rbnHex
            // 
            this.rbnHex.AutoSize = true;
            this.rbnHex.Location = new System.Drawing.Point(100, 189);
            this.rbnHex.Name = "rbnHex";
            this.rbnHex.Size = new System.Drawing.Size(53, 21);
            this.rbnHex.TabIndex = 6;
            this.rbnHex.Text = "Hex";
            this.rbnHex.UseVisualStyleBackColor = true;
            // 
            // rbnChar
            // 
            this.rbnChar.AutoSize = true;
            this.rbnChar.Checked = true;
            this.rbnChar.Location = new System.Drawing.Point(22, 189);
            this.rbnChar.Name = "rbnChar";
            this.rbnChar.Size = new System.Drawing.Size(59, 21);
            this.rbnChar.TabIndex = 5;
            this.rbnChar.TabStop = true;
            this.rbnChar.Text = "Char";
            this.rbnChar.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "DATA:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 17);
            this.label5.TabIndex = 3;
            this.label5.Text = "CHCK:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "STOP:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "BAUD:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "COM:";
            // 
            // tabCtlDraw
            // 
            this.tabCtlDraw.Controls.Add(this.tabPage1);
            this.tabCtlDraw.Controls.Add(this.tabPage2);
            this.tabCtlDraw.Location = new System.Drawing.Point(181, 12);
            this.tabCtlDraw.Name = "tabCtlDraw";
            this.tabCtlDraw.SelectedIndex = 0;
            this.tabCtlDraw.Size = new System.Drawing.Size(757, 548);
            this.tabCtlDraw.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(749, 519);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Monitor";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbxSendData);
            this.groupBox3.Location = new System.Drawing.Point(16, 380);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(727, 133);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "DataSend";
            // 
            // tbxSendData
            // 
            this.tbxSendData.Location = new System.Drawing.Point(6, 23);
            this.tbxSendData.Multiline = true;
            this.tbxSendData.Name = "tbxSendData";
            this.tbxSendData.Size = new System.Drawing.Size(715, 104);
            this.tbxSendData.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbxRecvData);
            this.groupBox2.Location = new System.Drawing.Point(16, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(727, 363);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DataReceive";
            // 
            // tbxRecvData
            // 
            this.tbxRecvData.Location = new System.Drawing.Point(6, 23);
            this.tbxRecvData.Multiline = true;
            this.tbxRecvData.Name = "tbxRecvData";
            this.tbxRecvData.Size = new System.Drawing.Size(715, 334);
            this.tbxRecvData.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pictureBox1);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(749, 519);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Draw";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // SerialPortComm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(949, 572);
            this.Controls.Add(this.tabCtlDraw);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(720, 360);
            this.Name = "SerialPortComm";
            this.Text = "SerialPort";
            this.Load += new System.EventHandler(this.SerialPortComm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SerialPortComm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabCtlDraw.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;

        #region DrawPictureControl
        private System.Windows.Forms.PictureBox pictureBox1;
        //static DATA.
        static int CIRCLE = 360;
        static int BUF_LEN = 720;
        double[] DC_U = new double[BUF_LEN];        //Duty Cycle of U phase.
        double[] DC_V = new double[BUF_LEN];        //Duty Cycle of V phase.
        double[] DC_W = new double[BUF_LEN];        //Duty Cycle of W phase.
        double[] DC_COM = new double[BUF_LEN];      //Duty Cycle of U+V+W phase.
        //variable 
        int X_LEN = 0;
        int Y_LEN = 0;
        double Ub = 0;
        double xtoa = 0;
        //double mr = 0;                          //modulation radio.
        //double pi = 0;
        //object
        Bitmap bmp;
        Graphics grp;
        private Button button1;
        Panel panelPic = new Panel();
        StringFormat drawFormat = new StringFormat();
        #endregion //DrawPictureControl

        #region KalmanFilterDefine
        kalmanType kal = new kalmanType();
        Random rQ = new Random();
        Random rR = new Random();
        #endregion //KalmanFilterDefine

        #region SerialPortDefine

        private GroupBox groupBox1;
        private Label label2;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private RadioButton rbnHex;
        private RadioButton rbnChar;
        private Button btnCheckCOM;
        private Button btnOpenCOM;
        private Button btnSend;
        private Button btnCleanData;
        private ComboBox cbxDataBits;
        private ComboBox cbxParity;
        private ComboBox cbxStopBits;
        private ComboBox cbxBaudRate;
        private ComboBox cbxCOMPort;
        private TabControl tabCtlDraw;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private GroupBox groupBox3;
        private TextBox tbxSendData;
        private GroupBox groupBox2;
        private TextBox tbxRecvData;
        #endregion //SerialPortDefine
    }
}

