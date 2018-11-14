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
            InitializeParameter();
            InitializeDraw();
        }

        private void InitializeParameter()
        {
            pi = Math.PI;
            mr = 0.9;
            GetModulationRadio();

            Y_LEN = pictureBox1.Height;
            X_LEN = pictureBox1.Width;
            X_LEN = GetDrawLengthRange(X_LEN);

            Ub = (double)Y_LEN;                 //Voltage base.
            xtoa = (double)(CIRCLE) / X_LEN;    //x to angle.       x/X_LEN = i/360.   i = x * (360/X_LEN)
        }

        private void InitializeDraw()
        {
            bmp = new Bitmap(X_LEN, Y_LEN);
            grp = Graphics.FromImage(bmp);
            DrawPictureFrameLines(grp);
            pictureBox1.Image = bmp;
        }

        private void DrawGrayFromStartToTargetPoints(int px1, int py1, int px2, int py2, int width)
        {
            grp.DrawLine(new Pen(Color.Gray, width), new Point(px1, py1), new Point(px2, py2));
        }

        private void DrawPictureFrameLines(Graphics grp)
        {
            //Top board line.
            DrawGrayFromStartToTargetPoints(0, Y_LEN - Y_LEN, X_LEN, Y_LEN - Y_LEN, 2); //see the "-" right data (0, 0)->(0, Y).
            //Bottom board line.
            DrawGrayFromStartToTargetPoints(0, Y_LEN - 0, X_LEN, Y_LEN - 0, 2);
            //Left board line.
            DrawGrayFromStartToTargetPoints(0, Y_LEN - 0, 0, Y_LEN - Y_LEN, 2);
            //Right board line.
            DrawGrayFromStartToTargetPoints(X_LEN, Y_LEN - 0, X_LEN, Y_LEN - Y_LEN, 2);

            //draw grad lines.
            int columns = 12;                       //fix 30 degree grad. 360/30 = 12.
            int gapa = CIRCLE / columns;            //angle gap.
            float gapx = (float)(gapa / xtoa);      //x points gap.

            int lines = 8;  // (int)(Y_LEN / grad);
            float grad = (float)(Y_LEN / lines); //gapx;                      //y axis grad.

            int flag = 0;
            int dot = 5;
            //draw dash lines.
            for (int n = 0; n <= lines; n++ )
            {
                for (int p = 0; p < X_LEN; p = p + dot) //points
                {
                    if (flag == 0)
                    {
                        int cpy = (int)(n * grad); //lines point y axis.
                        DrawGrayFromStartToTargetPoints(p, Y_LEN - cpy, p + dot, Y_LEN - cpy, 1); //from south-west.
                        flag = 1;
                    }
                    else
                    {
                        flag = 0;
                    }
                }
            }
            //draw dash columns.
            for (int c = 0; c <= columns; c++)
            {
                for (int p = 0; p < Y_LEN; p = p + dot) //points
                {
                    if (flag == 0)
                    {
                        int cpx = (int)(c * gapa / xtoa); //column point x axis.
                        DrawGrayFromStartToTargetPoints(cpx, Y_LEN - p, cpx, Y_LEN - (p + dot), 1); //from south-west.
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

        private void GetModulationRadio()
        {
            double modulation = double.Parse(textBox1.Text);
            mr = (double)modulation;
            if (mr > 1)
            {
                mr = 1;
                textBox1.Text = "1.0";
            }
            else if (mr < 0)
            {
                mr = 0;
                textBox1.Text = "0.0";
            }
        }

        /** 五段式 SVPWM 占空比计算源代码(一相恒低)   //const high = mr*(1-uvw).
         *  File Name: DutyCycle_5_Segment_SVPWM_OnePhaseToGND
         *  Author: Jerry.Hua
         *  Description:
         *  Function: Calculation of 5 segments SVPWM , one phase is low(connected to
         *  GND) during one PWM period
         * */
        private void Calculate5SvpwmWithOneLow(float[] u, float[] v, float[] w, float[] com)
        {
            int range = X_LEN;
            for (int x = 0; x < range; x++)
            {
                double angle = (double)x * xtoa;
                angle = angle % 360;
                double radian = (angle / 180) * pi;
                if ((angle >= 0) && (angle < 60))
                {
                    u[x] = (float)(Ub * mr * Math.Cos(radian - pi / 6));
                    v[x] = (float)(Ub * mr * Math.Sin(radian));
                    w[x] = 0;
                    com[x] = u[x] * u[x] + v[x] * v[x] + (float)(u[x] * v[x] * Math.Cos(2 * pi / 3));
                }
                else if ((angle >= 60) && (angle < 120))
                {
                    u[x] = (float)(Ub * mr * Math.Cos(radian - pi / 6));
                    v[x] = (float)(Ub * mr * Math.Sin(radian));
                    w[x] = 0;
                    com[x] = u[x] * u[x] + v[x] * v[x] + (float)(u[x] * v[x] * Math.Cos(2 * pi / 3));
                }
                else if ((angle >= 120) && (angle < 180))
                {
                    u[x] = 0;
                    v[x] = (float)(Ub * mr * Math.Sin(radian - pi / 3));
                    w[x] = (float)(-Ub * mr * Math.Cos(radian - pi / 6));
                    com[x] = w[x] * w[x] + v[x] * v[x] + (float)(w[x] * v[x] * Math.Cos(2 * pi / 3));
                }
                else if ((angle >= 180) && (angle < 240))
                {
                    u[x] = 0;
                    v[x] = (float)(Ub * mr * Math.Sin(radian - pi / 3));
                    w[x] = (float)(-Ub * mr * Math.Cos(radian - pi / 6));
                    com[x] = w[x] * w[x] + v[x] * v[x] + (float)(w[x] * v[x] * Math.Cos(2 * pi / 3));
                }
                else if ((angle >= 240) && (angle < 300))
                {
                    u[x] = (float)(Ub * mr * Math.Cos(radian + pi / 6));
                    v[x] = 0;
                    w[x] = (float)(-Ub * mr * Math.Sin(radian));
                    com[x] = u[x] * u[x] + w[x] * w[x] + (float)(u[x] * w[x] * Math.Cos(2 * pi / 3));
                }
                else if ((angle >= 300) && (angle < 360))
                {
                    u[x] = (float)(Ub * mr * Math.Cos(radian + pi / 6));
                    v[x] = 0;
                    w[x] = (float)(-Ub * mr * Math.Sin(radian));
                    com[x] = u[x] * u[x] + w[x] * w[x] + (float)(u[x] * w[x] * Math.Cos(2 * pi / 3));
                }
                //com[x] = (u[x] + v[x] + w[x]) / 3;
                ////com[x] = u[x];
                ////com[x] += v[x] * (float)(Math.Cos(radian - 2 * pi / 3));
                ////com[x] += w[x] * (float)(Math.Cos(radian + 2 * pi / 3));
                ////com[x] -= Y_LEN / 2;
                com[x] = (float)Math.Sqrt(com[x]) / 2;
            }//for calculate.
        }

        /** 五段式 SVPWM 占空比计算源代码(一相恒高)   //const high = mr*(1-uvw).
         *  File Name: DutyCycle_5_Segment_SVPWM_OnePhaseToGND
         *  Author: Jerry.Hua
         *  Description:
         *  Function: Calculation of 5 segments SVPWM , one phase is low(connected to
         *  GND) during one PWM period
         * */
        private void Calculate5SvpwmWithOneHigh(float[] u, float[] v, float[] w, float[] com)
        {
            int range = X_LEN;
            for (int x = 0; x < range; x++)
            {
                double angle = (double)x * xtoa;
                angle = angle % 360;
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

        /** 七段式 SVPWM 占空比计算源代码
         *  File Name: DutyCycle_7_Segment_SVPWM
         *  Author: Jerry.Hua
         *  Description:
         *  Function: Calculation of 7 segments SVPWM.
         * */
        private void Calculate7Svpwm(float[] u, float[] v, float[] w, float[] com)
        {
            int range = X_LEN;
            for (int x = 0; x < range; x++)
            {
                double angle = (double)x * xtoa;
                angle = angle % 360;
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

        private void Calculate3PhaseSum(float[] u, float[] v, float[] w, float[] com)
        {
            int range = X_LEN;
            for (int x = 0; x < range; x++)
            {
                double angle = (double)x * xtoa;
                angle = angle % 360;
                double radian = (angle / 180) * pi;
                if ((angle >= 0) && (angle < 360))
                {
                    u[x] = (float)(Ub * (mr * Math.Cos(radian))/2 + Ub / 2);
                    v[x] = (float)(Ub * (mr * Math.Cos(radian - pi * 2 / 3)) / 2 + Ub / 2);
                    w[x] = (float)(Ub * (mr * Math.Cos(radian + pi * 2 / 3)) / 2 + Ub / 2);
                }
                
                com[x] = (u[x] + v[x] + w[x]) / 3;
            }//for calculate.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearCalculatedData();
            Calculate5SvpwmWithOneLow(DC_U, DC_V, DC_W, DC_COM);
            DrawPictureDataLines(grp);
        }

        private void button2_Click(object sender, EventArgs e)
        {            
            ClearCalculatedData();
            Calculate5SvpwmWithOneHigh(DC_U, DC_V, DC_W, DC_COM);
            DrawPictureDataLines(grp);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearCalculatedData();
            Calculate7Svpwm(DC_U, DC_V, DC_W, DC_COM);
            DrawPictureDataLines(grp);
        }

        private void clear_Click(object sender, EventArgs e)
        {
            ClearCalculatedData();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            GetModulationRadio();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClearCalculatedData();
            Calculate3PhaseSum(DC_U, DC_V, DC_W, DC_COM);
            DrawPictureDataLines(grp);
        }
    }
}
