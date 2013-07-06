using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace EasyObjectsQuickStart
{
	/// <summary>
	/// Summary description for Login.
	/// </summary>
	public class Login : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.ComboBox cboAuthenticationMethod;
		private System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtServer;
		private System.Windows.Forms.Label lblServer;
		private System.Windows.Forms.Label lblAuthentication;
		private System.Windows.Forms.Label lblUsername;
		private System.Windows.Forms.Label lblPassword;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Login()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cboAuthenticationMethod = new System.Windows.Forms.ComboBox();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnConnect = new System.Windows.Forms.Button();
			this.txtServer = new System.Windows.Forms.TextBox();
			this.lblServer = new System.Windows.Forms.Label();
			this.lblAuthentication = new System.Windows.Forms.Label();
			this.lblUsername = new System.Windows.Forms.Label();
			this.lblPassword = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// cboAuthenticationMethod
			// 
			this.cboAuthenticationMethod.Items.AddRange(new object[] {
																		 "Windows Authentication",
																		 "SQL Server Authentication"});
			this.cboAuthenticationMethod.Location = new System.Drawing.Point(144, 40);
			this.cboAuthenticationMethod.Name = "cboAuthenticationMethod";
			this.cboAuthenticationMethod.Size = new System.Drawing.Size(248, 21);
			this.cboAuthenticationMethod.TabIndex = 1;
			this.cboAuthenticationMethod.SelectedIndexChanged += new System.EventHandler(this.cboAuthenticationMethod_SelectedIndexChanged);
			// 
			// txtUsername
			// 
			this.txtUsername.Location = new System.Drawing.Point(160, 64);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(232, 20);
			this.txtUsername.TabIndex = 2;
			this.txtUsername.Text = "";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(160, 88);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(232, 20);
			this.txtPassword.TabIndex = 3;
			this.txtPassword.Text = "";
			// 
			// btnCancel
			// 
			this.btnCancel.CausesValidation = false;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(320, 144);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			// 
			// btnConnect
			// 
			this.btnConnect.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnConnect.Location = new System.Drawing.Point(232, 144);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.TabIndex = 4;
			this.btnConnect.Text = "Connect";
			this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
			// 
			// txtServer
			// 
			this.txtServer.Location = new System.Drawing.Point(144, 16);
			this.txtServer.Name = "txtServer";
			this.txtServer.Size = new System.Drawing.Size(248, 20);
			this.txtServer.TabIndex = 0;
			this.txtServer.Text = "(local)";
			// 
			// lblServer
			// 
			this.lblServer.AutoSize = true;
			this.lblServer.Location = new System.Drawing.Point(8, 16);
			this.lblServer.Name = "lblServer";
			this.lblServer.Size = new System.Drawing.Size(88, 16);
			this.lblServer.TabIndex = 6;
			this.lblServer.Text = "Server Name/IP:";
			// 
			// lblAuthentication
			// 
			this.lblAuthentication.AutoSize = true;
			this.lblAuthentication.Location = new System.Drawing.Point(8, 40);
			this.lblAuthentication.Name = "lblAuthentication";
			this.lblAuthentication.Size = new System.Drawing.Size(79, 16);
			this.lblAuthentication.TabIndex = 7;
			this.lblAuthentication.Text = "Authentication:";
			// 
			// lblUsername
			// 
			this.lblUsername.AutoSize = true;
			this.lblUsername.Location = new System.Drawing.Point(24, 64);
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new System.Drawing.Size(60, 16);
			this.lblUsername.TabIndex = 8;
			this.lblUsername.Text = "Username:";
			// 
			// lblPassword
			// 
			this.lblPassword.AutoSize = true;
			this.lblPassword.Location = new System.Drawing.Point(24, 88);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(57, 16);
			this.lblPassword.TabIndex = 9;
			this.lblPassword.Text = "Password:";
			// 
			// Login
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(408, 176);
			this.ControlBox = false;
			this.Controls.Add(this.lblPassword);
			this.Controls.Add(this.lblUsername);
			this.Controls.Add(this.lblAuthentication);
			this.Controls.Add(this.lblServer);
			this.Controls.Add(this.txtServer);
			this.Controls.Add(this.btnConnect);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.txtUsername);
			this.Controls.Add(this.cboAuthenticationMethod);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "Login";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Login";
			this.Load += new System.EventHandler(this.Login_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void Login_Load(object sender, System.EventArgs e)
		{
			Line line = new Line();
			int buffer = 10;

			line.X_Left = buffer;
			line.Y_Left = btnConnect.Top - buffer;
			line.X_Right = this.Width - buffer * 2;
			line.Y_Right = line.Y_Left;
			line.Color = Color.Black;

			this.Controls.Add(line);

			// Select the first item in the list
			this.cboAuthenticationMethod.SelectedIndex = 0;
			this.lblUsername.Enabled = false;
			this.lblPassword.Enabled = false;
			this.txtUsername.Enabled = false;
			this.txtPassword.Enabled = false;
		}

		private void cboAuthenticationMethod_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.lblUsername.Enabled = (this.cboAuthenticationMethod.SelectedIndex == 1);
			this.lblPassword.Enabled = (this.cboAuthenticationMethod.SelectedIndex == 1);
			this.txtUsername.Enabled = (this.cboAuthenticationMethod.SelectedIndex == 1);
			this.txtPassword.Enabled = (this.cboAuthenticationMethod.SelectedIndex == 1);
		}

		public string _username = string.Empty;
		public string _password = string.Empty;
		public bool _useIntegratedSecurity = true;
		public string _server = string.Empty;

		private void btnConnect_Click(object sender, System.EventArgs e)
		{
			_server = txtServer.Text;

			if (this.cboAuthenticationMethod.SelectedIndex == 0)
			{
				_useIntegratedSecurity = true;
				_username = string.Empty;
				_password = string.Empty;
			}
			else
			{
				_useIntegratedSecurity = false;
				_username = txtUsername.Text;
				_password = txtPassword.Text;
			}
		}
	}

	public class Line : System.Windows.Forms.Control 
	{ 
		public Line(){} 

		#region "Public Properties"
		private System.Drawing.Color _color = Color.Black; 
		public System.Drawing.Color Color 
		{ 
			get 
			{ 
				return _color; 
			} 
			set 
			{ 
				_color = value; 
			} 
		} 

		private int xLeft = 0; 
		public int X_Left 
		{ 
			get 
			{ 
				return xLeft; 
			} 
			set 
			{ 
				xLeft = value; 
			} 
		} 
	
		private int yLeft = 0; 
		public int Y_Left 
		{ 
			get 
			{ 
				return yLeft; 
			} 
			set 
			{ 
				yLeft = value; 
			} 
		} 

		private int xRight = 0; 
		public int X_Right 
		{ 
			get 
			{ 
				return xRight; 
			} 
			set 
			{ 
				xRight = value; 
				if (xLeft <= xRight) 
				{ 
					Width = xRight - xLeft; 
				} 
				else 
				{ 
					Width = xLeft - xRight; 
				} 
			} 
		} 

		private int yRight = 0; 
		public int Y_Right 
		{ 
			get 
			{ 
				return yRight; 
			} 
			set 
			{ 
				yRight = value; 
				if (yLeft <= yRight) 
				{ 
					Height = yRight - yLeft; 
				} 
				else 
				{ 
					Height = yLeft - yRight; 
				} 
			} 
		} 
		#endregion

		#region "Protected Methods"
		protected override void Dispose(bool disposing) 
		{ 
			base.Dispose(disposing); 
		} 

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) 
		{ 
			if (xLeft <= xRight) 
			{ 
				Left = xLeft; 
			} 
			else 
			{ 
				Left = xRight; 
			} 
			if (yLeft <= yRight) 
			{ 
				Top = yLeft; 
			} 
			else 
			{ 
				Top = yRight; 
			} 
			SolidBrush brush = new SolidBrush(this.Parent.BackColor); 
			e.Graphics.FillRectangle(brush, 0, 0, this.Width, this.Height); 
			Pen pen = new Pen(_color); 
			if (xLeft < xRight & yLeft < yRight) 
			{ 
				e.Graphics.DrawLine(pen, 0, 0, this.Width, this.Height); 
			} 
			else if (xLeft > xRight & yLeft < yRight) 
			{ 
				e.Graphics.DrawLine(pen, this.Width, 0, 0, this.Height); 
			} 
			else if (xLeft > xRight & yLeft > yRight) 
			{ 
				e.Graphics.DrawLine(pen, this.Width, this.Height, 0, 0); 
			} 
			else if (xLeft < xRight & yLeft > yRight) 
			{ 
				e.Graphics.DrawLine(pen, 0, this.Height, this.Width, 0); 
			} 
		} 
		#endregion
	}
}
