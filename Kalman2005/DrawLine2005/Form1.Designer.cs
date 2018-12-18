using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

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

namespace Kalman2005
{

    partial class KalmanFilter
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.pictureBox1.Location = new System.Drawing.Point(28, 62);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(954, 411);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Picture file|*.jpg;*.jpeg;*.png;*.bmp";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mod Radio(0~1):";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.textBox1.Location = new System.Drawing.Point(148, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(45, 26);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "0.9";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 502);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 48);
            this.button1.TabIndex = 2;
            this.button1.Text = "KalmanFilter";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // KalmanFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(1018, 572);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(720, 360);
            this.Name = "KalmanFilter";
            this.Text = "SVPWM";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;

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
        double mr = 0;                          //modulation radio.
        //double pi = 0;
        //object
        Bitmap bmp;
        Graphics grp;
        private Label label1;
        private TextBox textBox1;
        private Button button1;
        Panel panelPic = new Panel();
        StringFormat drawFormat = new StringFormat();
        //private Label xLabel[12];

        kalmanType kal = new kalmanType();
        Random rQ = new Random();
        Random rR = new Random();
    }
}

