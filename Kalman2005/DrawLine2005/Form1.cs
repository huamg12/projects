using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kalman2005
{
    public partial class KalmanFilter : Form
    {
        public KalmanFilter()
        {
            InitializeComponent();
            InitializeParameter();
            InitializeDraw();
        }

        private void InitializeParameter()
        {
            //pi = Math.PI;
            mr = 0.9;
            GetModulationRadio();

            Y_LEN = pictureBox1.Height;
            X_LEN = pictureBox1.Width;
            if (X_LEN > 720)
            {
                X_LEN = 720;
            }
            else if (X_LEN < 360)
            {
                X_LEN = 360;
            }

            Ub = (double)Y_LEN;                 //Voltage base.
            xtoa = (double)(CIRCLE) / X_LEN;    //x to angle.       x/X_LEN = i/360.   i = x * (360/X_LEN)
        }

        //=================================================================//background.
        private void InitializeDraw()
        {
            bmp = new Bitmap(X_LEN, Y_LEN);
            grp = Graphics.FromImage(bmp);
            DrawPictureFrameLines(grp);
            DrawYLine(/*Panel pan*/ panelPic, /*float maxY*/ Y_LEN, /*int len*/ 10);
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
                        if (n == lines)
                        {
                            //Label xPole[];
                        }
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

        /// <summary>
        /// ����Y���ϵķ�ֵ�ߣ����㿪ʼ
        /// </summary>
        /// <param name="pan"></param>
        /// <param name="maxY"></param>
        /// <param name="len"></param>
        #region   ����Y���ϵķ�ֵ�ߣ����㿪ʼ
        public void DrawYLine(Panel pan, float maxY, int len)
        {
            float move = 50f;
            float LenX = pan.Width - 2 * move;
            float LenY = pan.Height - 2 * move;
            //Graphics g = pan.CreateGraphics();
            for (int i = 0; i <= len; i++)    //len�ȷ�Y��
            {
                PointF px1 = new PointF(move, LenY * i / len + move);
                PointF px2 = new PointF(move + 50, LenY * i / len + move);
                string sx = (10 * (len-i)).ToString();
                grp.DrawLine(new Pen(Brushes.Black, 2), px1, px2);
                //StringFormat drawFormat = new StringFormat();

                //drawFormat.Alignment = StringAlignment.Far;
                //drawFormat.LineAlignment = StringAlignment.Center;
                grp.DrawString(sx, new Font("Consolas", 8f), Brushes.Black, new PointF(move / 2f, 30 * i ), drawFormat);
            }
        }
        #endregion

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
                //grp.DrawLine(new Pen(Color.LightPink, 2), new Point(x, Y_LEN - (int)DC_COM[k]), new Point(x + 1, Y_LEN - (int)DC_COM[k + 1]));
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

        //=================================================================//

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

        private void Init_k(double Q, double R)
        {
            kal.X = 22;    //�����ĳ�ʼֵ
            kal.P = 1.2;   //����״̬����ֵ���ķ���ĳ�ʼֵ����ҪΪ0���ⲻ��
            kal.A = 1;    //ת��ϵ��
            kal.H = 1;    //�۲�ϵ��
            kal.Q = Q;    //Ԥ�⣨���̣��������� Ӱ���������ʣ����Ը���ʵ���������
            kal.R = R;    //�������۲⣩�������� ����ͨ��ʵ���ֶλ��
        }

        private void KalmanFilterUpdate(double zk)
        {
            double Xe = 0;
            double Pe = 0;
            Xe = (kal.A) * (kal.X);
            Pe = (kal.A) * (kal.A) * (kal.P) + (kal.Q);
            kal.K = (Pe * (kal.H)) / ((Pe) * (kal.H) * (kal.H) + (kal.R));
            kal.X = Xe + (kal.K) * (zk - ((kal.H) * Xe));
            kal.P = (1 - (kal.K) * (kal.H)) * Pe;
        }

        private void TestKalmanFilter(double[] kmX, double[] kmP, double[] kmK) //kalman data array parameters.
        {
            Init_k(/*double Q*/ 3, /*double R*/ 4);

            double zk = Y_LEN/2;
            int range = X_LEN;
            for (int x = 0; x < range; x++)
            {
                //if (x < (range / 2))
                {
                    kal.Q = 2*(rQ.NextDouble());
                    kal.R = 2*(rR.NextDouble());
                }
                if (x % 2 == 0)
                {
                    zk = Y_LEN / 2 + 5 * (kal.R);
                }
                else
                {
                    zk = Y_LEN / 2 - 5 * (kal.Q);
                }
                if (x % 50 == 0)
                {
                    zk = Y_LEN / 2 + 20 * (kal.R);
                }
                else
                {
                    zk = Y_LEN / 2 - 20 * (kal.Q);
                }
                if (x == 120)
                {
                    zk = zk + 20;
                }
                if (x == 150)
                {
                    zk = zk - 25;
                }
                if (x == 180)
                {
                    zk = zk - 10;
                }
                kal.R = kal.R * kal.R;
                kal.Q = kal.Q * kal.Q;

                if (x % 5 == 0)
                {
                    KalmanFilterUpdate(/*double zk*/ zk);
                }
                
                kmX[x] = (kal.X);
                kmP[x] = (kal.P)*10;
                //kmK[x] = (kal.K)*100;
                kmK[x] = (zk - 50);
            }
        }

        //====================================================================//events.
        private void button1_Click(object sender, EventArgs e)
        {
            ClearCalculatedData();
            TestKalmanFilter(DC_U, DC_V, DC_W);
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

    }
}