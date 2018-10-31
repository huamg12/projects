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
    public partial class SVPWM : Form
    {
        public SVPWM()
        {
            InitializeComponent();
        }

        private void SetStartToTargetPoints(int px1, int py1, int px2, int py2)
        {
            x1 = px1;
            y1 = py1;
            x2 = px2;
            y2 = py2;
        }

        /** 五段式 SVPWM 占空比计算 MATLAB 源代码(一相恒低)   //const high = mr*(1-uvw).
        % File Name: DutyCycle_5_Segment_SVPWM_OnePhaseToGND.m
        % Author: Jerry.Hua
        % Description:
        % 1. calculation of 5 segments SVPWM , one phase is low(connected to
        % GND) during one PWM period
        * */
        private void Calculate5SvpwmWithOneLow(float[] u, float[] v, float[] w, float[] com)
        {
            double mr = (double)Y_LEN;              //modulation radio.
            double sr = (double)(X_LEN / 360);      //scale radio.

            double pi = Math.PI;
            double a = 0;                           //alpha, or theta

            int i = 0;                              //angle index.
            int x = 0;                              //add index.
            int range = X_LEN;

            if (range > 720)
            {
                range = 720; //max to display 2 circles.
            }
            if (range < 360)
            {
                range = 360; //min to display 1 circles.
            }
            for (x = 0; x < range; x++)
            {
                i = x % 360;
                a = ((double)i / 180) * pi;
                if ((i >= 0) && (i < 60))
                {
                    u[x] = (float)(mr * Math.Cos(a - pi / 6));
                    v[x] = (float)(mr * Math.Sin(a));
                    w[x] = 0;
                }
                else if ((i >= 60) && (i < 120))
                {
                    u[x] = (float)(mr * Math.Cos(a - pi / 6));
                    v[x] = (float)(mr * Math.Sin(a));
                    w[x] = 0;
                }
                else if ((i >= 120) && (i < 180))
                {
                    u[x] = 0;
                    v[x] = (float)(mr * Math.Sin(a - pi / 3));
                    w[x] = (float)(-mr * Math.Cos(a - pi / 6));
                }
                else if ((i >= 120) && (i < 180))
                {
                    u[x] = 0;
                    v[x] = (float)(mr * Math.Sin(a - pi / 3));
                    w[x] = (float)(-mr * Math.Cos(a - pi / 6));
                }
                else if ((i >= 180) && (i < 240))
                {
                    u[x] = 0;
                    v[x] = (float)(mr * Math.Sin(a - pi / 3));
                    w[x] = (float)(-mr * Math.Cos(a - pi / 6));
                }
                else if ((i >= 240) && (i < 300))
                {
                    u[x] = (float)(mr * Math.Cos(a + pi / 6));
                    v[x] = 0;
                    w[x] = (float)(-mr * Math.Sin(a));
                }
                else if ((i >= 300) && (i < 360))
                {
                    u[x] = (float)(mr * Math.Cos(a + pi / 6));
                    v[x] = 0;
                    w[x] = (float)(-mr * Math.Sin(a));
                }
                com[x] = (u[x] + v[x] + w[x]) / 3;
            }//for calculate.
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(X_LEN, Y_LEN);
            Graphics grp = Graphics.FromImage(bmp);
            ///Graphics grp = pictureBox1.CreateGraphics(); //can not display correct, and disappear.
            SetStartToTargetPoints(0, Y_LEN - 0,
                                   0, Y_LEN - Y_LEN); //see the right data (0, 0)->(0, Y).
            grp.DrawLine(new Pen(Color.Gray, 1), new Point(x1, y1), new Point(x2, y2)); //left

            SetStartToTargetPoints(X_LEN, Y_LEN - 0,
                                   X_LEN, Y_LEN - Y_LEN); //see the right data (X, 0)->(X, Y).
            grp.DrawLine(new Pen(Color.Gray, 2), new Point(x1, y1), new Point(x2, y2)); //right

            SetStartToTargetPoints(0, Y_LEN - Y_LEN,
                                   X_LEN, Y_LEN - Y_LEN); //see the right data (0, Y)->(X, Y).
            grp.DrawLine(new Pen(Color.Gray, 1), new Point(x1, y1), new Point(x2, y2)); //top

            SetStartToTargetPoints(0, Y_LEN - 0,
                                   X_LEN, Y_LEN - 0); //see the right data (0, 0)->(X,0).
            grp.DrawLine(new Pen(Color.Gray, 1), new Point(x1, y1), new Point(x2, y2)); //bottom

            Calculate5SvpwmWithOneLow(DC_U, DC_V, DC_W, DC_COM);
            //double mr = (double)Y_LEN;              //modulation radio.
            //double sr = (double)(X_LEN / 360);      //scale radio.

            //draw lines.
            for (int x = 0; x < X_LEN; x++)
            {
                //int k = (int)((double)x / sr);
                int k = x;
                if (k > X_LEN - 2)
                {
                    k = X_LEN - 2;
                }
                grp.DrawLine(new Pen(Color.Red, 2), new Point(x, Y_LEN - (int)DC_U[k]), new Point(x + 1, Y_LEN - (int)DC_U[k + 1]));
                grp.DrawLine(new Pen(Color.Blue, 2), new Point(x, Y_LEN - (int)DC_V[k]), new Point(x + 1, Y_LEN - (int)DC_V[k + 1]));
                grp.DrawLine(new Pen(Color.Green, 2), new Point(x, Y_LEN - (int)DC_W[k]), new Point(x + 1, Y_LEN - (int)DC_W[k + 1]));
                grp.DrawLine(new Pen(Color.LightPink, 2), new Point(x, Y_LEN - (int)DC_COM[k]), new Point(x + 1, Y_LEN - (int)DC_COM[k + 1]));
            }

            //grp.DrawLine(new Pen(Color.Red, 10), new Point(0, 0), new Point(100, 100));
            //grp.DrawLine(new Pen(Color.Red, 10), new Point(0, Y_LEN - 0), new Point(100, Y_LEN - 100));//Y should move to left bottom point.
            pictureBox1.Image = bmp;
        }//button 2 click.
    }
}
