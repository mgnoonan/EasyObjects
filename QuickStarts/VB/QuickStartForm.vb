'===============================================================================
' Microsoft patterns & practices Enterprise Library
' Data Access Application Block
'===============================================================================
' Copyright © 2004 Microsoft Corporation.  All rights reserved.
' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
' OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
' LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
' FITNESS FOR A PARTICULAR PURPOSE.
'===============================================================================
Option Strict On

Imports System
Imports System.ComponentModel
'Imports System.Data
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Reflection
Imports System.Threading
Imports System.Windows.Forms

Imports NCI.EasyObjects

Namespace EasyObjectsQuickStart
    Public Class QuickStartForm
        Inherits System.Windows.Forms.Form

        Public Shared AppForm As System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

        Public Sub New()
            MyBase.New()

            'This call is required by the Windows Form Designer.
            InitializeComponent()

            'Add any initialization after the InitializeComponent() call

        End Sub

        'Form overrides dispose to clean up the component list.
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        Friend WithEvents groupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents logoPictureBox As System.Windows.Forms.PictureBox
        Friend WithEvents groupBox As System.Windows.Forms.GroupBox
        Friend WithEvents viewWalkthroughButton As System.Windows.Forms.Button
        Friend WithEvents quitButton As System.Windows.Forms.Button
        Friend WithEvents retrieveUsingXmlReaderButton As System.Windows.Forms.Button
        Friend WithEvents useCaseLabel As System.Windows.Forms.Label
        Friend WithEvents label4 As System.Windows.Forms.Label
        Friend WithEvents transactionalUpdateButton As System.Windows.Forms.Button
        Friend WithEvents singleItemButton As System.Windows.Forms.Button
        Friend WithEvents resultsTextBox As System.Windows.Forms.TextBox
        Friend WithEvents resultsDataGrid As System.Windows.Forms.DataGrid

        Private viewerProcess As Process = Nothing
        Private Const HelpViewerExecutable As String = "dexplore.exe"
        Private Const HelpTopicNamespace As String = "ms-help://MS.EntLib.2005Jun.Da.QS"
        Friend WithEvents btnSimpleQuery As System.Windows.Forms.Button
        Friend WithEvents btnLoadAll As System.Windows.Forms.Button
        Friend WithEvents btnProductsAddDelete As System.Windows.Forms.Button

        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(QuickStartForm))
            Me.groupBox1 = New System.Windows.Forms.GroupBox
            Me.logoPictureBox = New System.Windows.Forms.PictureBox
            Me.groupBox = New System.Windows.Forms.GroupBox
            Me.viewWalkthroughButton = New System.Windows.Forms.Button
            Me.quitButton = New System.Windows.Forms.Button
            Me.retrieveUsingXmlReaderButton = New System.Windows.Forms.Button
            Me.useCaseLabel = New System.Windows.Forms.Label
            Me.label4 = New System.Windows.Forms.Label
            Me.btnSimpleQuery = New System.Windows.Forms.Button
            Me.transactionalUpdateButton = New System.Windows.Forms.Button
            Me.singleItemButton = New System.Windows.Forms.Button
            Me.btnLoadAll = New System.Windows.Forms.Button
            Me.btnProductsAddDelete = New System.Windows.Forms.Button
            Me.resultsTextBox = New System.Windows.Forms.TextBox
            Me.resultsDataGrid = New System.Windows.Forms.DataGrid
            Me.groupBox1.SuspendLayout()
            Me.groupBox.SuspendLayout()
            CType(Me.resultsDataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'groupBox1
            '
            Me.groupBox1.AccessibleDescription = resources.GetString("groupBox1.AccessibleDescription")
            Me.groupBox1.AccessibleName = resources.GetString("groupBox1.AccessibleName")
            Me.groupBox1.Anchor = CType(resources.GetObject("groupBox1.Anchor"), System.Windows.Forms.AnchorStyles)
            Me.groupBox1.BackColor = System.Drawing.Color.White
            Me.groupBox1.BackgroundImage = CType(resources.GetObject("groupBox1.BackgroundImage"), System.Drawing.Image)
            Me.groupBox1.Controls.Add(Me.logoPictureBox)
            Me.groupBox1.Dock = CType(resources.GetObject("groupBox1.Dock"), System.Windows.Forms.DockStyle)
            Me.groupBox1.Enabled = CType(resources.GetObject("groupBox1.Enabled"), Boolean)
            Me.groupBox1.Font = CType(resources.GetObject("groupBox1.Font"), System.Drawing.Font)
            Me.groupBox1.ImeMode = CType(resources.GetObject("groupBox1.ImeMode"), System.Windows.Forms.ImeMode)
            Me.groupBox1.Location = CType(resources.GetObject("groupBox1.Location"), System.Drawing.Point)
            Me.groupBox1.Name = "groupBox1"
            Me.groupBox1.RightToLeft = CType(resources.GetObject("groupBox1.RightToLeft"), System.Windows.Forms.RightToLeft)
            Me.groupBox1.Size = CType(resources.GetObject("groupBox1.Size"), System.Drawing.Size)
            Me.groupBox1.TabIndex = CType(resources.GetObject("groupBox1.TabIndex"), Integer)
            Me.groupBox1.TabStop = False
            Me.groupBox1.Text = resources.GetString("groupBox1.Text")
            Me.groupBox1.Visible = CType(resources.GetObject("groupBox1.Visible"), Boolean)
            '
            'logoPictureBox
            '
            Me.logoPictureBox.AccessibleDescription = resources.GetString("logoPictureBox.AccessibleDescription")
            Me.logoPictureBox.AccessibleName = resources.GetString("logoPictureBox.AccessibleName")
            Me.logoPictureBox.Anchor = CType(resources.GetObject("logoPictureBox.Anchor"), System.Windows.Forms.AnchorStyles)
            Me.logoPictureBox.BackgroundImage = CType(resources.GetObject("logoPictureBox.BackgroundImage"), System.Drawing.Image)
            Me.logoPictureBox.Cursor = System.Windows.Forms.Cursors.Hand
            Me.logoPictureBox.Dock = CType(resources.GetObject("logoPictureBox.Dock"), System.Windows.Forms.DockStyle)
            Me.logoPictureBox.Enabled = CType(resources.GetObject("logoPictureBox.Enabled"), Boolean)
            Me.logoPictureBox.Font = CType(resources.GetObject("logoPictureBox.Font"), System.Drawing.Font)
            Me.logoPictureBox.Image = CType(resources.GetObject("logoPictureBox.Image"), System.Drawing.Image)
            Me.logoPictureBox.ImeMode = CType(resources.GetObject("logoPictureBox.ImeMode"), System.Windows.Forms.ImeMode)
            Me.logoPictureBox.Location = CType(resources.GetObject("logoPictureBox.Location"), System.Drawing.Point)
            Me.logoPictureBox.Name = "logoPictureBox"
            Me.logoPictureBox.RightToLeft = CType(resources.GetObject("logoPictureBox.RightToLeft"), System.Windows.Forms.RightToLeft)
            Me.logoPictureBox.Size = CType(resources.GetObject("logoPictureBox.Size"), System.Drawing.Size)
            Me.logoPictureBox.SizeMode = CType(resources.GetObject("logoPictureBox.SizeMode"), System.Windows.Forms.PictureBoxSizeMode)
            Me.logoPictureBox.TabIndex = CType(resources.GetObject("logoPictureBox.TabIndex"), Integer)
            Me.logoPictureBox.TabStop = False
            Me.logoPictureBox.Text = resources.GetString("logoPictureBox.Text")
            Me.logoPictureBox.Visible = CType(resources.GetObject("logoPictureBox.Visible"), Boolean)
            '
            'groupBox
            '
            Me.groupBox.AccessibleDescription = resources.GetString("groupBox.AccessibleDescription")
            Me.groupBox.AccessibleName = resources.GetString("groupBox.AccessibleName")
            Me.groupBox.Anchor = CType(resources.GetObject("groupBox.Anchor"), System.Windows.Forms.AnchorStyles)
            Me.groupBox.BackgroundImage = CType(resources.GetObject("groupBox.BackgroundImage"), System.Drawing.Image)
            Me.groupBox.Controls.Add(Me.viewWalkthroughButton)
            Me.groupBox.Controls.Add(Me.quitButton)
            Me.groupBox.Dock = CType(resources.GetObject("groupBox.Dock"), System.Windows.Forms.DockStyle)
            Me.groupBox.Enabled = CType(resources.GetObject("groupBox.Enabled"), Boolean)
            Me.groupBox.Font = CType(resources.GetObject("groupBox.Font"), System.Drawing.Font)
            Me.groupBox.ImeMode = CType(resources.GetObject("groupBox.ImeMode"), System.Windows.Forms.ImeMode)
            Me.groupBox.Location = CType(resources.GetObject("groupBox.Location"), System.Drawing.Point)
            Me.groupBox.Name = "groupBox"
            Me.groupBox.RightToLeft = CType(resources.GetObject("groupBox.RightToLeft"), System.Windows.Forms.RightToLeft)
            Me.groupBox.Size = CType(resources.GetObject("groupBox.Size"), System.Drawing.Size)
            Me.groupBox.TabIndex = CType(resources.GetObject("groupBox.TabIndex"), Integer)
            Me.groupBox.TabStop = False
            Me.groupBox.Text = resources.GetString("groupBox.Text")
            Me.groupBox.Visible = CType(resources.GetObject("groupBox.Visible"), Boolean)
            '
            'viewWalkthroughButton
            '
            Me.viewWalkthroughButton.AccessibleDescription = resources.GetString("viewWalkthroughButton.AccessibleDescription")
            Me.viewWalkthroughButton.AccessibleName = resources.GetString("viewWalkthroughButton.AccessibleName")
            Me.viewWalkthroughButton.Anchor = CType(resources.GetObject("viewWalkthroughButton.Anchor"), System.Windows.Forms.AnchorStyles)
            Me.viewWalkthroughButton.BackgroundImage = CType(resources.GetObject("viewWalkthroughButton.BackgroundImage"), System.Drawing.Image)
            Me.viewWalkthroughButton.Dock = CType(resources.GetObject("viewWalkthroughButton.Dock"), System.Windows.Forms.DockStyle)
            Me.viewWalkthroughButton.Enabled = CType(resources.GetObject("viewWalkthroughButton.Enabled"), Boolean)
            Me.viewWalkthroughButton.FlatStyle = CType(resources.GetObject("viewWalkthroughButton.FlatStyle"), System.Windows.Forms.FlatStyle)
            Me.viewWalkthroughButton.Font = CType(resources.GetObject("viewWalkthroughButton.Font"), System.Drawing.Font)
            Me.viewWalkthroughButton.Image = CType(resources.GetObject("viewWalkthroughButton.Image"), System.Drawing.Image)
            Me.viewWalkthroughButton.ImageAlign = CType(resources.GetObject("viewWalkthroughButton.ImageAlign"), System.Drawing.ContentAlignment)
            Me.viewWalkthroughButton.ImageIndex = CType(resources.GetObject("viewWalkthroughButton.ImageIndex"), Integer)
            Me.viewWalkthroughButton.ImeMode = CType(resources.GetObject("viewWalkthroughButton.ImeMode"), System.Windows.Forms.ImeMode)
            Me.viewWalkthroughButton.Location = CType(resources.GetObject("viewWalkthroughButton.Location"), System.Drawing.Point)
            Me.viewWalkthroughButton.Name = "viewWalkthroughButton"
            Me.viewWalkthroughButton.RightToLeft = CType(resources.GetObject("viewWalkthroughButton.RightToLeft"), System.Windows.Forms.RightToLeft)
            Me.viewWalkthroughButton.Size = CType(resources.GetObject("viewWalkthroughButton.Size"), System.Drawing.Size)
            Me.viewWalkthroughButton.TabIndex = CType(resources.GetObject("viewWalkthroughButton.TabIndex"), Integer)
            Me.viewWalkthroughButton.Text = resources.GetString("viewWalkthroughButton.Text")
            Me.viewWalkthroughButton.TextAlign = CType(resources.GetObject("viewWalkthroughButton.TextAlign"), System.Drawing.ContentAlignment)
            Me.viewWalkthroughButton.Visible = CType(resources.GetObject("viewWalkthroughButton.Visible"), Boolean)
            '
            'quitButton
            '
            Me.quitButton.AccessibleDescription = resources.GetString("quitButton.AccessibleDescription")
            Me.quitButton.AccessibleName = resources.GetString("quitButton.AccessibleName")
            Me.quitButton.Anchor = CType(resources.GetObject("quitButton.Anchor"), System.Windows.Forms.AnchorStyles)
            Me.quitButton.BackgroundImage = CType(resources.GetObject("quitButton.BackgroundImage"), System.Drawing.Image)
            Me.quitButton.Dock = CType(resources.GetObject("quitButton.Dock"), System.Windows.Forms.DockStyle)
            Me.quitButton.Enabled = CType(resources.GetObject("quitButton.Enabled"), Boolean)
            Me.quitButton.FlatStyle = CType(resources.GetObject("quitButton.FlatStyle"), System.Windows.Forms.FlatStyle)
            Me.quitButton.Font = CType(resources.GetObject("quitButton.Font"), System.Drawing.Font)
            Me.quitButton.Image = CType(resources.GetObject("quitButton.Image"), System.Drawing.Image)
            Me.quitButton.ImageAlign = CType(resources.GetObject("quitButton.ImageAlign"), System.Drawing.ContentAlignment)
            Me.quitButton.ImageIndex = CType(resources.GetObject("quitButton.ImageIndex"), Integer)
            Me.quitButton.ImeMode = CType(resources.GetObject("quitButton.ImeMode"), System.Windows.Forms.ImeMode)
            Me.quitButton.Location = CType(resources.GetObject("quitButton.Location"), System.Drawing.Point)
            Me.quitButton.Name = "quitButton"
            Me.quitButton.RightToLeft = CType(resources.GetObject("quitButton.RightToLeft"), System.Windows.Forms.RightToLeft)
            Me.quitButton.Size = CType(resources.GetObject("quitButton.Size"), System.Drawing.Size)
            Me.quitButton.TabIndex = CType(resources.GetObject("quitButton.TabIndex"), Integer)
            Me.quitButton.Text = resources.GetString("quitButton.Text")
            Me.quitButton.TextAlign = CType(resources.GetObject("quitButton.TextAlign"), System.Drawing.ContentAlignment)
            Me.quitButton.Visible = CType(resources.GetObject("quitButton.Visible"), Boolean)
            '
            'retrieveUsingXmlReaderButton
            '
            Me.retrieveUsingXmlReaderButton.AccessibleDescription = resources.GetString("retrieveUsingXmlReaderButton.AccessibleDescription")
            Me.retrieveUsingXmlReaderButton.AccessibleName = resources.GetString("retrieveUsingXmlReaderButton.AccessibleName")
            Me.retrieveUsingXmlReaderButton.Anchor = CType(resources.GetObject("retrieveUsingXmlReaderButton.Anchor"), System.Windows.Forms.AnchorStyles)
            Me.retrieveUsingXmlReaderButton.BackgroundImage = CType(resources.GetObject("retrieveUsingXmlReaderButton.BackgroundImage"), System.Drawing.Image)
            Me.retrieveUsingXmlReaderButton.Dock = CType(resources.GetObject("retrieveUsingXmlReaderButton.Dock"), System.Windows.Forms.DockStyle)
            Me.retrieveUsingXmlReaderButton.Enabled = CType(resources.GetObject("retrieveUsingXmlReaderButton.Enabled"), Boolean)
            Me.retrieveUsingXmlReaderButton.FlatStyle = CType(resources.GetObject("retrieveUsingXmlReaderButton.FlatStyle"), System.Windows.Forms.FlatStyle)
            Me.retrieveUsingXmlReaderButton.Font = CType(resources.GetObject("retrieveUsingXmlReaderButton.Font"), System.Drawing.Font)
            Me.retrieveUsingXmlReaderButton.Image = CType(resources.GetObject("retrieveUsingXmlReaderButton.Image"), System.Drawing.Image)
            Me.retrieveUsingXmlReaderButton.ImageAlign = CType(resources.GetObject("retrieveUsingXmlReaderButton.ImageAlign"), System.Drawing.ContentAlignment)
            Me.retrieveUsingXmlReaderButton.ImageIndex = CType(resources.GetObject("retrieveUsingXmlReaderButton.ImageIndex"), Integer)
            Me.retrieveUsingXmlReaderButton.ImeMode = CType(resources.GetObject("retrieveUsingXmlReaderButton.ImeMode"), System.Windows.Forms.ImeMode)
            Me.retrieveUsingXmlReaderButton.Location = CType(resources.GetObject("retrieveUsingXmlReaderButton.Location"), System.Drawing.Point)
            Me.retrieveUsingXmlReaderButton.Name = "retrieveUsingXmlReaderButton"
            Me.retrieveUsingXmlReaderButton.RightToLeft = CType(resources.GetObject("retrieveUsingXmlReaderButton.RightToLeft"), System.Windows.Forms.RightToLeft)
            Me.retrieveUsingXmlReaderButton.Size = CType(resources.GetObject("retrieveUsingXmlReaderButton.Size"), System.Drawing.Size)
            Me.retrieveUsingXmlReaderButton.TabIndex = CType(resources.GetObject("retrieveUsingXmlReaderButton.TabIndex"), Integer)
            Me.retrieveUsingXmlReaderButton.Text = resources.GetString("retrieveUsingXmlReaderButton.Text")
            Me.retrieveUsingXmlReaderButton.TextAlign = CType(resources.GetObject("retrieveUsingXmlReaderButton.TextAlign"), System.Drawing.ContentAlignment)
            Me.retrieveUsingXmlReaderButton.Visible = CType(resources.GetObject("retrieveUsingXmlReaderButton.Visible"), Boolean)
            '
            'useCaseLabel
            '
            Me.useCaseLabel.AccessibleDescription = resources.GetString("useCaseLabel.AccessibleDescription")
            Me.useCaseLabel.AccessibleName = resources.GetString("useCaseLabel.AccessibleName")
            Me.useCaseLabel.Anchor = CType(resources.GetObject("useCaseLabel.Anchor"), System.Windows.Forms.AnchorStyles)
            Me.useCaseLabel.AutoSize = CType(resources.GetObject("useCaseLabel.AutoSize"), Boolean)
            Me.useCaseLabel.Dock = CType(resources.GetObject("useCaseLabel.Dock"), System.Windows.Forms.DockStyle)
            Me.useCaseLabel.Enabled = CType(resources.GetObject("useCaseLabel.Enabled"), Boolean)
            Me.useCaseLabel.Font = CType(resources.GetObject("useCaseLabel.Font"), System.Drawing.Font)
            Me.useCaseLabel.Image = CType(resources.GetObject("useCaseLabel.Image"), System.Drawing.Image)
            Me.useCaseLabel.ImageAlign = CType(resources.GetObject("useCaseLabel.ImageAlign"), System.Drawing.ContentAlignment)
            Me.useCaseLabel.ImageIndex = CType(resources.GetObject("useCaseLabel.ImageIndex"), Integer)
            Me.useCaseLabel.ImeMode = CType(resources.GetObject("useCaseLabel.ImeMode"), System.Windows.Forms.ImeMode)
            Me.useCaseLabel.Location = CType(resources.GetObject("useCaseLabel.Location"), System.Drawing.Point)
            Me.useCaseLabel.Name = "useCaseLabel"
            Me.useCaseLabel.RightToLeft = CType(resources.GetObject("useCaseLabel.RightToLeft"), System.Windows.Forms.RightToLeft)
            Me.useCaseLabel.Size = CType(resources.GetObject("useCaseLabel.Size"), System.Drawing.Size)
            Me.useCaseLabel.TabIndex = CType(resources.GetObject("useCaseLabel.TabIndex"), Integer)
            Me.useCaseLabel.Text = resources.GetString("useCaseLabel.Text")
            Me.useCaseLabel.TextAlign = CType(resources.GetObject("useCaseLabel.TextAlign"), System.Drawing.ContentAlignment)
            Me.useCaseLabel.Visible = CType(resources.GetObject("useCaseLabel.Visible"), Boolean)
            '
            'label4
            '
            Me.label4.AccessibleDescription = resources.GetString("label4.AccessibleDescription")
            Me.label4.AccessibleName = resources.GetString("label4.AccessibleName")
            Me.label4.Anchor = CType(resources.GetObject("label4.Anchor"), System.Windows.Forms.AnchorStyles)
            Me.label4.AutoSize = CType(resources.GetObject("label4.AutoSize"), Boolean)
            Me.label4.Dock = CType(resources.GetObject("label4.Dock"), System.Windows.Forms.DockStyle)
            Me.label4.Enabled = CType(resources.GetObject("label4.Enabled"), Boolean)
            Me.label4.Font = CType(resources.GetObject("label4.Font"), System.Drawing.Font)
            Me.label4.Image = CType(resources.GetObject("label4.Image"), System.Drawing.Image)
            Me.label4.ImageAlign = CType(resources.GetObject("label4.ImageAlign"), System.Drawing.ContentAlignment)
            Me.label4.ImageIndex = CType(resources.GetObject("label4.ImageIndex"), Integer)
            Me.label4.ImeMode = CType(resources.GetObject("label4.ImeMode"), System.Windows.Forms.ImeMode)
            Me.label4.Location = CType(resources.GetObject("label4.Location"), System.Drawing.Point)
            Me.label4.Name = "label4"
            Me.label4.RightToLeft = CType(resources.GetObject("label4.RightToLeft"), System.Windows.Forms.RightToLeft)
            Me.label4.Size = CType(resources.GetObject("label4.Size"), System.Drawing.Size)
            Me.label4.TabIndex = CType(resources.GetObject("label4.TabIndex"), Integer)
            Me.label4.Text = resources.GetString("label4.Text")
            Me.label4.TextAlign = CType(resources.GetObject("label4.TextAlign"), System.Drawing.ContentAlignment)
            Me.label4.Visible = CType(resources.GetObject("label4.Visible"), Boolean)
            '
            'btnSimpleQuery
            '
            Me.btnSimpleQuery.AccessibleDescription = resources.GetString("btnSimpleQuery.AccessibleDescription")
            Me.btnSimpleQuery.AccessibleName = resources.GetString("btnSimpleQuery.AccessibleName")
            Me.btnSimpleQuery.Anchor = CType(resources.GetObject("btnSimpleQuery.Anchor"), System.Windows.Forms.AnchorStyles)
            Me.btnSimpleQuery.BackgroundImage = CType(resources.GetObject("btnSimpleQuery.BackgroundImage"), System.Drawing.Image)
            Me.btnSimpleQuery.Dock = CType(resources.GetObject("btnSimpleQuery.Dock"), System.Windows.Forms.DockStyle)
            Me.btnSimpleQuery.Enabled = CType(resources.GetObject("btnSimpleQuery.Enabled"), Boolean)
            Me.btnSimpleQuery.FlatStyle = CType(resources.GetObject("btnSimpleQuery.FlatStyle"), System.Windows.Forms.FlatStyle)
            Me.btnSimpleQuery.Font = CType(resources.GetObject("btnSimpleQuery.Font"), System.Drawing.Font)
            Me.btnSimpleQuery.Image = CType(resources.GetObject("btnSimpleQuery.Image"), System.Drawing.Image)
            Me.btnSimpleQuery.ImageAlign = CType(resources.GetObject("btnSimpleQuery.ImageAlign"), System.Drawing.ContentAlignment)
            Me.btnSimpleQuery.ImageIndex = CType(resources.GetObject("btnSimpleQuery.ImageIndex"), Integer)
            Me.btnSimpleQuery.ImeMode = CType(resources.GetObject("btnSimpleQuery.ImeMode"), System.Windows.Forms.ImeMode)
            Me.btnSimpleQuery.Location = CType(resources.GetObject("btnSimpleQuery.Location"), System.Drawing.Point)
            Me.btnSimpleQuery.Name = "btnSimpleQuery"
            Me.btnSimpleQuery.RightToLeft = CType(resources.GetObject("btnSimpleQuery.RightToLeft"), System.Windows.Forms.RightToLeft)
            Me.btnSimpleQuery.Size = CType(resources.GetObject("btnSimpleQuery.Size"), System.Drawing.Size)
            Me.btnSimpleQuery.TabIndex = CType(resources.GetObject("btnSimpleQuery.TabIndex"), Integer)
            Me.btnSimpleQuery.Text = resources.GetString("btnSimpleQuery.Text")
            Me.btnSimpleQuery.TextAlign = CType(resources.GetObject("btnSimpleQuery.TextAlign"), System.Drawing.ContentAlignment)
            Me.btnSimpleQuery.Visible = CType(resources.GetObject("btnSimpleQuery.Visible"), Boolean)
            '
            'transactionalUpdateButton
            '
            Me.transactionalUpdateButton.AccessibleDescription = resources.GetString("transactionalUpdateButton.AccessibleDescription")
            Me.transactionalUpdateButton.AccessibleName = resources.GetString("transactionalUpdateButton.AccessibleName")
            Me.transactionalUpdateButton.Anchor = CType(resources.GetObject("transactionalUpdateButton.Anchor"), System.Windows.Forms.AnchorStyles)
            Me.transactionalUpdateButton.BackgroundImage = CType(resources.GetObject("transactionalUpdateButton.BackgroundImage"), System.Drawing.Image)
            Me.transactionalUpdateButton.Dock = CType(resources.GetObject("transactionalUpdateButton.Dock"), System.Windows.Forms.DockStyle)
            Me.transactionalUpdateButton.Enabled = CType(resources.GetObject("transactionalUpdateButton.Enabled"), Boolean)
            Me.transactionalUpdateButton.FlatStyle = CType(resources.GetObject("transactionalUpdateButton.FlatStyle"), System.Windows.Forms.FlatStyle)
            Me.transactionalUpdateButton.Font = CType(resources.GetObject("transactionalUpdateButton.Font"), System.Drawing.Font)
            Me.transactionalUpdateButton.Image = CType(resources.GetObject("transactionalUpdateButton.Image"), System.Drawing.Image)
            Me.transactionalUpdateButton.ImageAlign = CType(resources.GetObject("transactionalUpdateButton.ImageAlign"), System.Drawing.ContentAlignment)
            Me.transactionalUpdateButton.ImageIndex = CType(resources.GetObject("transactionalUpdateButton.ImageIndex"), Integer)
            Me.transactionalUpdateButton.ImeMode = CType(resources.GetObject("transactionalUpdateButton.ImeMode"), System.Windows.Forms.ImeMode)
            Me.transactionalUpdateButton.Location = CType(resources.GetObject("transactionalUpdateButton.Location"), System.Drawing.Point)
            Me.transactionalUpdateButton.Name = "transactionalUpdateButton"
            Me.transactionalUpdateButton.RightToLeft = CType(resources.GetObject("transactionalUpdateButton.RightToLeft"), System.Windows.Forms.RightToLeft)
            Me.transactionalUpdateButton.Size = CType(resources.GetObject("transactionalUpdateButton.Size"), System.Drawing.Size)
            Me.transactionalUpdateButton.TabIndex = CType(resources.GetObject("transactionalUpdateButton.TabIndex"), Integer)
            Me.transactionalUpdateButton.Text = resources.GetString("transactionalUpdateButton.Text")
            Me.transactionalUpdateButton.TextAlign = CType(resources.GetObject("transactionalUpdateButton.TextAlign"), System.Drawing.ContentAlignment)
            Me.transactionalUpdateButton.Visible = CType(resources.GetObject("transactionalUpdateButton.Visible"), Boolean)
            '
            'singleItemButton
            '
            Me.singleItemButton.AccessibleDescription = resources.GetString("singleItemButton.AccessibleDescription")
            Me.singleItemButton.AccessibleName = resources.GetString("singleItemButton.AccessibleName")
            Me.singleItemButton.Anchor = CType(resources.GetObject("singleItemButton.Anchor"), System.Windows.Forms.AnchorStyles)
            Me.singleItemButton.BackgroundImage = CType(resources.GetObject("singleItemButton.BackgroundImage"), System.Drawing.Image)
            Me.singleItemButton.Dock = CType(resources.GetObject("singleItemButton.Dock"), System.Windows.Forms.DockStyle)
            Me.singleItemButton.Enabled = CType(resources.GetObject("singleItemButton.Enabled"), Boolean)
            Me.singleItemButton.FlatStyle = CType(resources.GetObject("singleItemButton.FlatStyle"), System.Windows.Forms.FlatStyle)
            Me.singleItemButton.Font = CType(resources.GetObject("singleItemButton.Font"), System.Drawing.Font)
            Me.singleItemButton.Image = CType(resources.GetObject("singleItemButton.Image"), System.Drawing.Image)
            Me.singleItemButton.ImageAlign = CType(resources.GetObject("singleItemButton.ImageAlign"), System.Drawing.ContentAlignment)
            Me.singleItemButton.ImageIndex = CType(resources.GetObject("singleItemButton.ImageIndex"), Integer)
            Me.singleItemButton.ImeMode = CType(resources.GetObject("singleItemButton.ImeMode"), System.Windows.Forms.ImeMode)
            Me.singleItemButton.Location = CType(resources.GetObject("singleItemButton.Location"), System.Drawing.Point)
            Me.singleItemButton.Name = "singleItemButton"
            Me.singleItemButton.RightToLeft = CType(resources.GetObject("singleItemButton.RightToLeft"), System.Windows.Forms.RightToLeft)
            Me.singleItemButton.Size = CType(resources.GetObject("singleItemButton.Size"), System.Drawing.Size)
            Me.singleItemButton.TabIndex = CType(resources.GetObject("singleItemButton.TabIndex"), Integer)
            Me.singleItemButton.Text = resources.GetString("singleItemButton.Text")
            Me.singleItemButton.TextAlign = CType(resources.GetObject("singleItemButton.TextAlign"), System.Drawing.ContentAlignment)
            Me.singleItemButton.Visible = CType(resources.GetObject("singleItemButton.Visible"), Boolean)
            '
            'btnLoadAll
            '
            Me.btnLoadAll.AccessibleDescription = resources.GetString("btnLoadAll.AccessibleDescription")
            Me.btnLoadAll.AccessibleName = resources.GetString("btnLoadAll.AccessibleName")
            Me.btnLoadAll.Anchor = CType(resources.GetObject("btnLoadAll.Anchor"), System.Windows.Forms.AnchorStyles)
            Me.btnLoadAll.BackgroundImage = CType(resources.GetObject("btnLoadAll.BackgroundImage"), System.Drawing.Image)
            Me.btnLoadAll.Dock = CType(resources.GetObject("btnLoadAll.Dock"), System.Windows.Forms.DockStyle)
            Me.btnLoadAll.Enabled = CType(resources.GetObject("btnLoadAll.Enabled"), Boolean)
            Me.btnLoadAll.FlatStyle = CType(resources.GetObject("btnLoadAll.FlatStyle"), System.Windows.Forms.FlatStyle)
            Me.btnLoadAll.Font = CType(resources.GetObject("btnLoadAll.Font"), System.Drawing.Font)
            Me.btnLoadAll.Image = CType(resources.GetObject("btnLoadAll.Image"), System.Drawing.Image)
            Me.btnLoadAll.ImageAlign = CType(resources.GetObject("btnLoadAll.ImageAlign"), System.Drawing.ContentAlignment)
            Me.btnLoadAll.ImageIndex = CType(resources.GetObject("btnLoadAll.ImageIndex"), Integer)
            Me.btnLoadAll.ImeMode = CType(resources.GetObject("btnLoadAll.ImeMode"), System.Windows.Forms.ImeMode)
            Me.btnLoadAll.Location = CType(resources.GetObject("btnLoadAll.Location"), System.Drawing.Point)
            Me.btnLoadAll.Name = "btnLoadAll"
            Me.btnLoadAll.RightToLeft = CType(resources.GetObject("btnLoadAll.RightToLeft"), System.Windows.Forms.RightToLeft)
            Me.btnLoadAll.Size = CType(resources.GetObject("btnLoadAll.Size"), System.Drawing.Size)
            Me.btnLoadAll.TabIndex = CType(resources.GetObject("btnLoadAll.TabIndex"), Integer)
            Me.btnLoadAll.Text = resources.GetString("btnLoadAll.Text")
            Me.btnLoadAll.TextAlign = CType(resources.GetObject("btnLoadAll.TextAlign"), System.Drawing.ContentAlignment)
            Me.btnLoadAll.Visible = CType(resources.GetObject("btnLoadAll.Visible"), Boolean)
            '
            'btnProductsAddDelete
            '
            Me.btnProductsAddDelete.AccessibleDescription = resources.GetString("btnProductsAddDelete.AccessibleDescription")
            Me.btnProductsAddDelete.AccessibleName = resources.GetString("btnProductsAddDelete.AccessibleName")
            Me.btnProductsAddDelete.Anchor = CType(resources.GetObject("btnProductsAddDelete.Anchor"), System.Windows.Forms.AnchorStyles)
            Me.btnProductsAddDelete.BackgroundImage = CType(resources.GetObject("btnProductsAddDelete.BackgroundImage"), System.Drawing.Image)
            Me.btnProductsAddDelete.Dock = CType(resources.GetObject("btnProductsAddDelete.Dock"), System.Windows.Forms.DockStyle)
            Me.btnProductsAddDelete.Enabled = CType(resources.GetObject("btnProductsAddDelete.Enabled"), Boolean)
            Me.btnProductsAddDelete.FlatStyle = CType(resources.GetObject("btnProductsAddDelete.FlatStyle"), System.Windows.Forms.FlatStyle)
            Me.btnProductsAddDelete.Font = CType(resources.GetObject("btnProductsAddDelete.Font"), System.Drawing.Font)
            Me.btnProductsAddDelete.Image = CType(resources.GetObject("btnProductsAddDelete.Image"), System.Drawing.Image)
            Me.btnProductsAddDelete.ImageAlign = CType(resources.GetObject("btnProductsAddDelete.ImageAlign"), System.Drawing.ContentAlignment)
            Me.btnProductsAddDelete.ImageIndex = CType(resources.GetObject("btnProductsAddDelete.ImageIndex"), Integer)
            Me.btnProductsAddDelete.ImeMode = CType(resources.GetObject("btnProductsAddDelete.ImeMode"), System.Windows.Forms.ImeMode)
            Me.btnProductsAddDelete.Location = CType(resources.GetObject("btnProductsAddDelete.Location"), System.Drawing.Point)
            Me.btnProductsAddDelete.Name = "btnProductsAddDelete"
            Me.btnProductsAddDelete.RightToLeft = CType(resources.GetObject("btnProductsAddDelete.RightToLeft"), System.Windows.Forms.RightToLeft)
            Me.btnProductsAddDelete.Size = CType(resources.GetObject("btnProductsAddDelete.Size"), System.Drawing.Size)
            Me.btnProductsAddDelete.TabIndex = CType(resources.GetObject("btnProductsAddDelete.TabIndex"), Integer)
            Me.btnProductsAddDelete.Text = resources.GetString("btnProductsAddDelete.Text")
            Me.btnProductsAddDelete.TextAlign = CType(resources.GetObject("btnProductsAddDelete.TextAlign"), System.Drawing.ContentAlignment)
            Me.btnProductsAddDelete.Visible = CType(resources.GetObject("btnProductsAddDelete.Visible"), Boolean)
            '
            'resultsTextBox
            '
            Me.resultsTextBox.AccessibleDescription = resources.GetString("resultsTextBox.AccessibleDescription")
            Me.resultsTextBox.AccessibleName = resources.GetString("resultsTextBox.AccessibleName")
            Me.resultsTextBox.Anchor = CType(resources.GetObject("resultsTextBox.Anchor"), System.Windows.Forms.AnchorStyles)
            Me.resultsTextBox.AutoSize = CType(resources.GetObject("resultsTextBox.AutoSize"), Boolean)
            Me.resultsTextBox.BackgroundImage = CType(resources.GetObject("resultsTextBox.BackgroundImage"), System.Drawing.Image)
            Me.resultsTextBox.Dock = CType(resources.GetObject("resultsTextBox.Dock"), System.Windows.Forms.DockStyle)
            Me.resultsTextBox.Enabled = CType(resources.GetObject("resultsTextBox.Enabled"), Boolean)
            Me.resultsTextBox.Font = CType(resources.GetObject("resultsTextBox.Font"), System.Drawing.Font)
            Me.resultsTextBox.ImeMode = CType(resources.GetObject("resultsTextBox.ImeMode"), System.Windows.Forms.ImeMode)
            Me.resultsTextBox.Location = CType(resources.GetObject("resultsTextBox.Location"), System.Drawing.Point)
            Me.resultsTextBox.MaxLength = CType(resources.GetObject("resultsTextBox.MaxLength"), Integer)
            Me.resultsTextBox.Multiline = CType(resources.GetObject("resultsTextBox.Multiline"), Boolean)
            Me.resultsTextBox.Name = "resultsTextBox"
            Me.resultsTextBox.PasswordChar = CType(resources.GetObject("resultsTextBox.PasswordChar"), Char)
            Me.resultsTextBox.ReadOnly = True
            Me.resultsTextBox.RightToLeft = CType(resources.GetObject("resultsTextBox.RightToLeft"), System.Windows.Forms.RightToLeft)
            Me.resultsTextBox.ScrollBars = CType(resources.GetObject("resultsTextBox.ScrollBars"), System.Windows.Forms.ScrollBars)
            Me.resultsTextBox.Size = CType(resources.GetObject("resultsTextBox.Size"), System.Drawing.Size)
            Me.resultsTextBox.TabIndex = CType(resources.GetObject("resultsTextBox.TabIndex"), Integer)
            Me.resultsTextBox.TabStop = False
            Me.resultsTextBox.Text = resources.GetString("resultsTextBox.Text")
            Me.resultsTextBox.TextAlign = CType(resources.GetObject("resultsTextBox.TextAlign"), System.Windows.Forms.HorizontalAlignment)
            Me.resultsTextBox.Visible = CType(resources.GetObject("resultsTextBox.Visible"), Boolean)
            Me.resultsTextBox.WordWrap = CType(resources.GetObject("resultsTextBox.WordWrap"), Boolean)
            '
            'resultsDataGrid
            '
            Me.resultsDataGrid.AccessibleDescription = resources.GetString("resultsDataGrid.AccessibleDescription")
            Me.resultsDataGrid.AccessibleName = resources.GetString("resultsDataGrid.AccessibleName")
            Me.resultsDataGrid.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(173, Byte), CType(207, Byte), CType(239, Byte))
            Me.resultsDataGrid.Anchor = CType(resources.GetObject("resultsDataGrid.Anchor"), System.Windows.Forms.AnchorStyles)
            Me.resultsDataGrid.BackgroundImage = CType(resources.GetObject("resultsDataGrid.BackgroundImage"), System.Drawing.Image)
            Me.resultsDataGrid.CaptionFont = CType(resources.GetObject("resultsDataGrid.CaptionFont"), System.Drawing.Font)
            Me.resultsDataGrid.CaptionText = resources.GetString("resultsDataGrid.CaptionText")
            Me.resultsDataGrid.DataMember = ""
            Me.resultsDataGrid.Dock = CType(resources.GetObject("resultsDataGrid.Dock"), System.Windows.Forms.DockStyle)
            Me.resultsDataGrid.Enabled = CType(resources.GetObject("resultsDataGrid.Enabled"), Boolean)
            Me.resultsDataGrid.Font = CType(resources.GetObject("resultsDataGrid.Font"), System.Drawing.Font)
            Me.resultsDataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText
            Me.resultsDataGrid.ImeMode = CType(resources.GetObject("resultsDataGrid.ImeMode"), System.Windows.Forms.ImeMode)
            Me.resultsDataGrid.Location = CType(resources.GetObject("resultsDataGrid.Location"), System.Drawing.Point)
            Me.resultsDataGrid.Name = "resultsDataGrid"
            Me.resultsDataGrid.RightToLeft = CType(resources.GetObject("resultsDataGrid.RightToLeft"), System.Windows.Forms.RightToLeft)
            Me.resultsDataGrid.Size = CType(resources.GetObject("resultsDataGrid.Size"), System.Drawing.Size)
            Me.resultsDataGrid.TabIndex = CType(resources.GetObject("resultsDataGrid.TabIndex"), Integer)
            Me.resultsDataGrid.TabStop = False
            Me.resultsDataGrid.Visible = CType(resources.GetObject("resultsDataGrid.Visible"), Boolean)
            '
            'QuickStartForm
            '
            Me.AccessibleDescription = resources.GetString("$this.AccessibleDescription")
            Me.AccessibleName = resources.GetString("$this.AccessibleName")
            Me.AutoScaleBaseSize = CType(resources.GetObject("$this.AutoScaleBaseSize"), System.Drawing.Size)
            Me.AutoScroll = CType(resources.GetObject("$this.AutoScroll"), Boolean)
            Me.AutoScrollMargin = CType(resources.GetObject("$this.AutoScrollMargin"), System.Drawing.Size)
            Me.AutoScrollMinSize = CType(resources.GetObject("$this.AutoScrollMinSize"), System.Drawing.Size)
            Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
            Me.ClientSize = CType(resources.GetObject("$this.ClientSize"), System.Drawing.Size)
            Me.Controls.Add(Me.groupBox1)
            Me.Controls.Add(Me.groupBox)
            Me.Controls.Add(Me.retrieveUsingXmlReaderButton)
            Me.Controls.Add(Me.useCaseLabel)
            Me.Controls.Add(Me.label4)
            Me.Controls.Add(Me.btnSimpleQuery)
            Me.Controls.Add(Me.transactionalUpdateButton)
            Me.Controls.Add(Me.singleItemButton)
            Me.Controls.Add(Me.btnLoadAll)
            Me.Controls.Add(Me.btnProductsAddDelete)
            Me.Controls.Add(Me.resultsTextBox)
            Me.Controls.Add(Me.resultsDataGrid)
            Me.Enabled = CType(resources.GetObject("$this.Enabled"), Boolean)
            Me.Font = CType(resources.GetObject("$this.Font"), System.Drawing.Font)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.ImeMode = CType(resources.GetObject("$this.ImeMode"), System.Windows.Forms.ImeMode)
            Me.Location = CType(resources.GetObject("$this.Location"), System.Drawing.Point)
            Me.MaximumSize = CType(resources.GetObject("$this.MaximumSize"), System.Drawing.Size)
            Me.MinimumSize = CType(resources.GetObject("$this.MinimumSize"), System.Drawing.Size)
            Me.Name = "QuickStartForm"
            Me.RightToLeft = CType(resources.GetObject("$this.RightToLeft"), System.Windows.Forms.RightToLeft)
            Me.StartPosition = CType(resources.GetObject("$this.StartPosition"), System.Windows.Forms.FormStartPosition)
            Me.Text = resources.GetString("$this.Text")
            Me.groupBox1.ResumeLayout(False)
            Me.groupBox.ResumeLayout(False)
            CType(Me.resultsDataGrid, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
#End Region

        <STAThread()> _
           Shared Sub Main()
            AppForm = New QuickStartForm
            ' Unhandled exceptions will be delivered to our ThreadException handler
            AddHandler Application.ThreadException, New ThreadExceptionEventHandler(AddressOf AppThreadException)
            Application.Run(AppForm)
        End Sub

        Private Sub QuickStartForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            ' Initialize image on the form to the embedded logo
            Me.logoPictureBox.Image = Me.GetEmbeddedImage("logo.gif")
        End Sub

        ' Displays dialog with information about exceptions that occur in the application. 
        Private Shared Sub AppThreadException(ByVal source As Object, ByVal e As ThreadExceptionEventArgs)
            Dim errorMsg As String = SR.GeneralExceptionMessage(e.Exception.Message)
            errorMsg += Environment.NewLine + SR.DbRequirementsMessage

            Dim result As DialogResult = MessageBox.Show(errorMsg, SR.ApplicationErrorMessage, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop)

            ' Exits the program when the user clicks Abort.
            If (result = System.Windows.Forms.DialogResult.Abort) Then
                Application.Exit()
            End If
            QuickStartForm.AppForm.Cursor = System.Windows.Forms.Cursors.Default
        End Sub

        ' Retrieves the specified embedded image resource.
        Private Function GetEmbeddedImage(ByVal resourceName As String) As Image

            Dim resourceStream As Stream = System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream(resourceName)
            If (resourceStream Is Nothing) Then
                Return Nothing
            End If
            Dim img As Image = Image.FromStream(resourceStream)
            Return img
        End Function

        ' Updates the results textbox on the form with the information for a use case.
        Private Sub DisplayResults(ByVal useCase As String, ByVal results As String)
            Me.useCaseLabel.Text = useCase
            Me.resultsTextBox.Text = results
            Me.resultsDataGrid.Hide()
            Me.resultsTextBox.Show()
        End Sub

        ' Displays the grid showing the results of a use case.
        Private Sub DisplayResults(ByVal useCase As String)
            Me.useCaseLabel.Text = useCase
            Me.resultsDataGrid.Show()
            Me.resultsTextBox.Hide()
        End Sub

        ' Demonstrates how to retrieve multiple rows of data using
        ' a DataReader.
        Private Sub btnLoadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadAll.Click
            Cursor = Cursors.WaitCursor

            Dim emp As Employees = New Employees
            If Not emp.LoadAll() Then
                Me.DisplayResults(Me.btnLoadAll.Text, emp.ErrorMessage)
                Return
            End If

            ' Bind the EasyObject's DefaultView to the DataGrid for display
            Me.resultsDataGrid.SetDataBinding(emp.DefaultView, Nothing)

            Me.DisplayResults(Me.btnLoadAll.Text)

            Cursor = Cursors.Arrow
        End Sub

        ' Demonstrates how to retrieve multiple rows of data using
        ' a DataSet.
        Private Sub btnSimpleQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpleQuery.Click
            Cursor = Cursors.WaitCursor

            Dim emp As Employees = New Employees

            ' Limit the columns returned by the SELECT query
            emp.Query.AddResultColumn(EmployeesSchema.LastName)
            emp.Query.AddResultColumn(EmployeesSchema.FirstName)
            emp.Query.AddResultColumn(EmployeesSchema.City)
            emp.Query.AddResultColumn(EmployeesSchema.Region)

            ' Add an ORDER BY clause
            emp.Query.AddOrderBy(EmployeesSchema.LastName)

            ' Add a WHERE clause
            emp.Where.Region.Value = "WA"

            If Not emp.Query.Load() Then
                Me.DisplayResults(Me.btnSimpleQuery.Text, emp.ErrorMessage)
                Return
            End If

            ' Bind the EasyObject's DefaultView to the DataGrid for display
            Me.resultsDataGrid.SetDataBinding(emp.DefaultView, Nothing)

            Me.DisplayResults(Me.btnSimpleQuery.Text)

            Cursor = Cursors.Arrow
        End Sub

        ' Demonstrates how to retrieve a single row of data.
        Private Sub btnProductsAddDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProductsAddDelete.Click
            Cursor = Cursors.WaitCursor

            Dim prod As Products = New Products

            ' Call AddNew() to add a new row to the EasyObject. You must fill in all 
            ' required fields or an error will result when you call Save().
            prod.AddNew()

            ' Note the use of the 's_' fields, which take strings as arguments. If this object
            ' were being loaded from TextBox objects on a WinForm, you don't have to worry about
            ' the datatype because this is handled for you in EasyObjects
            prod.s_ProductName = "EasyObjects"
            prod.s_Discontinued = "True"
            prod.s_QuantityPerUnit = "10"
            prod.s_ReorderLevel = "100"
            prod.s_UnitPrice = "49.95"
            prod.s_UnitsInStock = "200"

            ' Save the changes
            prod.Save()

            ' Display the XML representation of the EasyObject
            Dim productDetails As String = prod.ToXml()

            Me.DisplayResults(Me.btnProductsAddDelete.Text, productDetails)

            ' Delete the new addition
            prod.MarkAsDeleted()
            prod.Save()

            Cursor = Cursors.Arrow
        End Sub

        ' Demonstrates how to retrieve a single data item from the database.
        Private Sub singleItemButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles singleItemButton.Click
            Cursor = Cursors.WaitCursor

            Dim prod As Products = New Products

            ' Load a single row via the primary key
            prod.LoadByPrimaryKey(4)

            Dim productName As String = prod.s_ProductName

            Me.DisplayResults(Me.singleItemButton.Text, productName)

            Cursor = Cursors.Arrow
        End Sub

        ' Demonstrates how to update the database multiple times in the
        ' context of a transaction. All updates will succeed or all will be 
        ' rolled back.
        Private Sub transactionalUpdateButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles transactionalUpdateButton.Click
            Cursor = Cursors.WaitCursor

            Dim results As String = ""
            Dim prod As Products = New Products
            Dim emp As Employees = New Employees

            ' Update the requested product
            prod.LoadByPrimaryKey(4)
            prod.UnitsInStock += CType(1, Short)

            ' Update the requested employee
            emp.LoadByPrimaryKey(1)
            emp.s_Country = "USA"

            ' Retrieve the current transaction manager
            Dim tx As TransactionManager = TransactionManager.ThreadTransactionMgr()

            Try
                tx.BeginTransaction()

                ' Save both objects within the same transaction
                emp.Save()
                prod.Save()

                ' Deliberately throw an error, to cause the transaction to rollback
                Throw New Exception("Deliberate exception, transaction rolled back.")

                tx.CommitTransaction()

                Me.DisplayResults(Me.transactionalUpdateButton.Text, results)
            Catch ex As Exception
                tx.RollbackTransaction()
                TransactionManager.ThreadTransactionMgrReset()
                Me.DisplayResults(Me.transactionalUpdateButton.Text, ex.Message)
            End Try

            Cursor = Cursors.Arrow
        End Sub

        ' Demonstrates how to retrieve XML data from a SQL Server database.
        Private Sub retrieveUsingXmlReaderButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles retrieveUsingXmlReaderButton.Click
            Cursor = Cursors.WaitCursor

            Dim prod As Products = New Products
            prod.LoadAll()

            DisplayResults(Me.retrieveUsingXmlReaderButton.Text, prod.ToXml())

            Cursor = Cursors.Arrow
        End Sub

        ' Quits the application.
        Private Sub quitButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles quitButton.Click
            Me.Close()
        End Sub

        ' Displays Quick Start help topics using the Help 2 Viewer.
        Private Sub viewWalkthroughButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles viewWalkthroughButton.Click
            Dim p As New Process

            Cursor = Cursors.WaitCursor

            p.StartInfo.UseShellExecute = True
            p.StartInfo.FileName = "..\help\index.htm"
            p.Start()

            Cursor = Cursors.Default

        End Sub

        Private Sub logoPictureBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles logoPictureBox.Click
            Dim p As New Process

            Cursor = Cursors.WaitCursor

            p.StartInfo.UseShellExecute = True
            p.StartInfo.FileName = "http://www.easyobjects.net"
            p.Start()

            Cursor = Cursors.Default

        End Sub
    End Class
End Namespace