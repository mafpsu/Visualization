namespace bsensor
{
    partial class FrmMain
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
            this.ssStatus = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNumPoints = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDataSource = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDataSourceBikeData = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDataSourceBusData = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewTrip = new System.Windows.Forms.ToolStripMenuItem();
            this.cbTripIDs = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewGraphType = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewGraphTypeRange = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewGraphTypeRangeColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewGraphTypeRangeSpline = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.webBrowserContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnPlayPauseReverse = new System.Windows.Forms.Button();
            this.btnStepReverse = new System.Windows.Forms.Button();
            this.btnStepForward = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPlayPauseForward = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.lblCloseProperties = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbProperties = new System.Windows.Forms.ComboBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.tmrAsyncColor = new System.Windows.Forms.Timer(this.components);
            this.tmrPlay = new System.Windows.Forms.Timer(this.components);
            this.ssStatus.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // ssStatus
            // 
            this.ssStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.lblNumPoints});
            this.ssStatus.Location = new System.Drawing.Point(0, 810);
            this.ssStatus.Name = "ssStatus";
            this.ssStatus.Size = new System.Drawing.Size(803, 22);
            this.ssStatus.TabIndex = 0;
            this.ssStatus.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.lblStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(775, 17);
            this.lblStatus.Spring = true;
            this.lblStatus.Text = "Ready";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNumPoints
            // 
            this.lblNumPoints.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.lblNumPoints.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblNumPoints.Name = "lblNumPoints";
            this.lblNumPoints.Size = new System.Drawing.Size(13, 17);
            this.lblNumPoints.Text = "0";
            this.lblNumPoints.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuView,
            this.mnuHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(803, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileNew,
            this.mnuFileOpen,
            this.toolStripMenuItem4,
            this.mnuFileSave,
            this.mnuFileSaveAs,
            this.toolStripMenuItem3,
            this.mnuDataSource,
            this.toolStripMenuItem1,
            this.mnuFileLogin,
            this.toolStripMenuItem2,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Name = "mnuFileNew";
            this.mnuFileNew.Size = new System.Drawing.Size(137, 22);
            this.mnuFileNew.Text = "New";
            this.mnuFileNew.Click += new System.EventHandler(this.mnuFileNew_Click);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.Size = new System.Drawing.Size(137, 22);
            this.mnuFileOpen.Text = "Open";
            this.mnuFileOpen.Click += new System.EventHandler(this.mnuFileOpen_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(134, 6);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.Size = new System.Drawing.Size(137, 22);
            this.mnuFileSave.Text = "Save";
            this.mnuFileSave.Click += new System.EventHandler(this.mnuFileSave_Click);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.Name = "mnuFileSaveAs";
            this.mnuFileSaveAs.Size = new System.Drawing.Size(137, 22);
            this.mnuFileSaveAs.Text = "Save As";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.mnuFileSaveAs_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(134, 6);
            // 
            // mnuDataSource
            // 
            this.mnuDataSource.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDataSourceBikeData,
            this.mnuDataSourceBusData});
            this.mnuDataSource.Name = "mnuDataSource";
            this.mnuDataSource.Size = new System.Drawing.Size(137, 22);
            this.mnuDataSource.Text = "Data Source";
            this.mnuDataSource.Click += new System.EventHandler(this.mnuFileImport_Click);
            // 
            // mnuDataSourceBikeData
            // 
            this.mnuDataSourceBikeData.Name = "mnuDataSourceBikeData";
            this.mnuDataSourceBikeData.Size = new System.Drawing.Size(132, 22);
            this.mnuDataSourceBikeData.Text = "Bike Data...";
            this.mnuDataSourceBikeData.Click += new System.EventHandler(this.mnuDataSourceBikeData_Click);
            // 
            // mnuDataSourceBusData
            // 
            this.mnuDataSourceBusData.Name = "mnuDataSourceBusData";
            this.mnuDataSourceBusData.Size = new System.Drawing.Size(132, 22);
            this.mnuDataSourceBusData.Text = "Bus Data...";
            this.mnuDataSourceBusData.Click += new System.EventHandler(this.mnuDataSourceBusData_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(134, 6);
            // 
            // mnuFileLogin
            // 
            this.mnuFileLogin.Name = "mnuFileLogin";
            this.mnuFileLogin.Size = new System.Drawing.Size(137, 22);
            this.mnuFileLogin.Text = "Login";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(134, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.mnuFileExit.Size = new System.Drawing.Size(137, 22);
            this.mnuFileExit.Text = "Exit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewTrip,
            this.toolStripMenuItem5,
            this.mnuViewGraphType,
            this.toolStripMenuItem6,
            this.mnuViewProperties});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(44, 20);
            this.mnuView.Text = "View";
            // 
            // mnuViewTrip
            // 
            this.mnuViewTrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbTripIDs});
            this.mnuViewTrip.Name = "mnuViewTrip";
            this.mnuViewTrip.Size = new System.Drawing.Size(131, 22);
            this.mnuViewTrip.Text = "Trip";
            // 
            // cbTripIDs
            // 
            this.cbTripIDs.Name = "cbTripIDs";
            this.cbTripIDs.Size = new System.Drawing.Size(121, 23);
            this.cbTripIDs.SelectedIndexChanged += new System.EventHandler(this.cbTripIDs_SelectedIndexChanged);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(128, 6);
            // 
            // mnuViewGraphType
            // 
            this.mnuViewGraphType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewGraphTypeRange,
            this.mnuViewGraphTypeRangeColumn,
            this.mnuViewGraphTypeRangeSpline});
            this.mnuViewGraphType.Name = "mnuViewGraphType";
            this.mnuViewGraphType.Size = new System.Drawing.Size(131, 22);
            this.mnuViewGraphType.Text = "GraphType";
            // 
            // mnuViewGraphTypeRange
            // 
            this.mnuViewGraphTypeRange.Name = "mnuViewGraphTypeRange";
            this.mnuViewGraphTypeRange.Size = new System.Drawing.Size(150, 22);
            this.mnuViewGraphTypeRange.Text = "Range";
            this.mnuViewGraphTypeRange.Click += new System.EventHandler(this.mnuViewGraphTypeRange_Click);
            // 
            // mnuViewGraphTypeRangeColumn
            // 
            this.mnuViewGraphTypeRangeColumn.Name = "mnuViewGraphTypeRangeColumn";
            this.mnuViewGraphTypeRangeColumn.Size = new System.Drawing.Size(150, 22);
            this.mnuViewGraphTypeRangeColumn.Text = "RangeColumn";
            this.mnuViewGraphTypeRangeColumn.Click += new System.EventHandler(this.mnuViewGraphTypeRangeColumn_Click);
            // 
            // mnuViewGraphTypeRangeSpline
            // 
            this.mnuViewGraphTypeRangeSpline.Name = "mnuViewGraphTypeRangeSpline";
            this.mnuViewGraphTypeRangeSpline.Size = new System.Drawing.Size(150, 22);
            this.mnuViewGraphTypeRangeSpline.Text = "RangeSpline";
            this.mnuViewGraphTypeRangeSpline.Click += new System.EventHandler(this.mnuViewGraphTypeRangeSpline_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(128, 6);
            // 
            // mnuViewProperties
            // 
            this.mnuViewProperties.Checked = true;
            this.mnuViewProperties.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuViewProperties.Name = "mnuViewProperties";
            this.mnuViewProperties.Size = new System.Drawing.Size(131, 22);
            this.mnuViewProperties.Text = "Properties";
            this.mnuViewProperties.Click += new System.EventHandler(this.mnuViewProperties_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(107, 22);
            this.mnuHelpAbout.Text = "About";
            this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 24);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.ContextMenuStrip = this.webBrowserContextMenu;
            this.scMain.Panel1.Controls.Add(this.btnPlayPauseReverse);
            this.scMain.Panel1.Controls.Add(this.btnStepReverse);
            this.scMain.Panel1.Controls.Add(this.btnStepForward);
            this.scMain.Panel1.Controls.Add(this.btnStop);
            this.scMain.Panel1.Controls.Add(this.btnPlayPauseForward);
            this.scMain.Panel1.Controls.Add(this.webBrowser1);
            this.scMain.Panel1.Padding = new System.Windows.Forms.Padding(4, 4, 2, 4);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.scMain.Panel2.Controls.Add(this.lblCloseProperties);
            this.scMain.Panel2.Controls.Add(this.label1);
            this.scMain.Panel2.Controls.Add(this.cbProperties);
            this.scMain.Panel2.Controls.Add(this.propertyGrid1);
            this.scMain.Panel2.Padding = new System.Windows.Forms.Padding(2, 4, 8, 4);
            this.scMain.Size = new System.Drawing.Size(803, 786);
            this.scMain.SplitterDistance = 615;
            this.scMain.TabIndex = 6;
            // 
            // webBrowserContextMenu
            // 
            this.webBrowserContextMenu.Name = "chartContextMenu";
            this.webBrowserContextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // btnPlayPauseReverse
            // 
            this.btnPlayPauseReverse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPlayPauseReverse.Location = new System.Drawing.Point(194, 751);
            this.btnPlayPauseReverse.Name = "btnPlayPauseReverse";
            this.btnPlayPauseReverse.Size = new System.Drawing.Size(65, 32);
            this.btnPlayPauseReverse.TabIndex = 13;
            this.btnPlayPauseReverse.Text = "<<< Play";
            this.btnPlayPauseReverse.UseVisualStyleBackColor = true;
            this.btnPlayPauseReverse.Click += new System.EventHandler(this.btnPlayPauseReverse_Click);
            // 
            // btnStepReverse
            // 
            this.btnStepReverse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStepReverse.Location = new System.Drawing.Point(166, 751);
            this.btnStepReverse.Name = "btnStepReverse";
            this.btnStepReverse.Size = new System.Drawing.Size(22, 32);
            this.btnStepReverse.TabIndex = 12;
            this.btnStepReverse.Text = "<";
            this.btnStepReverse.UseVisualStyleBackColor = true;
            this.btnStepReverse.Click += new System.EventHandler(this.btnStepReverse_Click);
            // 
            // btnStepForward
            // 
            this.btnStepForward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStepForward.Location = new System.Drawing.Point(380, 751);
            this.btnStepForward.Name = "btnStepForward";
            this.btnStepForward.Size = new System.Drawing.Size(21, 32);
            this.btnStepForward.TabIndex = 11;
            this.btnStepForward.Text = ">";
            this.btnStepForward.UseVisualStyleBackColor = true;
            this.btnStepForward.Click += new System.EventHandler(this.btnStepForward_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStop.Location = new System.Drawing.Point(265, 751);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(38, 32);
            this.btnStop.TabIndex = 10;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPlayPauseForward
            // 
            this.btnPlayPauseForward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPlayPauseForward.Location = new System.Drawing.Point(309, 751);
            this.btnPlayPauseForward.Name = "btnPlayPauseForward";
            this.btnPlayPauseForward.Size = new System.Drawing.Size(65, 32);
            this.btnPlayPauseForward.TabIndex = 8;
            this.btnPlayPauseForward.Text = "Play >>>";
            this.btnPlayPauseForward.UseVisualStyleBackColor = true;
            this.btnPlayPauseForward.Click += new System.EventHandler(this.btnPlayPauseForward_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.CausesValidation = false;
            this.webBrowser1.ContextMenuStrip = this.webBrowserContextMenu;
            this.webBrowser1.Location = new System.Drawing.Point(4, 4);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(609, 745);
            this.webBrowser1.TabIndex = 7;
            this.webBrowser1.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // lblCloseProperties
            // 
            this.lblCloseProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCloseProperties.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCloseProperties.Location = new System.Drawing.Point(148, 2);
            this.lblCloseProperties.Name = "lblCloseProperties";
            this.lblCloseProperties.Size = new System.Drawing.Size(24, 26);
            this.lblCloseProperties.TabIndex = 4;
            this.lblCloseProperties.Text = "×";
            this.lblCloseProperties.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCloseProperties.Click += new System.EventHandler(this.lblCloseProperties_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Properties";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbProperties
            // 
            this.cbProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbProperties.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProperties.FormattingEnabled = true;
            this.cbProperties.Location = new System.Drawing.Point(0, 32);
            this.cbProperties.Name = "cbProperties";
            this.cbProperties.Size = new System.Drawing.Size(179, 21);
            this.cbProperties.TabIndex = 1;
            this.cbProperties.SelectedIndexChanged += new System.EventHandler(this.cbProperties_SelectedIndexChanged);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid1.Location = new System.Drawing.Point(0, 59);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(179, 727);
            this.propertyGrid1.TabIndex = 0;
            // 
            // tmrAsyncColor
            // 
            this.tmrAsyncColor.Interval = 1000;
            this.tmrAsyncColor.Tick += new System.EventHandler(this.tmrAsyncColor_Tick);
            // 
            // tmrPlay
            // 
            this.tmrPlay.Interval = 1000;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 832);
            this.Controls.Add(this.scMain);
            this.Controls.Add(this.ssStatus);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.Text = "BSENSOR: untitled";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ssStatus.ResumeLayout(false);
            this.ssStatus.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip ssStatus;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.OpenFileDialog dlgOpenFile;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuFileLogin;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripMenuItem mnuDataSource;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripStatusLabel lblNumPoints;
        private System.Windows.Forms.ToolStripMenuItem mnuDataSourceBikeData;
        private System.Windows.Forms.ToolStripMenuItem mnuDataSourceBusData;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSave;
        private System.Windows.Forms.SaveFileDialog dlgSaveFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileNew;
        private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSaveAs;
        private System.Windows.Forms.ToolStripMenuItem mnuView;
        private System.Windows.Forms.ToolStripMenuItem mnuViewTrip;
        private System.Windows.Forms.ToolStripComboBox cbTripIDs;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem mnuViewGraphType;
        private System.Windows.Forms.ToolStripMenuItem mnuViewGraphTypeRange;
        private System.Windows.Forms.ToolStripMenuItem mnuViewGraphTypeRangeColumn;
        private System.Windows.Forms.ToolStripMenuItem mnuViewGraphTypeRangeSpline;
        private System.Windows.Forms.Timer tmrAsyncColor;
        private System.Windows.Forms.ContextMenuStrip webBrowserContextMenu;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem mnuViewProperties;
        private System.Windows.Forms.ComboBox cbProperties;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCloseProperties;
        private System.Windows.Forms.Button btnPlayPauseForward;
        private System.Windows.Forms.Timer tmrPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPlayPauseReverse;
        private System.Windows.Forms.Button btnStepReverse;
        private System.Windows.Forms.Button btnStepForward;
    }
}

