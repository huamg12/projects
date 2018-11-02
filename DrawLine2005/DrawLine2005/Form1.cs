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

            pi = Math.PI;
            InitializeDraw();
        }

        private void InitializeDraw()
        {
            bmp = new Bitmap(X_LEN, Y_LEN);
            grp = Graphics.FromImage(bmp);
            DrawPictureFrameLines(grp);
            pictureBox1.Image = bmp;
        }

        private void SetStartToTargetPoints(int px1, int py1, int px2, int py2)
        {
            x1 = px1;
            y1 = py1;
            x2 = px2;
            y2 = py2;
        }

        private void DrawPictureFrameLines(Graphics grp)
        {
            SetStartToTargetPoints(0, Y_LEN - 0, 0, Y_LEN - Y_LEN); //see the right data (0, 0)->(0, Y).
            grp.DrawLine(new Pen(Color.Gray, 1), new Point(x1, y1), new Point(x2, y2)); //left

            SetStartToTargetPoints(X_LEN, Y_LEN - 0, X_LEN, Y_LEN - Y_LEN); //see the right data (X, 0)->(X, Y).
            grp.DrawLine(new Pen(Color.Gray, 2), new Point(x1, y1), new Point(x2, y2)); //right

            SetStartToTargetPoints(0, Y_LEN - Y_LEN, X_LEN, Y_LEN - Y_LEN); //see the right data (0, Y)->(X, Y).
            grp.DrawLine(new Pen(Color.Gray, 1), new Point(x1, y1), new Point(x2, y2)); //top

            SetStartToTargetPoints(0, Y_LEN - 0, X_LEN, Y_LEN - 0); //see the right data (0, 0)->(X,0).
            grp.DrawLine(new Pen(Color.Gray, 2), new Point(x1, y1), new Point(x2, y2)); //bottom

            //draw grad
            int grad = 60;
            int dot = 5;
            int flag = 0;
            int lines = Y_LEN / grad;
            int columns = X_LEN / grad;
            //draw lines.
            for (int y = 0; y <= lines; y++ )
            {
                for (int p = 0; p < X_LEN; p = p + dot) //points
                {
                    if (flag == 1)
                    {
                        SetStartToTargetPoints(p, Y_LEN - y * grad, p + dot, Y_LEN - y * grad); //from south-west.
                        grp.DrawLine(new Pen(Color.Gray, 1), new Point(x1, y1), new Point(x2, y2)); //lines
                        flag = 0;
                    }
                    else
                    {
                        flag = 1;
                    }
                }
            }
            //draw columns.
            int cpx = 0; //column point x axis.

            for (int x = 0; x <= columns; x++)
            {
                for (int p = 0; p < Y_LEN; p = p + dot) //points
                {
                    if (flag == 0)
                    {
                        cpx = (int)(x * grad / xsr);
                        SetStartToTargetPoints(cpx, Y_LEN - p, cpx, Y_LEN - (p + dot)); //from south-west.
                        grp.DrawLine(new Pen(Color.Gray, 1), new Point(x1, y1), new Point(x2, y2)); //columns
                        flag = 1;
                    }
                    else
                    {
                        flag = 0;
                    }
                }
            }
            pictureBox1.Image = bmp;
        }

        private int GetDrawLengthRange(int LenRange)
        {
            int LimitRange = LenRange;
            if (LenRange > 720)
            {
                LimitRange = 720; //max to display 2 circles.
            }
            else if (LenRange < 360)
            {
                LimitRange = 360; //min to display 1 circles.
            }
            else
            {
                LimitRange = LenRange;
            }
            return LimitRange;
        }

        private void DrawPictureDataLines(Graphics grp)
        {
            for (int x = 0; x < X_LEN; x++)
            {
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
            pictureBox1.Image = bmp;
        }

        private void ClearCalculatedData()
        {
            for (int i = 0; i < X_LEN; i++)
            {
                DC_U[i] = 0;
                DC_V[i] = 0;
                DC_W[i] = 0;
                DC_COM[i] = 0;
            }
            InitializeDraw();
        }

        /** 五段式 SVPWM 占空比计算 MATLAB 源代码(一相恒低)   //const high = mr*(1-uvw).
         *  File Name: DutyCycle_5_Segment_SVPWM_OnePhaseToGND.m
         *  Author: Jerry.Hua
         *  Description:
         *  1. calculation of 5 segments SVPWM , one phase is low(connected to
         *  GND) during one PWM period
         * */
        private void Calculate5SvpwmWithOneLow(float[] u, float[] v, float[] w, float[] com)
        {
            int range = GetDrawLengthRange( X_LEN );
            for (int x = 0; x < range; x++)
            {
                double angle = (double)x * xsr;
                double radian = (angle / 180) * pi;
                if ((angle >= 0) && (angle < 60))
                {
                    u[x] = (float)(Ub * mr * Math.Cos(radian - pi / 6));
                    v[x] = (float)(Ub * mr * Math.Sin(radian));
                    w[x] = 0;
                }
                else if ((angle >= 60) && (angle < 120))
                {
                    u[x] = (float)(Ub * mr * Math.Cos(radian - pi / 6));
                    v[x] = (float)(Ub * mr * Math.Sin(radian));
                    w[x] = 0;
                }
                else if ((angle >= 120) && (angle < 180))
                {
                    u[x] = 0;
                    v[x] = (float)(Ub * mr * Math.Sin(radian - pi / 3));
                    w[x] = (float)(-Ub * mr * Math.Cos(radian - pi / 6));
                }
                else if ((angle >= 180) && (angle < 240))
                {
                    u[x] = 0;
                    v[x] = (float)(Ub * mr * Math.Sin(radian - pi / 3));
                    w[x] = (float)(-Ub * mr * Math.Cos(radian - pi / 6));
                }
                else if ((angle >= 240) && (angle < 300))
                {
                    u[x] = (float)(Ub * mr * Math.Cos(radian + pi / 6));
                    v[x] = 0;
                    w[x] = (float)(-Ub * mr * Math.Sin(radian));
                }
                else if ((angle >= 300) && (angle < 360))
                {
                    u[x] = (float)(Ub * mr * Math.Cos(radian + pi / 6));
                    v[x] = 0;
                    w[x] = (float)(-Ub * mr * Math.Sin(radian));
                }
                com[x] = (u[x] + v[x] + w[x]) / 3;
            }//for calculate.
        }

        /** 五段式 SVPWM 占空比计算 MATLAB 源代码(一相恒高)   //const high = mr*(1-uvw).
         *  File Name: DutyCycle_5_Segment_SVPWM_OnePhaseToGND.m
         *  Author: Jerry.Hua
         *  Description:
         *  1. calculation of 5 segments SVPWM , one phase is low(connected to
         *  GND) during one PWM period
         * */
        private void Calculate5SvpwmWithOneHigh(float[] u, float[] v, float[] w, float[] com)
        {
            int range = GetDrawLengthRange(X_LEN);
            for (int x = 0; x < range; x++)
            {
                double angle = (double)x * xsr;
                double radian = (angle / 180) * pi;
                if ((angle >= 0) && (angle < 60))
                {
                    u[x] = (float)(Ub * mr * Math.Cos(radian - pi / 6));
                    v[x] = (float)(Ub * mr * Math.Sin(radian));
                    w[x] = 0;
                }
                else if ((angle >= 60) && (angle < 120))
                {
                    u[x] = (float)(Ub * mr * Math.Cos(radian - pi / 6));
                    v[x] = (float)(Ub * mr * Math.Sin(radian));
                    w[x] = 0;
                }
                else if ((angle >= 120) && (angle < 180))
                {
                    u[x] = 0;
                    v[x] = (float)(Ub * mr * Math.Sin(radian - pi / 3));
                    w[x] = (float)(-Ub * mr * Math.Cos(radian - pi / 6));
                }
                else if ((angle >= 180) && (angle < 240))
                {
                    u[x] = 0;
                    v[x] = (float)(Ub * mr * Math.Sin(radian - pi / 3));
                    w[x] = (float)(-Ub * mr * Math.Cos(radian - pi / 6));
                }
                else if ((angle >= 240) && (angle < 300))
                {
                    u[x] = (float)(Ub * mr * Math.Cos(radian + pi / 6));
                    v[x] = 0;
                    w[x] = (float)(-Ub * mr * Math.Sin(radian));
                }
                else if ((angle >= 300) && (angle < 360))
                {
                    u[x] = (float)(Ub * mr * Math.Cos(radian + pi / 6));
                    v[x] = 0;
                    w[x] = (float)(-Ub * mr * Math.Sin(radian));
                }
                u[x] = (float)(Ub * mr) - u[x];
                v[x] = (float)(Ub * mr) - v[x];
                w[x] = (float)(Ub * mr) - w[x];
                com[x] = (u[x] + v[x] + w[x]) / 3;
            }//for calculate.
        }

        /** 七段式 SVPWM 占空比计算 MATLAB 源代码
         *  File Name: DutyCycle_5_Segment_SVPWM_OnePhaseToGND.m
         *  Author: Jerry.Hua
         *  Description:
         *  1. calculation of 7 segments SVPWM.
         * */
        private void Calculate7Svpwm(float[] u, float[] v, float[] w, float[] com)
        {
            int range = GetDrawLengthRange(X_LEN);
            for (int x = 0; x < range; x++)
            {
                double angle = (double)x * xsr;
                double radian = (angle / 180) * pi;
                if ((angle >= 0) && (angle < 60))
                {
                    u[x] = (float)(Ub *  (1 + mr * Math.Cos(radian - pi / 6))/2);
                    v[x] = (float)(Ub * ((1 - mr * Math.Cos(radian - pi / 6))/2 + mr * Math.Sin(radian)));
                    w[x] = (float)(Ub *  (1 - mr * Math.Cos(radian - pi / 6))/2);
                }
                else if ((angle >= 60) && (angle < 120))
                {
                    u[x] = (float)(Ub * ((1 - mr * Math.Sin(radian))/2 + mr * Math.Sin(radian + pi / 3)));
                    v[x] = (float)(Ub *  (1 + mr * Math.Sin(radian))/2);
                    w[x] = (float)(Ub *  (1 - mr * Math.Sin(radian))/2);
                }
                else if ((angle >= 120) && (angle < 180))
                {
                    u[x] = (float)(Ub *  (1 - mr * Math.Sin(radian - pi / 3))/2);
                    v[x] = (float)(Ub *  (1 + mr * Math.Sin(radian - pi / 3))/2);
                    w[x] = (float)(Ub * ((1 - mr * Math.Sin(radian - pi / 3))/2 - mr * Math.Sin(radian + pi / 3)));
                }
                else if ((angle >= 180) && (angle < 240))
                {
                    u[x] = (float)(Ub *  (1 + mr * Math.Cos(radian - pi / 6))/2);
                    v[x] = (float)(Ub * ((1 + mr * Math.Cos(radian - pi / 6))/2 + mr * Math.Sin(radian - pi / 3)));
                    w[x] = (float)(Ub *  (1 - mr * Math.Cos(radian - pi / 6))/2);
                }
                else if ((angle >= 240) && (angle < 300))
                {
                    u[x] = (float)(Ub * ((1 + mr * Math.Sin(radian))/2 - mr * Math.Sin(radian - pi / 3)));
                    v[x] = (float)(Ub *  (1 + mr * Math.Sin(radian))/2);
                    w[x] = (float)(Ub *  (1 - mr * Math.Sin(radian))/2);
                }
                else if ((angle >= 300) && (angle < 360))
                {
                    u[x] = (float)(Ub *  (1 + mr * Math.Cos(radian + pi / 6))/2);
                    v[x] = (float)(Ub *  (1 - mr * Math.Cos(radian + pi / 6))/2);
                    w[x] = (float)(Ub * ((1 - mr * Math.Cos(radian + pi / 6))/2 - mr * Math.Sin(radian)));
                }
                com[x] = (u[x] + v[x] + w[x])/3;
            }//for calculate.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //calculate
            ClearCalculatedData();
            Calculate5SvpwmWithOneLow(DC_U, DC_V, DC_W, DC_COM);
            //draw lines.
            DrawPictureDataLines(grp);
        }//button 1 click.

        private void button2_Click(object sender, EventArgs e)
        {
            //calculate
            ClearCalculatedData();
            Calculate5SvpwmWithOneHigh(DC_U, DC_V, DC_W, DC_COM);
            //draw lines.
            DrawPictureDataLines(grp);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //calculate
            ClearCalculatedData();
            Calculate7Svpwm(DC_U, DC_V, DC_W, DC_COM);
            //draw lines.
            DrawPictureDataLines(grp);
        }

        private void clear_Click(object sender, EventArgs e)
        {
            ClearCalculatedData();
        }
    }
}
