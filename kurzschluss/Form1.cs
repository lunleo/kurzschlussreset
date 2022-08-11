using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Ediabas;
using Microsoft.VisualBasic;

namespace kurzschluss
{
	public class Form1 : Form
	{
		private string ediabasver = "";

		private string ediabasinterface = "";

		private string SessionName = "test";

		private string SessionReportPath;

		private string sgbm_id1;

		private string sgbm_id2;

		public string resultVin;

		private bool Status;

		private char[] CharBuffer;

		private string TextBuffer;

		private string psdzPath;

		private string sgbm_identifier1;

		private string sgbm_identifier2;

		private string sgbmInfo;

		private string engineInfo;

		private string carInfo;

		private IContainer components;

		private Button connect_button;

		private TextBox textBox1;

		private Label label1;

		private Button kurzschluss_button;

		private Button disconnect_button;

		private Label label3;

		private TextBox textBox3;

		private Label label2;

		private Button button1;

		private Button button2;

		public Form1()
		{
			InitializeComponent();
			kurzschluss_button.Enabled = false;
			button1.Enabled = false;
			button2.Enabled = false;
			disconnect_button.Enabled = false;
		}

		private void connect_button_Click(object sender, EventArgs e)
		{
			Connect();
		}

		private void kurzschluss_Click(object sender, EventArgs e)
		{
			kurzschluss_60();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			kurzschluss_87();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			kurzschluss_70();
		}

		private void disconnect_button_Click(object sender, EventArgs e)
		{
			Disconnect();
		}

		private void textBox6_TextChanged(object sender, EventArgs e)
		{
		}

		private void textBox3_TextChanged(object sender, EventArgs e)
		{
		}

		private void Connect()
		{
			try
			{
                if (API.apiInit())
                {
                    if (API.apiGetConfig("EdiabasVersion", out ediabasver) && API.apiGetConfig("Interface", out ediabasinterface))
                    {
                        textBox1.Text = "Ediabas V" + ediabasver + ",  Interface: " + ediabasinterface;
                        detect_vin();
                        if (resultVin != null && resultVin.Length > 7)
                        {
                            Strings.Right(resultVin, 7);
                        }
                        kurzschluss_button.Enabled = true;
                        disconnect_button.Enabled = true;
                        button1.Enabled = true;
                        button2.Enabled = true;
                        connect_button.Enabled = false;
                    }
                }
                else
                {
                    textBox1.Text = "Check Interface, Ediabas connection failed";
                    API.apiEnd();
                }
            }
			catch
			{
				textBox1.Text = "Check Interface, Ediabas connection failed";
			}
		}

		private void Disconnect()
		{
			API.apiEnd();
			kurzschluss_button.Enabled = false;
			disconnect_button.Enabled = false;
			textBox1.Text = "Ediabas Disconnected";
			connect_button.Enabled = true;
		}

		private void detect_vin()
		{
			try
			{
				API.apiInit();
				API.apiJob("CAS", "c_fg_lesen", "", "fg_nr");
				Status = API.apiResultText(out CharBuffer, "FG_NR", (ushort)1, "");
				if (Status)
				{
					TextBuffer = new string(CharBuffer);
					string textBuffer = TextBuffer;
					char[] trimChars = new char[1];
					TextBuffer = textBuffer.Trim(trimChars);
					resultVin = TextBuffer;
					textBox3.Text = resultVin;
				}
				else
				{
					textBox3.Text = "Vin not detected";
				}
			}
			catch
			{
				textBox3.Text = "Vin not detected";
			}
			API.apiEnd();
		}

		private void kurzschluss_60()
		{
			try
			{
				API.apiInit();
				int num = 1;
				do
				{
					if (num == 32)
					{
						num = 255;
					}
					string para = Convert.ToString(num);
					API.apiJob("LM_60", "reset_kurzschluss_sperre", para, "");
					if (num != 255)
					{
						num++;
					}
				}
				while (num != 255);
			}
			catch
			{
				textBox1.Text = "Check Interface, ECU LM60 connection failed";
			}
			API.apiEnd();
		}

		private void kurzschluss_70()
		{
			try
			{
				API.apiInit();
				int num = 1;
				do
				{
					if (num == 34)
					{
						num = 255;
					}
					string para = Convert.ToString(num);
					API.apiJob("FRM_70", "steuern_reset_kurzschlussabschaltung", para, "");
					if (num != 255)
					{
						num++;
					}
				}
				while (num != 255);
			}
			catch
			{
				textBox1.Text = "Check Interface, ECU FRM70 ECU connection failed";
			}
			API.apiEnd();
		}

		private void kurzschluss_87()
		{
			try
			{
				API.apiInit();
				int num = 1;
				do
				{
					if (num == 32)
					{
						num = 255;
					}
					string para = Convert.ToString(num);
					API.apiJob("FRM_87", "_reset_kurzschluss_sperre", para, "");
					if (num != 255)
					{
						num++;
					}
				}
				while (num != 255);
			}
			catch
			{
				textBox1.Text = "Check Interface, ECU FRM87 connection failed";
			}
			API.apiEnd();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(kurzschluss.Form1));
			this.connect_button = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.kurzschluss_button = new System.Windows.Forms.Button();
			this.disconnect_button = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			base.SuspendLayout();
			this.connect_button.Location = new System.Drawing.Point(40, 40);
			this.connect_button.Name = "connect_button";
			this.connect_button.Size = new System.Drawing.Size(115, 23);
			this.connect_button.TabIndex = 0;
			this.connect_button.Text = "Connect";
			this.connect_button.UseVisualStyleBackColor = true;
			this.connect_button.Click += new System.EventHandler(connect_button_Click);
			this.textBox1.Location = new System.Drawing.Point(191, 81);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(345, 20);
			this.textBox1.TabIndex = 1;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(188, 65);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Ediabas parameters";
			this.kurzschluss_button.Location = new System.Drawing.Point(40, 119);
			this.kurzschluss_button.Name = "kurzschluss_button";
			this.kurzschluss_button.Size = new System.Drawing.Size(150, 23);
			this.kurzschluss_button.TabIndex = 3;
			this.kurzschluss_button.Text = "Reset kurzschluss LM60";
			this.kurzschluss_button.UseVisualStyleBackColor = true;
			this.kurzschluss_button.Click += new System.EventHandler(kurzschluss_Click);
			this.disconnect_button.Location = new System.Drawing.Point(39, 81);
			this.disconnect_button.Name = "disconnect_button";
			this.disconnect_button.Size = new System.Drawing.Size(115, 23);
			this.disconnect_button.TabIndex = 6;
			this.disconnect_button.Text = "Disconnect";
			this.disconnect_button.UseVisualStyleBackColor = true;
			this.disconnect_button.Click += new System.EventHandler(disconnect_button_Click);
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(188, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(22, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "Vin";
			this.textBox3.Location = new System.Drawing.Point(191, 40);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(345, 20);
			this.textBox3.TabIndex = 7;
			this.textBox3.TextChanged += new System.EventHandler(textBox3_TextChanged);
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(463, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(73, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "bmwpeople.ru";
			this.button1.Location = new System.Drawing.Point(212, 119);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(150, 23);
			this.button1.TabIndex = 10;
			this.button1.Text = "Reset kurzschluss FRM87";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(button1_Click);
			this.button2.Location = new System.Drawing.Point(386, 119);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(150, 23);
			this.button2.TabIndex = 11;
			this.button2.Text = "Reset kurzschluss FRM70";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(button2_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(576, 178);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.textBox3);
			base.Controls.Add(this.disconnect_button);
			base.Controls.Add(this.kurzschluss_button);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.connect_button);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "Form1";
			this.Text = "kurzschlussreset tool repaired by Leonid";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
