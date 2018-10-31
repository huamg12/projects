using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DrawLine2005
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /** 五段式 SVPWM 占空比计算 MATLAB 源代码(一相恒低)   //const high = mr*(1-uvw).
        % File Name: DutyCycle_5_Segment_SVPWM_OnePhaseToGND.m
        % Author: Jerry.Hua
        % Description:
        % 1. calculation of 5 segments SVPWM , one phase is low(connected to
        % GND) during one PWM period
        * */
        private void button2_Click(object sender, EventArgs e)
        {
            int X_LEN = 720; //can be get from user setting.
            int Y_LEN = 400; //can be get from user setting.
            Bitmap bmp = new Bitmap(X_LEN, Y_LEN);
            Graphics grp = Graphics.FromImage(bmp);
            ///Graphics grp = pictureBox1.CreateGraphics(); //can not display correct, and disappear.

            float[] DC_U = new float[360];
            float[] DC_V = new float[360];
            float[] DC_W = new float[360];

            double mr = (double)Y_LEN;              //modulation radio.
            double sr = (double)(X_LEN / 360);      //scale radio.

            double pi = Math.PI;
            double a = 0;                           //alpha, or theta

            for (int i = 0; i < 360; i++)
            {
                a = ((double)i / 180) * pi;
                if ((i>=0) && (i<60))
                {
                    DC_U[i] = (float)(mr * Math.Cos(a - pi / 6));
                    DC_V[i] = (float)(mr * Math.Sin(a));
                    DC_W[i] = 0;
                }
                else if ((i >= 60) && (i < 120))
                {
                    DC_U[i] = (float)(mr * Math.Cos(a - pi / 6));
                    DC_V[i] = (float)(mr * Math.Sin(a));
                    DC_W[i] = 0;
                }
                else if ((i >= 120) && (i < 180))
                {
                    DC_U[i] = 0;
                    DC_V[i] = (float)(mr * Math.Sin(a - pi / 3));
                    DC_W[i] = (float)(-mr * Math.Cos(a - pi / 6));
                }
                else if ((i >= 120) && (i < 180))
                {
                    DC_U[i] = 0;
                    DC_V[i] = (float)(mr * Math.Sin(a - pi / 3));
                    DC_W[i] = (float)(-mr * Math.Cos(a - pi / 6));
                }
                else if ((i >= 180) && (i < 240))
                {
                    DC_U[i] = 0;
                    DC_V[i] = (float)(mr * Math.Sin(a - pi / 3));
                    DC_W[i] = (float)(-mr * Math.Cos(a - pi / 6));
                }
                else if ((i >= 240) && (i < 300))
                {
                    DC_U[i] = (float)(mr * Math.Cos(a + pi / 6));
                    DC_V[i] = 0;
                    DC_W[i] = (float)(-mr * Math.Sin(a));
                }
                else if ((i >= 300) && (i < 360))
                {
                    DC_U[i] = (float)(mr * Math.Cos(a + pi / 6));
                    DC_V[i] = 0;
                    DC_W[i] = (float)(-mr * Math.Sin(a));
                }
            }//for calculate.
            //draw lines.
            for (int j = 0; j < X_LEN; j++)
            {
                int k = (int)((double)j / sr);
                if (k > 360-2)
                {
                    k = 360-2;
                }
                grp.DrawLine(new Pen(Color.Red, 2), new Point(k, Y_LEN - (int)DC_U[k]), new Point(k + 1, Y_LEN - (int)DC_U[k + 1]));
                grp.DrawLine(new Pen(Color.Blue, 2), new Point(k, Y_LEN - (int)DC_V[k]), new Point(k + 1, Y_LEN - (int)DC_V[k + 1]));
                grp.DrawLine(new Pen(Color.Green, 2), new Point(k, Y_LEN - (int)DC_W[k]), new Point(k + 1, Y_LEN - (int)DC_W[k + 1]));
            }

            //grp.DrawLine(new Pen(Color.Red, 10), new Point(0, 0), new Point(100, 100));
            //grp.DrawLine(new Pen(Color.Red, 10), new Point(0, Y_LEN - 0), new Point(100, Y_LEN - 100));//Y should move to left bottom point.
            pictureBox1.Image = bmp;
        }//button 2 click.

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }
    }
}
