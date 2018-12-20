using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace SerialPort2005
{
    public partial class SerialPortComm : Form
    {
        #region SerialPortDefine
        //serial port
        SerialPort comm01 = new SerialPort();
        bool isOpen = false;
        bool isSetProperty = false;
        bool isHex = false;
        int comNum = 32;
        #endregion //SerialPortDefine

        public SerialPortComm()
        {
            InitializeComponent();
            InitializeParameter();
            InitializeDraw();
        }

        #region SerialPortInit

        private void SerialPortComm_Load(object sender, EventArgs e)
        {
            InitializeSerialPort();
        }

        private void InitializeSerialPort()
        {
            //baudrate
            cbxBaudRate.Items.Add("1200");
            cbxBaudRate.Items.Add("2400");
            cbxBaudRate.Items.Add("4800");
            cbxBaudRate.Items.Add("9600"); //ix=3
            cbxBaudRate.Items.Add("19200");
            cbxBaudRate.Items.Add("38400");
            cbxBaudRate.Items.Add("43000"); //ix=6
            cbxBaudRate.Items.Add("56000");
            cbxBaudRate.Items.Add("57600");
            cbxBaudRate.Items.Add("115200"); //ix=9
            cbxBaudRate.SelectedIndex = 9;
            //stop bits
            cbxStopBits.Items.Add("0");
            cbxStopBits.Items.Add("1");
            cbxStopBits.Items.Add("1.5");
            cbxStopBits.Items.Add("2");
            cbxStopBits.SelectedIndex = 1;
            //data bits
            cbxDataBits.Items.Add("8");
            cbxDataBits.Items.Add("7");
            cbxDataBits.Items.Add("6");
            cbxDataBits.Items.Add("5");
            cbxDataBits.SelectedIndex = 0;
            //even-odd check
            cbxParity.Items.Add("None");
            cbxParity.Items.Add("Odd");
            cbxParity.Items.Add("Even");
            cbxParity.SelectedIndex = 0;
            //char or hex
            rbnChar.Checked = true;

            //check there is COMM or not.
            string[] nameArray = SerialPort.GetPortNames();
            if (nameArray == null)
            {
                MessageBox.Show("No comm port!", "Error!");
                return;
            }
            foreach (string name in nameArray)
            {
                cbxCOMPort.Items.Add(name);
            }
            cbxCOMPort.SelectedIndex = 0;
        }

        private bool CheckPortSetting()
        {
            if (cbxCOMPort.Text.Trim() == "") return false;
            if (cbxBaudRate.Text.Trim() == "") return false;
            if (cbxDataBits.Text.Trim() == "") return false;
            if (cbxParity.Text.Trim() == "") return false;
            if (cbxStopBits.Text.Trim() == "") return false;
            return true;
        }

        private bool CheckSendData()
        {
            if (tbxSendData.Text.Trim() == "") return false;
            return true;
        }

        private void SetPortProperty()
        {
            comm01.PortName = cbxCOMPort.SelectedItem.ToString();
            comm01.BaudRate = Convert.ToInt32(cbxBaudRate.Text);
            comm01.DataBits = Convert.ToInt16(cbxDataBits.Text);

            float fStopBit = Convert.ToSingle(cbxStopBits.Text);
            if (fStopBit == 0)
            {
                comm01.StopBits = StopBits.None;
            }
            else if (fStopBit == 1)
            {
                comm01.StopBits = StopBits.OnePointFive;
            }
            else if (fStopBit == 1.5)
            {
                comm01.StopBits = StopBits.One;
            }
            else if (fStopBit == 2)
            {
                comm01.StopBits = StopBits.Two;
            }
            else
            {
                comm01.StopBits = StopBits.One;
            }

            string sParity = cbxParity.Text.Trim();
            if (sParity.CompareTo("None") == 0)
            {
                comm01.Parity = Parity.None;
            }
            else if (sParity.CompareTo("Odd") == 0)
            {
                comm01.Parity = Parity.Odd;
            }
            else if (sParity.CompareTo("Even") == 0)
            {
                comm01.Parity = Parity.Even;
            }
            else
            {
                comm01.Parity = Parity.None;
            }
            if (rbnHex.Checked)
            {
                isHex = true;
            }
            else
            {
                isHex = false;
            }

            //data reveived event.
            Control.CheckForIllegalCrossThreadCalls = false;
            comm01.DataReceived += new SerialDataReceivedEventHandler(comm01_DataReceived);

            comm01.DtrEnable = true;
            comm01.RtsEnable = true;
            comm01.ReadTimeout = 2000;
            comm01.Close();
        }

        #endregion SerialPortInit

        #region InitPictureParamters
        private void InitializeParameter()
        {
            //pi = Math.PI;
            //mr = 0.9;
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
        #endregion //InitPictureParamters

        #region InitializeDraw
        private void InitializeDraw()
        {
            bmp = new Bitmap(X_LEN, Y_LEN);
            grp = Graphics.FromImage(bmp);
            DrawPictureFrameLines(grp);
            pictureBox1.Image = bmp;
        }
        #endregion //InitializeDraw

        #region DrawGradLines

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
            DrawYLine(/*Panel pan*/ panelPic, /*float maxY*/ Y_LEN, /*int lines*/ lines);
            DrawXColumns(/*Panel pan*/ panelPic, /*float maxX*/ X_LEN, /*int columns*/ columns);
            pictureBox1.Image = bmp;
        }
        #endregion //DrawGradLines

        #region DarwYlinesFromZero
        /// <summary>
        /// Darw Y lines from zero.
        /// </summary>
        /// <param name="pan"></param>
        /// <param name="maxY"></param>
        /// <param name="lines"></param>
        public void DrawYLine(Panel pan, float maxY, int lines)
        {
            float LenY = maxY;
            float grad = (LenY / lines);
            for (int i = 0; i <= lines; i++) //lines to divide Y axis.
            {
                float yScale = (LenY - grad * i);
                grp.DrawLine(new Pen(Brushes.Black, 2), new PointF(0, yScale), new PointF(5, yScale));
                string yLabel = ((int)(grad * i)).ToString();
                grp.DrawString(yLabel, new Font("Consolas", 8F), Brushes.Black, new PointF(0, yScale));
            }
        }
        #endregion //DarwYlinesFromZero

        #region DarwXcolumnsFromZero
        /// <summary>
        /// Darw X columns from zero.
        /// </summary>
        /// <param name="pan"></param>
        /// <param name="maxX"></param>
        /// <param name="lines"></param>
        public void DrawXColumns(Panel pan, float maxX, int columns)
        {
            float LenX = maxX;
            float gap = (LenX / columns);
            for (int i = 0; i <= columns; i++) //columns to divide X axis.
            {
                float xScale = (gap * i);
                grp.DrawLine(new Pen(Brushes.Black, 2), new PointF(xScale, Y_LEN - 0), new PointF(xScale, Y_LEN - 5));
                string xLabel = ((int)(gap * i)).ToString();
                grp.DrawString(xLabel, new Font("Consolas", 8F), Brushes.Black, new PointF(xScale, Y_LEN - 15));
            }
        }
        #endregion //DarwXcolumnsFromZero

        #region DrawDataLines
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
        #endregion // DrawDataLines

        #region ClearData
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
        #endregion //ClearData

        #region SVPWM_MOD
        private void GetModulationRadio()
        {
            /*
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
            }*/
        }
        #endregion //SVPWM_MOD

        #region KalmanFilterAlgorithm
        private void Init_k(double Q, double R)
        {
            kal.X = 22;    //测量的初始值
            kal.P = 1.2;   //后验状态估计值误差的方差的初始值（不要为0问题不大）
            kal.A = 1;    //转换系数
            kal.H = 1;    //观测系数
            kal.Q = Q;    //预测（过程）噪声方差 影响收敛速率，可以根据实际需求给出
            kal.R = R;    //测量（观测）噪声方差 可以通过实验手段获得
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
        #endregion //KalmanFilterAlgorithm

        #region DrawEvents
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
        #endregion //DrawEvents

        #region SerialPortEvents
        private void btnCheckCOM_Click(object sender, EventArgs e)
        {
            bool comExist = false;
            cbxCOMPort.Items.Clear();
            for (int i = 0; i < comNum; i++)
            {
                try
                {
                    SerialPort sp = new SerialPort(("COM" + (i + 1)).ToString());
                    sp.Open();
                    sp.Close();
                    cbxCOMPort.Items.Add(("COM" + (i + 1)).ToString());
                    comExist = true;
                }
                catch (Exception)
                {
                    continue;
                }
            }
            if (comExist)
            {
                cbxCOMPort.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("COM not found!", "Error!");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (isOpen)
            {
                try
                {
                    comm01.WriteLine(tbxSendData.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Send Data Error!", "Error!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Comm not opened!", "Error!");
                return;
            }
            if (CheckSendData() == false)
            {
                MessageBox.Show("Please input send data!", "Error!");
                return;
            }
        }

        private void btnOpenCOM_Click(object sender, EventArgs e)
        {
            if (isOpen == false)
            {
                if (CheckPortSetting() == false)
                {
                    MessageBox.Show("Comm not setted!", "Error!");
                    return;
                }
                if (isSetProperty == false)
                {
                    SetPortProperty();
                    isSetProperty = true;
                }
                try
                {
                    if (comm01.IsOpen)
                    {
                        comm01.Close();
                    }
                    comm01.Open();
                    isOpen = true;
                    btnOpenCOM.Text = "Close";
                    //disable buttons and settings after comm open.
                    cbxCOMPort.Enabled = false;
                    cbxBaudRate.Enabled = false;
                    cbxDataBits.Enabled = false;
                    cbxParity.Enabled = false;
                    cbxStopBits.Enabled = false;
                    rbnChar.Enabled = false;
                    rbnHex.Enabled = false;
                }
                catch (Exception)
                {
                    isSetProperty = false;
                    isOpen = false;
                    MessageBox.Show("Comm used!", "Error!");
                }
            }
            else //opened.
            {
                try
                {
                    comm01.Close();
                    isOpen = false;
                    isSetProperty = false;
                    btnOpenCOM.Text = "Open";
                    //disable buttons and settings after comm open.
                    cbxCOMPort.Enabled = true;
                    cbxBaudRate.Enabled = true;
                    cbxDataBits.Enabled = true;
                    cbxParity.Enabled = true;
                    cbxStopBits.Enabled = true;
                    rbnChar.Enabled = true;
                    rbnHex.Enabled = true;

                }
                catch (Exception)
                {
                    MessageBox.Show("Close comm error!", "Error!");
                    //lblStatus.Text="Close comm error!";
                }
            }
        }

        private void comm01_DataReceived (object sender, SerialDataReceivedEventArgs e)
        {
            //System.Threading.Thread.Sleep(100); //delay 100ms.
            this.Invoke((EventHandler)(delegate
            {
                if (isHex == false)
                {
                    tbxRecvData.Text += comm01.ReadLine();
                }
                else
                {
                    Byte[] ReceivedData = new Byte[comm01.BytesToRead];
                    comm01.Read(ReceivedData, 0, ReceivedData.Length);
                    String RecvDataText = null;
                    for (int i = 0; i < (ReceivedData.Length - 1); i++)
                    {
                        RecvDataText += ("0x" + ReceivedData[i].ToString("X2") + " ");
                    }
                    tbxRecvData.Text += RecvDataText;
                }
            }));
        }

        private void btnCleanData_Click(object sender, EventArgs e)
        {
            tbxRecvData.Text = "";
            tbxSendData.Text = "";
            ClearCalculatedData();
        }

        #endregion //SerialPortEvents

        private void SerialPortComm_FormClosing(object sender, FormClosingEventArgs e)
        {
            comm01.Close();
        }
    }
}
