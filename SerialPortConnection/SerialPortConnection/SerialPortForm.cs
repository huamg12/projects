using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using INIFILE;
using System.Text.RegularExpressions;
using System.Threading;

namespace SerialPortConnection
{
    public partial class SerialPortTool : Form
    {
        //initialize.
        #region WindowFormInitialize

        public SerialPortTool()
        {
            InitializeComponent();
            InitializeParameter();
            InitializeDraw();
        }

        #endregion WindowFormInitialize

        //defines.
        #region TimersDefine

        System.Threading.Timer thTimSample;
        int timeTick = 0;
        int timeTickPre = 0;

        #endregion TimersDefine

        #region SerialPortMembers

        SerialPort comPort = new SerialPort();
        //comPort.ReceivedBytesThreshold = 1;//只要有1个字符送达端口时便触发DataReceived事件 
        //private System.Timers.Timer timDataSample = new System.Timers.Timer(1000);

        string serialDataLineText = "0";
        string serialDataBuffText = "Sample special variable in serial data buff.";
        int serialDataBuffDeep = 5;
        int serialDataBuffIndex = 0;
        int serialSampleIndex = 0;
        
        #endregion SerialPortMembers

        #region DrawPictureMembers

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
        //double mr = 0;                            //modulation radio.
        //double pi = 0;
        //object
        Bitmap bmp;
        Graphics grp;
        Panel panelPic = new Panel();
        StringFormat drawFormat = new StringFormat();

        #endregion DrawPictureMembers

        #region KalmanFilterDefine

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
        kalmanType kal = new kalmanType();
        Random rQ = new Random();
        Random rR = new Random();

        #endregion KalmanFilterDefine

        //program.
        #region TimersTickProgram
        private void TimerTick(object o)
        {
            timeTick++;
            if (timeTick > 100)
            {
                timeTick = 0;
            }
        }
        #endregion TimersTickProgram

        #region SerialPortProgram

        //configurations.
        private void SerialPortForm_Load(object sender, EventArgs e)
        {
            INIFILE.Profile.LoadProfile();// load file.
            //baudrates.
            ReadBaudRateConfig(Profile.G_BAUDRATE);
            //data bits.
            ReadDataBitsConfig(Profile.G_DATABITS);
            //stop bits.
            ReadStopBitsConfig(Profile.G_STOP);
            //parity bits.
            ReadParityBitsConfig(Profile.G_PARITY);
            //check ports.
            string[] str = SerialPort.GetPortNames();
            if (str == null)
            {
                MessageBox.Show("No comm!", "Error");
                return;
            }
            //add ports.
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
            {
                //System.Diagnostics.Debug.WriteLine(s);
                cbSerial.Items.Add(s);
            }

            //port setting.
            cbSerial.SelectedIndex = 0; //if no port, it will wrong.
            //comPort.BaudRate = 9600;

            Control.CheckForIllegalCrossThreadCalls = false;
            //这个类中我们不检查跨线程的调用是否合法(因为.net 2.0以后加强了安全机制，
            //不允许在winform中直接跨线程访问控件的属性)
            comPort.DataReceived += new SerialDataReceivedEventHandler(commPort_DataReceived);
            //comPort.ReceivedBytesThreshold = 1;

            //rdSendhex.Checked = true;
            rdSendStr.Checked = true;
            rbRcvStr.Checked = true;

            //port ready.             
            comPort.DtrEnable = true;
            comPort.RtsEnable = true;
            //read timeout 1000ms.
            comPort.ReadTimeout = -1;

            comPort.Close();
        }
        private void ReadBaudRateConfig(string strBaudRate)
        {
            switch ( strBaudRate )
            {
                case "4800":
                    cbBaudRate.SelectedIndex = 4;
                    break;
                case "9600":
                    cbBaudRate.SelectedIndex = 5;
                    break;
                case "19200":
                    cbBaudRate.SelectedIndex = 6;
                    break;
                case "38400":
                    cbBaudRate.SelectedIndex = 7;
                    break;
                case "115200":
                    cbBaudRate.SelectedIndex = 8;
                    break;
                default:
                    {
                        MessageBox.Show("BaudRate Error!");
                        return;
                    }
            }
        }
        private void ReadDataBitsConfig(string strDataBits)
        {
            switch (strDataBits)
            {
                case "5":
                    cbDataBits.SelectedIndex = 0;
                    break;
                case "6":
                    cbDataBits.SelectedIndex = 1;
                    break;
                case "7":
                    cbDataBits.SelectedIndex = 2;
                    break;
                case "8":
                    cbDataBits.SelectedIndex = 3;
                    break;
                default:
                    {
                        MessageBox.Show("Databits legth Error!");
                        return;
                    }
            }
        }
        private void ReadStopBitsConfig(string strStopBits)
        {
            switch (strStopBits)
            {
                case "1":
                    cbStop.SelectedIndex = 0;
                    break;
                case "1.5":
                    cbStop.SelectedIndex = 1;
                    break;
                case "2":
                    cbStop.SelectedIndex = 2;
                    break;
                default:
                    {
                        MessageBox.Show("Stop bit Error!");
                        return;
                    }
            }
        }
        private void ReadParityBitsConfig(string strParityBits)
        {
            switch (strParityBits)
            {
                case "NONE":
                    cbParity.SelectedIndex = 0;
                    break;
                case "ODD":
                    cbParity.SelectedIndex = 1;
                    break;
                case "EVEN":
                    cbParity.SelectedIndex = 2;
                    break;
                default:
                    {
                        MessageBox.Show("Check bits Error!");
                        return;
                    }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string strBaudRate = cbBaudRate.Text;
            string strDateBits = cbDataBits.Text;
            string strStopBits = cbStop.Text;
            Int32 iBaudRate = Convert.ToInt32(strBaudRate);
            Int32 iDateBits = Convert.ToInt32(strDateBits);

            Profile.G_BAUDRATE = iBaudRate + "";
            Profile.G_DATABITS = iDateBits + "";
            switch (cbStop.Text)
            {
                case "1":
                    Profile.G_STOP = "1";
                    break;
                case "1.5":
                    Profile.G_STOP = "1.5";
                    break;
                case "2":
                    Profile.G_STOP = "2";
                    break;
                default:
                    MessageBox.Show("Error stop bit save.", "Error");
                    break;
            }
            switch (cbParity.Text)
            {
                case "NONE":
                    Profile.G_PARITY = "NONE";
                    break;
                case "ODD":
                    Profile.G_PARITY = "ODD";
                    break;
                case "EVEN":
                    Profile.G_PARITY = "EVEN";
                    break;
                default:
                    MessageBox.Show("Error parity bit save.", "Error!");
                    break;
            }

            //保存设置
            // public static string G_BAUDRATE = "1200";//给ini文件赋新值，并且影响界面下拉框的显示
            //public static string G_DATABITS = "8";
            //public static string G_STOP = "1";
            //public static string G_PARITY = "NONE";
            Profile.SaveProfile();
        }

        //receive.
        private void btnOpenCloseCommPort_Click(object sender, EventArgs e)
        {
            serialSampleIndex = 0;
            //serialPort1.IsOpen
            if (!comPort.IsOpen)
            {
                try
                {
                    //set port number.
                    string serialName = cbSerial.SelectedItem.ToString();
                    comPort.PortName = serialName;

                    //port settings:
                    string strBaudRate = cbBaudRate.Text;
                    string strDateBits = cbDataBits.Text;
                    string strStopBits = cbStop.Text;
                    Int32 iBaudRate = Convert.ToInt32(strBaudRate);
                    Int32 iDateBits = Convert.ToInt32(strDateBits);

                    comPort.BaudRate = iBaudRate;
                    comPort.DataBits = iDateBits;
                    switch (cbStop.Text)
                    {
                        case "1":
                            comPort.StopBits = StopBits.One;
                            break;
                        case "1.5":
                            comPort.StopBits = StopBits.OnePointFive;
                            break;
                        case "2":
                            comPort.StopBits = StopBits.Two;
                            break;
                        default:
                            MessageBox.Show("Error StopBits.", "Error");
                            break;
                    }
                    switch (cbParity.Text)
                    {
                        case "NONE":
                            comPort.Parity = Parity.None;
                            break;
                        case "ODD":
                            comPort.Parity = Parity.Odd;
                            break;
                        case "EVEN":
                            comPort.Parity = Parity.Even;
                            break;
                        default:
                            MessageBox.Show("Error Parity!", "Error");
                            break;
                    }

                    if (comPort.IsOpen == true)//如果打开状态，则先关闭一下
                    {
                        comPort.Close();
                    }
                    //状态栏设置
                    tsSpNum.Text = "Port:" + comPort.PortName + "|";
                    tsBaudRate.Text = "Baud:" + comPort.BaudRate + "|";
                    tsDataBits.Text = "DataBit:" + comPort.DataBits + "|";
                    tsStopBits.Text = "StopBit:" + comPort.StopBits + "|";
                    tsParity.Text = "ChckBit:" + comPort.Parity + "|";

                    //设置必要控件不可用
                    cbSerial.Enabled = false;
                    cbBaudRate.Enabled = false;
                    cbDataBits.Enabled = false;
                    cbStop.Enabled = false;
                    cbParity.Enabled = false;

                    comPort.Open();     //打开串口
                    btnSwitch.Text = "Close";
                    //timSend.Enabled = true;
                    int iMsecond = int.Parse(txtSecond.Text);
                    thTimSample = new System.Threading.Timer(TimerTick, null, 20, iMsecond);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message, "Error");
                    timSend.Enabled = false;
                    return;
                }
            }
            else
            {
                //状态栏设置
                tsSpNum.Text = "Port: Not Def|";
                tsBaudRate.Text = "Baud: Not Def|";
                tsDataBits.Text = "DataBit: Not Def|";
                tsStopBits.Text = "StopBit: Not Def|";
                tsParity.Text = "ChckBit: Not Def|";
                //恢复控件功能
                cbSerial.Enabled = true;
                cbBaudRate.Enabled = true;
                cbDataBits.Enabled = true;
                cbStop.Enabled = true;
                cbParity.Enabled = true;

                comPort.Close();                 //关闭串口
                btnSwitch.Text = "Open";
                timSend.Enabled = false;         //关闭计时器
            }
        }

        void commPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (timeTickPre != timeTick)
            {
                if (timeTick == 10)
                {
                    btnDrawPic.PerformClick();
                }
                timeTickPre = timeTick;
            }
            else
            {
                return;
            }
            if (comPort.IsOpen)
            {
                //time. //look at comments, not code self!
                //DateTime dt = DateTime.Now;
                //txtReceive.Text += dt.GetDateTimeFormats('f')[0].ToString() + "\r\n";
                txtReceive.SelectAll();
                txtReceive.SelectionColor = Color.Blue;             //font color.

                byte[] byteRead = new byte[comPort.BytesToRead];    //BytesToRead bytes numbers.
                if (rbRcvStr.Checked)
                {
                    //txtReceive.Text += comPort.ReadLine() + "\r\n"; //注意：回车换行必须这样写，单独使用"\r"和"\n"都不会有效果
                    string newMsg = comPort.ReadLine() + "\r\n";
                    //string newMsg = comPort.ReadLine();
                    serialDataLineText = comPort.ReadLine();
                    txtReceive.AppendText(newMsg);
                    txtReceive.ScrollToCaret();
                    //comPort.DiscardInBuffer();                    //清空SerialPort控件的Buffer 
                    if (serialDataBuffIndex < serialDataBuffDeep)
                    {
                        serialDataBuffText += newMsg;
                        serialDataBuffIndex++;
                    }
                    else
                    {
                        serialDataBuffText = newMsg; //restart.
                        serialDataBuffIndex = 0;
                    }
                    vAlterLineToValue();
                }
                else // HEX.
                {
                    try
                    {
                        Byte[] receivedData = new Byte[comPort.BytesToRead];        //创建接收字节数组
                        comPort.Read(receivedData, 0, receivedData.Length);         //读取数据
                        //string text = comPort.Read();   //Encoding.ASCII.GetString(receivedData);
                        comPort.DiscardInBuffer();                                  //清空SerialPort控件的Buffer
                        //这是用以显示字符串
                        //    string strRcv = null;
                        //    for (int i = 0; i < receivedData.Length; i++ )
                        //    {
                        //        strRcv += ((char)Convert.ToInt32(receivedData[i])) ;
                        //    }
                        //    txtReceive.Text += strRcv + "\r\n";             //显示信息
                        //}
                        string strRcv = null;
                        //int decNum = 0;//存储十进制
                        for (int i = 0; i < receivedData.Length; i++) //窗体显示
                        {
                            strRcv += receivedData[i].ToString("X2");  //16进制显示
                        }
                        txtReceive.Text += strRcv + "\r\n";
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error!");
                        txtSend.Text = "";
                    }
                }
            }
            else
            {
                MessageBox.Show("Please open a port.", "Error!");
            }
        }

        //sending.
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (cbTimeSend.Checked)
            {
                timSend.Enabled = true;
            }
            else
            {
                timSend.Enabled = false;
            }

            if (!comPort.IsOpen) //如果没打开
            {
                MessageBox.Show("Please open port first！", "Error");
                return;
            }

            String strSend = txtSend.Text;
            if (rdSendhex.Checked == true)	//“HEX发送” 按钮 
            {
                //处理数字转换
                string sendBuf = strSend;
                string sendnoNull = sendBuf.Trim();
                string sendNOComma = sendnoNull.Replace(',', ' ');    //去掉英文逗号
                string sendNOComma1 = sendNOComma.Replace('，', ' '); //去掉中文逗号
                string strSendNoComma2 = sendNOComma1.Replace("0x", "");   //去掉0x
                strSendNoComma2.Replace("0X", "");   //去掉0X
                string[] strArray = strSendNoComma2.Split(' ');

                int byteBufferLength = strArray.Length;
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (strArray[i] == "")
                    {
                        byteBufferLength--;
                    }
                }
                // int temp = 0;
                byte[] byteBuffer = new byte[byteBufferLength];
                int ii = 0;
                for (int i = 0; i < strArray.Length; i++)        //对获取的字符做相加运算
                {

                    Byte[] bytesOfStr = Encoding.Default.GetBytes(strArray[i]);

                    int decNum = 0;
                    if (strArray[i] == "")
                    {
                        //ii--;     //加上此句是错误的，下面的continue以延缓了一个ii，不与i同步
                        continue;
                    }
                    else
                    {
                        decNum = Convert.ToInt32(strArray[i], 16); //atrArray[i] == 12时，temp == 18 
                    }

                    try    //防止输错，使其只能输入一个字节的字符
                    {
                        byteBuffer[ii] = Convert.ToByte(decNum);
                    }
                    catch (System.Exception)
                    {
                        MessageBox.Show("Over Load!", "Error!");
                        timSend.Enabled = false;
                        return;
                    }

                    ii++;
                }
                comPort.Write(byteBuffer, 0, byteBuffer.Length);
            }
            else //以字符串形式发送时 
            {
                comPort.WriteLine(txtSend.Text);    //写入数据
            }
        }

        private void txtSend_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (rdSendhex.Checked == true)
            {
                //正则匹配
                string patten = "[0-9a-fA-F]|\b|0x|0X| "; //“\b”：退格键
                Regex r = new Regex(patten);
                Match m = r.Match(e.KeyChar.ToString());

                if (m.Success )//&&(txtSend.Text.LastIndexOf(" ") != txtSend.Text.Length-1))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }//end of rdSendhex
            else
            {
                e.Handled = false;
            }
        }

        private void txtSend_TextChanged(object sender, EventArgs e)
        {

        }

        //sample.
        private void tmSend_Tick(object sender, EventArgs e)
        {
            //转换时间间隔
            string strSecond = txtSecond.Text;
            try
            {
                int iMsecond = int.Parse(strSecond);//Interval unit is ms.
                timSend.Interval = iMsecond;
                if (timSend.Enabled == true)
                {
                    if (cbTimeSend.Checked)
                    {
                        btnSend.PerformClick();
                    }
                    else //not checked, used for sample data.
                    {
                        //vFindWordInString();
                        //vAlterLineToValue();
                    }
                }
            }
            catch (System.Exception)
            {
                timSend.Enabled = false;
                MessageBox.Show("Error time input.", "Error!");
            }
        }

        private void txtSecond_KeyPress(object sender, KeyPressEventArgs e)
        {
            string patten = "[0-9]|\b"; //“\b”：退格键
            Regex r = new Regex(patten);
            Match m = r.Match(e.KeyChar.ToString());

            if (m.Success)
            {
                e.Handled = false;   //没操作“过”，系统会处理事件    
            }
            else
            {
                e.Handled = true;
            }
        }

        private void timSample_Tick(object sender, EventArgs e)
        {
            //string strSampTime = tbxSampTime.Text;
            //try
            //{
            //    int mSecond = int.Parse(strSampTime) * 1000;//*1000ms.
            //    timSample.Interval = mSecond;
            //    if (timSample.Enabled == true)
            //    {
            //        //btnSend.PerformClick();
            //        vFindWordInString();
            //    }
            //}
            //catch (System.Exception)
            //{
            //    timSample.Enabled = false;
            //    MessageBox.Show("Error sample time.", "Error!");
            //}
        }

        private void vFindWordInString()
        {
            int index = serialDataBuffText.IndexOf(tbxSample.Text);
            int lenSam = tbxSample.Text.Length;
            int idend = serialDataBuffText.IndexOf(tbxSampUnit.Text);
            int lenUnt = tbxSampUnit.Text.Length;
            string sampleValue = "0";
            if ((idend > serialDataBuffDeep) && (idend > index))
            {
                sampleValue = serialDataBuffText.Substring(index + lenSam + 1, idend - index - lenSam -2); //space.
                if (serialSampleIndex == 0)
                {
                    ClearCalculatedData();
                }
                if (serialSampleIndex < CIRCLE)
                {
                    DC_U[serialSampleIndex] = Convert.ToDouble(sampleValue) + Y_LEN / 2;
                    serialSampleIndex++;
                }
                if (serialSampleIndex >= CIRCLE - 1)
                {
                    serialSampleIndex = 0;
                }
            }
        }

        private void vAlterLineToValue()
        {
            if (serialSampleIndex == 0)
            {
                ClearCalculatedData();
            }
            if (serialSampleIndex < X_LEN)
            {
                DC_U[serialSampleIndex] = Convert.ToDouble(serialDataLineText) + Y_LEN / 2;
                serialSampleIndex++;
            }
            if (serialSampleIndex >= X_LEN - 1)
            {
                serialSampleIndex = 0;
            }
        }
        
        //clear.
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtReceive.Text = "";       //清空文本
            ClearCalculatedData();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //closing.
        private void SerialPort_FormClosing(object sender, FormClosingEventArgs e)
        {
            INIFILE.Profile.SaveProfile();
            comPort.Close();
            //thTimSample.Dispose();
        }

        #endregion SerialPortProgram

        #region DrawPictureProgram

        #region InitPictureParamters
        private void InitializeParameter()
        {
            //pi = Math.PI;

            Y_LEN = picBoxDraw.Height;
            X_LEN = picBoxDraw.Width;
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
            picBoxDraw.Image = bmp;
        }
        #endregion //InitializeDraw

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
                string yLabel = ((int)(grad * i - LenY/2)).ToString();
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
            for (int n = 0; n <= lines; n++)
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
            picBoxDraw.Image = bmp;
        }
        #endregion //DrawGradLines

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
            picBoxDraw.Image = bmp;
        }
        #endregion // DrawDataLines

        #region ClearData
        private void ClearCalculatedData()
        {
            serialSampleIndex = 0;
            for (int i = 0; i < X_LEN; i++)
            {
                DC_U[i] = Y_LEN / 2;
                DC_V[i] = 0;
                DC_W[i] = 0;
                DC_COM[i] = 0;
            }
            InitializeDraw();
        }
        #endregion //ClearData

        private void btnDraw_Click(object sender, EventArgs e)
        {
            ClearCalculatedData();
            TestKalmanFilter(DC_U, DC_V, DC_W);
            DrawPictureDataLines(grp);
        }

        private void btnDrawPic_Click(object sender, EventArgs e)
        {
            InitializeDraw();
            DrawPictureDataLines(grp);
        }

        #endregion DrawPictureProgram

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

            double zk = Y_LEN / 2;
            int range = X_LEN;
            for (int x = 0; x < range; x++)
            {
                //if (x < (range / 2))
                {
                    kal.Q = 2 * (rQ.NextDouble());
                    kal.R = 2 * (rR.NextDouble());
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
                kmP[x] = (kal.P) * 10;
                //kmK[x] = (kal.K)*100;
                kmK[x] = (zk - 50);
            }
        }
        #endregion //KalmanFilterAlgorithm
    }
}
