namespace SerialPortConnection
{
    partial class SerialPortTool
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxSample = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbxSampTime = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbParity = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbStop = new System.Windows.Forms.ComboBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cbDataBits = new System.Windows.Forms.ComboBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cbBaudRate = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.rbRcvStr = new System.Windows.Forms.RadioButton();
            this.rbRcv16 = new System.Windows.Forms.RadioButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.rdSendhex = new System.Windows.Forms.RadioButton();
            this.rdSendStr = new System.Windows.Forms.RadioButton();
            this.btnSwitch = new System.Windows.Forms.Button();
            this.cbSerial = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.txtSecond = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbTimeSend = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtReceive = new System.Windows.Forms.RichTextBox();
            this.timSend = new System.Windows.Forms.Timer(this.components);
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsSpNum = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsBaudRate = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsDataBits = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsStopBits = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsParity = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabSerialPort = new System.Windows.Forms.TabControl();
            this.tabMonitor = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tabDraw = new System.Windows.Forms.TabPage();
            this.btnDraw = new System.Windows.Forms.Button();
            this.picBoxDraw = new System.Windows.Forms.PictureBox();
            this.timSample = new System.Windows.Forms.Timer(this.components);
            this.tbxSampUnit = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabSerialPort.SuspendLayout();
            this.tabMonitor.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabDraw.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxDraw)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cbParity);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.cbStop);
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbDataBits);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbBaudRate);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.groupBox8);
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Controls.Add(this.btnSwitch);
            this.groupBox1.Controls.Add(this.cbSerial);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(193, 514);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Setting:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tbxSampUnit);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.tbxSample);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.tbxSampTime);
            this.groupBox5.Location = new System.Drawing.Point(12, 309);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(170, 93);
            this.groupBox5.TabIndex = 33;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "sample";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1, 28);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 17);
            this.label4.TabIndex = 17;
            this.label4.Text = "Sample:";
            // 
            // tbxSample
            // 
            this.tbxSample.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxSample.Location = new System.Drawing.Point(73, 24);
            this.tbxSample.Margin = new System.Windows.Forms.Padding(4);
            this.tbxSample.Name = "tbxSample";
            this.tbxSample.Size = new System.Drawing.Size(47, 25);
            this.tbxSample.TabIndex = 18;
            this.tbxSample.Text = "speed";
            this.tbxSample.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSecond_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(1, 62);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 17);
            this.label9.TabIndex = 17;
            this.label9.Text = "Time-ms";
            // 
            // tbxSampTime
            // 
            this.tbxSampTime.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxSampTime.Location = new System.Drawing.Point(73, 58);
            this.tbxSampTime.Margin = new System.Windows.Forms.Padding(4);
            this.tbxSampTime.Name = "tbxSampTime";
            this.tbxSampTime.Size = new System.Drawing.Size(95, 25);
            this.tbxSampTime.TabIndex = 18;
            this.tbxSampTime.Text = "1000";
            this.tbxSampTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSecond_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(13, 120);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 18);
            this.label8.TabIndex = 27;
            this.label8.Text = "Chck:";
            // 
            // cbParity
            // 
            this.cbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParity.FormattingEnabled = true;
            this.cbParity.Items.AddRange(new object[] {
            "NONE",
            "ODD",
            "EVEN"});
            this.cbParity.Location = new System.Drawing.Point(71, 119);
            this.cbParity.Margin = new System.Windows.Forms.Padding(4);
            this.cbParity.Name = "cbParity";
            this.cbParity.Size = new System.Drawing.Size(109, 24);
            this.cbParity.TabIndex = 29;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(93, 151);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 28);
            this.btnSave.TabIndex = 32;
            this.btnSave.Text = "SaveSet";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbStop
            // 
            this.cbStop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStop.FormattingEnabled = true;
            this.cbStop.Items.AddRange(new object[] {
            "1",
            "1.5",
            "2"});
            this.cbStop.Location = new System.Drawing.Point(71, 94);
            this.cbStop.Margin = new System.Windows.Forms.Padding(4);
            this.cbStop.Name = "cbStop";
            this.cbStop.Size = new System.Drawing.Size(109, 24);
            this.cbStop.TabIndex = 28;
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(16, 461);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(79, 38);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(13, 96);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 18);
            this.label7.TabIndex = 27;
            this.label7.Text = "Stop:";
            // 
            // cbDataBits
            // 
            this.cbDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataBits.FormattingEnabled = true;
            this.cbDataBits.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.cbDataBits.Location = new System.Drawing.Point(71, 69);
            this.cbDataBits.Margin = new System.Windows.Forms.Padding(4);
            this.cbDataBits.Name = "cbDataBits";
            this.cbDataBits.Size = new System.Drawing.Size(109, 24);
            this.cbDataBits.TabIndex = 26;
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(103, 461);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(79, 38);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(13, 72);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 18);
            this.label6.TabIndex = 25;
            this.label6.Text = "Data:";
            // 
            // cbBaudRate
            // 
            this.cbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBaudRate.FormattingEnabled = true;
            this.cbBaudRate.Items.AddRange(new object[] {
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "115200"});
            this.cbBaudRate.Location = new System.Drawing.Point(71, 44);
            this.cbBaudRate.Margin = new System.Windows.Forms.Padding(4);
            this.cbBaudRate.Name = "cbBaudRate";
            this.cbBaudRate.Size = new System.Drawing.Size(109, 24);
            this.cbBaudRate.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(13, 48);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 18);
            this.label5.TabIndex = 23;
            this.label5.Text = "Baud:";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.rbRcvStr);
            this.groupBox8.Controls.Add(this.rbRcv16);
            this.groupBox8.Location = new System.Drawing.Point(16, 197);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox8.Size = new System.Drawing.Size(164, 48);
            this.groupBox8.TabIndex = 15;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Recv Format";
            // 
            // rbRcvStr
            // 
            this.rbRcvStr.AutoSize = true;
            this.rbRcvStr.Location = new System.Drawing.Point(77, 19);
            this.rbRcvStr.Margin = new System.Windows.Forms.Padding(4);
            this.rbRcvStr.Name = "rbRcvStr";
            this.rbRcvStr.Size = new System.Drawing.Size(67, 21);
            this.rbRcvStr.TabIndex = 2;
            this.rbRcvStr.TabStop = true;
            this.rbRcvStr.Text = "CHAR";
            this.rbRcvStr.UseVisualStyleBackColor = true;
            // 
            // rbRcv16
            // 
            this.rbRcv16.AutoSize = true;
            this.rbRcv16.Location = new System.Drawing.Point(12, 19);
            this.rbRcv16.Margin = new System.Windows.Forms.Padding(4);
            this.rbRcv16.Name = "rbRcv16";
            this.rbRcv16.Size = new System.Drawing.Size(57, 21);
            this.rbRcv16.TabIndex = 1;
            this.rbRcv16.TabStop = true;
            this.rbRcv16.Text = "HEX";
            this.rbRcv16.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.rdSendhex);
            this.groupBox7.Controls.Add(this.rdSendStr);
            this.groupBox7.Location = new System.Drawing.Point(16, 253);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox7.Size = new System.Drawing.Size(164, 49);
            this.groupBox7.TabIndex = 14;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "SendFormat";
            // 
            // rdSendhex
            // 
            this.rdSendhex.AutoSize = true;
            this.rdSendhex.Location = new System.Drawing.Point(8, 20);
            this.rdSendhex.Margin = new System.Windows.Forms.Padding(4);
            this.rdSendhex.Name = "rdSendhex";
            this.rdSendhex.Size = new System.Drawing.Size(57, 21);
            this.rdSendhex.TabIndex = 7;
            this.rdSendhex.TabStop = true;
            this.rdSendhex.Text = "HEX";
            this.rdSendhex.UseVisualStyleBackColor = true;
            // 
            // rdSendStr
            // 
            this.rdSendStr.AutoSize = true;
            this.rdSendStr.Location = new System.Drawing.Point(77, 20);
            this.rdSendStr.Margin = new System.Windows.Forms.Padding(4);
            this.rdSendStr.Name = "rdSendStr";
            this.rdSendStr.Size = new System.Drawing.Size(67, 21);
            this.rdSendStr.TabIndex = 6;
            this.rdSendStr.TabStop = true;
            this.rdSendStr.Text = "CHAR";
            this.rdSendStr.UseVisualStyleBackColor = true;
            // 
            // btnSwitch
            // 
            this.btnSwitch.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSwitch.Location = new System.Drawing.Point(16, 151);
            this.btnSwitch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(69, 28);
            this.btnSwitch.TabIndex = 9;
            this.btnSwitch.Text = "Open";
            this.btnSwitch.UseVisualStyleBackColor = true;
            this.btnSwitch.Click += new System.EventHandler(this.btnOpnClsPort_Click);
            // 
            // cbSerial
            // 
            this.cbSerial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSerial.FormattingEnabled = true;
            this.cbSerial.Location = new System.Drawing.Point(71, 19);
            this.cbSerial.Margin = new System.Windows.Forms.Padding(4);
            this.cbSerial.Name = "cbSerial";
            this.cbSerial.Size = new System.Drawing.Size(109, 24);
            this.cbSerial.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(13, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "Port:";
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(628, 14);
            this.btnSend.Margin = new System.Windows.Forms.Padding(4);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(80, 33);
            this.btnSend.TabIndex = 22;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(0, 19);
            this.txtSend.Margin = new System.Windows.Forms.Padding(4);
            this.txtSend.Multiline = true;
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(719, 64);
            this.txtSend.TabIndex = 21;
            this.txtSend.Text = "When use HEX format, separated by space or comma.";
            this.txtSend.TextChanged += new System.EventHandler(this.txtSend_TextChanged);
            this.txtSend.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSend_KeyPress);
            // 
            // txtSecond
            // 
            this.txtSecond.Location = new System.Drawing.Point(220, 21);
            this.txtSecond.Margin = new System.Windows.Forms.Padding(4);
            this.txtSecond.Name = "txtSecond";
            this.txtSecond.Size = new System.Drawing.Size(57, 22);
            this.txtSecond.TabIndex = 18;
            this.txtSecond.Text = "1000";
            this.txtSecond.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSecond_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "Interval(/s):";
            // 
            // cbTimeSend
            // 
            this.cbTimeSend.AutoSize = true;
            this.cbTimeSend.Location = new System.Drawing.Point(8, 22);
            this.cbTimeSend.Margin = new System.Windows.Forms.Padding(4);
            this.cbTimeSend.Name = "cbTimeSend";
            this.cbTimeSend.Size = new System.Drawing.Size(108, 21);
            this.cbTimeSend.TabIndex = 16;
            this.cbTimeSend.Text = "Send Period";
            this.cbTimeSend.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtReceive);
            this.groupBox2.Location = new System.Drawing.Point(7, 7);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(726, 315);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DataReceive";
            // 
            // txtReceive
            // 
            this.txtReceive.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtReceive.Location = new System.Drawing.Point(8, 24);
            this.txtReceive.Margin = new System.Windows.Forms.Padding(4);
            this.txtReceive.Name = "txtReceive";
            this.txtReceive.ReadOnly = true;
            this.txtReceive.Size = new System.Drawing.Size(710, 283);
            this.txtReceive.TabIndex = 0;
            this.txtReceive.Text = "";
            // 
            // timSend
            // 
            this.timSend.Tick += new System.EventHandler(this.tmSend_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsSpNum,
            this.tsBaudRate,
            this.tsDataBits,
            this.tsStopBits,
            this.tsParity});
            this.statusStrip1.Location = new System.Drawing.Point(0, 546);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(999, 25);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsSpNum
            // 
            this.tsSpNum.Name = "tsSpNum";
            this.tsSpNum.Size = new System.Drawing.Size(99, 20);
            this.tsSpNum.Text = "Port: Not Def|";
            // 
            // tsBaudRate
            // 
            this.tsBaudRate.Name = "tsBaudRate";
            this.tsBaudRate.Size = new System.Drawing.Size(137, 20);
            this.tsBaudRate.Text = "BaudRate: Not Def|";
            // 
            // tsDataBits
            // 
            this.tsDataBits.Name = "tsDataBits";
            this.tsDataBits.Size = new System.Drawing.Size(129, 20);
            this.tsDataBits.Text = "DataBits: Not Def|";
            // 
            // tsStopBits
            // 
            this.tsStopBits.Name = "tsStopBits";
            this.tsStopBits.Size = new System.Drawing.Size(91, 20);
            this.tsStopBits.Text = "Bit: Not Def|";
            // 
            // tsParity
            // 
            this.tsParity.Name = "tsParity";
            this.tsParity.Size = new System.Drawing.Size(130, 20);
            this.tsParity.Text = "CheckBit: Not Def|";
            // 
            // tabSerialPort
            // 
            this.tabSerialPort.Controls.Add(this.tabMonitor);
            this.tabSerialPort.Controls.Add(this.tabDraw);
            this.tabSerialPort.Location = new System.Drawing.Point(213, 12);
            this.tabSerialPort.Name = "tabSerialPort";
            this.tabSerialPort.SelectedIndex = 0;
            this.tabSerialPort.Size = new System.Drawing.Size(759, 513);
            this.tabSerialPort.TabIndex = 1;
            // 
            // tabMonitor
            // 
            this.tabMonitor.Controls.Add(this.groupBox3);
            this.tabMonitor.Controls.Add(this.groupBox2);
            this.tabMonitor.Location = new System.Drawing.Point(4, 25);
            this.tabMonitor.Name = "tabMonitor";
            this.tabMonitor.Padding = new System.Windows.Forms.Padding(3);
            this.tabMonitor.Size = new System.Drawing.Size(751, 484);
            this.tabMonitor.TabIndex = 0;
            this.tabMonitor.Text = "Monitor";
            this.tabMonitor.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.txtSend);
            this.groupBox3.Location = new System.Drawing.Point(7, 327);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(726, 154);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "DataSend";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbTimeSend);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.btnSend);
            this.groupBox4.Controls.Add(this.txtSecond);
            this.groupBox4.Location = new System.Drawing.Point(0, 90);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(718, 51);
            this.groupBox4.TabIndex = 23;
            this.groupBox4.TabStop = false;
            // 
            // tabDraw
            // 
            this.tabDraw.Controls.Add(this.btnDraw);
            this.tabDraw.Controls.Add(this.picBoxDraw);
            this.tabDraw.Location = new System.Drawing.Point(4, 25);
            this.tabDraw.Name = "tabDraw";
            this.tabDraw.Padding = new System.Windows.Forms.Padding(3);
            this.tabDraw.Size = new System.Drawing.Size(751, 484);
            this.tabDraw.TabIndex = 1;
            this.tabDraw.Text = "Draw";
            this.tabDraw.UseVisualStyleBackColor = true;
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(658, 444);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(85, 33);
            this.btnDraw.TabIndex = 1;
            this.btnDraw.Text = "Draw";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // picBoxDraw
            // 
            this.picBoxDraw.Location = new System.Drawing.Point(3, 3);
            this.picBoxDraw.Name = "picBoxDraw";
            this.picBoxDraw.Size = new System.Drawing.Size(742, 434);
            this.picBoxDraw.TabIndex = 0;
            this.picBoxDraw.TabStop = false;
            // 
            // timSample
            // 
            this.timSample.Tick += new System.EventHandler(this.timSample_Tick);
            // 
            // tbxSampUnit
            // 
            this.tbxSampUnit.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxSampUnit.Location = new System.Drawing.Point(121, 24);
            this.tbxSampUnit.Margin = new System.Windows.Forms.Padding(4);
            this.tbxSampUnit.Name = "tbxSampUnit";
            this.tbxSampUnit.Size = new System.Drawing.Size(47, 25);
            this.tbxSampUnit.TabIndex = 19;
            this.tbxSampUnit.Text = "mrad/s";
            // 
            // SerialPortTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(999, 571);
            this.Controls.Add(this.tabSerialPort);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SerialPortTool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SerialPortTool";
            this.Load += new System.EventHandler(this.SerialPortForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SerialPort_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabSerialPort.ResumeLayout(false);
            this.tabMonitor.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabDraw.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxDraw)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbSerial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.RadioButton rbRcvStr;
        private System.Windows.Forms.RadioButton rbRcv16;
        private System.Windows.Forms.RadioButton rdSendStr;
        //private System.Windows.Forms.RadioButton rdse;
        private System.Windows.Forms.TextBox txtSecond;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbTimeSend;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.RadioButton rdSendhex;
        private System.Windows.Forms.RichTextBox txtReceive;
        private System.Windows.Forms.Timer timSend;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnExit;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsSpNum;
        private System.Windows.Forms.ToolStripStatusLabel tsBaudRate;
        private System.Windows.Forms.ToolStripStatusLabel tsDataBits;
        private System.Windows.Forms.ToolStripStatusLabel tsStopBits;
        private System.Windows.Forms.ToolStripStatusLabel tsParity;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TabControl tabSerialPort;
        private System.Windows.Forms.TabPage tabMonitor;
        private System.Windows.Forms.TabPage tabDraw;
        private System.Windows.Forms.ComboBox cbParity;
        private System.Windows.Forms.ComboBox cbStop;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbDataBits;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbBaudRate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxSample;
        private System.Windows.Forms.Timer timSample;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbxSampTime;
        private System.Windows.Forms.PictureBox picBoxDraw;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.TextBox tbxSampUnit;
    }
}

