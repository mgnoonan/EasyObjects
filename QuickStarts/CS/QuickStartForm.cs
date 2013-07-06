//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Data Access Application Block
//===============================================================================
// Copyright © 2004 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

using NCI.EasyObjects;
using EasyObjectsQuickStart.BLL;

namespace EasyObjectsQuickStart
{
    /// <summary>
    /// Enterprise Library Data Access Block Quick Start Sample.
    /// Please run SetupQuickStartsDB.bat to create database objects 
    /// used by this sample.
    /// </summary>
    public class QuickStartForm : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        private Label label4;
        private GroupBox groupBox1;
        private GroupBox groupBox;

        private Process viewerProcess = null;
        private DataGrid resultsDataGrid;
        private Button transactionalUpdateButton;
        private Button singleItemButton;
        private Label useCaseLabel;
        private Button retrieveUsingXmlReaderButton;
        private Button viewWalkthroughButton;
        private Button quitButton;
        private PictureBox logoPictureBox;
        private TextBox resultsTextBox;

        private const string HelpViewerExecutable = "iexplore.exe";
        private const string HelpTopicNamespace = @"ms-help://MS.EntLib.2005Jun.Da.QS";
		private System.Windows.Forms.Button btnLoadAll;
		private System.Windows.Forms.Button btnSimpleQuery;
		private System.Windows.Forms.Button btnProductsAddDelete;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblQuery;
		private System.Windows.Forms.LinkLabel linkDownload;
		private System.Windows.Forms.CheckBox chkStoredProcedures;

		public static System.Windows.Forms.Form AppForm;

        public QuickStartForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuickStartForm));
            this.resultsTextBox = new System.Windows.Forms.TextBox();
            this.transactionalUpdateButton = new System.Windows.Forms.Button();
            this.singleItemButton = new System.Windows.Forms.Button();
            this.btnLoadAll = new System.Windows.Forms.Button();
            this.btnProductsAddDelete = new System.Windows.Forms.Button();
            this.btnSimpleQuery = new System.Windows.Forms.Button();
            this.useCaseLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.retrieveUsingXmlReaderButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.linkDownload = new System.Windows.Forms.LinkLabel();
            this.viewWalkthroughButton = new System.Windows.Forms.Button();
            this.quitButton = new System.Windows.Forms.Button();
            this.resultsDataGrid = new System.Windows.Forms.DataGrid();
            this.lblQuery = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkStoredProcedures = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // resultsTextBox
            // 
            resources.ApplyResources(this.resultsTextBox, "resultsTextBox");
            this.resultsTextBox.Name = "resultsTextBox";
            this.resultsTextBox.ReadOnly = true;
            this.resultsTextBox.TabStop = false;
            // 
            // transactionalUpdateButton
            // 
            resources.ApplyResources(this.transactionalUpdateButton, "transactionalUpdateButton");
            this.transactionalUpdateButton.Name = "transactionalUpdateButton";
            this.transactionalUpdateButton.Click += new System.EventHandler(this.transactionalUpdateButton_Click);
            // 
            // singleItemButton
            // 
            resources.ApplyResources(this.singleItemButton, "singleItemButton");
            this.singleItemButton.Name = "singleItemButton";
            this.singleItemButton.Click += new System.EventHandler(this.singleItemButton_Click);
            // 
            // btnLoadAll
            // 
            resources.ApplyResources(this.btnLoadAll, "btnLoadAll");
            this.btnLoadAll.Name = "btnLoadAll";
            this.btnLoadAll.Click += new System.EventHandler(this.btnLoadAll_Click);
            // 
            // btnProductsAddDelete
            // 
            resources.ApplyResources(this.btnProductsAddDelete, "btnProductsAddDelete");
            this.btnProductsAddDelete.Name = "btnProductsAddDelete";
            this.btnProductsAddDelete.Click += new System.EventHandler(this.btnProductsAddDelete_Click);
            // 
            // btnSimpleQuery
            // 
            resources.ApplyResources(this.btnSimpleQuery, "btnSimpleQuery");
            this.btnSimpleQuery.Name = "btnSimpleQuery";
            this.btnSimpleQuery.Click += new System.EventHandler(this.btnSimpleQuery_Click);
            // 
            // useCaseLabel
            // 
            resources.ApplyResources(this.useCaseLabel, "useCaseLabel");
            this.useCaseLabel.Name = "useCaseLabel";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // retrieveUsingXmlReaderButton
            // 
            resources.ApplyResources(this.retrieveUsingXmlReaderButton, "retrieveUsingXmlReaderButton");
            this.retrieveUsingXmlReaderButton.Name = "retrieveUsingXmlReaderButton";
            this.retrieveUsingXmlReaderButton.Click += new System.EventHandler(this.retrieveUsingXmlReaderButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.logoPictureBox);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.logoPictureBox, "logoPictureBox");
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.TabStop = false;
            this.logoPictureBox.Click += new System.EventHandler(this.logoPictureBox_Click);
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.linkDownload);
            this.groupBox.Controls.Add(this.viewWalkthroughButton);
            this.groupBox.Controls.Add(this.quitButton);
            this.groupBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.groupBox, "groupBox");
            this.groupBox.Name = "groupBox";
            this.groupBox.TabStop = false;
            // 
            // linkDownload
            // 
            resources.ApplyResources(this.linkDownload, "linkDownload");
            this.linkDownload.CausesValidation = false;
            this.linkDownload.Name = "linkDownload";
            this.linkDownload.TabStop = true;
            this.linkDownload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkDownload_LinkClicked);
            // 
            // viewWalkthroughButton
            // 
            resources.ApplyResources(this.viewWalkthroughButton, "viewWalkthroughButton");
            this.viewWalkthroughButton.Name = "viewWalkthroughButton";
            this.viewWalkthroughButton.Click += new System.EventHandler(this.viewWalkthroughButton_Click);
            // 
            // quitButton
            // 
            this.quitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.quitButton, "quitButton");
            this.quitButton.Name = "quitButton";
            this.quitButton.Click += new System.EventHandler(this.quitButton_Click);
            // 
            // resultsDataGrid
            // 
            this.resultsDataGrid.AlternatingBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(207)))), ((int)(((byte)(239)))));
            this.resultsDataGrid.DataMember = "";
            this.resultsDataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.resultsDataGrid, "resultsDataGrid");
            this.resultsDataGrid.Name = "resultsDataGrid";
            this.resultsDataGrid.TabStop = false;
            // 
            // lblQuery
            // 
            resources.ApplyResources(this.lblQuery, "lblQuery");
            this.lblQuery.Name = "lblQuery";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // chkStoredProcedures
            // 
            this.chkStoredProcedures.Checked = true;
            this.chkStoredProcedures.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.chkStoredProcedures, "chkStoredProcedures");
            this.chkStoredProcedures.Name = "chkStoredProcedures";
            // 
            // QuickStartForm
            // 
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.quitButton;
            this.Controls.Add(this.chkStoredProcedures);
            this.Controls.Add(this.lblQuery);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.retrieveUsingXmlReaderButton);
            this.Controls.Add(this.useCaseLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.transactionalUpdateButton);
            this.Controls.Add(this.singleItemButton);
            this.Controls.Add(this.btnLoadAll);
            this.Controls.Add(this.btnProductsAddDelete);
            this.Controls.Add(this.btnSimpleQuery);
            this.Controls.Add(this.resultsTextBox);
            this.Controls.Add(this.resultsDataGrid);
            this.MaximizeBox = false;
            this.Name = "QuickStartForm";
            this.Load += new System.EventHandler(this.QuickStartForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultsDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
			AppForm = new QuickStartForm();
            // Unhandled exceptions will be delivered to our ThreadException handler
            Application.ThreadException += new ThreadExceptionEventHandler(AppThreadException);
            Application.Run(AppForm);
        }

		private string _username = string.Empty;
		private string _password = string.Empty;
		private bool _useIntegratedSecurity = true;
		private string _server = string.Empty;

        private void QuickStartForm_Load(object sender, EventArgs e)
        {
            // Initialize image on the form to the embedded logo
            this.logoPictureBox.Image = this.GetEmbeddedImage("EasyObjectsQuickStart.logo.gif");
			this.Show();

			Login dlg = new Login();
			if (dlg.ShowDialog(this) == DialogResult.Cancel)
			{
				this.Close();
			}

			_username = dlg._username;
			_password = dlg._password;
			_useIntegratedSecurity = dlg._useIntegratedSecurity;
			_server = dlg._server;
        }

        /// <summary>
        /// Displays dialog with information about exceptions that occur in the application. 
        /// </summary>
        private static void AppThreadException(object source, ThreadExceptionEventArgs e)
        {
            string errorMsg = SR.GeneralExceptionMessage(e.Exception.Message);
            errorMsg += Environment.NewLine + SR.DbRequirementsMessage;

            DialogResult result = MessageBox.Show(errorMsg, SR.ApplicationErrorMessage, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);

            // Exits the program when the user clicks Abort.
            if (result == DialogResult.Abort)
            {
                Application.Exit();
            }
            QuickStartForm.AppForm.Cursor = System.Windows.Forms.Cursors.Default;
        }

        /// <summary>
        /// Retrieves the specified embedded image resource.
        /// </summary>
        private Image GetEmbeddedImage(string resourceName)
        {
            Stream resourceStream = Assembly.GetEntryAssembly().GetManifestResourceStream(resourceName);

            if (resourceStream == null)
            {
                return null;
            }

            Image img = Image.FromStream(resourceStream);

            return img;
        }

        /// <summary>
        /// Updates the results textbox on the form with the information for a use case.
        /// </summary>
        private void DisplayResults(string useCase, string query, string results)
        {
            this.useCaseLabel.Text = useCase;
			this.lblQuery.Text = query;
            this.resultsTextBox.Text = results;
            this.resultsDataGrid.Hide();
            this.resultsTextBox.Show();
        }

		/// <summary>
		/// Displays the grid showing the results of a use case.
		/// </summary>
//		private void DisplayResults(string useCase)
//		{
//			DisplayResults(useCase, string.Empty);
//		}

        /// <summary>
        /// Displays the grid showing the results of a use case.
        /// </summary>
        private void DisplayResults(string useCase, string query)
        {
            this.useCaseLabel.Text = useCase;
			this.lblQuery.Text = query;
			this.resultsDataGrid.Show();
            this.resultsTextBox.Hide();
        }

        /// <summary>
        /// Demonstrates how to retrieve multiple rows of data using
        /// a DataReader.
        /// </summary>
        private void btnLoadAll_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

			Employees emp = new Employees();
            emp.DefaultCommandType = chkStoredProcedures.Checked ? System.Data.CommandType.StoredProcedure : System.Data.CommandType.Text;

			if (!emp.LoadAll())
			{
				this.DisplayResults(this.btnLoadAll.Text, emp.ErrorMessage);
				return;
			}

			// Bind the EasyObject's DefaultView to the DataGrid for display
			this.resultsDataGrid.SetDataBinding(emp.DefaultView, null);

            this.DisplayResults(this.btnLoadAll.Text, emp.Query.LastQuery);

            Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// Demonstrates how to retrieve multiple rows of data using
        /// a DataSet.
        /// </summary>
        private void btnSimpleQuery_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            //Employees emp = new Employees(this._server, this._useIntegratedSecurity, this._username, this._password);
            Employees emp = new Employees();
            // Note: no need to set the DefaultCommmandType, custom queries are always run as inline SQL

            // Limit the columns returned by the SELECT query
            emp.Query.AddResultColumn(EmployeesSchema.LastName);
            emp.Query.AddResultColumn(EmployeesSchema.FirstName);
            emp.Query.AddResultColumn(EmployeesSchema.City);
            emp.Query.AddResultColumn(EmployeesSchema.Region);

            // Note that we can inject a custom field to the
            // dynamic query as well
            emp.Query.AddResultColumn(string.Format("{0} + ' ' + {1} AS FullName", EmployeesSchema.FirstName.FieldName, EmployeesSchema.LastName.FieldName));

            // Add an ORDER BY clause
            emp.Query.AddOrderBy(EmployeesSchema.LastName);

            // Add a NOLOCK to prevent any locks (optional)
            emp.Query.UseNoLock = true;

            // Add a WHERE clause
            emp.Where.Region.Value = "WA";

            if (!emp.Query.Load())
            {
                this.DisplayResults(this.btnSimpleQuery.Text, emp.ErrorMessage);
                return;
            }

            // Bind the EasyObject's DefaultView to the DataGrid for display
            this.resultsDataGrid.SetDataBinding(emp.DefaultView, null);

            this.DisplayResults(this.btnSimpleQuery.Text, emp.Query.LastQuery);

            Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// Demonstrates how to retrieve a single row of data.
        /// </summary>
        private void btnProductsAddDelete_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

			Products prod = new Products();
			prod.DefaultCommandType = chkStoredProcedures.Checked ? System.Data.CommandType.StoredProcedure : System.Data.CommandType.Text;

			// Call AddNew() to add a new row to the EasyObject. You must fill in all 
			// required fields or an error will result when you call Save().
			prod.AddNew();

            // We're going to provide our own IDENTITY column for this record
            prod.IdentityInsert = true;

			// Note the use of the 's_' fields, which take strings as arguments. If this object
			// were being loaded from TextBox objects on a WinForm, you don't have to worry about
			// the datatype because this is handled for you in EasyObjects
            prod.s_ProductID = "78";
			prod.s_ProductName = "EasyObjects";
			prod.s_Discontinued = "True";
			prod.s_QuantityPerUnit = "10";
			prod.s_ReorderLevel = "100";
			prod.s_UnitPrice = "49.95";
			prod.s_UnitsInStock = "200";

			// Save the changes
			prod.Save();

			// Display the XML representation of the EasyObject
            string productDetails = prod.ToXml();

            this.DisplayResults(this.btnProductsAddDelete.Text, prod.Query.LastQuery, productDetails);

			// Delete the new addition
			prod.MarkAsDeleted();
			prod.Save();

            Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// Demonstrates how to retrieve a single data item from the database.
        /// </summary>
        private void singleItemButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

			Products prod = new Products();

			// Load a single row via the primary key
			prod.LoadByPrimaryKey(4);

            string productName = prod.s_ProductName;

            this.DisplayResults(this.singleItemButton.Text, "Stored Procedure", productName);

            Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// Demonstrates how to update the database multiple times in the
        /// context of a transaction. All updates will succeed or all will be 
        /// rolled back.
        /// </summary>
        private void transactionalUpdateButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            string results = "";
			Products prod = new Products();
			prod.DefaultCommandType = chkStoredProcedures.Checked ? System.Data.CommandType.StoredProcedure : System.Data.CommandType.Text;

			Employees emp = new Employees();
			emp.DefaultCommandType = chkStoredProcedures.Checked ? System.Data.CommandType.StoredProcedure : System.Data.CommandType.Text;

			// Update the requested product
			prod.LoadByPrimaryKey(4);
			prod.UnitsInStock += 1;

			// Update the requested employee
			emp.LoadByPrimaryKey(1);
			emp.s_Country = "CAN";

			// Retrieve the current transaction manager
			TransactionManager tx = TransactionManager.ThreadTransactionMgr();

			try
			{
				tx.BeginTransaction();

				// Save both objects within the same transaction
				emp.Save();
				prod.Save();

				// Deliberately throw an error, to cause the transaction to rollback
				throw new Exception("Deliberate exception, transaction rolled back.");

				tx.CommitTransaction();

                //this.DisplayResults(this.transactionalUpdateButton.Text, chkStoredProcedures.Checked ? "Stored Procedure" : emp.Query.LastQuery, results);
			}
			catch(Exception ex)
			{
				tx.RollbackTransaction();
				TransactionManager.ThreadTransactionMgrReset();
				this.DisplayResults(this.transactionalUpdateButton.Text, ex.Message);
			}

            Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// Demonstrates how to retrieve XML data from a SQL Server database.
        /// </summary>
        private void retrieveUsingXmlReaderButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

			Products prod = new Products();
			prod.DefaultCommandType = chkStoredProcedures.Checked ? System.Data.CommandType.StoredProcedure : System.Data.CommandType.Text;
			prod.LoadAll();

            DisplayResults(this.retrieveUsingXmlReaderButton.Text, chkStoredProcedures.Checked ? "Stored Procedure" : prod.Query.LastQuery, prod.ToXml());

            Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// Quits the application.
        /// </summary>
        private void quitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Displays Quick Start help topics using the Help 2 Viewer.
        /// </summary>
        private void viewWalkthroughButton_Click(object sender, EventArgs e)
        {
			Process process = new Process();

			process.StartInfo.UseShellExecute = true;
			process.StartInfo.FileName = @"..\..\help\index.htm";
			process.Start();
		}

		private void logoPictureBox_Click(object sender, System.EventArgs e)
		{
			Process process = new Process();

			process.StartInfo.UseShellExecute = true;
			process.StartInfo.FileName = "http://www.easyobjects.net";
			process.Start();
		}

		private void linkDownload_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			Process process = new Process();

			process.StartInfo.UseShellExecute = true;
			process.StartInfo.FileName = "http://www.easyobjects.net/Downloads/tabid/125/Default.aspx";
			process.Start();
		}
    }
}